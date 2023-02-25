namespace SevereWeatherWarnings.Map
{
    public class SevereWeatherWarningList
    {
        public IList<SevereWeatherWarning> Features { get; set; }
    }

    public class SevereWeatherWarning
    {
        public string id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public IList<double[][]> coordinates { get; set; }
    }

    public class Properties
    {
        public string id { get; set; }
        public string type { get; set; }
        public string areaDesc { get; set; }
        public string severity { get; set; }
        public string @event { get;set; }
    }
}