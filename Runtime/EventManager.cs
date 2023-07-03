using System;

namespace DJM.EventManager
{
    public class EventManager : IEventManager
    {
        private readonly NestedEventHandlerTable<int, Type> _eventHandlerTable;
        
        public EventManager()
        {
            _eventHandlerTable = new NestedEventHandlerTable<int, Type>();
        }
        
        public void AddHandler(int eventId, Action handler)
        {
            _eventHandlerTable.AddHandler(eventId, typeof(void), handler);
        }

        public void AddHandler<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            _eventHandlerTable.AddHandler(eventId, typeof(THandlerParam), handler);
        }

        public void RemoveHandler(int eventId, Action handler)
        {
            _eventHandlerTable.RemoveHandler(eventId, typeof(void), handler);
        }

        public void RemoveHandler<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            _eventHandlerTable.RemoveHandler(eventId, typeof(THandlerParam), handler);
        }

        public void TriggerEvent(int eventId)
        {
            var handlers = _eventHandlerTable.GetHandlers(eventId, typeof(void)) as Action;
            handlers?.Invoke();
        }

        public void TriggerEvent<THandlerParam>(int eventId, THandlerParam param)
        {
            var handlers = _eventHandlerTable.GetHandlers(eventId, typeof(THandlerParam)) as Action<THandlerParam>;
            handlers?.Invoke(param);
        }

        public void RemoveEvent(int eventId)
        {
            _eventHandlerTable.ClearEvent(eventId);
        }

        public void RemoveEventHandlerType(int eventId, Type handlerParamType)
        {
            handlerParamType ??= typeof(void);
            _eventHandlerTable.ClearHandlers(eventId, handlerParamType);
        }

        public void RemoveAll()
        {
            _eventHandlerTable.ClearAll();
        }
    }
}