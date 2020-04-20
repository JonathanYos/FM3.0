using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Familias3._1.APAD
{
    /// <summary>
    /// Summary description for ImagenG
    /// </summary>
    public class ImagenG : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string imageFile = null;
            string path = null;
            string locacion2;
            locacion2 = @"\\SVRAPP\FamilyFotos\Apadrinados" + imageFile;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;


            imageFile = request.QueryString["imageID"];
            if (!string.IsNullOrEmpty(imageFile))
            {
                response.ContentType = "image/jpeg";
                //path = @"\\SVRFAMILIAS\FamilyFotos" + @"\F3_20190121113204.jpg";


                path = @"\\SVRAPP\FamilyFotos\ExApadrinados" + imageFile;

                try
                {
                    response.WriteFile(path);
                }
                catch (Exception e)
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('Error en foto, envie mensaje a Sistemas:');window.location ='../SearchProf.aspx';", true);

                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}