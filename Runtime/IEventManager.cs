using System;

namespace DJM.EventManager
{
    public interface IEventManager<TEventId>
    {
        public void AddObserver(TEventId eventId, Action handler);
        public void AddObserver<TParam>(TEventId eventId, Action<TParam> handler);
        public void AddObserver<TParam1, TParam2>(TEventId eventId, Action<TParam1, TParam2> handler);
        public void AddObserver<TParam1, TParam2, TParam3>(TEventId eventId, Action<TParam1, TParam2, TParam3> handler);
        public void AddObserver<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, Action<TParam1, TParam2, TParam3, TParam4> handler);

        public void RemoveObserver(TEventId eventId, Action handler);
        public void RemoveObserver<TParam>(TEventId eventId, Action<TParam> handler);
        public void RemoveObserver<TParam1, TParam2>(TEventId eventId, Action<TParam1, TParam2> handler);
        public void RemoveObserver<TParam1, TParam2, TParam3>(TEventId eventId, Action<TParam1, TParam2, TParam3> handler);
        public void RemoveObserver<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, Action<TParam1, TParam2, TParam3, TParam4> handler);

        public void TriggerEvent(TEventId eventId);
        public void TriggerEvent<TParam>(TEventId eventId, TParam param);
        public void TriggerEvent<TParam1, TParam2>(TEventId eventId, TParam1 param1, TParam2 param2);
        public void TriggerEvent<TParam1, TParam2, TParam3>(TEventId eventId, TParam1 param1, TParam2 param2, TParam3 param3);
        public void TriggerEvent<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);

        public int GetObserverCount(TEventId eventId);
        
        public void ClearObservers(TEventId eventId);
        
        public void ClearAllObservers();
    }
}


