using System;

namespace DJM.EventManager
{
    public interface IEventManagerOneParam<TEventId>
    {
        public void AddObserver<TEventParam>(TEventId eventId, Action<TEventParam> handler);

        public void RemoveObserver<TEventParam>(TEventId eventId, Action<TEventParam> handler);

        public void TriggerEvent<TEventParam>(TEventId eventId, TEventParam param);

        public void ClearEvent<TEventParam>(TEventId eventId);

        public void ClearAllEvents();
    }
}


