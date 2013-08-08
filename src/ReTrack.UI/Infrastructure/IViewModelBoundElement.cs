namespace ReTrack.UI.Infrastructure
{
    public interface IViewModelBoundElement<TViewModel>
            where TViewModel : ViewModelBase
    {
        TViewModel ViewModel { get; }
    }
}