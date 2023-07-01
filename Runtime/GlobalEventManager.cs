using System;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow.Diff;

namespace DJM.EventManager
{
    /// <summary>
    /// Facilitates the decoupled communication of components. Is a singleton, so only one instance can exist at a time.
    /// Events are identified using an int, and support parallel event handlers with zero or one generic parameter.
    /// Each event can have multiple sets of event handlers with different parameter types. This type may need to be specified when adding handlers,
    /// and when triggering its event. Only event handlers with that specified type will be triggered.
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
            var handlers = GetEventHandlers(eventId, typeof(void)) as Action;
            
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
            var handlers = GetEventHandlers(eventId, typeof(THandlerParam)) as Action<THandlerParam>;
            
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
        
        
        
        private void AddHandler(int eventId, Type handlerSignature, Delegate handler)
        {
            var handlerTable = GetEventHandlerTable(eventId);
            
            if (handlerTable is null)
            {
                _eventTable[eventId] = new Dictionary<Type, Delegate> {[handlerSignature] = handler};
                return;
            }
            
            var handlers = GetEventHandlers(handlerSignature, handlerTable);
            
            if (handlers is null)
            {
                handlerTable[handlerSignature] = handler;
                return;
            }
            
            handlerTable[handlerSignature] = Delegate.Combine(handlerTable[handlerSignature], handler);
        }
        
        private void RemoveHandler(int eventId, Type handlerSignature, Delegate handler)
        {
            var handlerTable = GetEventHandlerTable(eventId);
            
            if (handlerTable is null) return;
            
            var handlers = GetEventHandlers(handlerSignature, handlerTable);

            if(handlers is null) return;
            
            handlerTable[handlerSignature] = Delegate.Remove(handlers, handler);
        }
        
        
        
        private Delegate GetEventHandlers(int eventId, Type handlerSignature)
        {
            var eventHandlers = GetEventHandlerTable(eventId);

            if (eventHandlers is null)
                return null;

            return eventHandlers.TryGetValue(handlerSignature, out var value) ? value : null;
        }
        
        private static Delegate GetEventHandlers(Type handlerSignature, IReadOnlyDictionary<Type, Delegate> handlerTable)
        {
            return handlerTable.TryGetValue(handlerSignature, out var value) ? value : null;
        }

        private Dictionary<Type, Delegate> GetEventHandlerTable(int eventId)
        {
            return _eventTable.TryGetValue(eventId, out var handlerTable) 
                ? handlerTable : null;
        }
    }
}