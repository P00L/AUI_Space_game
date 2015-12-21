using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Win32;

namespace AuiSpaceGame.Model.Parser
{
    public static class Parser
    {
        public static void saveGame(Game game)
        {
            var sw = new StringWriter();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.ConformanceLevel = ConformanceLevel.Auto;
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    writer.WriteStartElement("game");
                    writer.WriteAttributeString("gameName", game.Name);
                    writer.WriteAttributeString("childName", "BAMBINO"); //TODO cambiare nome
                    writer.WriteAttributeString("durationInMilliseconds", game.GameDuration.TotalMilliseconds.ToString());
                    writer.WriteStartElement("animationsSequence");
                    foreach (var anim in game.AnimationsSequence)
                    {
                        writer.WriteStartElement("animation");
                        if (anim.GetType() == typeof(Asteroid))
                        {
                            writer.WriteAttributeString("type", "asteroid");
                            Asteroid ast = ((Asteroid)anim);
                            writer.WriteAttributeString("lane", ast.Lane.ToString());
                            writer.WriteAttributeString("speed", ast.Speed.ToString());
                        }
                        writer.WriteEndElement(); //end of animation
                    }
                    writer.WriteEndElement(); //end of animationSequence
                    writer.WriteEndElement(); //end of game
                    writer.Flush();
                }
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string fileName = "game";
            if (game.Name != null) //TODO generazione nomi (se non c'è nomeGioco)?
                fileName = game.Name;
            saveFileDialog.FileName = "NOMEBAMBINO_" + fileName; //TODO nome bambino
            saveFileDialog.Filter = "XML file | *.xml";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, sw.ToString());
        }

        public static Game loadGame()
        {
            Game game = new Game();
            String xmlString = "EmptyGame"; //TODO default?!

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML-File | *.xml";
            if (openFileDialog.ShowDialog() == true)
                xmlString = File.ReadAllText(openFileDialog.FileName);
            if (xmlString != "EmptyGame")
            {
                // Create an XmlReader
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "game"))
                        {
                            if (reader.HasAttributes)
                            {
                                game.Name = reader.GetAttribute("gameName");
                                //TODO CHILD
                                Double gameDuration = Convert.ToDouble(reader.GetAttribute("durationInMilliseconds"));
                                game.GameDuration = TimeSpan.FromMilliseconds(gameDuration);
                            }
                        }
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "animationsSequence"))
                        {
                            if (reader.ReadToDescendant("animation"))
                            {
                                do
                                {
                                    if (reader.GetAttribute("type") == "asteroid")
                                    {
                                        double Lane = Convert.ToDouble(reader.GetAttribute("lane"));
                                        double Speed = Convert.ToDouble(reader.GetAttribute("speed"));
                                        Asteroid asteroid = new Asteroid(Lane, Speed);
                                        game.AnimationsSequence.Add(asteroid);
                                    }
                                    else if (reader.GetAttribute("type") == "logicBlock")
                                    {
                                        //TODO
                                    }
                                } while (reader.ReadToNextSibling("animation"));
                            }
                        }
                    }
                }
                return game;
            }
            return null;
        }
    }
}
