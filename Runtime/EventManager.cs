using System;

namespace DJM.EventManager
{
    public class EventManager<TEventId> : EventManagerBase<TEventId>, IEventManager<TEventId>
    {
        public void AddObserver(TEventId eventId, Action handler)
        {
            AddHandler(eventId, handler, null);
        }

        public void AddObserver<TParam>(TEventId eventId, Action<TParam> handler)
        {
            var handlerSignature = new[] { typeof(TParam) };
            AddHandler(eventId, handler, handlerSignature);
        }

        public void AddObserver<TParam1, TParam2>(TEventId eventId, Action<TParam1, TParam2> handler)
        {
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2) };
            AddHandler(eventId, handler, handlerSignature);
        }

        public void AddObserver<TParam1, TParam2, TParam3>(TEventId eventId, Action<TParam1, TParam2, TParam3> handler)
        {
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) };
            AddHandler(eventId, handler, handlerSignature);
        }

        public void AddObserver<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, Action<TParam1, TParam2, TParam3, TParam4> handler)
        {
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4) };
            AddHandler(eventId, handler, handlerSignature);
        }

        public void RemoveObserver(TEventId eventId, Action handler)
        {
            RemoveHandler(eventId, handler, null);
        }

        public void RemoveObserver<TParam>(TEventId eventId, Action<TParam> handler)
        {
            var handlerSignature = new[] { typeof(TParam) };
            RemoveHandler(eventId, handler, handlerSignature);
        }

        public void RemoveObserver<TParam1, TParam2>(TEventId eventId, Action<TParam1, TParam2> handler)
        {
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2) };
            RemoveHandler(eventId, handler, handlerSignature);
        }

        public void RemoveObserver<TParam1, TParam2, TParam3>(TEventId eventId, Action<TParam1, TParam2, TParam3> handler)
        {
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) };
            RemoveHandler(eventId, handler, handlerSignature);
        }

        public void RemoveObserver<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, Action<TParam1, TParam2, TParam3, TParam4> handler)
        {
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4) };
            RemoveHandler(eventId, handler, handlerSignature);
        }

        public void TriggerEvent(TEventId eventId)
        {
            if (!EventTable.TryGetValue(eventId, out var eventData))
                return;

            ValidateHandlerSignature(eventId, eventData.HandlerParamSignature, null);
            
            var handlers = eventData.Handlers as Action;
            handlers?.Invoke();
        }

        public void TriggerEvent<TParam>(TEventId eventId, TParam param)
        {
            if (!EventTable.TryGetValue(eventId, out var eventData))
                return;
            
            var handlerSignature = new[] { typeof(TParam) };
            ValidateHandlerSignature(eventId, eventData.HandlerParamSignature, handlerSignature);
            
            var handlers = eventData.Handlers as Action<TParam>;
            handlers?.Invoke(param);
        }

        public void TriggerEvent<TParam1, TParam2>(TEventId eventId, TParam1 param1, TParam2 param2)
        {
            if (!EventTable.TryGetValue(eventId, out var eventData))
                return;
            
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2) };
            ValidateHandlerSignature(eventId, eventData.HandlerParamSignature, handlerSignature);
            
            var handlers = eventData.Handlers as Action<TParam1, TParam2>;
            handlers?.Invoke(param1, param2);
        }

        public void TriggerEvent<TParam1, TParam2, TParam3>(TEventId eventId, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            if (!EventTable.TryGetValue(eventId, out var eventData))
                return;
            
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) };
            ValidateHandlerSignature(eventId, eventData.HandlerParamSignature, handlerSignature);
            
            var handlers = eventData.Handlers as Action<TParam1, TParam2, TParam3>;
            handlers?.Invoke(param1, param2, param3);
        }

        public void TriggerEvent<TParam1, TParam2, TParam3, TParam4>(TEventId eventId, TParam1 param1, TParam2 param2, TParam3 param3,
            TParam4 param4)
        {
            if (!EventTable.TryGetValue(eventId, out var eventData))
                return;
            
            var handlerSignature = new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4) };
            ValidateHandlerSignature(eventId, eventData.HandlerParamSignature, handlerSignature);
            
            var handlers = eventData.Handlers as Action<TParam1, TParam2, TParam3, TParam4>;
            handlers?.Invoke(param1, param2, param3, param4);
        }

        public int GetObserverCount(TEventId eventId)
        {
            return EventTable.TryGetValue(eventId, out var eventData)
                ? eventData.Handlers.GetInvocationList().Length
                : 0;
        }

        public void ClearObservers(TEventId eventId)
        {
            EventTable.Remove(eventId);
        }

        public void ClearAllObservers()
        {
            EventTable.Clear();
        }
    }
}


