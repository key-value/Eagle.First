using System;
using System.Collections.Generic;
using Eagle.Infrastructrue.Utility;
using Microsoft.Practices.Unity.InterceptionExtension;
using NLog.Fluent;

namespace Eagle.Infrastructrue.Aop.Interception
{
    public class ExceptionLogBehaviors : IInterceptionBehavior
    {
        /// <summary>
        /// 指示拦截行为是否可被执行(设置为true,表示可执行).
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }
        /// <summary>
        /// 获取当前行为需要拦截的对象类型接口。
        /// </summary>
        /// <returns>所有需要拦截的对象类型接口。</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        /// <summary>
        /// 通过实现此方法来拦截调用并执行所需的拦截行为。
        /// </summary>
        /// <param name="input">调用拦截目标时的输入信息。</param>
        /// <param name="getNext">通过行为链来获取下一个拦截行为的委托。</param>
        /// <returns>从拦截目标获得的返回信息。</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var methodReturn = getNext().Invoke(input, getNext);
            if (methodReturn.Exception != null)
            {
                LogUtility.SendError(methodReturn.Exception);
            }
            return methodReturn;
        }
    }
}
