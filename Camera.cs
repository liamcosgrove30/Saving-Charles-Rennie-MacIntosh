using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavingCharlesRennieMacintosh
{
    class Camera
    {
        public Vector2 cameraPos;
        public Rectangle viewArea;
        public float rotation = 0;
        public float scale = 1f;

        public Camera(Vector2 pos, Rectangle view)
        {
            cameraPos = pos;
            viewArea = view;
        }

        public Matrix getMatrix()
        {
            Matrix mat = Matrix.Identity;

            // move camera back to origin to perform rotation and scale
            Vector3 translation = new Vector3(-cameraPos, 0);
            mat *= Matrix.CreateTranslation(translation);
            mat *= Matrix.CreateRotationZ(rotation);
            mat *= Matrix.CreateScale(scale);

            // return camera to middle of view area
            translation.X = viewArea.Width / 2;
            translation.Y = viewArea.Height / 2;
            mat *= Matrix.CreateTranslation(translation);

            return mat;
        }
    }
}
