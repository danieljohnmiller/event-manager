namespace DJM.EventManager
{
    public sealed class EventManagerSingleton
    {
        private static EventManagerSingleton _eventManagerSingleton;

        private EventManagerSingleton()
        {
        }

        public static EventManagerSingleton Instance
        {
            get { return _eventManagerSingleton ??= new EventManagerSingleton(); }
        }
        
        
    }
}


