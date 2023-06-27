using System.Collections.Generic;
using System;

namespace DJM.EventManager
{
    /// <summary>
    /// Simple event manager for event-driven architecture. Events are identified with a generic type. Currently return types are not supported.
    /// </summary>
    /// <typeparam name="TEventId">Event identifier type</typeparam>
    public class EventManager<TEventId>
    {
        private readonly IDictionary<TEventId, Action> _eventTable;
        
        public EventManager()
        {
            _eventTable = new Dictionary<TEventId, Action>();
        }
        
        public void AddObserver(TEventId eventId, Action handler)
        {
            var messageIdExists = _eventTable.TryGetValue(eventId, out var eventAction);

            eventAction += handler;

            if (messageIdExists)
            {
                _eventTable[eventId] = eventAction;
            }
            else
            {
                _eventTable.Add(eventId, eventAction);
            }
        }
        
        public void RemoveObserver(TEventId eventId, Action handler)
        {
            if (!_eventTable.TryGetValue(eventId, out var eventAction)) return;
            
            eventAction -= handler;

            if (eventAction is null)
                _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent(TEventId eventId)
        {
            if (!_eventTable.TryGetValue(eventId, out var eventAction)) return;
            
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
}