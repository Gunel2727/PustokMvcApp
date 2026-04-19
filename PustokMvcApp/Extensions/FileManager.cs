namespace PustokMvcApp.Extensions
{
    public static class FileManager
    {
        public static string SaveFile(this IFormFile file, string rootpath)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(rootpath, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;

        }
    }
}
