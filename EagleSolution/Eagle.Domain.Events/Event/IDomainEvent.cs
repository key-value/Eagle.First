using Eagle.Domain.Core.Model;

namespace Eagle.Domain.Events.Event
{
    public interface IDomainEvent : IEvent
    {
        #region Properties
        /// <summary>
        /// 获取产生领域事件的事件源对象。
        /// </summary>
        IEntity Source { get; }
        #endregion
    }
}
