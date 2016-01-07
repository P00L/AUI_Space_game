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
                writer.WriteAttributeString("childName", game.Child);
                writer.WriteAttributeString("therapistName", game.Therapist);
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
                    else if (anim.GetType() == typeof(LogicBlock))
                    {
                        writer.WriteAttributeString("type", "logicBlock");
                        LogicBlock logicBlock = ((LogicBlock)anim);
                        writer.WriteAttributeString("target", logicBlock.Target.ToString());
                        foreach (var shape in logicBlock.Shapes)
                        {
                            writer.WriteStartElement("shape");
                            writer.WriteAttributeString("color", shape.Color.ToString());
                            writer.WriteAttributeString("figure", shape.Figure.ToString());
                            writer.WriteAttributeString("x", shape.X.ToString());
                            writer.WriteAttributeString("z", shape.Z.ToString());
                            writer.WriteEndElement();
                        }
                    }
                        writer.WriteEndElement(); //end of animation
                }
                writer.WriteEndElement(); //end of animationSequence
                writer.WriteEndElement(); //end of game
                writer.Flush();
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string fileName = "";
            if (game.Child != null)
                fileName = game.Child + "_";
            if (game.Name != null)
                fileName = fileName + game.Name;
            else fileName = fileName + "game";
            saveFileDialog.FileName = fileName;
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
                                game.Child = reader.GetAttribute("childName");
                                game.Therapist = reader.GetAttribute("therapistName");
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
                                        int Target = Convert.ToInt16(reader.GetAttribute("target"));
                                        int i = 0;
                                        Shape[] Shapes = new Shape[Constant.NumberOfCarpetSquares];
                                        LogicBlock LogicBlock = new LogicBlock();
                                        if (reader.ReadToDescendant("shape"))
                                        {
                                            do
                                            {
                                                string Color = reader.GetAttribute("color");
                                                string Figure = reader.GetAttribute("figure");
                                                double X = Convert.ToDouble(reader.GetAttribute("x"));
                                                double Z = Convert.ToDouble(reader.GetAttribute("z"));
                                                Shape TempShape = new Model.Shape(Color, Figure, LogicBlock, X, Z);
                                                Shapes[i] = TempShape;
                                                i++;
                                            }
                                            while (reader.ReadToNextSibling("shape"));
                                        }
                                        LogicBlock.Shapes = Shapes;
                                        LogicBlock.Target = Target;
                                        game.AnimationsSequence.Add(LogicBlock);
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
