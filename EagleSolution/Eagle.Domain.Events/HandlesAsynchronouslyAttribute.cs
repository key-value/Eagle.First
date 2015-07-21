using System;

namespace Eagle.Domain.Events
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HandlesAsynchronouslyAttribute : Attribute
    {

    }
}