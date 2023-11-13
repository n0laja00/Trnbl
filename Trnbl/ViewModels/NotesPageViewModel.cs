using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Trnbl.Models;

namespace Trnbl.ViewModels;

[QueryProperty("PlayerName", "PlayerName")]
[QueryProperty("Player", nameof(NotesPage))]
public partial class NotesPageViewModel : ObservableObject
{


    [ObservableProperty]
    string playerName;

    [ObservableProperty]
    string noteEntry;

    [ObservableProperty]
    PlayerModel player;

    [ObservableProperty]
    ObservableCollection<NoteModel> playerNotes;

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public void AddNote()
    {
        if (string.IsNullOrWhiteSpace(NoteEntry))
        {
            return;
        }
        Player.AddPlayerNote(NoteEntry);
        NoteEntry = string.Empty;
    }

    [RelayCommand]
    public void DeleteNote(NoteModel note)
    {
        Player.DeletePlayerNote(note);
    }

    [RelayCommand]
    public void EditNote(NoteModel note)
    {
        if (note.Content == null)
        {
            return;
        }

        NoteEntry = note.Content;

        Player.DeletePlayerNote(note);
    }


}

