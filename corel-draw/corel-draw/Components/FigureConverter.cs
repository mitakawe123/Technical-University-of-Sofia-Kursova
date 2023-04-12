using corel_draw.Figures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace corel_draw.Components
{
    internal class FigureConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Figure);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string figureName = jsonObject["Name"].Value<string>();
            Type figureType = Type.GetType($"corel_draw.Figures.{figureName}");

            Figure figure;
            if (figureType == typeof(Polygon))
            {
                JArray pointsArray = (JArray)jsonObject["Points"];
                List<Point> points = new List<Point>();
                foreach (var pointString in pointsArray)
                {
                    string[] pointValues = pointString.ToString().Split(',');
                    int pointX = int.Parse(pointValues[0]);
                    int pointY = int.Parse(pointValues[1]);
                    points.Add(new Point(pointX, pointY));
                }
                figure = new Polygon(points);
                points.Clear();
            }
            else
            {
                ConstructorInfo constructor = figureType.GetConstructor(new[] { typeof(int), typeof(int), typeof(int), typeof(int) });
                string[] locationValuesCircle = jsonObject["Location"].Value<string>().Split(',');
                int x_Circle = int.Parse(locationValuesCircle[0].Trim());
                int y_Circle = int.Parse(locationValuesCircle[1].Trim());
                object[] parameters = new object[] { x_Circle, y_Circle, jsonObject["Width"].Value<int>(), jsonObject["Height"].Value<int>() };
                figure = (Figure)constructor.Invoke(parameters);
            }
            serializer.Populate(jsonObject.CreateReader(), figure);

            return figure;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer){}
    }
}
