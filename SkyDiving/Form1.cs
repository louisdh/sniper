using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SniperEngine;
using System.IO;
using System.Reflection;

namespace SkyDiving {
    public partial class Form1 : Form {

        private static Random rnd = new Random();

        private int maxFrameRate = 100;
        private Timer timer;
        private World world;
        private int framesCount;

        private SolidObject centerObject;

        // Global variables for GDI+
        private Graphics screen;
        private Bitmap backBuffer;

        private SolidObject parachute;
        private SolidObject skyDiver;

        public Form1() {
            InitializeComponent();
            InitRenderer();
            InitTimer();

            simulationsComboBox.Items.Add("Earth 1");
            simulationsComboBox.Items.Add("Earth 2");
            simulationsComboBox.Items.Add("Earth Night");
            simulationsComboBox.Items.Add("The Moon");
            simulationsComboBox.Items.Add("Mars");

        }

        private void AddRandomStarsToWorld(bool onlySmall) {

            for (int i = 0; i < world.Height / 20; i++) {

                for (int j = 0; j < 8; j++) {

                    SolidObject gi = new SolidObject(new SniperEngine.Point(80 + rnd.Next(900) - 150, 100 + 50 * i + rnd.Next(30)), 2, 2);
                    gi.Color = Color.LightYellow;
                    gi.Shape = Shapes.Ellipse;

                    if (rnd.Next(2) + 1 == 1) {
                        if (onlySmall) continue;
                        gi.ZIndex = 1;
                    } else {
                        gi.ZIndex = 220;
                        gi.Width /= 2.0f;
                        gi.Height /= 2.0f;
                    }

                    world.AddBackgroundObject(gi);

                }
            }
        }

        private void AddRandomCloudsToWorld(bool isMars) {

            List<Bitmap> clouds = new List<Bitmap>();

            if (isMars) {
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud1-mars_"));
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud2-mars_"));
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud3-mars_"));
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud4-mars_"));
            } else {
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud1"));
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud2"));
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud3"));
                clouds.Add(BitmapHelper.GetBitmapForResource("cloud4"));
            }


            for (int i = 0; i < world.Height/200; i++) {

                for (int j = 0; j < 4; j++) {

                    Bitmap rand = clouds[rnd.Next(clouds.Count)];

                    SolidObject gi = new SolidObject(new SniperEngine.Point(80 + rnd.Next(900) - 150, 100 + 300 * i + rnd.Next(30)), rand.Width, rand.Height);
                    gi.Bmp = rand;
                    if (rnd.Next(2) + 1 == 1) {
                        gi.ZIndex = 1;
                        gi.VelocityX = 0.5f;
                    } else {
                        gi.ZIndex = 2;
                        gi.Width /= 2.0f;
                        gi.Height /= 2.0f;
                        gi.VelocityX = 0.3f;
                    }

                    gi.ZIndex = rnd.Next(3) + 1;
                    gi.Width /= (float)gi.ZIndex;
                    gi.Height /= (float)gi.ZIndex;

                    world.AddBackgroundObject(gi);

                }
            }
        }

        private void UpdateLabels() {
            if (centerObject != null) {
                massLbl.Text = centerObject.Mass + " kg";
            }
        }
        // Timing

        private void InitTimer() {

            timer = new Timer();
            timer.Interval = (int)(1000.0 / (float)maxFrameRate);
            timer.Tick += new EventHandler(timer_Tick);
        }

        Stopwatch sw = Stopwatch.StartNew();
        private int realFrameRate = 1;

        private void timer_Tick(object sender, EventArgs e) {

            if (world == null) return;

            framesCount++;

            if (framesCount > 10) {

                sw.Stop();
                TimeSpan elapsedTime = sw.Elapsed;

                realFrameRate = (int)Math.Min((1000.0 / ((float)elapsedTime.TotalMilliseconds / (float)framesCount)), maxFrameRate);
                fpsLbl.Text = realFrameRate + "FPS";
                sw = Stopwatch.StartNew();
                framesCount = 0;
            }

            DoGame();
            UpdateLabels();

            display.Invalidate(); // force redraw (& paint event);

            distanceTravelledLbl.Text = IntegerHelper.RoundToDecimalPlace((centerObject.DistanceTravelled/100.0f), 2) + " m";

        }



        #region game loop

        private void DoGame() {
            if (world == null) return;
            world.Update();
        }

        #endregion game loop

        #region renderer

        private void InitRenderer() {
            backBuffer = new Bitmap(display.Width, display.Height);
            screen = Graphics.FromImage(backBuffer);
            display.Paint += new PaintEventHandler(PaintDisplay);
        }


