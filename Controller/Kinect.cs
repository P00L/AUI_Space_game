using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using AuiSpaceGame.Model;

namespace AuiSpaceGame.Controller
{
    public class Kinect
    {
        /// <summary>
        /// Array for the bodies
        /// </summary>
        private Body[] bodies = null;

        /// <summary>
        /// Body of the child
        /// </summary>
        private Body childBody;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Coordinate mapper to map one type of point to another
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// Reader for body frames
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary>
        /// Current status text to display
        /// </summary>
        private string statusText = null;

        private Game game;
        private GameState gameState;
        private Animation currentAnimation;

        public Kinect(Game game, GameState gameState)
        {
            this.game = game;
            this.gameState = gameState;

            // one sensor is currently supported
            this.kinectSensor = KinectSensor.GetDefault();

            // set the status text
            this.statusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.NoSensorStatusText;

            // get the coordinate mapper
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            // open the reader for the body frames
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();
        }

        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            // on failure, set the status text
            this.statusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.SensorNotAvailableStatusText;
        }

        /// <summary>
        /// Handles the body frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                    // As long as those body objects are not disposed and not set to null in the array,
                    // those body objects will be re-used.
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                float childHeight = 3;
                childBody = this.bodies[0];
                foreach (Body body in this.bodies)
                {
                    if (body.IsTracked) //TODO checkare se il bambino è presente o no
                    {
                        if (body.Joints[JointType.SpineMid].Position.Y < childHeight)
                        {
                            childBody = body;
                            childHeight = body.Joints[JointType.SpineMid].Position.Y;
                        }
                    }

                }
                //now we have the body of the child
                if (gameState.AnimationOn)
                {
                    currentAnimation = game.AnimationsSequence.ElementAt(gameState.AnimationId);
                    if (currentAnimation.GetType() == typeof(Asteroid))
                        CheckChildPositionAsteroid();
                    else if (currentAnimation.GetType() == typeof(LogicBlock))
                        CheckChildPositionLogicBlock();
                }

            }
        }

        private void CheckChildPositionAsteroid()
        {
            /*Console.WriteLine("X = " + childBody.Joints[JointType.SpineMid].Position.X);
              Console.WriteLine("Y = " + childBody.Joints[JointType.SpineMid].Position.Y);
              Console.WriteLine("Z = " + childBody.Joints[JointType.SpineMid].Position.Z);*/

            double Z = 0, X = 0;
            TimeSpan T;
            Asteroid currentAsteroid = (Asteroid)currentAnimation;
            X = currentAsteroid.Lane;
            T = DateTime.Now - currentAsteroid.StartingAnimationTime;
            double time = T.TotalMilliseconds + Constant.TPharos;
            Z = currentAsteroid.Z0 + Constant.ZCarpet + currentAsteroid.Speed * time;


            //if the child hits the asteroid..    
            if (childBody.Joints[JointType.SpineMid].Position.Z >= Z - Constant.Delta &&
                childBody.Joints[JointType.SpineMid].Position.Z <= Z + Constant.Delta &&
                childBody.Joints[JointType.SpineMid].Position.X >= X - Constant.Delta &&
                childBody.Joints[JointType.SpineMid].Position.X <= X + Constant.Delta)
            {
                Console.WriteLine(System.DateTime.Now.ToString("hh.mm.ss.ffffff")); //TODO TOGLIERE!!
                gameState.ExecuteReinforcement = false;
            }
        }

        private void CheckChildPositionLogicBlock()
        {
            /*Console.WriteLine("X = " + childBody.Joints[JointType.SpineMid].Position.X);
              Console.WriteLine("Y = " + childBody.Joints[JointType.SpineMid].Position.Y);
              Console.WriteLine("Z = " + childBody.Joints[JointType.SpineMid].Position.Z);*/

            double Z = 0, X = 0;
            LogicBlock currentLogicBlock = (LogicBlock)currentAnimation;
            X = currentLogicBlock.Shapes[currentLogicBlock.Target].X;
            Z = currentLogicBlock.Shapes[currentLogicBlock.Target].Z;


            //if the child hits the asteroid..    
            if (childBody.Joints[JointType.SpineMid].Position.Z >= Z - Constant.DeltaLogicBlock &&
                childBody.Joints[JointType.SpineMid].Position.Z <= Z + Constant.DeltaLogicBlock &&
                childBody.Joints[JointType.SpineMid].Position.X >= X - Constant.DeltaLogicBlock &&
                childBody.Joints[JointType.SpineMid].Position.X <= X + Constant.DeltaLogicBlock)
            {
                Console.WriteLine(System.DateTime.Now.ToString("hh.mm.ss.ffffff")); //TODO TOGLIERE!!
                gameState.ExecuteReinforcement = true;
            }
        }


    }
}
