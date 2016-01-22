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
    class APIServer
    {
        private HttpWebRequest httpWebRequest;
        private StreamWriter streamWriter;

        public APIServer()
        {
            Connect();
        }
        public void HueRequest(string color, string position, string luminosity)
        {

            string json = new JavaScriptSerializer().Serialize(new
            {
                Action = "EnvironmentAction",
                Color = color,
                Position = position, //possible values: front-middle-rear
                Luminosity = luminosity //value between 1 and 100
            });


            Console.WriteLine(json + " " + DateTime.Now);
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            streamWriter.Write(json);
        }

        public void LuminousCarpetRequest(string trigger)
        {

            string json = new JavaScriptSerializer().Serialize(new
            {
                Action = "CarpetAction",
                Trigger = trigger
            });


            Console.WriteLine(json + " " + DateTime.Now);
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



            streamWriter.Write(json);

        }

        public void ShowImageOnScreenRequest(string targetScreen, string image)
        {


            string json = new JavaScriptSerializer().Serialize(new
            {
                Action = "ShowImageOnScreen",
                TargetScreen = targetScreen,
                Image = image
            });


            Console.WriteLine(json + " " + DateTime.Now);
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            streamWriter.Write(json);

        }

        public void ShowVideoOnScreenRequest(string targetScreen, string video)
        {

            string json = new JavaScriptSerializer().Serialize(new
            {
                Action = "ShowVideoOnScreen",
                TargetScreen = targetScreen,
                Video = video
            });


            Console.WriteLine(json + " " + DateTime.Now);
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            streamWriter.Write(json);

        }

        public void ShowTextImageOnScreenRequest(string targetScreen, string image, string text)
        {

            string json = new JavaScriptSerializer().Serialize(new
            {
                Action = "ShowTextImageOnScreen",
                TargetScreen = targetScreen,
                Image = image,
                Text = text
            });


            Console.WriteLine(json + " " + DateTime.Now);
            return; //TODO TOGLIERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            streamWriter.Write(json);
        }

        public async void Connect()
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Console.WriteLine("vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv");
            try
            {
                Stream stream = await httpWebRequest.GetRequestStreamAsync();
                streamWriter = new StreamWriter(stream);

            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }


        /*
                public static void StaticHueRequest(string color, string position, string luminosity)
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
                            Position = position, //possible values: front-middle-rear
                            Luminosity = luminosity //value between 1 and 100
                        });
                        streamWriter.Write(json);
                    }
                }

                public static void StaticLuminousCarpetRequest(string trigger)
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

                public static void StaticShowImageOnScreenRequest(string targetScreen, string image)
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

                public static void StaticShowVideoOnScreenRequest(string targetScreen, string video)
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

                public static void StaticShowTextImageOnScreenRequest(string targetScreen, string image, string text)
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
                */
    }
}
