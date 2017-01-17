using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SniperEngine {

    /// <summary>
    /// Represents a 2D solid object with lots of physical properties
    /// as well as some appearance properties (texture, bitmap, color, ...)
    /// </summary>
    public class SolidObject {

        private Bitmap bmp;

        /// <summary>
        /// Bitmap of the object
        /// </summary>
        public Bitmap Bmp {
            get { return bmp; }
            set { bmp = value; }
        }

        private Color color;

        /// <summary>
        /// Color of the object
        /// </summary>
        public Color Color {
            get { return color; }
            set { color = value; }
        }

        private bool inflatable;

        /// <summary>
        /// Is object inflatable (will collapse when dropped)
        /// </summary>
        public bool Inflatable {
            get { return inflatable; }
            set { inflatable = value; }
        }

        private SolidObject attachedObject;

        /// <summary>
        /// Attached object to this object (e.g. parachute)
        /// </summary>
        public SolidObject AttachedObject {
            get { return attachedObject; }
            set { attachedObject = value; }
        }

        private TextureBrush texture;

        /// <summary>
        /// Texture of the object
        /// </summary>
        public TextureBrush Texture {
            get { return texture; }
            set { texture = value; }
        }

        private int zIndex;

        /// <summary>
        /// Z-index of the object 
        /// (1000 being further away from the viewport than 0)
        /// </summary>
        public int ZIndex {
            get { return zIndex; }
            set { zIndex = value; }
        }

        private Shapes shape;

        /// <summary>
        /// Shape of the object
        /// </summary>
        public Shapes Shape {
            get { return shape; }
            set { shape = value; }
        }

        private float mass;

        /// <summary>
        /// Mass of the object in kg
        /// </summary>
        public float Mass {
            get { return mass; }
            set { mass = value; }
        }

        private float elasticity;

        /// <summary>
        /// Elasticity of the object
        /// Law of energy conservation dictates that this should be between 0.0f and 1.0f
        /// </summary>
        public float Elasticity {
            get { return elasticity; }
            set { elasticity = value; }
        }

        private float fragility;

        /// <summary>
        /// Fragility of the object (lower is more fragile) 
        /// </summary>
        public float Fragility {
            get { return fragility; }
            set { fragility = value; }
        }

        private float friction;

        /// <summary>
        /// Friction of the object
        /// </summary>
        public float Friction {
            get { return friction; }
            set { friction = value; }
        }

        private Vector velocity;

        /// <summary>
        /// Velocity of the object
        /// </summary>
        public Vector Velocity {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// X velocity of the object
        /// </summary>
        public float VelocityX {
            get { return velocity.X; }
            set { velocity.X = value; }
        }

        /// <summary>
        /// Y velocity of the object
        /// </summary>
        public float VelocityY {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        private Point position;
        private Point prevPosition;

        /// <summary>
        /// Previous position of the object 
        /// (can be used to keep track of travelled distance)
        /// </summary>
        public Point PrevPosition {
            get { return prevPosition; }
        }

        /// <summary>
        /// Current position of the object
        /// </summary>
        public Point Position {
            get { return position; }
        }

        /// <summary>
        /// Previous x position of the object
        /// </summary>
        public float PrevX {
            get { return (float)prevPosition.X; }
            set { prevPosition.X = value; }
        }

        /// <summary>
        /// Previous y position of the object
        /// </summary>
        public float PrevY {
            get { return (float)prevPosition.Y; }
            set { prevPosition.Y = value; }
        }

        /// <summary>
        /// Current x position of the object
        /// </summary>
        public float X {
            get { return (float)position.X; }
            set { position.X = value; }
        }

        /// <summary>
        /// Current y position of the object
        /// </summary>
        public float Y {
            get { return (float)position.Y; }
            set { position.Y = value; }
        }

        private float angle;

        /// <summary>
        /// Angle of the object (in degrees)
        /// </summary>
        public float Angle {
            get { return angle; }
            set { angle = value; }
        }

        private float width;
        private float height;
        private float depth;

        private float targetWidth;
        private float targetHeight;
        private float targetDepth;
        private float targetAcceleration;

        /// <summary>
        /// Width of the object (in cm)
        /// </summary>
        public float Width {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Height of the object (in cm)
        /// </summary>
        public float Height {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Depth of the object (in cm)
        /// </summary>
        public float Depth {
            get { return depth; }
            set { depth = value; }
        }

        /// <summary>
        /// Target width of the object (in cm)
        /// The width will move towards this target each update cycle
        /// </summary>
        public float TargetWidth {
            get { return targetWidth; }
            set { targetWidth = value; }
        }

        /// <summary>
        /// Target height of the object (in cm)
        /// The height will move towards this target each update cycle
        /// </summary>
        public float TargetHeight {
            get { return targetHeight; }
            set { targetHeight = value; }
        }

        /// <summary>
        /// Target depth of the object (in cm)
        /// The depth will move towards this target each update cycle
        /// </summary>
        public float TargetDepth {
            get { return targetDepth; }
            set { targetDepth = value; }
        }

        /// <summary>
        /// Target acceleration of the object (in cm/s)
        /// The acceleration will move towards this target each update cycle
        /// </summary>
        public float TargetAcceleration {
            get { return targetAcceleration; }
            set { targetAcceleration = value; }
        }

        /// <summary>
        /// The surface area of the object
        /// </summary>
        public float SurfaceArea {
            get { return width * height * depth; }
        }

        /// <summary>
        /// The volume of the object (in cm^3)
        /// </summary>
        public float Volume {
            get { return width * height * depth; }
        }

        private float density;

        /// <summary>
        /// Density of the object
        /// </summary>
        public float Density {
            get { return (mass / Volume); }
            set { density = value; }
        }

        /// <summary>
        /// Projected area (in cm^2)
        /// </summary>
        public float ProjectedArea {
            get { return width * depth; }
        }

        private float distanceTravelled;

        /// <summary>
        /// The distance the object has travelled (in cm)
        /// </summary>
        public float DistanceTravelled {
            get { return distanceTravelled; }
            set { distanceTravelled = value; }
        }

        /// <summary>
        /// The center point of the object
        /// </summary>
        public Point Center {
            get { return new Point((float)position.X + width/2.0f,
                                    (float)position.Y - height / 2.0f);
            }
        }

        /// <summary>
        /// The top position of the object
        /// </summary>
        public float Top {
            get { return (float)position.Y; }
        }

        /// <summary>
        /// The outer left position of the object
        /// </summary>
        public float Left {
            get { return (float)position.X; }
        }

        /// <summary>
        /// The outer right position of the object
        /// </summary>
        public float Right {
            get { return (float)position.X + width; }
        }

        /// <summary>
        /// The bottom position of the object
        /// </summary>
        public float Bottom {
            get { return (float)position.Y - height; }
        }

        /// <summary>
        /// Drag coefficient of the object
        /// </summary>
        public float DragCoefficient {
            get {
                if (Shape == Shapes.Ellipse) {
                    return 0.47f;
                } else if (Shape == Shapes.Rect) {
                    return 1.05f;
                } else {
                    return 1.0f;
                }
            }
        }

        /// <summary>
        /// Rectangle representing the object
        /// </summary>
        public Rect Rectangle {
            get {
                Rect r = new Rect();
                r.Location = new System.Windows.Point(position.X, position.Y);
                r.Size = new System.Windows.Size(width, height);
                return r;
            }

        }

        /// <summary>
        /// Create new solid object
        /// </summary>
        public SolidObject() {
            position = new Point();
            velocity = new Vector();
            width = 1;
            height = 1;
            depth = 1;
            elasticity = 0;
            density = 1;
            targetWidth = -1;
            targetHeight = -1;
            targetDepth = -1;
            friction = 1.0f;
            fragility = 1000.0f;
            zIndex = 1;
            targetAcceleration = 1.3f;
            inflatable = false;
        }

        /// <summary>
        /// Create new solid object at position
        /// </summary>
        /// <param name="position">Position of object</param>
        public SolidObject(Point position)
            : this() {
            this.position = position;
            this.prevPosition = new Point(position.X, position.Y);
        }

        /// <summary>
        /// Create new solid object at position with width and height
        /// </summary>
        /// <param name="position">Position of object</param>
        /// <param name="width">Width of object</param>
        /// <param name="height">Height of object</param>
        public SolidObject(Point position, float width, float height)
            : this() {
            this.position = position;
            this.prevPosition = new Point(position.X, position.Y);
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Create new solid object at position with width, height and depth
        /// </summary>
        /// <param name="position">Position of object</param>
        /// <param name="width">Width of object</param>
        /// <param name="height">Height of object</param>
        /// <param name="depth">Depth of object</param>
        public SolidObject(Point position, float width, float height, float depth)
            : this() {
            this.position = position;
            this.prevPosition = new Point(position.X, position.Y);
            this.width = width;
            this.height = height;
            this.depth = depth;

        }

    }
}
