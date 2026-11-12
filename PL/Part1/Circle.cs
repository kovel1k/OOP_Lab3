using System;
using System.Runtime.Serialization;

namespace PL.Part1
{
    [Serializable]
    public class Circle : ISerializable
    {
        public string FillColor { get; set; }
        public string OutlineColor { get; set; }
        public double Radius { get; set; }

        public Circle() { }

        public Circle(string fill, string outline, double radius)
        {
            FillColor = fill; OutlineColor = outline; Radius = radius;
        }

        protected Circle(SerializationInfo info, StreamingContext context)
        {
            FillColor = info.GetString("FillColor");
            OutlineColor = info.GetString("OutlineColor");
            Radius = info.GetDouble("Radius");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FillColor", FillColor);
            info.AddValue("OutlineColor", OutlineColor);
            info.AddValue("Radius", Radius);
        }

        public override string ToString() => $"Коло (R={Radius}, Fill={FillColor}, Outline={OutlineColor})";
    }
}