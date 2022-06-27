/*------------------------------------------------------------------------------
|
| COPYRIGHT (C) 2022 - 2028 All Right Reserved
|
| FILE NAME  : \ActionEngine\Runtime\Framework\Event\EventMgr.cs
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
    using System;
    using System.Collections.Generic;

    public sealed class EventMgr
    {
        private Dictionary<Type, IEventAggregator> eventDict = new Dictionary<Type, IEventAggregator>();

        public void AddEvent<TEvent>(System.Action<TEvent> handler) where TEvent : struct
        {
            var eventType = typeof(TEvent);
            if (!eventDict.TryGetValue(eventType, out var evtAggregator))
            {
                evtAggregator = new EventAggregator<TEvent>();
                eventDict.Add(eventType, evtAggregator);
            }

            var holder = (EventAggregator<TEvent>)evtAggregator;
            holder.AddEvent(handler);
        }

        public void RemoveEvent<TEvent>(System.Action<TEvent> handler) where TEvent : struct
        {
            var eventType = typeof(TEvent);
            if (null == handler)
            {
                eventDict.Remove(eventType);
            }
            else
            {
                if (eventDict.TryGetValue(eventType, out var evtAggregator))
                {
                    var holder = (EventAggregator<TEvent>)evtAggregator;
                    holder.RemoveEvent(handler);
                }
            }
        }

        public void SendEvent<TEvent>(TEvent @event) where TEvent : struct
        {
            var eventType = typeof(TEvent);
            if (eventDict.TryGetValue(eventType, out var evtAggregator))
            {
                var holder = (EventAggregator<TEvent>)evtAggregator;
                holder.SendEvent(@event);
            }
        }

        public void PostEvent<TEvent>(TEvent @event) where TEvent : struct
        {
            var eventType = typeof(TEvent);
            if (eventDict.TryGetValue(eventType, out var evtAggregator))
            {
                var holder = (EventAggregator<TEvent>)evtAggregator;
                holder.PostEvent(@event);
            }
        }

        public void PollEvents()
        {
            using (var itr = eventDict.GetEnumerator())
            {
                while (itr.MoveNext())
                {
                    itr.Current.Value.Publish();
                }
            }
        }

    }
}