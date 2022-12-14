using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

public class TimeBlockReccurence
{
    public FrequencyType? Frequency { get; set; }

    public int? Interval { get; set; }

    public List<WeekDay>? ByDay { get; set; }

    public int? Count { get; set; }

    public DateTime? Until { get; set; }
}
