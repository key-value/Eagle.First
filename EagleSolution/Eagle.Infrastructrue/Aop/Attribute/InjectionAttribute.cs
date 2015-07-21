using System;

namespace Eagle.Infrastructrue.Aop.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InjectionAttribute : System.Attribute
    {
        public InjectionAttribute(Type interfaceName)
        {
            this.InterfaceName = interfaceName;
        }

        public Type InterfaceName
        {
            get;
            set;
        }
    }
}