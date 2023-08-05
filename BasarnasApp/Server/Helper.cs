using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BasarnasApp.Server;

public static class Helper
{

    public static string ImageKejadianPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/kejadian/");
    public static string ThumbPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/images/thumbs/");
    public static string VideoPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/videos/");
    public static string ProfilePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles/");
    public static string LogoPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/logo/");


    public static string CreateImageFileName()
    {
        Guid guid = Guid.NewGuid();
        return guid.ToString() + ".png";
    }


    public static Task<(string File, string Thumb)> CreatePhotoKejadian(byte[] data, string? oldFile = null)
    {
        try
        {
            var fileName = CreateImageFileName();

            if (!Directory.Exists(ImageKejadianPath))
            {
                Directory.CreateDirectory(ImageKejadianPath);
            }

            File.WriteAllBytes(ImageKejadianPath + fileName, data);
           var thumb = CreateThumbFile(data);

            if (oldFile != null)
            {
                File.Delete(ImageKejadianPath + oldFile);
            }

            return Task.FromResult((fileName, thumb));
        }
        catch (Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }


    private static string CreateThumbFile(byte[] data)
    {
         var thumb = CreateImageFileName();
        if (!Directory.Exists(ThumbPath))
        {
            Directory.CreateDirectory(ThumbPath);
        }
        File.WriteAllBytes(ThumbPath + thumb, CreateThumb(data));

        return thumb;
    }


    public static Task<(string File, string Thumb)> CreatePhotoProfile(byte[] data, string? oldFile = null)
    {
        try
        {
            var fileName = CreateImageFileName();
            if (!Directory.Exists(ProfilePath))
            {
                Directory.CreateDirectory(ProfilePath);
            }
            File.WriteAllBytes(ProfilePath + fileName, data);
            var thumb = CreateThumbFile(data);      

            if (oldFile != null)
            {
                File.Delete(ImageKejadianPath + oldFile);
            }

            return Task.FromResult((fileName, thumb));
        }
        catch (Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }

    public static Task<(string File, string Thumb)> CreateLogo(byte[] data, string? oldFile=null)
    {
        try
        {
            var fileName = CreateImageFileName();
            if (!Directory.Exists(LogoPath))
            {
                Directory.CreateDirectory(LogoPath);
            }

            File.WriteAllBytes(LogoPath + fileName, data);
            var thumb = CreateThumbFile(data);  
             if (oldFile != null)
            {
                File.Delete(ImageKejadianPath + oldFile);
            }    
            return Task.FromResult((fileName, thumb));
        }
        catch (Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }

    private static byte[] CreateThumb(byte[] byteArray)
    {
        try
        {
            System.Drawing.Image imThumbnailImage;
            System.Drawing.Image OriginalImage;
            MemoryStream ms = new MemoryStream();

            // Stream / Write Image to Memory Stream from the Byte Array.
            ms.Write(byteArray, 0, byteArray.Length);

            OriginalImage = System.Drawing.Image.FromStream(ms);

            // Shrink the Original Image to a thumbnail size.
            imThumbnailImage = OriginalImage.GetThumbnailImage(100, 100,
                    new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);

            // Save Thumbnail to Memory Stream for Conversion to Byte Array.
            MemoryStream myMS = new MemoryStream();
            imThumbnailImage.Save(myMS, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] test_imge = myMS.ToArray();
            return test_imge;
        }
        catch (System.Exception)
        {
            return byteArray;
        }
    }




}