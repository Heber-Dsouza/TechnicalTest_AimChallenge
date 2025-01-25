using System.Text.Json.Serialization;

namespace TargetMasters.Models.GameModels
{
    public class Target
    {
        public bool ShowTarget { get; set; } = false; // criado durante o desenvolvimento para testes no front:
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
        top,
        bottom,
        left,
        right
    }

    public class TargetGenerator
    {
        private static readonly Random Random = new Random();
        public static List<Target> GenerateRandomTargets(int numberOfTargets)
        {
            var targets = new List<Target>();

            for (int i = 0; i < numberOfTargets; i++)
            {
                targets.Add(new Target
                {
                    TargetSize = Random.Next(50, 151),
                    TargetPosition = new TargetPosition
                    {
                        Horizontal = new Axis
                        {
                            Name = (AxisName)Random.Next(2),
                            Value = Random.Next(0, 51)
                        },
                        Vertical = new Axis
                        {
                            Name = (AxisName)(Random.Next(2) + 2),
                            Value = Random.Next(0, 51)
                        }
                    },
                    ShowTarget = false
                });
            }

            return targets;
        }
    }
}
