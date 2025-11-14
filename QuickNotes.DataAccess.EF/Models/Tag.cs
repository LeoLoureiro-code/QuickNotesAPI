using System;
using System.Collections.Generic;

namespace QuickNotes.DataAccess.EF.Models;

public partial class Tag
{
    public uint TagId { get; set; }

    public string TagName { get; set; } = null!;

    public uint UserId { get; set; }

    public uint NoteId { get; set; }

    public virtual Note Note { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
