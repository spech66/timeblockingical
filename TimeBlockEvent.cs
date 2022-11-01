using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

public class TimeBlockEvent
{
    public string? Summary { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public List<TimeBlockReccurence> RecurrenceRules { get; set; }

    public TimeBlockEvent()
    {
        RecurrenceRules = new List<TimeBlockReccurence>();
    }

    public CalendarEvent ToEvent()
    {
        var e = new CalendarEvent
        {
            Summary = Summary ?? "",
            Start = new CalDateTime(Start),
            End = new CalDateTime(End),
        };

        foreach(var r in RecurrenceRules)
        {
            e.RecurrenceRules.Add(new RecurrencePattern() {
                Count = r.Count,
                Frequency = r.Frequency,
            });
        }

        return e;
    }
}


/*
var rrule = new RecurrencePattern(FrequencyType.Daily, 1) { Count = 5 };
var e = new CalendarEvent
{
    Start = new CalDateTime(DateTime.Now),
    End = new CalDateTime(DateTime.Now.AddHours(1)),
    RecurrenceRules = new List<RecurrencePattern> { rrule },
};
*/
