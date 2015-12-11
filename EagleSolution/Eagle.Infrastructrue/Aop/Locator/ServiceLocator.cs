using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Aop.Interception;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Eagle.Infrastructrue.Aop.Locator
{
    public class ServiceLocator : IServiceProvider
    {
        private static readonly ServiceLocator instance = new ServiceLocator();

        private readonly IUnityContainer _container;
        /// <summary>
        /// Initializes a new instance of <c>ServiceLocator</c> class.
        /// </summary>
        private ServiceLocator()
        {
            _container = new UnityContainer();
            _container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            var nameSpaces = new List<string>() { "Eagle.Server", "Eagle.Domain.Events.Handles", "Eagle.Application.Server" };

            var interceptor = new InjectionMember[]
                              {
                                  new Interceptor<InterfaceInterceptor>(),
                                  new InterceptionBehavior<ExceptionLogBehaviors>()
                              };
            foreach (var nameSpace in nameSpaces)
            {
                var interfaceTypes = AppDomain.CurrentDomain.Load(nameSpace).GetTypes().Where(x => x.GetCustomAttributes(typeof(InjectionAttribute), false).Any()).ToArray();


                foreach (var injectionType in interfaceTypes)
                {
                    var unityInjectionAttribute = injectionType.GetCustomAttributes(typeof(InjectionAttribute), false) as InjectionAttribute[];
                    if (unityInjectionAttribute == null)
                    {
                        continue;
                    }
                    foreach (var customAttributeData in unityInjectionAttribute)
                    {
                        var classBody = customAttributeData.InterfaceName;
                        if (classBody != null)
                        {
                            _container.RegisterType(customAttributeData.InterfaceName, injectionType, customAttributeData.AliasName, new MvcPerRequestLifetimeManager(), interceptor);
                        }
                    }
                }
            }
        }

        #region Public Static Properties
        /// <summary>
        /// Gets the singleton instance of the <c>ServiceLocator</c> class.
        /// </summary>
        public static ServiceLocator Instance
        {

            get
            {
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// Gets the service instance with the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service instance.</returns>
        public T GetService<T>()
        {
            return _container.Resolve<T>("Default");
        }

        public T GetService<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.ResolveAll<T>();
        }

        public object GetService(Type typeName)
        {
            return _container.Resolve(typeName);
        }
    }
}