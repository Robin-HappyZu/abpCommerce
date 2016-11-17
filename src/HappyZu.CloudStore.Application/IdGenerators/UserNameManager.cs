using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using IdGen;

namespace HappyZu.CloudStore.IdGenerators
{
    public class UserNameManager : ISingletonDependency
    {
        private readonly IdGenerator _idGenerator;
        private readonly object _lockObject;
        public UserNameManager()
        {
            _idGenerator = IdGenerator.GetFromConfig("UserName");
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
