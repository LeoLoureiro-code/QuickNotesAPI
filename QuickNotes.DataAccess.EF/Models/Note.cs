using System;
using System.Collections.Generic;

namespace QuickNotes.DataAccess.EF.Models;

public partial class Note
{
    public uint NoteId { get; set; }

    public string NoteTitle { get; set; } = null!;

    public string? NoteContent { get; set; }

    public uint UserId { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual User User { get; set; } = null!;
}
