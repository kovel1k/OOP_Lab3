using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace PL.Part1
{
    public static class CircleSerializationDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== ЧАСТИНА 1: ДОСЛІДЖЕННЯ СЕРІАЛІЗАЦІЇ (Клас Коло) ===");
            Circle[] originalArray = new Circle[]
            {
                new Circle("Червоний", "Чорний", 5.5),
                new Circle("Синій", "Жовтий", 2.0)
            };

            // 1. Бінарна серіалізація
            string binFile = "circles.bin";
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(binFile, FileMode.Create)) bf.Serialize(fs, originalArray);
            
            Circle[] restoredBin;
            using (FileStream fs = new FileStream(binFile, FileMode.Open)) restoredBin = (Circle[])bf.Deserialize(fs);
            Console.WriteLine("\n[Binary Відновлено]:");
            foreach(var c in restoredBin) Console.WriteLine(c);

            // 2. XML Серіалізація
            string xmlFile = "circles.xml";
            XmlSerializer xmlSer = new XmlSerializer(typeof(Circle[]));
            using (FileStream fs = new FileStream(xmlFile, FileMode.Create)) xmlSer.Serialize(fs, originalArray);
            
            Circle[] restoredXml;
            using (FileStream fs = new FileStream(xmlFile, FileMode.Open)) restoredXml = (Circle[])xmlSer.Deserialize(fs);
            Console.WriteLine("\n[XML Відновлено]:");
            foreach(var c in restoredXml) Console.WriteLine(c);

            // 3. JSON Серіалізація
            string jsonFile = "circles.json";
            JavaScriptSerializer js = new JavaScriptSerializer();
            File.WriteAllText(jsonFile, js.Serialize(originalArray));
            
            Circle[] restoredJson = js.Deserialize<Circle[]>(File.ReadAllText(jsonFile));
            Console.WriteLine("\n[JSON Відновлено]:");
            foreach(var c in restoredJson) Console.WriteLine(c);
            
            Console.WriteLine("\nЧастину 1 завершено.\n");
        }
    }
}