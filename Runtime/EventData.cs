using System;

namespace DJM.EventManager
{
    internal sealed class EventData
    {
        internal EventData(Delegate handlers, Type[] handlerParamSignature)
        {
            Handlers = handlers;
            HandlerParamSignature = handlerParamSignature;
        }
        
        internal Delegate Handlers;
        internal Type[] HandlerParamSignature { get; private set; }
    }
}