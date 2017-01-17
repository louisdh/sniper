using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyDiving {
    public class BitmapHelper {

        /// <summary>
        /// Rotate Bitmap by an angle
        /// </summary>
        /// <param name="bmp">Bitmap to rotate</param>
        /// <param name="angle">Angle in degress</param>
        /// <returns>Rotated bitmap</returns>
        public static Bitmap RotateImage(Bitmap bmp, float angle) {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage)) {

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.High;
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2); 
                g.DrawImage(bmp, new System.Drawing.Point(0, 0));
            }

            return rotatedImage;
        }

        /// <summary>
        /// Get Bitmap object from resource (png expected)
        /// </summary>
        /// <param name="res">Resource name</param>
        /// <returns>Bitmap for resource</returns>
        public static Bitmap GetBitmapForResource(string res) {

            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("SkyDiving.Resources." + res + ".png");
            return new Bitmap(str);

        }

        /// <summary>
        /// Get TextureBrush object from resource (png expected)
        /// </summary>
        /// <param name="res">Resource name</param>
        /// <returns>TextureBrush</returns>
        public static TextureBrush GetBitmapTextureForResource(string res) {

            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("SkyDiving.Resources." + res + ".png");

            TextureBrush texture = new TextureBrush(BitmapHelper.GetBitmapForResource(res));
            texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;

            return texture;

        }

    }
}
