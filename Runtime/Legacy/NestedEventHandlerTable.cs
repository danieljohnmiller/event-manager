using System;
using System.Collections.Generic;

namespace DJM.Legacy.EventManager
{
    public class NestedEventHandlerTable<TOuterKey, TInnerKey>
    {
        private readonly Dictionary<TOuterKey, Dictionary<TInnerKey, Delegate>> _outerDict;

        public NestedEventHandlerTable()
        {
            _outerDict = new Dictionary<TOuterKey, Dictionary<TInnerKey, Delegate>>();
        }

        public Delegate GetHandlers(TOuterKey outerKey, TInnerKey innerKey)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
                return default(Delegate);
            
            return innerDict.TryGetValue(innerKey, out var dictValue) ? dictValue : default(Delegate);
        }
        
        public void AddHandler(TOuterKey outerKey, TInnerKey innerKey, Delegate handler)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
            {
                _outerDict[outerKey] = new Dictionary<TInnerKey, Delegate> {[innerKey] = handler};
                return;
            }

            if (!innerDict.TryGetValue(innerKey, out var innerValue))
            {
                innerDict[innerKey] = handler;
                return;
            }
            
            innerDict[innerKey] = Delegate.Combine(innerValue, handler);
        }
        
        public void RemoveHandler(TOuterKey outerKey, TInnerKey innerKey, Delegate handler)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
                return;
            
            if (!innerDict.TryGetValue(innerKey, out var handlers))
                return;
            
            var result = Delegate.Remove(handlers, handler);

            if (result is null)
            {
                innerDict.Remove(innerKey);

                if (innerDict.Count == 0)
                    ClearEvent(outerKey);
            }
            else
            {
                innerDict[innerKey] = result;
            }
        }

        public void ClearHandlers(TOuterKey outerKey, TInnerKey innerKey)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
                return;

            innerDict.Remove(innerKey);

            if (innerDict.Count == 0)
            {
                ClearEvent(outerKey);
            }
        }
        
        public void ClearEvent(TOuterKey outerKey)
        {
            _outerDict.Remove(outerKey);
        }

        public void ClearAll()
        {
            _outerDict.Clear();
        }
    }
}