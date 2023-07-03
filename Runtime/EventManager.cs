using System;
using System.Collections.Generic;

namespace DJM.EventManager
{
    /// <summary>
    /// The Event Manager facilitates decoupling of components in event-driven architecture. Events are identified using an integer value and invoke event handlers that do not require any parameters.
    /// </summary>
    [Obsolete("Replaced by GlobalEventManager", false)]
    public class EventManager : EventManagerBase<int, Action>
    {
        public override void AddObserver(int eventId, Action handler)
        {
            if (EventTable.ContainsKey(eventId))
                EventTable[eventId] += handler;
            else
                EventTable.Add(eventId, handler);
        }
        
        public override void RemoveObserver(int eventId, Action handler)
        {
            if (!EventTable.ContainsKey(eventId))
                return;
            
            var eventAction = EventTable[eventId] -= handler;

            if(eventAction is null)
                EventTable.Remove(eventId);
        }
        
        public void TriggerEvent(int eventId)
        {
            if (EventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke();
        }
    }
    
    /// <summary>
    /// The Event Manager facilitates decoupling of components in event-driven architecture. Events are identified using an integer value and invoke event handlers that require one generic parameter.
    /// </summary>
    /// <typeparam name="TParam">Event handler parameter.</typeparam>
    [Obsolete("Replaced by GlobalEventManager", false)]
    public class EventManager<TParam> : EventManagerBase<int, Action<TParam>>
    {

        public IDictionary<int,Action<TParam>> GetDictTemp()
        {
            return EventTable;
        }

        public override void AddObserver(int eventId, Action<TParam> handler)
        {
            if (EventTable.ContainsKey(eventId))
                EventTable[eventId] += handler;
            else
                EventTable.Add(eventId, handler);
        }
        
        public override void RemoveObserver(int eventId, Action<TParam> handler)
        {
            if (!EventTable.ContainsKey(eventId))
                return;
            
            var eventAction = EventTable[eventId] -= handler;

            if(eventAction is null)
                EventTable.Remove(eventId);
        }

        public void TriggerEvent(int eventId, TParam param)
        {
            if (EventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke(param);
        }
    }

    /// <summary>
    /// The Event Manager facilitates decoupling of components in event-driven architecture. Events are identified using an integer value and invoke event handlers that require two generic parameters.
    /// </summary>
    /// <typeparam name="TParam1">First event handler parameter.</typeparam>
    /// <typeparam name="TParam2">Second event handler parameter.</typeparam>
    [Obsolete("Replaced by GlobalEventManager", false)]
    public class EventManager<TParam1, TParam2> : EventManagerBase<int, Action<TParam1, TParam2>>
    {
        public override void AddObserver(int eventId, Action<TParam1, TParam2> handler)
        {
            if (EventTable.ContainsKey(eventId))
                EventTable[eventId] += handler;
            else
                EventTable.Add(eventId, handler);
        }
        
        public override void RemoveObserver(int eventId, Action<TParam1, TParam2> handler)
        {
            if (!EventTable.ContainsKey(eventId))
                return;
            
            var eventAction = EventTable[eventId] -= handler;
    
            if(eventAction is null)
                EventTable.Remove(eventId);
        }
        
        public void TriggerEvent(int eventId, TParam1 param1, TParam2 param2)
        {
            if (EventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke(param1, param2);
        }
    }
}