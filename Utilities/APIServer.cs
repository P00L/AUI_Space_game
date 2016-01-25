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
            //Connect();
        }

        public async void LuminousCarpetRequest(string trigger)
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

        public async void ShowImageOnScreenRequest(string targetScreen, string image)
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

        public async void ShowVideoOnScreenRequest(string targetScreen, string video)
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

        public async void ShowTextImageOnScreenRequest(string targetScreen, string image, string text)
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

        public async void HueRequest(string color, string position, string luminosity)
        {
            string messageToSend = new JavaScriptSerializer().Serialize(new
            {
                Action = "EnvironmentAction",
                Color = color,
                Position = position, //possible values: front-middle-rear
                Luminosity = luminosity //value between 1 and 100
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

        private void ResponseReceived(object p)
        {
            throw new NotImplementedException();
        }
    }
}
