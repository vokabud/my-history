using System.Globalization;
using System.Text;

namespace PhotoHistory;

public class CsvWriter
{
    public static void WriteToCsv(string filePath, List<ImageMetadata> metadataList)
    {
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Latitude,Longitude,Date");

            foreach (var item in metadataList)
            {
                if (item.Latitude == 0.0 || item.Longitude == 0.0)
                {
                    continue;
                }

                string line = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0},{1},{2:yyyy-MM-dd}",
                    item.Latitude,
                    item.Longitude,
                    item.DateTaken
                );
                writer.WriteLine(line);
            }
        }
    }

    public static void WriteToKml(string filePath)
    {
        var point1 = (Lon: 30.5234, Lat: 50.4501, Time: "2025-04-01T10:00:00Z");
        var point2 = (Lon: 30.5245, Lat: 50.4512, Time: "2025-04-01T10:10:00Z");

        var kml = new StringBuilder();

        kml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        kml.AppendLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\">");
        kml.AppendLine("<Document>");
        kml.AppendLine("  <name>Directed Line Example</name>");

        // Arrow line style (uses a Google Earth-only icon to simulate direction)
        kml.AppendLine(@"
  <Style id='arrowLine'>
    <LineStyle>
      <color>ff0000ff</color>
      <width>4</width>
    </LineStyle>
    <PolyStyle>
      <color>7dff0000</color>
    </PolyStyle>
    <IconStyle>
      <scale>1.2</scale>
      <Icon>
        <href>http://maps.google.com/mapfiles/kml/shapes/arrow.png</href>
      </Icon>
    </IconStyle>
  </Style>
");

        // First point
        kml.AppendLine("  <Placemark>");
        kml.AppendLine("    <name>Start</name>");
        kml.AppendLine($"    <TimeStamp><when>{point1.Time}</when></TimeStamp>");
        kml.AppendLine("    <Point>");
        kml.AppendLine($"      <coordinates>{point1.Lon},{point1.Lat},0</coordinates>");
        kml.AppendLine("    </Point>");
        kml.AppendLine("  </Placemark>");

        // Second point
        kml.AppendLine("  <Placemark>");
        kml.AppendLine("    <name>End</name>");
        kml.AppendLine($"    <TimeStamp><when>{point2.Time}</when></TimeStamp>");
        kml.AppendLine("    <Point>");
        kml.AppendLine($"      <coordinates>{point2.Lon},{point2.Lat},0</coordinates>");
        kml.AppendLine("    </Point>");
        kml.AppendLine("  </Placemark>");

        // Line with direction (uses the style defined above)
        kml.AppendLine("  <Placemark>");
        kml.AppendLine("    <name>Path</name>");
        kml.AppendLine("    <styleUrl>#arrowLine</styleUrl>");
        kml.AppendLine("    <LineString>");
        kml.AppendLine("      <tessellate>1</tessellate>");
        kml.AppendLine("      <coordinates>");
        kml.AppendLine($"        {point1.Lon},{point1.Lat},0");
        kml.AppendLine($"        {point2.Lon},{point2.Lat},0");
        kml.AppendLine("      </coordinates>");
        kml.AppendLine("    </LineString>");
        kml.AppendLine("  </Placemark>");

        kml.AppendLine("</Document>");
        kml.AppendLine("</kml>");

        // Save the file
        File.WriteAllText(filePath, kml.ToString());
    }
}