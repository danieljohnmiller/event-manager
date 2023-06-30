using System;

namespace DJM.EventManager
{
    public interface IGlobalEventManager<TEventId>
    {
        public void AddHandler(TEventId eventId, Action handler);
        public void AddHandler<THandlerParam>(TEventId eventId, Action<THandlerParam> handler);

        public void RemoveHandler(TEventId eventId, Action handler);
        public void RemoveHandler<THandlerParam>(TEventId eventId, Action<THandlerParam> handler);

        public void TriggerEvent(TEventId eventId);
        public void TriggerEvent<THandlerParam>(TEventId eventId, THandlerParam param);
        
        public void ClearEvent(TEventId eventId);
        public void ClearAll();
    }
}


