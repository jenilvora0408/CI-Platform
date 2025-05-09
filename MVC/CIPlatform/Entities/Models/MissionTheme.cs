﻿using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class MissionTheme
{
    public long MissionThemeId { get; set; }

    public string Title { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();
}
