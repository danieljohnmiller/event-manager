using System;

namespace DJM.EventManager
{
    /// <summary>
    /// Event manager to help decouple components with event-driven architecture. Events are identified with an int, and have no return type.
    /// </summary>
    public class EventManager : EventManagerBase<Action>
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
    /// Event manager to help decouple components with event-driven architecture. Events are identified with an int, and return one generic value to observers.
    /// </summary>
    /// <typeparam name="TParam">Type passed to observers when event triggered</typeparam>
    public class EventManager<TParam> : EventManagerBase<Action<TParam>>
    {
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
    /// Event manager to help decouple components with event-driven architecture. Events are identified with an int, and return one generic value to observers.
    /// </summary>
    /// <typeparam name="TParam1">First type passed to observers when event triggered</typeparam>
    /// <typeparam name="TParam2">Second type passed to observers when event triggered</typeparam>
    public class EventManager<TParam1, TParam2> : EventManagerBase<Action<TParam1, TParam2>>
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