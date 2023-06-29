using System;
using System.Collections.Generic;

namespace DJM.EventManager
{
    public abstract class EventManagerBase<T> where T : MulticastDelegate
    {
        protected readonly IDictionary<int, T> EventTable;

        protected EventManagerBase()
        {
            EventTable = new Dictionary<int, T>();
        }


        public abstract void AddObserver(int eventId, T handler);

        public abstract void RemoveObserver(int eventId, T handler);

        public void ClearEvent(int eventId)
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


