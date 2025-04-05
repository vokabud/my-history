using System.Globalization;

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
}