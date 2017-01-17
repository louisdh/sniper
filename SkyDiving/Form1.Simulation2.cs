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
    internal class Unused1 {
    }

    public partial class Form1 {

        /// <summary>
        /// Simulation on Earth
        /// </summary>
        public void CreateTestWorld2() {

            world = new World();

            // 10 x 30 m
            world.Width = 1000;
            world.Height = 3000;

            Bitmap ballBmp = BitmapHelper.GetBitmapForResource("ball-50");

            SolidObject g1 = new SolidObject(new SniperEngine.Point(100, 150), 50, 50, 50);
            g1.Shape = Shapes.Ellipse;
            g1.Mass = 10f;
            g1.Bmp = ballBmp;
            g1.Elasticity = 0.9f;
            g1.Friction = 0.99f;

            world.AddSolidObject(g1);
            centerObject = g1;

            SolidObject g2 = new SolidObject(new SniperEngine.Point(400, 250), 50, 50, 50);
            g2.Shape = Shapes.Ellipse;
            g2.VelocityX = 25.1f;
            g2.Mass = 10;
            g2.Bmp = ballBmp;
            g2.Elasticity = 0.7f;
            g2.Friction = 0.99f;
            centerObject = g1;
            world.AddSolidObject(g2);

            TextureBrush texture = BitmapHelper.GetBitmapTextureForResource("bricks");

            SolidObject wallRight = new SolidObject(new SniperEngine.Point(540, 700), 60, 700, 100);
            wallRight.Shape = Shapes.Rect;
            wallRight.Texture = texture;
            wallRight.Mass = 200000000;
            world.AddSolidObject(wallRight);
            wallRight.Friction = 0.0f;


            SolidObject wallLeft = new SolidObject(new SniperEngine.Point(0, 700), 60, 700, 100);
            wallLeft.Shape = Shapes.Rect;
            wallLeft.Mass = 200000000;
            wallLeft.Texture = texture;
            wallLeft.Friction = 0.0f;

            world.AddSolidObject(wallLeft);

            SolidObject sun = new SolidObject(new SniperEngine.Point(200, 2990), 175, 175, 175);
            sun.Bmp = BitmapHelper.GetBitmapForResource("sun");
            world.AddBackgroundObject(sun);
            sun.ZIndex = 1000;

            AddRandomCloudsToWorld(false);

        }

    }
}
