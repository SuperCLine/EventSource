/*------------------------------------------------------------------------------
|
| COPYRIGHT (C) 2022 - 2028 All Right Reserved
|
| FILE NAME  : \ActionEngine\Runtime\Framework\Event\IEvent.cs
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
    public interface IEvent
    { }

    public struct Event<TEvent> : IEvent where TEvent : struct
    {
        public TEvent evt;
    }
}