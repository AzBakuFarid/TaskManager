
namespace TaskManager.Notification.Interfaces
{
    public interface INotification<TData>
    {
        TData Data { get; set; }
        void Notify();
    }

}
