using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Helpers
{
    public static class FileManager
    {
        public static string UploadImg(this IFormFile file, string env, string folderName)
        {
            string fileName = file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName;
            fileName = Guid.NewGuid().ToString() + fileName;

            string path = env + folderName + fileName;
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }
        public static void DeleteImg(this string imgUrl, string envPath, string folderName)
        {
            string path = envPath + folderName + imgUrl;
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public static bool CheckImg(this IFormFile file)
        {
            return file.FileName.Contains("image/") && file.FileName.Length / 1024 / 1024 <= 3;
        }
    }
}
