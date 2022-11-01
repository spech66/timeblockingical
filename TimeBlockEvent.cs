using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

public class TimeBlockEvent
{
    public string? Summary { get; set; }

    public string? Description { get; set; }

    public DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public bool IsAllDay { get; set; }

    public List<TimeBlockReccurence> RecurrenceRules { get; set; }

    public List<TimeBlockAlarm> Alarms { get; set; }

    public TimeBlockEvent()
    {
        RecurrenceRules = new List<TimeBlockReccurence>();
        Alarms = new List<TimeBlockAlarm>();
        IsAllDay = false;
    }

    public CalendarEvent ToEvent()
    {
        var e = new CalendarEvent
        {
            Summary = Summary ?? "",
            Start = new CalDateTime(Start),
        };

        if (Description != null) e.Description = Description;
        if (End.HasValue) e.End = new CalDateTime(End.Value);
        if (IsAllDay) e.IsAllDay = IsAllDay;        

        foreach (var rule in RecurrenceRules)
        {
            var pattern = new RecurrencePattern();
            if (rule.ByDay != null) pattern.ByDay = rule.ByDay;
            if (rule.Count.HasValue) pattern.Count = rule.Count.Value;
            if (rule.Frequency.HasValue) pattern.Frequency = rule.Frequency.Value;
            if (rule.Interval.HasValue) pattern.Interval = rule.Interval.Value;
            if (rule.Until.HasValue) pattern.Until = rule.Until.Value;

            e.RecurrenceRules.Add(pattern);
        }
        
        foreach (var alarm in Alarms)
        {
            e.Alarms.Add(new Alarm() {
                Action = alarm.Action,
                Description = alarm.Action ?? Summary ?? "-",
                Trigger = new Trigger(alarm.Trigger),
            });
        }

        return e;
    }
}
