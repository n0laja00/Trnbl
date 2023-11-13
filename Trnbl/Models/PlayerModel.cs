
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Trnbl.Models;

public partial class PlayerModel : ObservableObject
{

    public PlayerModel(string name, int? num = null)
    {
        Id = Guid.NewGuid();
        OrderNumber = num ?? default(int);
        Name = name;
        Notes = new ObservableCollection<NoteModel>();
    }

    public Guid Id { get; }

    [ObservableProperty]
    public int? orderNumber;

    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public ObservableCollection<NoteModel> notes;

    [ObservableProperty]
    public bool active;

    public void AddPlayerNote(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return;
        }
        NoteModel note = new NoteModel(content);

        Notes.Add(note);
    }

    public async void DeletePlayerNote(NoteModel note)
    {
        if (note == null)
        {
            return;
        }
        if (Notes.Remove(note))
        {
            return;
        }
        else
        {
            await Shell.Current.DisplayAlert("Error with note deletion", "Program experienced an error", "OK");
            return;
        };
    }

    public void EditPlayerNote(NoteModel note)
    {
        var index = Notes.IndexOf(Notes.Where(e => e.Id == note.Id).FirstOrDefault());
        Notes[index].EditNote(note.Content);
    }

}

