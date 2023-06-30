using System;
using System.Collections.Generic;

namespace DJM.EventManager
{
    public abstract class EventManagerBase<TEventId, TDelegate> : IEventManager<TEventId, TDelegate> where TDelegate : Delegate
    {
        protected readonly IDictionary<TEventId, TDelegate> EventTable;

        protected EventManagerBase()
        {
            EventTable = new Dictionary<TEventId, TDelegate>();
        }
        
        public abstract void AddObserver(TEventId eventId, TDelegate handler);

        public abstract void RemoveObserver(TEventId eventId, TDelegate handler);

        public void ClearEvent(TEventId eventId)
        {
            if( EventTable.ContainsKey(eventId)) 
                EventTable.Remove(eventId);
        }
        
        public void ClearAllEvents()
        {
            EventTable.Clear();
        }
    }
}


