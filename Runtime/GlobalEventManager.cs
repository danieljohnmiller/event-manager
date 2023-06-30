using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DJM.EventManager
{
    public sealed class GlobalEventManager : IGlobalEventManager<int>
    {
        private static GlobalEventManager _globalEventManager;
        
        private readonly Dictionary<int, Dictionary<Type, Delegate>> _eventTable;

        private GlobalEventManager()
        {
            _eventTable = new Dictionary<int, Dictionary<Type, Delegate>>();
        }

        public static GlobalEventManager Instance
        {
            get { return _globalEventManager ??= new GlobalEventManager(); }
        }


        public void AddObserver(int eventId, Action handler)
        {
            // event id does not exist
            if (!_eventTable.ContainsKey(eventId))
            {
                _eventTable[eventId] = new Dictionary<Type, Delegate> {[typeof(void)] = handler};
                return;
            }

            // event id exists
            var handlerTypeTable = _eventTable[eventId];
            
            // no existing observers under given handler param type
            if (!handlerTypeTable.ContainsKey(typeof(void)))
            {
                handlerTypeTable[typeof(void)] = handler;
                return;
            }

            var observers = handlerTypeTable[typeof(void)];
            observers = Delegate.Combine(observers, handler);
        }

        public void AddObserver<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            // event id does not exist
            if (!_eventTable.ContainsKey(eventId))
            {
                _eventTable[eventId] = new Dictionary<Type, Delegate> {[typeof(THandlerParam)] = handler};
                return;
            }

            // event id exists
            var handlerTypeTable = _eventTable[eventId];
            
            // no existing observers under given handler param type
            if (!handlerTypeTable.ContainsKey(typeof(THandlerParam)))
            {
                handlerTypeTable[typeof(THandlerParam)] = handler;
                return;
            }

            var observers = handlerTypeTable[typeof(THandlerParam)];
            observers = Delegate.Combine(observers, handler);
        }

        public void RemoveObserver(int eventId, Action handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver<THandlerParam>(int eventId, Action<THandlerParam> handler)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent(int eventId)
        {
            if (!_eventTable.TryGetValue(eventId, out var typeTable)) return;

            if (!typeTable.ContainsKey(typeof(void))) return;
            
            var b = typeTable[typeof(void)] as Action;
            b?.Invoke();
        }

        public void TriggerEvent<THandlerParam>(int eventId, THandlerParam param)
        {
            if (!_eventTable.TryGetValue(eventId, out var typeTable)) return;

            if (!typeTable.ContainsKey(typeof(THandlerParam))) return;
            
            var b = typeTable[typeof(THandlerParam)] as Action<THandlerParam>;
            b?.Invoke(param);
        }

        public void ClearEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public void ClearEvent<THandlerParam>(int eventId)
        {
            throw new NotImplementedException();
        }

        public void ClearAll(int eventId)
        {
            throw new NotImplementedException();
        }

        public void ClearAll()
        {
            throw new NotImplementedException();
        }
    }
}


