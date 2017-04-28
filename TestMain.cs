// <example>
//	> EventHandler_Event1 Handle!!! Event1
//	EventRouter Handle() : cannot be found event handler Event2
//	> EventHandler_Event3 Handle!!! Event3
//	EventRouter Handle() : cannot be found event handler Event4
//	> EventHandler_Event5 Handle!!! Event5 Foo
//	> EventHandler_Event5 Handle!!! Event5 Bar
// </example>

using UnityEngine;

public class TestMain : MonoBehaviour
{
	public EventRouter eventRouter;

	void Awake()
	{
		eventRouter = new EventRouter();

		eventRouter.Subscribe(new EventHandler_Event1());
		eventRouter.Subscribe(new EventHandler_Event3());
		eventRouter.Subscribe(new EventHandler_Event5());
	}

	void Start()
	{
		eventRouter.Publish(new Event1());
		eventRouter.Publish(new Event2());
		eventRouter.Publish(new Event3());
		eventRouter.Publish(new Event4());
		eventRouter.Publish(new Event5 {tag = Event5.Tag.Foo});
		eventRouter.Publish(new Event5 {tag = Event5.Tag.Bar});

		eventRouter.Process();
	}
}

public class EventHandler_Event1 : IEventHandler<Event1>
{
	public void Handle(Event1 data)
	{
		Debug.Log("> EventHandler_Event1 Handle!!! " + data.GetType());
	}
}

public class EventHandler_Event3 : IEventHandler<Event3>
{
	public void Handle(Event3 data)
	{
		Debug.Log("> EventHandler_Event3 Handle!!! " + data.GetType());
	}
}

public class EventHandler_Event5 : IEventHandler<Event5>
{
	public void Handle(Event5 data)
	{
		Debug.Log("> EventHandler_Event5 Handle!!! " + data.GetType() + " " + data.tag);
	}
}

public class Event1 : EventBase { }
public class Event2 : EventBase { }
public class Event3 : EventBase { }
public class Event4 : EventBase { }

public class Event5 : EventBase<Event5.Tag>
{
	public enum Tag : byte
	{
		Foo,
		Bar,
	}
}