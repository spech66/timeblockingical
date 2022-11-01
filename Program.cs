using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var config = Environment.GetEnvironmentVariable("TBI_CONFIG") ?? "example.yaml";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Read YAML config
var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
    .WithNamingConvention(PascalCaseNamingConvention.Instance)
    .Build();

var calRules = deserializer.Deserialize<ICalConfig>(File.ReadAllText(config));

// Create the basic calendar
var calendar = new Calendar();
calendar.AddTimeZone(new VTimeZone(calRules.Timezone));

// Add events
if (calRules.TimeBlockEvents != null)
{
    foreach (var e in calRules.TimeBlockEvents)
    {
        calendar.Events.Add(e.ToEvent());
    }
}

// Serialize calendar to string
var serializer = new CalendarSerializer();
var serializedCalendar = serializer.SerializeToString(calendar);

// Provide GET endpoint to fetch file
app.MapGet("/", () =>
{
    return Results.Content(serializedCalendar, contentType: "text/calendar");
    // return Results.Content(serializedCalendar);
});

app.Run();
