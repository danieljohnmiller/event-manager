using System;
using System.Collections.Generic;

namespace DJM.EventManager
{
    public class NestedEventTable<TOuterKey, TInnerKey>
    {
        private readonly Dictionary<TOuterKey, Dictionary<TInnerKey, Delegate>> _outerDict;

        public NestedEventTable()
        {
            _outerDict = new Dictionary<TOuterKey, Dictionary<TInnerKey, Delegate>>();
        }

        public Delegate GetValue(TOuterKey outerKey, TInnerKey innerKey)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
                return default(Delegate);
            
            return innerDict.TryGetValue(innerKey, out var dictValue) ? dictValue : default(Delegate);
        }
        
        public void Add(TOuterKey outerKey, TInnerKey innerKey, Delegate value)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
            {
                _outerDict[outerKey] = new Dictionary<TInnerKey, Delegate> {[innerKey] = value};
                return;
            }

            if (!innerDict.TryGetValue(innerKey, out var innerValue))
            {
                innerDict[innerKey] = value;
                return;
            }
            
            innerDict[innerKey] = Delegate.Combine(innerValue, value);
        }
        
        public void Remove(TOuterKey outerKey, TInnerKey innerKey, Delegate value)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
                return;
            
            if (!innerDict.TryGetValue(innerKey, out var innerValue))
                return;
            
            innerDict[innerKey] = Delegate.Remove(innerValue, value);
        }

        public void Clear(TOuterKey outerKey, TInnerKey innerKey)
        {
            if (!_outerDict.TryGetValue(outerKey, out var innerDict))
                return;

            innerDict.Remove(innerKey);
        }
        
        public void Clear(TOuterKey outerKey)
        {
            _outerDict.Remove(outerKey);
        }

        public void Clear()
        {
            _outerDict.Clear();
        }
    }
}