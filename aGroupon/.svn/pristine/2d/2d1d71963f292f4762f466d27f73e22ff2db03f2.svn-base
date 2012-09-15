using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using B2B.Models;
using NREticaret.DI;

namespace NREticaret.Models
{
    public class BootStrapper
    {
        public static void ConfigureUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            // Registrations
            container.RegisterType<IUSERRepository, USERRepositoryEF>
                (new HttpContextLifetimeManager<IUSERRepository>());
            container.RegisterType<IROLERepository, ROLERepositoryEF>
                (new HttpContextLifetimeManager<IROLERepository>());
            
        }

        public static IUSERRepository GetCurrentUserRepository()
        {
            return new USERRepositoryEF();
        }

        public static IROLERepository GetCurrentRoleRepository()
        {
            return new ROLERepositoryEF();
        }


        //internal static ICATEGORYRepository GetCurrentCATEGORYRepository()
        //{
        //    return new CATEGORYRepositoryEF();
        //}

        //internal static ISITE_PAGERepository GetCurrentSitePageRepository()
        //{
        //    return new SITE_PAGERepositoryEF();
        //}

        //internal static IROLE_PAGERepository GetCurrentRolePageRepository()
        //{
        //    return new ROLE_PAGERepositoryEF();
        //}

        //internal static ITRADELEADRepository GetCurrentTradeLeadRepository()
        //{
        //    return new TRADELEADRepositoryEF();
        //}
    }
}