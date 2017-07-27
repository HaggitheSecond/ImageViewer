namespace ImageViewer.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidImageExtension(this string extension)
        {
            return extension == ".jpg"
                   || extension == ".png"
                   || extension == ".gif";
        }
    }
}