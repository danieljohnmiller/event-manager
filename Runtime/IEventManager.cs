using System;

namespace DJM.EventManager
{
    public interface IEventManager<TEventId, in THandler> where THandler : Delegate
    {
        public abstract void AddObserver(TEventId eventId, THandler handler);

        public abstract void RemoveObserver(TEventId eventId, THandler handler);

        public void ClearEvent(TEventId eventId);

        public void ClearAllEvents();
    }
}


