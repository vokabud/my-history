namespace PhotoHistory;

internal class DirectoryUtil
{
    public static string[] GetFilesByExtension(string directory, string extension)
    {
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException();
        }

        if (string.IsNullOrEmpty(extension))
        {
            throw new ArgumentException(null, nameof(extension));
        }

        return Directory
            .GetFiles(
                directory,
                $"*.{extension}",
                SearchOption.AllDirectories);
    }
}
