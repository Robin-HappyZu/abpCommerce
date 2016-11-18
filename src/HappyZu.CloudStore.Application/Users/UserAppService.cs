using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using HappyZu.CloudStore.Authorization;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.IdGenerators;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Users.Dto;
using Microsoft.AspNet.Identity;

namespace HappyZu.CloudStore.Users
{
    public class UserAppService : CloudStoreAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly LogInManager _logInManager;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager, LogInManager logInManager)   
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _logInManager = logInManager;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task SetUnsubscribe(string openId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(q => q.WechatOpenID == openId);

            if (user != null)
            {
                user.IsSubscribe = false;
            }
            await _userRepository.UpdateAsync(user);
        }

        public async Task SetUnsubscribe(long userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(q => q.Id == userId);

            if (user != null)
            {
                user.IsSubscribe = false;
            }
            await _userRepository.UpdateAsync(user);
        }

        public async Task SetSubscribe(string openId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(q=>q.WechatOpenID== openId);

            if (user != null)
            {
                user.IsSubscribe = true;
            }
            await _userRepository.UpdateAsync(user);
        }

        public async Task SetSubscribe(long userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(q => q.Id == userId);

            if (user != null)
            {
                user.IsSubscribe = true;
            }
            await _userRepository.UpdateAsync(user);
        }

        public async Task<ListResultDto<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserDto>(
                users.MapTo<List<UserDto>>()
                );
        }

        public Task<IPagedResult<UserDto>> QueryUsers(QueryUserInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            Func<IQueryable<User>, IQueryable<User>> query = null;
            if (!string.IsNullOrWhiteSpace(input.UserName) && !string.IsNullOrWhiteSpace(input.NickName))
            {
                query = q => q.Where(x => x.UserName == input.UserName && x.Name == input.NickName).OrderByDescending(x => x.CreationTime);
            }
            else if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                query= q => q.Where(x => x.UserName == input.UserName).OrderByDescending(x => x.CreationTime);
            }
            else if (!string.IsNullOrWhiteSpace(input.NickName))
            {
                query = q => q.Where(x => x.Name == input.NickName).OrderByDescending(x=>x.CreationTime);
            }

            var count = query != null ?
                   _userRepository.Query(query).Count() :
                   _userRepository.Count();


            var list = query == null ?
                _userRepository.GetAll().OrderBy(p => p.Id).PageBy(input).ToList() :
                _userRepository.Query(query).PageBy(input).ToList();

            var pageResult = new PagedResultDto<UserDto>
            {
                TotalCount = count,
                Items = list.MapTo<IReadOnlyList<UserDto>>()
            };
            return Task.FromResult((IPagedResult<UserDto>)pageResult);
        }

        public async Task CreateUserAsync(CreateUserInput input)
        {
            try
            {
                var user = input.MapTo<User>();

                user.TenantId = AbpSession.TenantId;
                user.Password = new PasswordHasher().HashPassword(input.Password);
                user.IsEmailConfirmed = true;

                await UserManager.CreateAsync(user);
                await UnitOfWorkManager.Current.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public async Task AddUserAsync(CreateUserInput input)
        {
            try
            {
                var user = input.MapTo<User>();

                user.TenantId = AbpSession.TenantId;
                user.Password = new PasswordHasher().HashPassword(input.Password);
                user.IsEmailConfirmed = true;

                await UserManager.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public async Task AddUserLoginAsync(UserLoginInput input)
        {
            var user = input.User.MapTo<User>();
            await UserManager.AddLoginAsync(user, new UserLoginInfo(input.LoginProvider, input.ProviderKey));
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<ClaimsIdentity> CreateIdentityAsync(User user)
        {
            return await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<UserDto> GetUserByWechatOpenIdAndUnionIdAsync(string wechatOpenId, string unionId)
        {
            User user;
            if (string.IsNullOrWhiteSpace(wechatOpenId) && !string.IsNullOrWhiteSpace(unionId))
            {
                user = await _userRepository.FirstOrDefaultAsync(u => u.UnionID == unionId);
            }
            else
            {
                user = await _userRepository.FirstOrDefaultAsync(u => u.WechatOpenID == wechatOpenId);
            }
            return user.MapTo<UserDto>();
        }

        public async Task<UserDto> GetUserByIdAsync(long userId)
        {
            var user =await _userRepository.GetAsync(userId);

            return user?.MapTo<UserDto>();
        }

        public async Task<AbpLoginResult<Tenant, User>> UserLoginAsync(UserLoginInput input)
        {
            var login = new UserLoginInfo(input.LoginProvider, input.ProviderKey);
            var loginResult = await _logInManager.LoginAsync(login);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw new Exception("Login Failed");
            }
        }

        public async Task<ResultOutputDto> SetPassword(SetPasswordInput input)
        {
            try
            {

            var user = await _userRepository.GetAsync(input.UserId);

            if (!string.IsNullOrWhiteSpace(input.OldPassword))
            {
                if (user==null)
                {
                    return ResultOutputDto.Failed;
                }
                var password = new PasswordHasher().VerifyHashedPassword(user.Password,input.OldPassword);
                if (password==PasswordVerificationResult.Failed)
                {
                    return ResultOutputDto.Failed;
                }
            }

            user.Password = new PasswordHasher().HashPassword(input.Password);

            await _userRepository.UpdateAsync(user);

            return ResultOutputDto.Successed;
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Failed;
            }
        }

        public async Task<ResultOutputDto> SetUserInfo(SetUserInfoInput input)
        {
            try
            {
                await _userRepository.UpdateAsync(input.UserId, action =>
                {
                    action.Surname = input.Surname;
                    action.Name = input.Name;
                    action.PhoneNumber = input.Mobile;
                    action.EmailAddress = input.EmailAddress;
                    return Task.FromResult(action);
                });
            }
            catch (Exception)
            {
                return ResultOutputDto.Failed;
            }

            return ResultOutputDto.Successed;
        }

        public async Task<ResultOutputDto> RemoveUser(long id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
            }
            catch (Exception)
            {

                return ResultOutputDto.Failed;
            }
            return ResultOutputDto.Successed;
        }

        public async Task<ResultOutputDto> ActiveUser(long id)
        {
            try
            {
                await _userRepository.UpdateAsync(id, action =>
                {
                    action.IsActive = !action.IsActive;
                    return Task.FromResult(action);
                });
            }
            catch (Exception)
            {
                return ResultOutputDto.Failed;
            }

            return ResultOutputDto.Successed;
        }

        public async Task<ResultOutputDto> SetUserRole(long id, params string[] roleName)
        {
            try
            {
                var user = await _userRepository.GetAsync(id);
                var identity=await UserManager.SetRoles(user, roleName);
                if (identity.Succeeded)
                {
                    return ResultOutputDto.Successed;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return ResultOutputDto.Failed;
        }

        public async Task<ResultOutputDto> BindingWechatOpenId(long id, string openId, string unionId)
        {
            try
            {
                await _userRepository.UpdateAsync(id, action =>
                {
                    action.WechatOpenID = openId;
                    action.UnionID = unionId;

                    return Task.FromResult(action);
                });
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Fail(500, ex.Message);
            }
            return ResultOutputDto.Failed;
        }

       
    }
}