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

                        string LaneString = "";
                        if (ast.Lane == Lane.Left)
                            LaneString = "left";
                        else if (ast.Lane == Lane.Middle)
                            LaneString = "middle";
                        else if (ast.Lane == Lane.Right)
                            LaneString = "right";
                        writer.WriteAttributeString("lane", LaneString);

                        string SpeedString = "";
                        if (ast.Speed == Speed.High)
                            SpeedString = "high";
                        else if (ast.Speed == Speed.Low)
                            SpeedString = "low";
                        writer.WriteAttributeString("speed", SpeedString);
                    }
                    else if (anim.GetType() == typeof(LogicBlock))
                    {
                        writer.WriteAttributeString("type", "logicBlock");
                        LogicBlock logicBlock = ((LogicBlock)anim);

                        string TargetString = "";
                        if (logicBlock.Target == Square.BottomLeft)
                            TargetString = "BottomLeft";
                        else if (logicBlock.Target == Square.BottomRight)
                            TargetString = "BottomRight";
                        else if (logicBlock.Target == Square.TopLeft)
                            TargetString = "TopLeft";
                        else if (logicBlock.Target == Square.TopRight)
                            TargetString = "TopRight";
                        writer.WriteAttributeString("target", TargetString);

                        foreach (var shape in logicBlock.Shapes)
                        {
                            writer.WriteStartElement("shape");


                            writer.WriteAttributeString("color", shape.Color.ToString());
                            writer.WriteAttributeString("figure", shape.Figure.ToString());

                            string XString = "";
                            if (shape.X == SquareCoordinate.Left)
                                XString = "left";
                            else if (shape.X == SquareCoordinate.Right)
                                XString = "right";

                            string ZString = "";
                            if (shape.Z == SquareCoordinate.Top)
                                ZString = "top";
                            else if (shape.Z == SquareCoordinate.Bottom)
                                ZString = "bottom";

                            writer.WriteAttributeString("x", XString);
                            writer.WriteAttributeString("z", ZString);
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
                                        string LaneString = reader.GetAttribute("lane");
                                        double LaneDouble = 0;
                                        if (LaneString == "left")
                                            LaneDouble = Lane.Left;
                                        else if (LaneString == "middle")
                                            LaneDouble = Lane.Middle;
                                        else if (LaneString == "right")
                                            LaneDouble = Lane.Right;

                                        string SpeedString = reader.GetAttribute("speed");
                                        double SpeedDouble = 0;
                                        if (SpeedString == "high")
                                            SpeedDouble = Speed.High;
                                        else if (SpeedString == "low")
                                            SpeedDouble = Speed.Low;

                                        Asteroid asteroid = new Asteroid(LaneDouble, SpeedDouble);
                                        game.AnimationsSequence.Add(asteroid);
                                    }
                                    else if (reader.GetAttribute("type") == "logicBlock")
                                    {

                                        string TargetString = reader.GetAttribute("target");
                                        int Target = 0;
                                        if (TargetString == "BottomLeft")
                                            Target = Square.BottomLeft;
                                        else if (TargetString == "BottomRight")
                                            Target = Square.BottomRight;
                                        else if (TargetString == "TopLeft")
                                            Target = Square.TopLeft;
                                        else if (TargetString == "TopRight")
                                            Target = Square.TopRight;

                                            int i = 0;
                                        Shape[] Shapes = new Shape[Constant.NumberOfCarpetSquares];
                                        LogicBlock LogicBlock = new LogicBlock();
                                        if (reader.ReadToDescendant("shape"))
                                        {
                                            do
                                            {
                                                string Color = reader.GetAttribute("color");
                                                string Figure = reader.GetAttribute("figure");

                                                string XString = reader.GetAttribute("x");
                                                double X = 0;
                                                if (XString == "left")
                                                    X = SquareCoordinate.Left;
                                                else if (XString == "right")
                                                    X = SquareCoordinate.Right;

                                                string ZString = reader.GetAttribute("z");
                                                double Z = 0;
                                                if (ZString == "top")
                                                    Z = SquareCoordinate.Top;
                                                else if (ZString == "bottom")
                                                    Z = SquareCoordinate.Bottom;

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
