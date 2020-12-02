using School.Dto.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
                var base64Image = Convert64Image(path);
                var data = new
                {
                    RealPath = string.Format("{0}", path),
                    ThumbPath = string.Format("{0}", thumbPath),
                    Base64Image = base64Image,
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

        [Authorize]
        [Route("api/Upload/imageandroid")]
        [AllowAnonymous]
        public IHttpActionResult Post(StudentDtoPost obj)
        {
            byte[] bytes = Convert.FromBase64String(obj.ImageBase64);
            Image image;
            var path = "";
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                var fileName = Guid.NewGuid() + Path.GetExtension(".png");
                path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/"), fileName);
                image.Save(path);
            };
            //}
            //var subpath = "~/Images/";
            //bool exist = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(subpath));
            //if (!exist) Directory.CreateDirectory(HttpContext.Current.Server.MapPath(subpath));

            // var base64Image = Convert64Image(path);
            var data = new
            {
                RealPath = string.Format("{0}", path),
                Message = "Success"

            };
            return Ok(data);

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
        public string Convert64Image(string Path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }
    }
}

