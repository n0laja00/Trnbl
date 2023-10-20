
using System.Threading.Tasks;

namespace Trnbl.Models;

public class PlayerModel
{

    public PlayerModel(int num, string name)
    {
        OrderNumber = num;
        Name = name;
    }

    public PlayerModel(string name)
    {
        Name = name;
    }

    public int? OrderNumber { get; set; }
    public string Name { get; set; }
    public List<NoteModel> Notes { get; set; }

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

