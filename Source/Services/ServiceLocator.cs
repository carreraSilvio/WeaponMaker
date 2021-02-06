using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponMaker
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static void Add(IService service)
        {
            _services.Add(service.GetType(), service);
        }

        public static T Fetch<T>() where T : IService
        {
            return (T)_services[typeof(T)];
        }
    }
}
