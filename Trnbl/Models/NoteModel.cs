
using System.ComponentModel.DataAnnotations;

namespace Trnbl.Models;

public class NoteModel
{
    [Key]
    public Guid Id { get; }
    [Required]
    [MinLength(1)]
    public string Content { get; set; }

    public NoteModel(string str)
    {
        Id = Guid.NewGuid();
        Content = str;
    }

    public void EditNote(string newContent)
    {
        Content = newContent;
    }
}

