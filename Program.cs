using Ical.Net;
using Ical.Net.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var config = Environment.GetEnvironmentVariable("TBI_CONFIG") ?? "example.yaml"; 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Read YAML config
var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
    .WithNamingConvention(PascalCaseNamingConvention.Instance)
    .Build();

var calRules = deserializer.Deserialize<List<TimeBlockEvent>>(File.ReadAllText(config));

// Create the basic calendar
var calendar = new Calendar();

// Add events
foreach(var e in calRules)
{
    calendar.Events.Add(e.ToEvent());
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
