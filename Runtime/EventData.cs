using System;

namespace DJM.EventManager
{
    public class EventData
    {
        public EventData(Delegate handlers, Type[] handlerParamSignature)
        {
            Handlers = handlers;
            HandlerParamSignature = handlerParamSignature;
        }
        
        public Delegate Handlers;
        public Type[] HandlerParamSignature { get; private set; }
    }
}