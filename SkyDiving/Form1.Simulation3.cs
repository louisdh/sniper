using SniperEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyDiving {

    /// <summary>
    /// Placeholder class to open this code (and not the designer)
    /// when you double click this class in Solution Explorer 
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    internal class Unused2 {
    }

    public partial class Form1 {

        /// <summary>
        /// Simulation on Earth with a basketball and skydiving woman
        /// </summary>
        public void CreateTestWorld3() {

            world = new World();

            // 10 x 300 m
            world.Width = 1000;
            world.Height = 30000;

            Bitmap ballBmp = BitmapHelper.GetBitmapForResource("ball-50");

            SolidObject g2 = new SolidObject(new SniperEngine.Point(450, 29800), 50, 50, 50);
            g2.Shape = Shapes.Ellipse;
            g2.Mass = 10;
            g2.Bmp = ballBmp;
            g2.Elasticity = 0.7f;
            centerObject = g2;
            world.AddSolidObject(g2);
            g2.Friction = 0.9f;

            parachute = new SolidObject(new SniperEngine.Point(310, 29900), 2, 0, 2);
            parachute.Bmp = BitmapHelper.GetBitmapForResource("parachute");
            parachute.Mass = 10f;
            world.AddSolidObject(parachute);
            centerObject = parachute;
            parachute.Inflatable = true;
            parachute.Shape = Shapes.Rect;


            skyDiver = new SolidObject(new SniperEngine.Point(275, 29900), 75, 150, 30);
            skyDiver.Shape = Shapes.Rect;
            skyDiver.Bmp = BitmapHelper.GetBitmapForResource("woman1");
            skyDiver.Mass = 80;
            skyDiver.Friction = 0.5f;
            skyDiver.Fragility = 1.0f;
            world.AddSolidObject(skyDiver);

            centerObject = skyDiver;
            skyDiver.AttachedObject = parachute;


            TextureBrush texture = BitmapHelper.GetBitmapTextureForResource("bricks");

            SolidObject wallRight = new SolidObject(new SniperEngine.Point(840, 700), 60, 700, 100);
            wallRight.Shape = Shapes.Rect;
            wallRight.Texture = texture;
            wallRight.Mass = 200000000;
            world.AddSolidObject(wallRight);


            SolidObject wallLeft = new SolidObject(new SniperEngine.Point(0, 700), 60, 700, 100);
            wallLeft.Shape = Shapes.Rect;
            wallLeft.Mass = 200000000;
            wallLeft.Texture = texture;

            world.AddSolidObject(wallLeft);

            SolidObject sun = new SolidObject(new SniperEngine.Point(200, 29900), 175, 175, 175);
            sun.Bmp = BitmapHelper.GetBitmapForResource("sun");
            world.AddBackgroundObject(sun);
            sun.ZIndex = 1000;

            AddRandomCloudsToWorld(false);

        }


    }
}
