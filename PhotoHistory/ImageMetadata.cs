namespace PhotoHistory;

public class ImageMetadata
{
    public double Latitude { get; set; } = 0.0;
    
    public double Longitude { get; set; } = 0.0;

    public DateTime DateTaken { get; set; } = DateTime.Now;
}
