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
    internal class Unused {
    }

    public partial class Form1 {

        /// <summary>
        /// Simulation on Earth (night)
        /// </summary>
        public void CreateTestWorld1() {

            world = new World();

            // 10 x 300 m
            world.Width = 1000;
            world.Height = 30000;

            Bitmap ballBmp = BitmapHelper.GetBitmapForResource("ball-50");
            TextureBrush texture = BitmapHelper.GetBitmapTextureForResource("bricks");


            SolidObject ground = new SolidObject(new SniperEngine.Point(0, 30), world.Width, 30, 100);
            ground.Shape = Shapes.Rect;
            ground.Mass = 200000000;
            ground.Texture = texture;

            world.AddSolidObject(ground);



            SolidObject g2 = new SolidObject(new SniperEngine.Point(450, 700), 50, 50, 50);
            g2.Shape = Shapes.Ellipse;
            g2.Mass = 10;
            g2.Bmp = ballBmp;
            g2.Elasticity = 0.7f;
            centerObject = g2;
            world.AddSolidObject(g2);
            g2.Friction = 0.9f;

            parachute = new SolidObject(new SniperEngine.Point(310, 800), 2, 0, 2);
            parachute.Bmp = BitmapHelper.GetBitmapForResource("parachute 2");
            parachute.Mass = 10f;
            world.AddSolidObject(parachute);
            parachute.Inflatable = true;
            parachute.Shape = Shapes.Rect;


            skyDiver = new SolidObject(new SniperEngine.Point(275, 800), 75, 150, 30);
            skyDiver.Shape = Shapes.Rect;
            skyDiver.Bmp = BitmapHelper.GetBitmapForResource("woman1");
            skyDiver.Mass = 80;
            skyDiver.Friction = 0.5f;
            skyDiver.Fragility = 1.0f;
            skyDiver.VelocityX = 1.1f;
            world.AddSolidObject(skyDiver);

            centerObject = skyDiver;
            skyDiver.AttachedObject = parachute;



            SolidObject wallRight = new SolidObject(new SniperEngine.Point(540, 730), 60, 700, 100);
            wallRight.Shape = Shapes.Rect;
            wallRight.Texture = texture;
            wallRight.Mass = 200000000;
            world.AddSolidObject(wallRight);


            SolidObject wallLeft = new SolidObject(new SniperEngine.Point(0, 730), 60, 700, 100);
            wallLeft.Shape = Shapes.Rect;
            wallLeft.Mass = 200000000;
            wallLeft.Texture = texture;

            world.AddSolidObject(wallLeft);


            world.SkyColor = Color.FromArgb(0, 0, 20);

            AddRandomStarsToWorld(false);

            SolidObject moon = new SolidObject(new SniperEngine.Point(200, 29900), 175, 175, 175);
            moon.Bmp = BitmapHelper.GetBitmapForResource("moon");
            world.AddBackgroundObject(moon);
            moon.ZIndex = 500;

        }


    }
}
