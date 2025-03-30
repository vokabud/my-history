using PhotoHistory;

Console.WriteLine("Start.");

var path = @"G:\Photo";
var extension = "jpg";

var files = DirectoryUtil.GetFilesByExtension(path, extension);


foreach (var file in files)
{
    Console.WriteLine(file);
}

Console.WriteLine("End.");
