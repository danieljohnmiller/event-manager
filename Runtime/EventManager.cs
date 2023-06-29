using System.Collections.Generic;
using System;

namespace DJM.EventManager
{
    /// <summary>
    /// Event manager to help decouple components with event-driven architecture. Events are identified with an int, and have no return type.
    /// </summary>
    public class EventManager
    {
        private readonly IDictionary<int, Action> _eventTable;
        
        public EventManager()
        {
            _eventTable = new Dictionary<int, Action>();
        }
        
        public void AddObserver(int eventId, Action handler)
        {
            if (_eventTable.ContainsKey(eventId))
                _eventTable[eventId] += handler;
            else
                _eventTable.Add(eventId, handler);
        }
        
        public void RemoveObserver(int eventId, Action handler)
        {
            if (!_eventTable.ContainsKey(eventId))
                return;
            
            var eventAction = _eventTable[eventId] -= handler;

            if(eventAction is null)
                _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent(int eventId)
        {
            if (_eventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke();
        }
        
        public void ClearEvent(int eventId)
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
    /// Event manager to help decouple components with event-driven architecture. Events are identified with an int, and return one generic value to observers.
    /// </summary>
    /// <typeparam name="TParam">Type passed to observers when event triggered</typeparam>
    public class EventManager<TParam>
    {
        private readonly IDictionary<int, Action<TParam>> _eventTable;
        
        public EventManager()
        {
            _eventTable = new Dictionary<int, Action<TParam>>();
        }
        
        public void AddObserver(int eventId, Action<TParam> handler)
        {
            if (_eventTable.ContainsKey(eventId))
                _eventTable[eventId] += handler;
            else
                _eventTable.Add(eventId, handler);
        }
        
        public void RemoveObserver(int eventId, Action<TParam> handler)
        {
            if (!_eventTable.ContainsKey(eventId))
                return;
            
            var eventAction = _eventTable[eventId] -= handler;

            if(eventAction is null)
                _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent(int eventId, TParam param)
        {
            if (_eventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke(param);
        }
        
        public void ClearEvent(int eventId)
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
    /// Event manager to help decouple components with event-driven architecture. Events are identified with an int, and return one generic value to observers.
    /// </summary>
    /// <typeparam name="TParam1">First type passed to observers when event triggered</typeparam>
    /// <typeparam name="TParam2">Second type passed to observers when event triggered</typeparam>
    public class EventManager<TParam1, TParam2>
    {
        private readonly IDictionary<int, Action<TParam1, TParam2>> _eventTable;
        
        public EventManager()
        {
            _eventTable = new Dictionary<int, Action<TParam1, TParam2>>();
        }
        
        public void AddObserver(int eventId, Action<TParam1, TParam2> handler)
        {
            if (_eventTable.ContainsKey(eventId))
                _eventTable[eventId] += handler;
            else
                _eventTable.Add(eventId, handler);
        }
        
        public void RemoveObserver(int eventId, Action<TParam1, TParam2> handler)
        {
            if (!_eventTable.ContainsKey(eventId))
                return;
            
            var eventAction = _eventTable[eventId] -= handler;

            if(eventAction is null)
                _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent(int eventId, TParam1 param1, TParam2 param2)
        {
            if (_eventTable.TryGetValue(eventId, out var eventAction))
                eventAction?.Invoke(param1, param2);
        }
        
        public void ClearEvent(int eventId)
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