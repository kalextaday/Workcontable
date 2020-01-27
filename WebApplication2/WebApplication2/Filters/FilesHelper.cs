using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebApplication2.Filters
{
    public class FilesHelper
    {
        public static string UploadPhoto(HttpPostedFileBase file, string folder)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if(file != null)
            {
                pic = Path.GetFileName(file.FileName); //obtiene el nombre del archivo al picture
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder),pic); //me trae la ruta relativa del servidor y lo concateno con el nombre de la imagen
                
                //cojen el archivo y lo suben
                file.SaveAs(path);
                using (MemoryStream ms=new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }

            return pic;
        }
    }
}