using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using AuiSpaceGame.Model;

namespace AuiSpaceGame.Utilities
{
    static class APIServer
    {
        public static void HueRequest(string color, string position, string luminosity)
        {
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    Action = "EnvironmentAction",
                    Color = color,
                    Position = position, //front-middle-rear
                    Luminosity = luminosity //valore tra 1 e 100
                });
                streamWriter.Write(json);
            }
        }

        public static void LuminousCarpetRequest(string trigger)
        {
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    Action = "CarpetAction",
                    Trigger = trigger
                }
);
                streamWriter.Write(json);
            }
        }

        public static void ShowImageOnScreenRequest(string targetScreen, string image)
        {
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    Action = "ShowImageOnScreen",
                    TargetScreen = targetScreen,
                    Image = image
                }
);
                streamWriter.Write(json);
            }
        }

        public static void ShowVideoOnScreenRequest(string targetScreen, string video)
        {
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    Action = "ShowVideoOnScreen",
                    TargetScreen = targetScreen,
                    Video = video
                }
);
                streamWriter.Write(json);
            }
        }

        public static void ShowTextImageOnScreenRequest(string targetScreen, string image, string text)
        {
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    Action = "ShowTextImageOnScreen",
                    TargetScreen = targetScreen,
                    Image = image,
                    Text = text
                }
);
                streamWriter.Write(json);
            }
        }
    }
}
