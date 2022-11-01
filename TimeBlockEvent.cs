using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

public class TimeBlockEvent
{
    public string? Summary { get; set; }

    public DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public bool IsAllDay { get; set; }

    public List<TimeBlockReccurence> RecurrenceRules { get; set; }

    public TimeBlockEvent()
    {
        RecurrenceRules = new List<TimeBlockReccurence>();
        IsAllDay = false;
    }

    public CalendarEvent ToEvent()
    {
        var e = new CalendarEvent
        {
            Summary = Summary ?? "",
            Start = new CalDateTime(Start),
        };

        if (End.HasValue)
        {
            e.End = new CalDateTime(End.Value);
        }

        if (IsAllDay)
        {
            e.IsAllDay = IsAllDay;
        }

        foreach (var r in RecurrenceRules)
        {
            var pattern = new RecurrencePattern();
            if (r.ByDay != null) pattern.ByDay = r.ByDay;
            if (r.Count.HasValue) pattern.Count = r.Count.Value;
            if (r.Frequency.HasValue) pattern.Frequency = r.Frequency.Value;
            if (r.Interval.HasValue) pattern.Interval = r.Interval.Value;
            if (r.Until.HasValue) pattern.Until = r.Until.Value;

            e.RecurrenceRules.Add(pattern);
        }

        return e;
    }
}
