using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using HappyZu.CloudStore.Authorization.Roles;

namespace HappyZu.CloudStore.Users
{
    public class UserStore : AbpUserStore<Role, User>
    {
        private readonly IRepository<User, long> _userRepository;

        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, long> userClaimRepository)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,
                userClaimRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByWechatOpenIdAndUnionIdAsync(string wechatOpenId, string unionId)
        {
            if (string.IsNullOrWhiteSpace(wechatOpenId)&&!string.IsNullOrWhiteSpace(unionId))
            {
                return await _userRepository.FirstOrDefaultAsync(user => user.UnionID == unionId);
            }
            return await _userRepository.FirstOrDefaultAsync(user => user.WechatOpenID == wechatOpenId);
        }
    }
}