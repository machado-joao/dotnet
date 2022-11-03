using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Activity.Utils
{
    public class Functions
    {
        public static bool CreateDirectory()
        {
            string directory = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return true;
            }
            return false;
        }

        public static string UploadFile(HttpPostedFileBase fileUpload, string name)
        {
            try
            {
                double allowedSize = 900;
                if (fileUpload != null)
                {
                    string file = Path.GetFileName(fileUpload.FileName);
                    double size = Convert.ToDouble(fileUpload.ContentLength) / 1024;
                    string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                    string directory = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\" + name;

                    if (size > allowedSize)
                    {
                        return "Max. file size allowed is " + allowedSize + " kB!";
                    }
                    if ((extension != ".png" && extension != ".jpg"))
                    {
                        return "Invalid format. Only .png and .jpg are allowed!";
                    }
                    fileUpload.SaveAs(directory);
                    return "success";
                }
                return "Error uploading file!";
            }
            catch
            {
                return "Error uploading file!";
            }
        }

        public static bool DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            return false;
        }
    }
}
