using Ical.Net;

public class ICalConfig
{
    public string Timezone { get; set; }

    public List<TimeBlockEvent> TimeBlockEvents { get; set; }

    public ICalConfig()
    {
        TimeBlockEvents = new List<TimeBlockEvent>();
        Timezone = "Etc/UTC";
    }
}
