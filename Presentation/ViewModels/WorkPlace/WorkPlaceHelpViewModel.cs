using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace;

public class WorkPlaceHelpViewModel : ReactiveObject, IRoutableViewModel
{
    public WorkPlaceHelpViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}