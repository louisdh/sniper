using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SniperEngine {

    /// <summary>
    /// Represents a 2D world containing objects and physics logic
    /// </summary>
    public class World {

        private List<Object> objects;
        private List<Object> backgroundObject;

        private Color skyColor;
        public Color SkyColor {
            get { return skyColor; }
            set { skyColor = value; }
        }

        private Dictionary<Object, List<Object>> objectsCollisionChecked;

        private int time;

        private float gravityAcceleration = 9.81f;

        /// <summary>
        /// Gravity acceleration (e.g. 9.81 m/s^2 on Earth)
        /// </summary>
        public float GravityAcceleration {
            get { return gravityAcceleration; }
            set { gravityAcceleration = value; }
        }

        private float airDensityMultiplier = 1.0f;

        /// <summary>
        /// Air density compared to Earth
        /// </summary>
        public float AirDensityMultiplier {
            get { return airDensityMultiplier; }
            set { airDensityMultiplier = value; }
        }

        private int height;

        /// <summary>
        /// World height
        /// </summary>
        public int Height {
            get { return height; }
            set { height = value; }
        }

        private int width;

        /// <summary>
        /// World width
        /// </summary>
        public int Width {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Objects in world
        /// </summary>
        public List<Object> Objects {
            get { return objects; }
        }

        /// <summary>
        /// Background objects in world 
        /// (background object don't get collision detection)
        /// </summary>
        public List<Object> BackgroundObjects {
            get { return backgroundObject; }
        }

        /// <summary>
        /// Create new world
        /// </summary>
        public World() {
            objects = new List<Object>();
            backgroundObject = new List<Object>();
            time = 1;
            skyColor = Color.LightBlue;
        }

        /// <summary>
        /// Add solid object to world
        /// </summary>
        /// <param name="o">Solid object</param>
        public void AddSolidObject(SolidObject o) {
            objects.Add(o);
        }

        /// <summary>
        /// Add background object to world
        /// </summary>
        /// <param name="o">Background objecy</param>
        public void AddBackgroundObject(SolidObject o) {
            backgroundObject.Add(o);
        }

        // http://en.wikipedia.org/wiki/Density#Air
        private float AirDensity(float temp) {
            return airDensityMultiplier * 1.2953f * (float)Math.Pow(Math.E, -0.004 * temp);
        }

        private float Temperature(float altitude) {
            return 25.0f * airDensityMultiplier;
        }

        // http://en.wikipedia.org/wiki/Terminal_velocity#Physics
        private float TerminalVelocity(SolidObject s) {
            float airDensity = AirDensity(Temperature(height - s.Y));

            // multiply by 100 for cm/s to m/s
            float terminalVelocity = 100 * (float)Math.Sqrt((2 * s.Mass * gravityAcceleration) / (float)(s.DragCoefficient * s.ProjectedArea * airDensity));
            return terminalVelocity;
        }

        /// <summary>
        /// Convert point to/from world coordination system
        /// </summary>
        /// <param name="p">Point to convert</param>
        /// <returns>Converted point</returns>
        public Point ConvertPointToWorld(Point p) {
            return new Point(p.X, ConvertYCoordinateToWorld(p.Y));
        }

        /// <summary>
        /// Converts y coordinate to/from world coordination system
        /// </summary>
        /// <param name="y">Y coordinate</param>
        /// <returns>Converted y coordinate</returns>
        public float ConvertYCoordinateToWorld(float y) {
            return -y+height;
        }

        /// <summary>
        /// Update the world
        /// </summary>
        public void Update() {
            foreach (SolidObject s in backgroundObject) {

                s.Y += s.VelocityY;
                s.X += s.VelocityX;

                if (s.X > width) {
                    s.X = -s.Width;
                }

            }

            foreach (Object o in objects.ToList<Object>()) {

                SolidObject s = (SolidObject)o;

                float dif = s.X - s.PrevX;
                if (s.Shape == Shapes.Ellipse && dif != 0) {
                    s.Angle += (float)(dif / 1.0f);
                }

                s.PrevX = s.X;
                s.PrevY = s.Y;

                if (s.Height == 0) {
                    continue;
                }

                if (s.Y == s.Height) {
                    SolidObjectHitGround(s);
                }


                if (s.VelocityY < 0) {
                   s.VelocityY = (float)Math.Max(-TerminalVelocity(s), s.VelocityY);
                }

                //s.VelocityY = IntegerHelper.RoundToDecimalPlace(s.VelocityY, 4);

                float newVal = gravityAcceleration - s.DragCoefficient*airDensityMultiplier / s.Mass;
                s.VelocityY -= ((newVal * 100.0f) / gravityAcceleration) / 60.0f;


                // friction:
                // doesn't take surface in to account,
                // needs a way to know what the underlying object is
                s.VelocityX *= s.Friction;

                if (s.AttachedObject != null) {
                    if (s.AttachedObject.Height != 0) {
                        s.VelocityY = s.AttachedObject.VelocityY;
                        s.VelocityX = s.AttachedObject.VelocityX;

                    } else {
                        s.AttachedObject.VelocityY = s.VelocityY;
                        s.AttachedObject.Y += s.AttachedObject.VelocityY;

                        s.AttachedObject.VelocityX = s.VelocityX;
                        s.AttachedObject.X += s.AttachedObject.VelocityX;
                    }

                }

                s.Y += s.VelocityY;

                // stop endless bounce
                if (s.Elasticity != 0 && Math.Abs(s.VelocityY) < 0.5) {
                    s.VelocityY *= 0;
                }

                s.X += s.VelocityX;
                s.Y = Math.Max(s.Height, s.Y);


                TransformToTargets(s);

            }

            CheckCollisionsX();
            CheckCollisionsY();

            foreach (Object o in objects.ToList<Object>()) {

                SolidObject s = (SolidObject)o;
                Point temp = new Point(s.Position.X, (int)s.Position.Y);
                Point temp1 = new Point(s.PrevPosition.X, (int)s.PrevPosition.Y);

                s.DistanceTravelled += temp.DistanceToPoint(temp1);

            }

            time++;

        }

        private void SolidObjectHitGround(SolidObject s) {

            if (-s.VelocityY > 70.0 * s.Fragility) {
                s.TargetHeight = 0;
                s.ZIndex = -200;

                int index = 0;
                int pW = 10;
                int pH = 20;
                Random r = new Random();

                for (int i = 0; i < pW; i++) {

                    for (int j = 0; j < pH; j++) {

                        float y = s.Bottom + 2.0f * (float)(j + (Math.Pow((pW / 2.0f) - i, 2) / 0.5f));
                        if (y < 0) continue;
                        
                        float x = s.Center.X - 2.0f * (i - (pW / 2.0f));
                        Point p = new Point(x, y);
                        SolidObject bloodParticle = new SolidObject(p, 10, 10, 1);
                        bloodParticle.Color = Color.DarkRed;
                        bloodParticle.VelocityY = j * .25f;
                        bloodParticle.VelocityX = (i - pW / 2.0f) + (r.Next(10) / 10.0f - 0.5f);
                        bloodParticle.ZIndex = -1 * index;
                        bloodParticle.Mass = 0.080f;

                        bloodParticle.Friction = 0.95f;
                        bloodParticle.TargetAcceleration = 1.05f;
                        bloodParticle.TargetHeight = 3;
                        bloodParticle.TargetWidth = 4;

                        AddSolidObject(bloodParticle);
                        index++;
                    }

                }

            }

            s.VelocityY *= -s.Elasticity;

            if (s.Inflatable) {
                s.ZIndex -= 1;
                s.TargetHeight = 0;
            }
        }

        private void CheckCollisionsX() {

            objectsCollisionChecked = new Dictionary<Object, List<Object>>();

            foreach (Object o in objects.ToList<Object>()) {

                foreach (Object o2 in objects.ToList<Object>()) {
                    if (o == o2 || (objectsCollisionChecked.ContainsKey(o) && ((List<Object>)objectsCollisionChecked[o]).Contains(o2))) continue;
    
                    SolidObject s1 = (SolidObject)o;
                    SolidObject s2 = (SolidObject)o2;

                    if (s2 == s1.AttachedObject) continue;
                    if (s1 == s2.AttachedObject) continue;
                    if (s1.ZIndex != s2.ZIndex) continue;

                    if (s1.Height == 0 || s2.Height == 0) {
                        continue;
                    }

                    if (!objectsCollisionChecked.ContainsKey(o)) {
                        objectsCollisionChecked.Add(o, new List<Object>());
                    }

                    ((List<Object>)objectsCollisionChecked[o]).Add(o2);

                    float s1Top = s1.Top;
                    float s2Top = s2.Top;

                    if ((s1.VelocityX != 0 || s2.VelocityX != 0) &&
                        ((s1.Right > s2.Left && s1.Right < s2.Right) && ((s1Top <= s2Top && s1Top >= s2Top - s2.Height))) ||
                        ((s1.Left > s2.Left && s1.Left < s2.Right) && ((s1Top <= s2Top && s1Top >= s2Top-s2.Height)))) {


                        float tempX = s1.VelocityX;

                        // s1 > s2
                        if (s1.VelocityX != 0 && s1.Right > s2.Left && s1.Right < s2.Right) {
                            s1.X = (float)Math.Round(s2.X - s1.Width);

                        // s2 > s1
                        } else if (s2.VelocityX != 0 && s1.Left >= s2.Left && s1.Left <= s2.Right) {
                            s2.X = s1.X - s2.Width-1;

                        } 
                        
                        if (s1.VelocityX != 0 && s1.Left >= s2.Left && s1.Left <= s2.Right) {
                            s1.X = s2.Right + 1;

                        } else if (s2.VelocityX != 0 && s1.Right > s2.Left && s1.Right < s2.Right) {
                            s2.X = s1.Right + 1;

                        }

                        if (s1.Mass == s2.Mass) {
                            s1.VelocityX = s2.VelocityX;
                            s2.VelocityX = tempX;
                        } else {

                            s1.VelocityX = ((s1.Mass - s2.Mass) / (s1.Mass + s2.Mass)) * s1.VelocityX + ((2 * s2.Mass) / (s1.Mass + s2.Mass)) * s2.VelocityX;
                            s2.VelocityX = ((s2.Mass - s1.Mass) / (s1.Mass + s2.Mass)) * s2.VelocityX + ((2 * s1.Mass) / (s1.Mass + s2.Mass)) * tempX;

                        }


                        s1.VelocityX = IntegerHelper.RoundToDecimalPlace(s1.VelocityX, 4);
                        s2.VelocityX = IntegerHelper.RoundToDecimalPlace(s2.VelocityX, 4);

                    }

                }

            }

        }

        private void CheckCollisionsY() {

            objectsCollisionChecked = new Dictionary<Object, List<Object>>();

            foreach (Object o in objects.ToList<Object>()) {

                foreach (Object o2 in objects.ToList<Object>()) {
                    if (o == o2 || (objectsCollisionChecked.ContainsKey(o) && ((List<Object>)objectsCollisionChecked[o]).Contains(o2))) continue;

                    SolidObject s1_ = (SolidObject)o;
                    SolidObject s2_ = (SolidObject)o2;

                    SolidObject s1 = s1_;
                    SolidObject s2 = s2_;

                    if (s2 == s1.AttachedObject) continue;
                    if (s1 == s2.AttachedObject) continue;
                    if (s1.ZIndex != s2.ZIndex) continue;

                    if (s1.Height == 0 || s2.Height == 0) {
                        continue;
                    }

                    if (!objectsCollisionChecked.ContainsKey(o)) {
                        objectsCollisionChecked.Add(o, new List<Object>());
                    }
                    
                    ((List<Object>)objectsCollisionChecked[o]).Add(o2);


                    float s1Top = GetAdjustedTop(s1);
                    float s2Top = GetAdjustedTop(s2);

                    if ((new Rect(Math.Round(s1.X) + 1, ConvertPointToWorld(new Point(0, s1Top)).Y + 0, (int)s1.Width - 2, (s1.Height) + 0)).IntersectsWith(
                        (new Rect(Math.Round(s2.X) + 1, ConvertPointToWorld(new Point(0, s2Top)).Y + 0, (int)s2.Width - 2, (s2.Height) + 0)))) {

                        if ((s1Top - s1.Height) < s2Top && s1Top > s2Top) {

                            s1.Y = (int)(s2Top + s1.Height)+1;

                            SolidObjectHitGround(s1);

                        } else if ((s2Top - s2.Height) < s1Top && (s2Top - s2.Height) > (s1Top - s1.Height)) {

                            s2.Y = (int)(s1Top + s2.Height) + 1;

                            SolidObjectHitGround(s2);

                        }

                    }
                    
                }

            }

        }

        private float GetAdjustedTop(SolidObject s) {

            float sTop = s.Top;
            if (Math.Abs(s.VelocityY) >= s.Height) {
                float temp = Math.Abs(s.VelocityY);
                while (temp >= s.Height) {
                    sTop += s.Height;
                    temp -= s.Height;
                }
            }

            return sTop;
        }

        private void TransformToTargets(SolidObject s) {

            float acceleration = s.TargetAcceleration;
            float deceleration = 1.0f / (float)s.TargetAcceleration;

            if (s.TargetDepth != -1) {
                s.Depth *= acceleration;
                s.Depth = Math.Min(s.Depth, s.TargetDepth);

                s.Depth = IntegerHelper.RoundToDecimalPlace(s.Depth, 4);

                if (IntegerHelper.RoundToDecimalPlace(s.Depth, 2) == s.TargetDepth) {
                    s.TargetDepth = -1;
                }
            }

            if (s.TargetWidth != -1) {
                float prevXCenter = s.Center.X;

                if (s.TargetWidth > s.Width) {
                    s.Width *= acceleration;
                    s.Width = Math.Min(s.Width, s.TargetWidth);
                } else {
                    s.Width *= deceleration;
                    s.Width = Math.Max(s.Width, s.TargetWidth);
                }

                if (IntegerHelper.RoundToDecimalPlace(s.Width, 2) == s.TargetWidth) {
                    s.Width = s.TargetWidth;
                    s.TargetWidth = -1;
                }

                s.X = prevXCenter - s.Width / 2.0f;
                s.PrevX = s.X;
            }

            if (s.TargetHeight != -1) {

                float prevBtm = s.Bottom;

                if (s.TargetHeight > s.Height) {
                    s.Height *= acceleration;
                    s.Height = Math.Min(s.Height, s.TargetHeight);

                } else {
                    s.Height *= deceleration;
                    s.Height = Math.Max(s.Height, s.TargetHeight);

                }

                s.Height = IntegerHelper.RoundToDecimalPlace(s.Height, 4);

                if (IntegerHelper.RoundToDecimalPlace(s.Height, 2) == s.TargetHeight) {
                    s.Height = s.TargetHeight;
                    s.TargetHeight = -1;
                }

                s.Y = prevBtm + s.Height;
                s.PrevY = s.Y;
            }

        }

    }
}
