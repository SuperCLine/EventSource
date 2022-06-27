/*------------------------------------------------------------------------------
|
| COPYRIGHT (C) 2022 - 2028 All Right Reserved
|
| FILE NAME  : \ActionEngine\Runtime\Framework\Event\IEventAggregator.cs
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
    using System.Collections.Generic;

    public interface IEventAggregator
    {
        void Publish();
    }

    public sealed class EventAggregator<TEvent> : IEventAggregator where TEvent : struct
    {
        private List<IEvent> eventList = new List<IEvent>();
        private List<IEventSource> handlerList = new List<IEventSource>();

        private List<IEvent> cacheList = new List<IEvent>();

        public void AddEvent(System.Action<TEvent> handler)
        {
            var evtSource = new EventSource<TEvent>() { eventHandler = handler };
            handlerList.Add(evtSource);
        }

        public void RemoveEvent(System.Action<TEvent> handler)
        {
            var key = handlerList.Find((h) =>
            {
                EventSource<TEvent> evtSource = (EventSource<TEvent>)h;
                return handler == evtSource.eventHandler;
            });

            handlerList.Remove(key);
        }

        public void SendEvent(TEvent @event)
        {
            handlerList.ForEach((e) =>
            {
                EventSource<TEvent> handler = (EventSource<TEvent>)e;
                handler.Publish(@event);
            });
        }

        public void PostEvent(TEvent @event)
        {
            lock (eventList)
            {
                var evt = new Event<TEvent>();
                evt.evt = @event;

                eventList.Add(evt);
            }
        }

        public void Publish()
        {
            lock (eventList)
            {
                cacheList.AddRange(eventList);
                eventList.Clear();
            }

            using (var itr = cacheList.GetEnumerator())
            {
                while (itr.MoveNext())
                {
                    var evt = (Event<TEvent>)itr.Current;
                    SendEvent(evt.evt);
                }
            }
            cacheList.Clear();
        }

    }
}