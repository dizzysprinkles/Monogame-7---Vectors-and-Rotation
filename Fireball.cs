using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_7___Vectors_and_Rotation
{
    public class Fireball
    {
        private Texture2D _texture;
        private Rectangle _rect;
        private Vector2 _location;
        private Vector2 _direction;
        private float _speed;
        private int _size;

		// Constructor to create a fireball
        public Fireball(Texture2D texture, Vector2 location, Vector2 target, int size)
		{
            _texture = texture;
            _location = location;
            _size = size;
            _rect = new Rectangle(location.ToPoint(), new Point(_size, _size));
            _direction = target - location;   // Determines the direction it's facing (pointing from location to target)
            _direction.Normalize(); // Converts to a unit vector!
            _speed = 2f;
		}


        // Allows read access to the location Rectangle for collision detection
        public Rectangle Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public void Update()
        {
            _location += _direction * _speed;
            _rect.Location = _location.ToPoint();

            //if (_rect.Size != Point.Zero)
            //{
            //    _rect.Size -= new Point(1, 1);
            //}
            //else
            //{
            //    _rect.Size = Point.Zero;
            //}


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.White);
        }
    }
}
