using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace WepApi.DependancyInjection.Controllers
{

    public class UploadController : ApiController
    {
        //localhost/api/user/PostUserImage
        [Authorize]
        [AllowAnonymous]
        public IHttpActionResult PostUserImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var file = httpRequest.Files[0];
                var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/"), filename);
                file.SaveAs(path);
                var thumbPath = MakeThumbnail(320, 240, file.InputStream, Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/Thumbnails"), filename));
                var data = new
                {
                    RealPath = string.Format("{0}", path),
                    ThumbPath = string.Format("{0}", thumbPath),
                    Message = "Success"

                };
                return Ok(data);
            }
            var FailRespoonse = new
            {
                Message = "Fail"
            };
            return Ok(FailRespoonse);
        }
        public string MakeThumbnail(int Width, int Height, Stream streamImg, string saveFilePath)
        {
            Bitmap sourceImage = new Bitmap(streamImg);
            using (Bitmap objBitmap = new Bitmap(Width, Height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    // Set the graphic format for better result cropping   
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                    // Save the file path, note we use png format to support png file   
                    objBitmap.Save(saveFilePath);
                    return saveFilePath;
                }
            }
        }

    }
}

