using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Happyzu.CloudStore.Common.Areas
{
    public abstract class AreaRegistrationBase : AreaRegistration
    {
        protected static List<AreaRegistrationContext> areaContent = new List<AreaRegistrationContext>();

        protected static List<AreaRegistrationBase> areaRegistration = new List<AreaRegistrationBase>();

        public abstract int Order
        {
            get;
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            AreaRegistrationBase.areaContent.Add(context);
            AreaRegistrationBase.areaRegistration.Add(this);
        }

        public abstract void RegisterAreaOrder(AreaRegistrationContext context);

        public static void RegisterAllAreasOrder()
        {
            AreaRegistration.RegisterAllAreas();
            AreaRegistrationBase.Register();
        }

        private static void Register()
        {
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < AreaRegistrationBase.areaRegistration.Count; i++)
            {
                list.Add(new int[]
                {
                    AreaRegistrationBase.areaRegistration[i].Order,
                    i
                });
            }
            list = (from o in list
                    orderby o[0]
                    select o).ToList<int[]>();
            foreach (int[] current in list)
            {
                AreaRegistrationBase.areaRegistration[current[1]].RegisterAreaOrder(AreaRegistrationBase.areaContent[current[1]]);
            }
        }
    }
}
