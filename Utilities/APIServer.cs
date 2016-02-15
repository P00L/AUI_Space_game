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

        /// <summary>
        /// Display an animation on the carpet
        /// </summary>
        /// <param name="trigger">Trigger to invoke</param>
        public async static void LuminousCarpetRequest(string trigger)
        {
            string messageToSend = new JavaScriptSerializer().Serialize(new
            {
                Action = "CarpetAction",
                Trigger = trigger
            });

            Console.WriteLine(messageToSend);
            Task t = new Task(() =>
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    if (messageToSend.Length > 1)
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = messageToSend;

                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseReceived(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ResponseReceived(null);
                }
            });
            t.Start();

        }

        /// <summary>
        /// Display an image on a screen
        /// </summary>
        /// <param name="targetScreen">Screen on which the image will be displayed</param>
        /// <param name="image">URI of the image</param>
        public async static void ShowImageOnScreenRequest(string targetScreen, string image)
        {
            string messageToSend = new JavaScriptSerializer().Serialize(new
            {
                Action = "ShowImageOnScreen",
                TargetScreen = targetScreen,
                Image = image
            });

            Console.WriteLine(messageToSend);
            Task t = new Task(() =>
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    if (messageToSend.Length > 1)
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = messageToSend;

                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseReceived(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ResponseReceived(null);
                }
            });
            t.Start();

        }

        /// <summary>
        /// Display a video on a screen
        /// </summary>
        /// <param name="targetScreen">Video on which the video will be displayed</param>
        /// <param name="video">URI of the video</param>
        public async static void ShowVideoOnScreenRequest(string targetScreen, string video)
        {
            string messageToSend = new JavaScriptSerializer().Serialize(new
            {
                Action = "ShowVideoOnScreen",
                TargetScreen = targetScreen,
                Video = video
            });

            Console.WriteLine(messageToSend);
            Task t = new Task(() =>
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    if (messageToSend.Length > 1)
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = messageToSend;

                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseReceived(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ResponseReceived(null);
                }
            });
            t.Start();

        }

        /// <summary>
        /// Display an image with a text on a screen
        /// </summary>
        /// <param name="targetScreen">Screen on which the image with text will be displayed</param>
        /// <param name="image">URI of the image</param>
        /// <param name="text">Text to display</param>
        public async static void ShowTextImageOnScreenRequest(string targetScreen, string image, string text)
        {
            string messageToSend = new JavaScriptSerializer().Serialize(new
            {
                Action = "ShowTextImageOnScreen",
                TargetScreen = targetScreen,
                Image = image,
                Text = text
            });

            Console.WriteLine(messageToSend);
            Task t = new Task(() =>
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    if (messageToSend.Length > 1)
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = messageToSend;

                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseReceived(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ResponseReceived(null);
                }
            });
            t.Start();

        }

        /// <summary>
        /// Change colors and luminosity of a Hue Light
        /// </summary>
        /// <param name="color">Color to show</param>
        /// <param name="position">Light to modify</param>
        /// <param name="luminosity">Luminosity to show</param>
        public async static void HueRequest(string color, string brightness)
        {
            string messageToSend = new JavaScriptSerializer().Serialize(new
            {
                Action = "EnvironmentAction",
                Color = color,
                Brightness = brightness //value between 1 and 100
            });

            Console.WriteLine(messageToSend);
            Task t = new Task(() =>
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constant.URLServer);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    if (messageToSend.Length > 1)
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = messageToSend;

                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseReceived(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ResponseReceived(null);
                }
            });
            t.Start();

        }

        private static void ResponseReceived(object p)
        {
            throw new NotImplementedException();
        }
    }
}
