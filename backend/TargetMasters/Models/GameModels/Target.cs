using System.Text.Json.Serialization;

namespace TargetMasters.Models.GameModels
{
    public class Target
    {
        public bool ShowTarget { get; set; } = false;
        public int TargetSize { get; set; }
        public TargetPosition TargetPosition { get; set; } = new TargetPosition();
    }

    public class TargetPosition
    {
        public Axis Horizontal { get; set; } = new Axis();
        public Axis Vertical { get; set; } = new Axis();
    }

    public class Axis
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AxisName Name { get; set; }
        public int Value { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AxisName
    {
        Top,
        Bottom,
        Left,
        Right
    }
}
