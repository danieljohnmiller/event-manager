using System;
using System.Collections.Generic;
using System.Linq;

namespace DJM.EventManager
{
    public abstract class EventManagerBase<TEventId>
    {
        private const string HandlerParamSignatureArgumentExceptionFormat = "handler: Handler’s signature does not match the expected signature for event ID {0}. Expected: {1}, Actual: {2}";
        
        protected readonly IDictionary<TEventId, EventData> EventTable;

        protected EventManagerBase()
        {
            EventTable = new Dictionary<TEventId, EventData>();
        }
        
        
        // the following methods will throw exceptions if you input incorrect handler params.
        
        protected void AddHandler(TEventId eventId, Delegate handler, Type[] handlerParamSignature)
        {
            if (EventTable.TryGetValue(eventId, out var eventData))
            {
                ValidateHandlerSignature(eventId, eventData.HandlerParamSignature, handlerParamSignature);
                
                if(eventData.Handlers.GetInvocationList().Contains(handler)) return;
                
                eventData.Handlers = Delegate.Combine(eventData.Handlers, handler);
                EventTable[eventId] = eventData;
            }
            else
                EventTable[eventId] = new EventData(handler, handlerParamSignature);
        }
        
        protected void RemoveHandler(TEventId eventId, Delegate handler, Type[] handlerParamSignature)
        {
            if (!EventTable.TryGetValue(eventId, out var eventData))
                return;
            
            ValidateHandlerSignature(eventId, eventData.HandlerParamSignature, handlerParamSignature);
            
            var remainingHandlers = Delegate.Remove(eventData.Handlers, handler);

            if (remainingHandlers is not null)
            {
                eventData.Handlers = remainingHandlers;
                EventTable[eventId] = eventData;
                return;
            }
            
            EventTable.Remove(eventId);
        }

        // protected EventData GetEvent(TEventId eventId)
        // {
        //     return EventTable.TryGetValue(eventId, out var eventData) ? eventData : null;
        // }
        //
        // protected void RemoveEvent(TEventId eventId)
        // {
        //     EventTable.Remove(eventId);
        // }

        protected static void ValidateHandlerSignature(TEventId eventId, Type[] expectedTypes, Type[] actualTypes)
        {
            // match as no parameters for either
            if(expectedTypes is null && actualTypes is null) return;
            
            // match as neither null, and sequence equal
            if (expectedTypes != null && actualTypes != null && expectedTypes.SequenceEqual(actualTypes)) return;
            
            // no match, throw exception
            var expected = expectedTypes is null ? "null" : string.Join(", ", expectedTypes.Select(t => t.Name));
            var actual = actualTypes is null ? "null" : string.Join(", ", actualTypes.Select(t => t.Name));
                    
            throw new ArgumentException(string.Format(HandlerParamSignatureArgumentExceptionFormat, eventId, expected, actual));
        }
    }
}