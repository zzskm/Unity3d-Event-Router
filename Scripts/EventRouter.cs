using System;
using System.Collections.Generic;

public class EventRouter
{
	private readonly Dictionary<Type, Action<object>> _handlers = new Dictionary<Type, Action<object>>();
	private readonly Queue<EventBase> _queue = new Queue<EventBase>();

	public void Process()
	{
		while (_queue.Count > 0)
		{
			EventBase evt = _queue.Dequeue();
			Handle(evt);
		}
	}

	private void Handle(EventBase evt)
	{
		Action<object> action;

		if (_handlers.TryGetValue(evt.GetType(), out action))
		{
			action(evt);
		}
		else
		{
			UnityEngine.Debug.LogWarning(GetType().Name + " Handle() : cannot be found event handler " + evt.GetType());
		}
	}

	public void Publish<T>(T evt) where T : EventBase
	{
		_queue.Enqueue(evt);
	}

	public void Subscribe<T>(IEventHandler<T> handler)
	{
		HandlerAdapter<T> adapted = new HandlerAdapter<T>(handler);
		_handlers.Add(typeof(T), adapted.Handle);
	}

	private class HandlerAdapter<T>
	{
		private readonly IEventHandler<T> _handler;

		public HandlerAdapter(IEventHandler<T> handle)
		{
			_handler = handle;
		}

		internal void Handle(object o)
		{
			_handler.Handle((T)o);
		}
	}
}
