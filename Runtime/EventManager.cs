using System;
using DJM.EventManager;


namespace DJM.EventManager
{
    public class EventManager<TEventId> : IEventManager<TEventId>
    {
        public void AddObserver(TEventId eventId, Action handler)
        {
            throw new NotImplementedException();
        }

        public void AddObserver<TParam>(TEventId eventId, Action<TParam> handler)
        {
            throw new NotImplementedException();
        }

        public void AddObserver<TParam1, TParam2>(TEventId eventId, Action<TParam1, TParam2> handler)
        {
            throw new NotImplementedException();
        }

        public void AddObserver<TParam1, TParam2, TParam3>(TEventId eventId, Action<TParam1, TParam2, TParam3> handler)
        {
            throw new NotImplementedException();
        }

        public void AddObserver<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, Action<TParam1, TParam2, TParam3, TParam4> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(TEventId eventId, Action handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver<TParam>(TEventId eventId, Action<TParam> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver<TParam1, TParam2>(TEventId eventId, Action<TParam1, TParam2> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver<TParam1, TParam2, TParam3>(TEventId eventId, Action<TParam1, TParam2, TParam3> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, Action<TParam1, TParam2, TParam3, TParam4> handler)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent(TEventId eventId)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent<TParam>(TEventId eventId, TParam param)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent<TParam1, TParam2>(TEventId eventId, TParam1 param1, TParam2 param2)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent<TParam1, TParam2, TParam3>(TEventId eventId, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, TParam1 param1, TParam2 param2, TParam3 param3,
            TParam4 param4)
        {
            throw new NotImplementedException();
        }

        public bool ContainsEvent(TEventId eventId)
        {
            throw new NotImplementedException();
        }

        public void ClearEvent(TEventId eventId)
        {
            throw new NotImplementedException();
        }

        public void ClearAllEvents()
        {
            throw new NotImplementedException();
        }
    }
}


