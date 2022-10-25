using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var now = DateTime.Now;
var later = now.AddHours(1);

// Repeat daily for 5 days
var rrule = new RecurrencePattern(FrequencyType.Daily, 1) { Count = 5 };
var e = new CalendarEvent
{
    Start = new CalDateTime(now),
    End = new CalDateTime(later),
    RecurrenceRules = new List<RecurrencePattern> { rrule },
};

// Create the basic calendar
var calendar = new Calendar();
calendar.Events.Add(e);

// Serialize calendar to string
var serializer = new CalendarSerializer();
var serializedCalendar = serializer.SerializeToString(calendar);

// Provide GET endpoint to fetch file
app.MapGet("/", () =>
{
    return Results.Content(serializedCalendar, contentType: "text/calendar");
});

app.Run();
