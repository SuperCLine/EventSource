# EventSource
Event Sourcing pattern in C# used by ECS Framework.

# [Architecture](https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing)

# USage
```c#
public struct TEventA
{
    public int a;
    public string b;
}

void Handle_TEventA(TEventA e)
{
    Debug.Log(e.a + ", " + e.b);
}

var eventMgr = new EventMgr();
eventMgr.AddEvent<TEventA>(Handle_TEventA);

var evt = new TEventA() { a = 999, b = "Hello World!" };
eventMgr.SendEvent(evt);
```