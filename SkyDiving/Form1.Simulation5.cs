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
    internal class Unused4 {
    }

    public partial class Form1 {

        /// <summary>
        /// Simulation on planet Mars with a skydiving alien
        /// </summary>
        public void CreateTestWorld5() {

            // Mars
            world = new World();
            world.GravityAcceleration = 3.711f;
            world.AirDensityMultiplier = 0.6f;

            // 10 x 300 m
            world.Width = 1000;
            world.Height = 30000;

            Bitmap ballBmp = BitmapHelper.GetBitmapForResource("ball-50");

            SolidObject g2 = new SolidObject(new SniperEngine.Point(450, 24000), 50, 50, 50);
            g2.Shape = Shapes.Ellipse;
            g2.Mass = 10;
            g2.Bmp = ballBmp;
            g2.Elasticity = 0.7f;
            centerObject = g2;
            world.AddSolidObject(g2);
            g2.Friction = 0.9f;

            parachute = new SolidObject(new SniperEngine.Point(310, 24000), 2, 0, 2);
            parachute.Bmp = BitmapHelper.GetBitmapForResource("parachute");
            parachute.Mass = 10f;
            world.AddSolidObject(parachute);
            centerObject = parachute;
            parachute.Inflatable = true;
            parachute.Shape = Shapes.Rect;


            skyDiver = new SolidObject(new SniperEngine.Point(275, 24000), 75, 150, 30);
            skyDiver.Shape = Shapes.Rect;
            skyDiver.Bmp = BitmapHelper.GetBitmapForResource("alien-1");
            skyDiver.Mass = 80;
            skyDiver.Friction = 0.5f;
            skyDiver.Fragility = 0.8f;
            world.AddSolidObject(skyDiver);

            centerObject = skyDiver;
            skyDiver.AttachedObject = parachute;

            SolidObject groundReal = new SolidObject(new SniperEngine.Point(0, 40), world.Width, 40, 100);
            groundReal.Shape = Shapes.Rect;
            groundReal.Color = Color.Transparent;
            groundReal.Mass = 200000000;
            world.AddSolidObject(groundReal);

            world.SkyColor = Color.FromArgb(70, 0, 0);

            AddRandomStarsToWorld(true);
            AddRandomCloudsToWorld(true);

            SolidObject earth = new SolidObject(new SniperEngine.Point(200, 29990), 10, 10, 10);
            earth.Bmp = BitmapHelper.GetBitmapForResource("earth-1");
            world.AddBackgroundObject(earth);
            earth.ZIndex = 1000;


            SolidObject ground = new SolidObject(new SniperEngine.Point(0, 50), world.Width, 50, 100);
            ground.Shape = Shapes.Rect;
            ground.Mass = 200000000;
            ground.ZIndex = 1;
            ground.Texture = BitmapHelper.GetBitmapTextureForResource("mars-ground");
            world.AddBackgroundObject(ground);


        }


    }
}