        private void PaintDisplay(object sender, PaintEventArgs e) {
            // On_Paint event handler voor display
            Render(e.Graphics);
        }

        private int OffsetY() {
            SniperEngine.Point converted = world.ConvertPointToWorld(centerObject.Center);
            int offsetY = Math.Min(world.Height - display.Height, Math.Max(0, (int)converted.Y - display.Height / 2));
            return offsetY;
        }

        private int OffsetX() {
            int offsetX = Math.Min(world.Width - display.Width, Math.Max(0, (int)centerObject.Center.X - display.Width / 2));
            return offsetX;
        }

        private void Render(Graphics output) {
            if (world == null) return;

            screen.Clear(world.SkyColor);
            screen.SmoothingMode = SmoothingMode.AntiAlias;

            int offsetY = OffsetY();
            int offsetX = OffsetX();

            foreach (SolidObject o in world.BackgroundObjects) {

                SniperEngine.Point con = world.ConvertPointToWorld(new SniperEngine.Point(o.X, o.Y));
                Rectangle boundingBox = new Rectangle((int)o.X - offsetX / o.ZIndex, (int)con.Y - offsetY / o.ZIndex, (int)o.Width, (int)o.Height);

                if (!boundingBox.IntersectsWith(new Rectangle(0, 0, display.Width, display.Height))) {
                    continue;
                }

                if (o.Bmp != null) {

                    Image img = new Bitmap(o.Bmp);
                    screen.DrawImage(img, boundingBox);

                } else if (o.Texture != null) {

                    o.Texture.ResetTransform();
                    o.Texture.TranslateTransform(boundingBox.X, boundingBox.Y);
                    screen.FillRectangle(o.Texture, boundingBox);

                } else {

                    if (o.Color != null) {
                        screen.FillEllipse(new SolidBrush(o.Color), boundingBox);

                    } else {
                        screen.FillEllipse(new SolidBrush(Color.Crimson), boundingBox);
                    }

                }
            }

            foreach (SolidObject o in world.Objects) {

                if (o.Height == 0) {
                    continue;
                }

                SniperEngine.Point con = world.ConvertPointToWorld(new SniperEngine.Point(o.X, o.Y));
                Rectangle boundingBox = new Rectangle((int)o.X - offsetX, ((int)con.Y - offsetY), (int)o.Width, (int)o.Height);

                if (!boundingBox.IntersectsWith(new Rectangle(0, 0, display.Width, display.Height))) {
                    continue;
                }

                if (o.Shape == Shapes.Ellipse) {

                    if (o.Bmp != null) {

                        Image img = o.Bmp;
                        img = BitmapHelper.RotateImage((Bitmap)img, o.Angle);
                        screen.DrawImage(img, boundingBox);

                    } else if (o.Texture != null) {

                        o.Texture.ResetTransform();
                        o.Texture.TranslateTransform(boundingBox.X, boundingBox.Y);
                        screen.FillRectangle(o.Texture, boundingBox);

                    } else {

                        if (o.Color != null) {
                            screen.FillEllipse(new SolidBrush(o.Color), boundingBox);

                        } else {
                            screen.FillEllipse(new SolidBrush(Color.Crimson), boundingBox);
                        }

                    }

                } else if (o.Shape == Shapes.Rect) {

                    if (o.Bmp != null) {

                        Image img = new Bitmap(o.Bmp);
                        screen.DrawImage(img, boundingBox);

                    } else if (o.Texture != null) {

                        o.Texture.ResetTransform();
                        o.Texture.TranslateTransform(boundingBox.X, boundingBox.Y);
                        screen.FillRectangle(o.Texture, boundingBox);

                    } else {

                        if (o.Color != null) {
                            screen.FillEllipse(new SolidBrush(o.Color), boundingBox);

                        } else {
                            screen.FillEllipse(new SolidBrush(Color.Crimson), boundingBox);
                        }

                    }
                }

            }


            string altitudeString = "Altitude: " + (world.Height - offsetY)/100 + " m";

            Font drawFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            if (world.SkyColor.GetBrightness() < 0.3f) {
                drawBrush = new SolidBrush(Color.White);
            }
            StringFormat drawFormat = new StringFormat();
            screen.DrawString(altitudeString, drawFont, drawBrush, 0, 0, drawFormat);


            // Draw vectors 

            if (drawVectorsCheckBox.Checked) {

                foreach (SolidObject o in world.Objects) {

                    SniperEngine.Point con = world.ConvertPointToWorld(new SniperEngine.Point(o.X, o.Y));
                    Rectangle boundingBox = new Rectangle((int)o.X - offsetX, ((int)con.Y - offsetY), (int)o.Width, (int)o.Height);
                    float bottomConverted = world.ConvertYCoordinateToWorld(o.Bottom);

                    if (!boundingBox.IntersectsWith(new Rectangle(0, 0, display.Width, display.Height))) {
                        continue;
                    }

                    float speed = Math.Max(0, IntegerHelper.RoundToDecimalPlace((float)((-o.Velocity.Y * 60 - 60) / 100.0), 0));
                    if (speed < 0.05) {
                        continue;
                    }

                    int offsetXForVectors = 4;

                    screen.FillRectangle(new SolidBrush(Color.Green), new Rectangle((int)o.Right + offsetXForVectors - offsetX, (int)con.Y - offsetY, 2, (int)o.Height));
                    screen.DrawLine(new Pen(Color.Green), (int)o.Right - offsetXForVectors - offsetX, (int)bottomConverted - offsetY - 9, (int)o.Right + 5 - offsetX, (int)bottomConverted - offsetY);
                    screen.DrawLine(new Pen(Color.Green), (int)o.Right + offsetXForVectors * 2 + 2 + 2 - offsetX, (int)bottomConverted - offsetY - 9, (int)o.Right + 5 - offsetX, (int)bottomConverted - offsetY);

                    string drawString = speed + " m/s";

                    screen.DrawString(drawString, drawFont, drawBrush, o.Right + 5 - offsetX, bottomConverted - 25 - offsetY, drawFormat);

                }

            }
 
            // show backbuffer on display
            output.DrawImage(backBuffer, new Rectangle(0, 0, display.Width, display.Height), new Rectangle(0, 0, display.Width, display.Height), GraphicsUnit.Pixel);

        }


