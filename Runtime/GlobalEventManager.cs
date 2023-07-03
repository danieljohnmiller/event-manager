using System;

namespace DJM.EventManager
{
    /// <summary>
    /// Facilitates the decoupled communication of components. Is a singleton, so only one instance can exist at a time.
    /// Events are identified using an int, and support event handlers with zero or one generic parameter.
    /// Each event can have multiple sets of event handlers with different parameter types. This type needs to be specified when adding the handlers,
    /// and triggering the event. Only event handlers with that specified type will be invoked.
    /// </summary>
    public sealed class GlobalEventManager : IGlobalEventManager
    {
        private static readonly object ThreadSafetyLock = new();
        
        private static GlobalEventManager _globalEventManager;
        
        private readonly NestedEventHandlerTable<int, Type> _eventHandlerTable;
        
        private GlobalEventManager()
        {
            _eventHandlerTable = new NestedEventHandlerTable<int, Type>();
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
            _eventHandlerTable.AddHandler(eventId, typeof(void), handler);
        }

        /// <summary>
        /// Add handler to event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Event handler with one generic parameters.</param>
        /// <typeparam name="THandlerParam">Generic type of handlers parameter.</typeparam>
        public void AddHandler<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            _eventHandlerTable.AddHandler(eventId, typeof(THandlerParam), handler);
        }
        
        /// <summary>
        /// Remove handler from event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Event handler with no parameters.</param>
        public void RemoveHandler(int eventId, Action handler)
        {
            _eventHandlerTable.RemoveHandler(eventId, typeof(void), handler);
        }

        /// <summary>
        /// Remove handler from event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Event handler with one generic parameter.</param>
        public void RemoveHandler<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            _eventHandlerTable.RemoveHandler(eventId, typeof(THandlerParam), handler);
        }

        /// <summary>
        /// Trigger event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        public void TriggerEvent(int eventId)
        {
            var handlers = _eventHandlerTable.GetHandlers(eventId, typeof(void)) as Action;
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
            var handlers = _eventHandlerTable.GetHandlers(eventId, typeof(THandlerParam)) as Action<THandlerParam>;
            handlers?.Invoke(param);
        }

        /// <summary>
        /// Clear all existing handlers from event.
        /// </summary>
        /// <param name="eventId"></param>
        public void ClearEvent(int eventId)
        {
           _eventHandlerTable.ClearEvent(eventId);
        }

        /// <summary>
        /// Clear event handlers with specified handler parameter type from event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handlerParamType">Handler parameter type to clear. Leave null to clear handlers with no parameter.</param>
        public void ClearEventHandlerType(int eventId, Type handlerParamType = null)
        {
            handlerParamType ??= typeof(void);
            _eventHandlerTable.ClearHandlers(eventId, handlerParamType);
        }

        /// <summary>
        /// Clear all existing handlers.
        /// </summary>
        public void ClearAll()
        {
            _eventHandlerTable.ClearAll();
        }
    }
}