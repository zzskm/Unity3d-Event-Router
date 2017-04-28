public interface IEventHandler<T>
{
	void Handle(T data);
}
