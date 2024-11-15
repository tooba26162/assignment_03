using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment_03
{
    internal class Program
    {
        static void Main(string[] args)
        {

            XmlWriterSettings settings = new XmlWriterSettings(); settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter w = XmlWriter.Create("GPS.xml", settings); w.WriteStartDocument();
            w.WriteStartElement("GPS_Log");
            w.WriteStartElement("Position"); w.WriteAttributeString("DateTime", DateTime.Now.ToString()); w.WriteElementString("x", "65.8973342");
            w.WriteElementString("y", "72.3452346");
            w.WriteStartElement("SatteliteInfo");
            w.WriteElementString("Speed", "40");
            w.WriteElementString("NoSatt", "7");
            w.WriteEndElement();
            w.WriteEndElement(); 

            w.WriteStartElement("Image");
            w.WriteAttributeString("Resolution", "1024x800");
            w.WriteElementString("Path", @"https://www.planetware.com/wpimages/2020/02/france-in-pictures-beautiful-places-to-photograph-eiffel-tower.jpg");
            w.WriteEndDocument();
            w.Close();

            ReadAndDisplayXml();

        }
        static void ReadAndDisplayXml()
        {
            if (!System.IO.File.Exists("GPS.xml"))
            {
                Console.WriteLine("GPS.xml file not found!");
                return;
            }

            Console.WriteLine("\nReading XML file content:\n");

            using (XmlReader reader = XmlReader.Create("GPS.xml"))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.WriteLine($"Element: {reader.Name}");
                            if (reader.HasAttributes)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    Console.WriteLine($"\tAttribute: {reader.Name} = {reader.Value}");
                                }
                                reader.MoveToElement(); // Move back to the element
                            }
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine($"\tText: {reader.Value}");
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine($"End Element: {reader.Name}");
                            break;
                    }
                }
            }
        }

    }
}