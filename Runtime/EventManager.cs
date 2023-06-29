using System.Collections.Generic;
using System;

namespace DJM.EventManager
{
    /// <summary>
    /// Event manager to help decouple components with event-driven architecture. Events are identified with a generic type, and have no return type.
    /// </summary>
    /// <typeparam name="TEventId">Event identifier type. If using a reference type, ensure it uses value equality. Otherwise instances of the same value will reference different events.</typeparam>
    public class EventManager<TEventId>
    {
        private readonly IDictionary<TEventId, Action> _eventTable;
        
        public EventManager()
        {
            _eventTable = new Dictionary<TEventId, Action>();
        }
        
        public void AddObserver(TEventId eventId, Action handler)
        {
            if (_eventTable.ContainsKey(eventId))
                _eventTable[eventId] += handler;
            else
                _eventTable.Add(eventId, handler);
        }
        
        public void RemoveObserver(TEventId eventId, Action handler)
        {
            if (!_eventTable.ContainsKey(eventId))
                return;
            
            var eventAction = _eventTable[eventId] -= handler;

            if(eventAction is null)
                _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent(TEventId eventId)
        {
            if (_eventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke();
        }
        
        public void ClearEvent(TEventId eventId)
        {
            if( _eventTable.ContainsKey(eventId)) 
                _eventTable.Remove(eventId);
        }
        
        public void ClearAllEvents()
        {
            _eventTable.Clear();
        }
    }
    
    /// <summary>
    /// Event manager to help decouple components with event-driven architecture. Events are identified with a generic type, and return one generic value to observers.
    /// </summary>
    /// <typeparam name="TEventId">Event identifier type. If using a reference type, ensure it uses value equality. Otherwise instances of the same value will reference different events.</typeparam>
    /// <typeparam name="TParam">Type passed to observers when event triggered</typeparam>
    public class EventManager<TEventId, TParam>
    {
        private readonly IDictionary<TEventId, Action<TParam>> _eventTable;
        
        public EventManager()
        {
            _eventTable = new Dictionary<TEventId, Action<TParam>>();
        }
        
        public void AddObserver(TEventId eventId, Action<TParam> handler)
        {
            if (_eventTable.ContainsKey(eventId))
                _eventTable[eventId] += handler;
            else
                _eventTable.Add(eventId, handler);
        }
        
        public void RemoveObserver(TEventId eventId, Action<TParam> handler)
        {
            if (!_eventTable.ContainsKey(eventId))
                return;
            
            var eventAction = _eventTable[eventId] -= handler;

            if(eventAction is null)
                _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent(TEventId eventId, TParam param)
        {
            if (_eventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke(param);
        }
        
        public void ClearEvent(TEventId eventId)
        {
            if( _eventTable.ContainsKey(eventId)) 
                _eventTable.Remove(eventId);
        }
        
        public void ClearAllEvents()
        {
            _eventTable.Clear();
        }
    }

    /// <summary>
    /// Event manager to help decouple components with event-driven architecture. Events are identified with a generic type, and return one generic value to observers.
    /// </summary>
    /// <typeparam name="TEventId">Event identifier type. If using a reference type, ensure it uses value equality. Otherwise instances of the same value will reference different events.</typeparam>
    /// <typeparam name="TParam1">First type passed to observers when event triggered</typeparam>
    /// <typeparam name="TParam2">Second type passed to observers when event triggered</typeparam>
    public class EventManager<TEventId, TParam1, TParam2>
    {
        private readonly IDictionary<TEventId, Action<TParam1, TParam2>> _eventTable;
        
        public EventManager()
        {
            _eventTable = new Dictionary<TEventId, Action<TParam1, TParam2>>();
        }
        
        public void AddObserver(TEventId eventId, Action<TParam1, TParam2> handler)
        {
            if (_eventTable.ContainsKey(eventId))
                _eventTable[eventId] += handler;
            else
                _eventTable.Add(eventId, handler);
        }
        
        public void RemoveObserver(TEventId eventId, Action<TParam1, TParam2> handler)
        {
            if (!_eventTable.ContainsKey(eventId))
                return;
            
            var eventAction = _eventTable[eventId] -= handler;

            if(eventAction is null)
                _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent(TEventId eventId, TParam1 param1, TParam2 param2)
        {
            if (_eventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke(param1, param2);
        }
        
        public void ClearEvent(TEventId eventId)
        {
            if( _eventTable.ContainsKey(eventId)) 
                _eventTable.Remove(eventId);
        }
        
        public void ClearAllEvents()
        {
            _eventTable.Clear();
        }
    }
}