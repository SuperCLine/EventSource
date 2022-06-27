/*------------------------------------------------------------------------------
|
| COPYRIGHT (C) 2022 - 2028 All Right Reserved
|
| FILE NAME  : \ActionEngine\Runtime\Framework\Event\IEventSource.cs
| AUTHOR     : https://supercline.com/
| PURPOSE    :
|
| SPEC       :
|
| MODIFICATION HISTORY
|
| Ver      Date            By              Details
| -----    -----------    -------------   ----------------------
| 1.0      2022-6-27      SuperCLine           Created
|
+-----------------------------------------------------------------------------*/

namespace ActionEngine.Framework
{
    public interface IEventSource
    { }

    public interface IEventSource<in TEvent> : IEventSource where TEvent : struct
    {
        void Publish(TEvent evt);
    }

    public struct EventSource<TEvent> : IEventSource<TEvent> where TEvent : struct
    {
        public System.Action<TEvent> eventHandler;
        public void Publish(TEvent @event)
        {
            eventHandler?.Invoke(@event);
        }
    }
}