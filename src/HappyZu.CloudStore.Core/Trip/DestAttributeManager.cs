using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.Trip
{
    public class DestAttributeManager : IDomainService
    {
        private readonly IRepository<DestAttribute> _destAttributeRepository;

        public DestAttributeManager(IRepository<DestAttribute> destAttributeRepository)
        {
            _destAttributeRepository = destAttributeRepository;
        }

        public async Task AddDestAttributeAsync(DestAttribute attr)
        {
            await _destAttributeRepository.InsertAsync(attr);
        }

        public async Task UpdateDestAttributeAsync(DestAttribute attr)
        {
            await _destAttributeRepository.UpdateAsync(attr);
        }

        public async Task RemoveDestAttributeAsync(int destAttributeId)
        {
            await _destAttributeRepository.DeleteAsync(destAttributeId);
        }

        public async Task<DestAttribute> GetDestAttributeByIdAsync(int destAttributeId)
        {
            return await _destAttributeRepository.GetAsync(destAttributeId);
        }

        public async Task<IList<DestAttribute>> GetSubDestAttributesByParentId(int parentId)
        {
            return await _destAttributeRepository.GetAllListAsync(attr => attr.ParentId == parentId);
        }
    }
}
