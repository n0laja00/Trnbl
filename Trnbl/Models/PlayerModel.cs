
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Trnbl.Models;

public partial class PlayerModel : ObservableObject
{

    public PlayerModel(string name, int? num = null)
    {
        Id = Guid.NewGuid();
        OrderNumber = num ?? default(int);
        Name = name;
    }

    public Guid Id { get; }

    [ObservableProperty]
    public int? orderNumber;

    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public List<NoteModel> notes;

    public void AddNote(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return;
        }
        NoteModel note = new NoteModel(content);

        Notes.Add(note);
    }

    public async void RemoveNote(NoteModel note)
    {
        if (note != null)
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

    public void EditList(NoteModel note)
    {
        var index = Notes.FindIndex(e => e.Id == note.Id);
        Notes[index].EditNote(note.Content);
    }
}

