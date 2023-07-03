using System;
using JetBrains.Annotations;

namespace DJM.EventManager
{
    public interface IGlobalEventManager
    {
        public void AddHandler(int eventId, Action handler);
        public void AddHandler<THandlerParam>(int eventId, Action<THandlerParam> handler);

        public void RemoveHandler(int eventId, Action handler);
        public void RemoveHandler<THandlerParam>(int eventId, Action<THandlerParam> handler);

        public void TriggerEvent(int eventId);
        public void TriggerEvent<THandlerParam>(int eventId, THandlerParam param);
        
        public void ClearEvent(int eventId);
        public void ClearEventHandlerType(int eventId, [CanBeNull] Type handlerParamType = null);
        public void ClearAll();
    }
}


