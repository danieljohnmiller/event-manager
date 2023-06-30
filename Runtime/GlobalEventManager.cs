using System;
using System.Collections.Generic;

namespace DJM.EventManager
{
    /// <summary>
    /// Facilitates the decoupled communication of components. Is a singleton, so only one instance can exist at any time.
    /// Events are identified using an int, and support multiple event handlers with zero or one generic parameter.
    /// Each event can have event handlers of different parameter types. This type may need to be specified when adding the handlers,
    /// and when triggering its event. Only event handlers with the given parameter type will be triggered.
    /// </summary>
    public sealed class GlobalEventManager : IGlobalEventManager<int>
    {
        private static readonly object ThreadSafetyLock = new();
        
        private static GlobalEventManager _globalEventManager;
        
        private readonly Dictionary<int, Dictionary<Type, Delegate>> _eventTable;

        private GlobalEventManager()
        {
            _eventTable = new Dictionary<int, Dictionary<Type, Delegate>>();
        }

        /// <summary>
        /// Singleton instance of GlobalEventManager.
        /// </summary>
        public static GlobalEventManager Instance
        {
            get
            {
                lock (ThreadSafetyLock)
                {
                    return _globalEventManager ??= new GlobalEventManager();
                }
            }
        }
        
        /// <summary>
        /// Add handler to event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Event handler with no parameters.</param>
        public void AddHandler(int eventId, Action handler)
        {
            AddHandler(eventId, typeof(void), handler);
        }

        /// <summary>
        /// Add handler to event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Event handler with one generic parameters.</param>
        /// <typeparam name="THandlerParam">Generic type of handlers parameter.</typeparam>
        public void AddHandler<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            AddHandler(eventId, typeof(THandlerParam), handler);
        }
        
        /// <summary>
        /// Remove handler from event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Event handler with no parameters.</param>
        public void RemoveHandler(int eventId, Action handler)
        {
            RemoveHandler(eventId, typeof(void), handler);
        }

        /// <summary>
        /// Remove handler from event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Event handler with one generic parameter.</param>
        public void RemoveHandler<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            RemoveHandler(eventId, typeof(THandlerParam), handler);
        }

        /// <summary>
        /// Trigger event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        public void TriggerEvent(int eventId)
        {
            var handlerParamId = typeof(void);
            
            if (!_eventTable.TryGetValue(eventId, out var handlerTable)) 
                return;

            if (!handlerTable.ContainsKey(handlerParamId)) 
                return;
            
            var handlers = handlerTable[handlerParamId] as Action;
            handlers?.Invoke();
        }

        /// <summary>
        /// Trigger event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="param">Event handler parameter.</param>
        /// <typeparam name="THandlerParam">Event handler parameter type.</typeparam>
        public void TriggerEvent<THandlerParam>(int eventId, THandlerParam param)
        {
            var handlerParamId = typeof(THandlerParam);
            
            if (!_eventTable.TryGetValue(eventId, out var handlerTable))
                return;

            if (!handlerTable.ContainsKey(handlerParamId)) 
                return;
            
            var handlers = handlerTable[handlerParamId] as Action<THandlerParam>;
            handlers?.Invoke(param);
        }

        /// <summary>
        /// Clear all existing handlers from event.
        /// </summary>
        /// <param name="eventId"></param>
        public void ClearEvent(int eventId)
        {
            _eventTable.Remove(eventId);
        }

        /// <summary>
        /// Clear all existing handlers.
        /// </summary>
        public void ClearAll()
        {
            _eventTable.Clear();
        }
        
        private void AddHandler(int eventId, Type handlerParamId, Delegate handler)
        {
            if (!_eventTable.ContainsKey(eventId))
            {
                _eventTable[eventId] = new Dictionary<Type, Delegate> {[handlerParamId] = handler};
                return;
            }
            
            var handlerTypeTable = _eventTable[eventId];
            
            if (!handlerTypeTable.ContainsKey(handlerParamId))
            {
                handlerTypeTable[handlerParamId] = handler;
                return;
            }
            
            handlerTypeTable[handlerParamId] = Delegate.Combine(handlerTypeTable[handlerParamId], handler);
        }
        
        private void RemoveHandler(int eventId, Type handlerParamId, Delegate handler)
        {
            if (!_eventTable.TryGetValue(eventId, out var handlerTable))
                return;

            if(!handlerTable.TryGetValue(handlerParamId, out var handlers))
                return;
            
            handlerTable[handlerParamId] = Delegate.Remove(handlers, handler);
        }
    }
}