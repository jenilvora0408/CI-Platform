using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? NotificationMessage { get; set; }

    public int? MessageId { get; set; }

    public bool? Status { get; set; }

    public int? UserId { get; set; }

    public string? NotificationType { get; set; }
}
