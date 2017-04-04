using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;


namespace FileRenamer.Decoder
{
    public class Jpg
    {
        public DateTime ShootingDateTime { get; set; }

        public ExifManager Exif { get; set; }

        public bool IsOpenError { get; set; } = false;

        public Jpg(FileInfo file)
        {
            try
            {
                using (var exif = new ExifManager(file.FullName))
                {
                    ShootingDateTime = exif.DateTimeOriginal;
                    Exif = exif;
                }
            }
            catch (Exception e)
            {
                IsOpenError = true;
                Console.WriteLine(e);
            }
        }

        public string GetProperty(string key)
        {
            switch (key)
            {
                case "%t":
                    return ShootingDateTime.ToString();
                default:
                    return null;
            }
        }
    }
    
}
