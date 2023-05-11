using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class NotificationSetting
{
    public long UserId { get; set; }

    public long NotificationSettingId { get; set; }

    public string NotificationType { get; set; } = null!;

    public bool Status { get; set; }

    public virtual User User { get; set; } = null!;
}
