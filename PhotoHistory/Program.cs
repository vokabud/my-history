using PhotoHistory;

Console.WriteLine("Start.");

var path = @"G:\Photo";
var extension = "jpg";

var files = DirectoryUtil.GetFilesByExtension(path, extension);
var metadatas = new List<ImageMetadata>();

foreach (var file in files)
{
    try
    {
        var metadata = ImageUtil.GetImageMetadata(file);

        metadatas.Add(metadata);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}. {Path.GetFileName(path)}");
    }
}

var result = metadatas
    .Where(metadata => metadata.Latitude != 0.0 && metadata.Longitude != 0.0)
    .OrderBy(metadata => metadata.DateTaken);

foreach (var metadata in result)
{
    Console.WriteLine($"Latitude: {metadata.Latitude}, Longitude: {metadata.Longitude}, DateTaken: {metadata.DateTaken}");
}

Console.WriteLine("End.");
