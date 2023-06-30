using System;

namespace DJM.EventManager
{
    public interface IGlobalEventManager<TEventId>
    {
        public void AddObserver(TEventId eventId, Action handler);
        public void AddObserver<THandlerParam>(TEventId eventId, Action<THandlerParam> handler);

        public void RemoveObserver(TEventId eventId, Action handler);
        public void RemoveObserver<THandlerParam>(TEventId eventId, Action<THandlerParam> handler);

        public void TriggerEvent(TEventId eventId);
        public void TriggerEvent<THandlerParam>(TEventId eventId, THandlerParam param);

        public void ClearEvent(TEventId eventId);
        public void ClearEvent<THandlerParam>(TEventId eventId);

        public void ClearAll(TEventId eventId);
        public void ClearAll();
    }
}


