using System;

namespace Eagle.Infrastructrue.Aop.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InjectionAttribute : System.Attribute
    {
        public InjectionAttribute(Type interfaceName)
        {
            this.InterfaceName = interfaceName;
            this.AliasName = "Default";
        }
        public InjectionAttribute(Type interfaceName, string aliasName)
        {
            this.InterfaceName = interfaceName;
            AliasName = aliasName;
        }

        public InjectionAttribute(Type interfaceName, Type className)
        {
            this.InterfaceName = interfaceName;
            this.AliasName = $"{className.FullName}";
        }

        public string AliasName
        {
            get; set;
        }

        public Type InterfaceName
        {
            get;
            set;
        }
    }
}