using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using IdGen;

namespace HappyZu.CloudStore.Trip
{
    public interface IUniqueIdManager
    {
        long CreateId();
    }

    public class UniqueIdManager : IUniqueIdManager, ISingletonDependency
    {
        private readonly IdGenerator _idGenerator;
        private readonly object _lockObject;
        public UniqueIdManager()
        {
            _idGenerator = IdGenerator.GetFromConfig("SerialNo");
            _lockObject = new object();
        }

        public long CreateId()
        {
            lock (_lockObject)
            {
                return _idGenerator.CreateId();
            }
        }
    }
}
