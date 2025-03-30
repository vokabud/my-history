using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace PhotoHistory;

internal class ImageUtil
{
    public static ImageMetadata GetImageMetadata(string path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File not found.", path);
        }

        var directories = ImageMetadataReader.ReadMetadata(path);

        var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        DateTime dateTime;

        if (subIfdDirectory != null)
        {
            dateTime = subIfdDirectory.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
        } 
        else
        {
            throw new Exception("DateTimeOriginal not found.");
        }

        var gpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();
        var location = gpsDirectory?.GetGeoLocation();

        double latitude;
        double longitude;

        if (location == null)
        {
            throw new Exception("GeoLocation not found.");
        }
        else
        {
            latitude = location.Latitude;
            longitude = location.Longitude;
        }

        return new ImageMetadata
        {
            Latitude = latitude,
            Longitude = longitude,
            DateTaken = dateTime
        };
    }
}
