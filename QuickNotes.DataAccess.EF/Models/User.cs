using System;
using System.Collections.Generic;

namespace QuickNotes.DataAccess.EF.Models;

public partial class User
{
    public uint UserId { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public string? UserRefreshToken { get; set; }

    public DateOnly? UserRefreshTokenExpiryTime { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
