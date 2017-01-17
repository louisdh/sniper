using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperEngine {

    /// <summary>
    /// Represents a 2D vector
    /// </summary>
    public class Vector {

        private float x;
        private float y;

        /// <summary>
        /// Gets or sets the x-coordinate of this vector
        /// </summary>
        public float X {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of this vector
        /// </summary>
        public float Y {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Initializes a new instance of the Vector class with components: 0, 0
        /// </summary>
        public Vector() {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Initializes a new instance of the Vector class with the specified components
        /// </summary>
        /// <param name="x">The x component of the vector</param>
        /// <param name="y">The y component of the vector</param>
        public Vector(float x, float y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Calculates the length (aka size) of the vector
        /// </summary>
        /// <returns>The length of the vector</returns>
        public float Length() {
            return (float)Math.Sqrt(x * x + y * y);
        }

    }
}
