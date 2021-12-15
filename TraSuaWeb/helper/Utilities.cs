using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace webtrasua.helper
{
    public class Utilities
    { 
        
        public static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;

            }
            catch
            {
                return false;
            }
        }
        public static void CreateIfMissing(String path)
        {
            bool folderExist = Directory.Exists(path);
            if (!folderExist)
            {
                Directory.CreateDirectory(path);

            }
        }
        public static string ToTitleCase(String str)
        {
            string resuilt = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for(int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }


                }
                resuilt = string.Join(" ", words);
            }
            return resuilt;
        }
        public static string SEOUrl(string url)
        {
            var result = url.ToLower().Trim();
            result = Regex.Replace(result, "áàạảãâấầậẩẫăắằặẳẵ", "a");
            result = Regex.Replace(result, "éèẹẻẽêếềệểễ", "e");
            result = Regex.Replace(result, "óòọỏõôốồộổỗơớờợởỡ", "o");
            result = Regex.Replace(result, "íìịỉĩ", "i");
            result = Regex.Replace(result, "ýỳỵỷỹ", "y");
            result = Regex.Replace(result, "úùụủũưứừựửữ", "u");
            result = Regex.Replace(result, "đ", "d");
            url = Regex.Replace(url.Trim(), @"[^a-z0-9-\s]", "").Trim();
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            url = Regex.Replace(url.Trim(), @"\s", "-");
            if (url.IndexOf("--") != 1)
            {
                url = url.Replace("--", "-");
            }
            return url;
        }
        public static string UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory,newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower())) // Khác các file định nghĩa
                {
                    return null;
                }
                else
                {
                   
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
     
    }
}
