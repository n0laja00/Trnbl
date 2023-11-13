using Trnbl.ViewModels;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;

namespace Trnbl;

public partial class NotesPage : ContentPage
{
    private readonly IPopup popupService;
    public NotesPage(NotesPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

    }

}