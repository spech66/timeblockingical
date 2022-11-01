using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

public class TimeBlockAlarm
{
    public string? Description { get; set; }

    public TimeSpan Trigger { get; set; }

    public string Action { get; }

    public TimeBlockAlarm()
    {
        Action = AlarmAction.Display;
        Trigger = new TimeSpan(0, -15, 0);
    }
}
