using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Helpers
{
    public static class FileManager
    {
        public static string Save(string root, string folder, IFormFile fileForm)
        {
            string newFileName = Guid.NewGuid().ToString() + (fileForm.FileName.Length > 64?fileForm.FileName.Substring(fileForm.FileName.Length-64,64):fileForm.FileName);

            string path = Path.Combine(root, folder, newFileName);
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                fileForm.CopyTo(stream);
            }
            return newFileName;
        }
        public static bool Delete(string root , string folder , string file)
        {
            var path = Path.Combine(root, folder, file);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
