using System;
using JetBrains.Annotations;

namespace DJM.Legacy.EventManager
{
    /// <summary>
    /// Facilitates decoupled communication of components with events.
    /// Events are identified using an int, and support event handlers with zero or one generic parameter.
    /// Event handlers with different parameter types can be added to the same event.
    /// Event handler parameter type needs to be specified when adding the handler, and triggering its event.
    /// When triggered, only event handlers with the specified parameter type will be invoked.
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// Add handler to event.
        /// </summary>
        /// <param name="eventId">Handler identifier.</param>
        /// <param name="handler">Handler to add.</param>
        public void AddHandler(int eventId, Action handler);
        
        /// <summary>
        /// Add handler to event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Handler to add.</param>
        /// <typeparam name="THandlerParam">Handler parameter type.</typeparam>
        public void AddHandler<THandlerParam>(int eventId, Action<THandlerParam> handler);

        /// <summary>
        /// Remove handler from event
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Handler to remove.</param>
        public void RemoveHandler(int eventId, Action handler);
        
        /// <summary>
        /// Remove handler from event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handler">Handler to remove.</param>
        /// <typeparam name="THandlerParam">Handler parameter type.</typeparam>
        public void RemoveHandler<THandlerParam>(int eventId, Action<THandlerParam> handler);

        /// <summary>
        /// Trigger event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        public void TriggerEvent(int eventId);
        
        /// <summary>
        /// Trigger event
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="param">Event handler parameter.</param>
        /// <typeparam name="THandlerParam">Event handler parameter type.</typeparam>
        public void TriggerEvent<THandlerParam>(int eventId, THandlerParam param);
        
        /// <summary>
        /// Remove event and its handlers.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        public void RemoveEvent(int eventId);
        
        /// <summary>
        /// Remove handlers of given parameter type from event.
        /// </summary>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="handlerParamType">Handler parameter type. Set to null for handlers with no parameter.</param>
        public void RemoveEventHandlerType(int eventId, [CanBeNull] Type handlerParamType);
        
        /// <summary>
        /// Remove all events and handlers.
        /// </summary>
        public void RemoveAll();
    }
}


