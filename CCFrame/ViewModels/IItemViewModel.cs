namespace CCFrame.ViewModels
{
    public interface IItemViewModel<out T>
    {
        T Item { get; }
    }
}