        #endregion renderer

        private void display_MouseClick(object sender, MouseEventArgs e) {

            if (world == null) return;

            int offsetY = OffsetY();
            int offsetX = OffsetX();

            foreach (SolidObject o in world.Objects) {
                float conY = world.ConvertYCoordinateToWorld(o.Y);

                Rectangle boundingBox = new Rectangle((int)o.X - offsetX, (int)conY - offsetY, (int)o.Width, (int)o.Height);

                if (!boundingBox.IntersectsWith(new Rectangle(0, 0, display.Width, display.Height))) {
                    continue;
                }

                if (boundingBox.IntersectsWith(new Rectangle(e.X, e.Y, 1, 1))) {
                    centerObject = o;
                    UpdateLabels();
                    display.Invalidate(); // force redraw (& paint event);
                    break;
                }

            }

        }

        private void startBtn_Click(object sender, EventArgs e) {

            Button btn = (Button)sender;

            if (!timer.Enabled) {
                timer.Start();
                btn.Text = "Pause";
            } else {
                timer.Stop();
                btn.Text = "Start";
            }

        }

        private void nextFrame_Click(object sender, EventArgs e) {
            timer_Tick(null, null);
        }

        private void frameRateTrackBar_Scroll(object sender, EventArgs e) {
            maxFrameRate = frameRateTrackBar.Value;
            timer.Interval = (int)(1000.0 / (float)maxFrameRate);
            framesCount = 0;
            maxFrameRateLbl.Text = maxFrameRate + "";
        }

        private void drawVectorsCheckBox_CheckedChanged(object sender, EventArgs e) {
            display.Invalidate(); // force redraw (& paint event);

        }

        private void openParachuteBtn_Click(object sender, EventArgs e) {
            if (parachute == null) return;
            if (parachute.Depth == 2) {
                parachute.Height = 1;
                parachute.TargetHeight = 110;
                parachute.TargetDepth = 100;
                parachute.TargetWidth = 100;

            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (skyDiver == null) return false;

            if (keyData == Keys.Left) {
                skyDiver.VelocityX -= 20.0f;
                return true;

            } else if (keyData == Keys.Right) {
                skyDiver.VelocityX += 20.0f;
                return true;

            } else if (keyData == Keys.Space) {
                if (parachute.Depth == 2) {
                    openParachuteBtn_Click(null, null);
                }

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void simulationsComboBox_SelectedIndexChanged(object sender, EventArgs e) {

            if (timer.Enabled) {
                startBtn_Click(startBtn, null);
            }

            switch (simulationsComboBox.SelectedIndex) {
                case 0:
                    CreateTestWorld3();
                    break;
                case 1:
                    CreateTestWorld2();
                    break;
                case 2:
                    CreateTestWorld1();
                    break;
                case 3:
                    CreateTestWorld4();
                    break;
                case 4:
                    CreateTestWorld5();
                    break;
            }

            display.Invalidate(); // force redraw (& paint event);

        }

    }

}
