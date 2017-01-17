using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperEngine {

    /// <summary>
    /// Represents a pair of x- and y-coordinates that defines a point in a two-dimensional plane
    /// </summary>
    public class Point {

        private float x;
        private float y;

        /// <summary>
        /// Gets or sets the x-coordinate of this point
        /// </summary>
        public float X {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of this point
        /// </summary>
        public float Y {
            get { return y; }
            set { y = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the Point class with coordinates: 0, 0
        /// </summary>
        public Point() {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Initializes a new instance of the Point class with the specified coordinates
        /// </summary>
        /// <param name="x">The horizontal position of the point</param>
        /// <param name="y">The vertical position of the point</param>
        public Point(float x, float y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Calculates distance to a given point
        /// </summary>
        /// <param name="p">Point to calculate distance to</param>
        /// <returns>Distance to point p</returns>
        public float DistanceToPoint(Point p) {
            return (float)Math.Sqrt(Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2));
        }

    }
}
