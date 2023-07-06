using System;
using System.Collections.Generic;
using System.Linq;

namespace DJM.EventManager
{
    public abstract class EventTable<TEventId>
    {
        private const string HandlerParamSignatureArgumentExceptionFormat = "handler: Handler’s signature does not match the expected signature for event ID {0}. Expected: {1}, Actual: {2}";
        
        private readonly IDictionary<TEventId, EventData> _eventTable;

        protected EventTable()
        {
            _eventTable = new Dictionary<TEventId, EventData>();
        }
        
        
        // the following methods will throw exceptions if you input incorrect handler params.
        
        protected void AddHandler(TEventId eventId, Delegate handler, Type[] handlerParamSignature)
        {
            if (_eventTable.TryGetValue(eventId, out var eventData))
            {
                ValidateHandlerParamSignature(eventId, eventData.HandlerParamSignature, handlerParamSignature);
                
                eventData.Handlers = Delegate.Combine(eventData.Handlers, handler);
                _eventTable[eventId] = eventData;
            }
            else
                _eventTable[eventId] = new EventData(handler, handlerParamSignature);
        }
        
        protected void RemoveHandler(TEventId eventId, Delegate handler, Type[] handlerParamSignature)
        {
            if (!_eventTable.TryGetValue(eventId, out var eventData))
                return;
            
            ValidateHandlerParamSignature(eventId, eventData.HandlerParamSignature, handlerParamSignature);
            
            var remainingHandlers = Delegate.Remove(eventData.Handlers, handler);

            if (remainingHandlers is not null)
            {
                eventData.Handlers = remainingHandlers;
                _eventTable[eventId] = eventData;
                return;
            }
            
            _eventTable.Remove(eventId);
        }

        protected static void ValidateHandlerParamSignature(TEventId eventId, Type[] expectedTypes, Type[] actualTypes)
        {
            if (expectedTypes.SequenceEqual(actualTypes)) return;
            
            var expected = string.Join(", ", expectedTypes.Select(t => t.Name));
            var actual = string.Join(", ", actualTypes.Select(t => t.Name));
                    
            throw new ArgumentException(string.Format(HandlerParamSignatureArgumentExceptionFormat, eventId, expected, actual));
        }
    }
}