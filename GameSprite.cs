using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavingCharlesRennieMacintosh
{
    class GameSprite
    {
        public Texture2D image;
        public Vector2 position;
        public Vector2 velocity;
        public bool flipHorizontally = false;
        public List<Animation> animations = new List<Animation>();
        public int currentAnimation;
        public bool onGround = false;
        protected Vector2 previousPos;
        public Rectangle collisionArea;
        public GameSprite groundUnderThis;
        public Vector2 origin;

        public void AddAnimation(Texture2D spriteSheet,
            int frames, int length)
        {
            Animation animToBeAdded =
                new Animation(spriteSheet, frames, length);
            animations.Add(animToBeAdded);
            if (collisionArea.Width == 0)
            {
                int sourceWidth =
                    animations[currentAnimation].sheet.Width /
                    animations[currentAnimation].numberOfFrames;
                collisionArea = new Rectangle(0, 0, sourceWidth, animations[currentAnimation].sheet.Height);
            }
        }

        public virtual void Update(int milliseconds)
        {
            previousPos = position;
            if (currentAnimation < 0) return;
            if (currentAnimation + 1 >
                animations.Count())
                return; // pre-validation
            animations[currentAnimation].Update(milliseconds);
            position += velocity;
        }

        public void Draw(SpriteBatch batch)
        {
            if (currentAnimation < 0) return;
            if (currentAnimation + 1 >
                animations.Count())
                return; // pre-validation
            SpriteEffects toFliporNot = SpriteEffects.None;
            if (flipHorizontally) toFliporNot = SpriteEffects.FlipHorizontally;
            animations[currentAnimation].Draw(batch, position, toFliporNot);

            
        }


        public bool collision(GameSprite target)
        {
            if (target == null) return false;
            if (currentAnimation + 1 > animations.Count()) return false; // pre-validation
            Rectangle sourceRectangle = new Rectangle(
                (int)position.X, (int)position.Y,
                collisionArea.Width, collisionArea.Height);
            if (target.currentAnimation + 1 > target.animations.Count())
                return false;   // pre-validate the target's animation
            Rectangle targetRectangle = new Rectangle(
                (int)target.position.X, (int)target.position.Y,
                target.collisionArea.Width,
                target.collisionArea.Height);
            return (sourceRectangle.Intersects(targetRectangle));
        }



        public void moveBackToEdge(GameSprite target)
        {
            if (currentAnimation + 1 > animations.Count()) return; // pre-validate the animation
            // make a rectangle around where you USED to be, so you can detect where you hit the object from
            Rectangle prev = new Rectangle((int)previousPos.X, (int)previousPos.Y, collisionArea.Width, collisionArea.Height);
            if (target.currentAnimation + 1 > target.animations.Count()) return; // more pre-validation
            Rectangle targRec = new Rectangle(
                (int)target.position.X, (int)target.position.Y, target.collisionArea.Width, target.collisionArea.Height);
            if (prev.Bottom <= targRec.Top)
            {   // hit from above
                position.Y = targRec.Top - prev.Height;
                velocity.Y = 0;
                onGround = true;
                groundUnderThis = target;
                return;
            }
            if (prev.Right <= targRec.Left)
            {   // hit from left
                position.X = targRec.Left - collisionArea.Width;
                velocity.X = 0;
            }

            if (prev.Left >= targRec.Right)
            {   // hit from right
                position.X = targRec.Right + 1;
                velocity.X = 0;
            }
            if (prev.Top >= targRec.Bottom)
            {   // hit from below
                position.Y = targRec.Bottom;
                velocity.Y = 0;
            }

        }
    }

    public static class sfunctions2d
    {

        public static char getnextkey()
        {
            // Read keyboard
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.A))
                return 'A';
            else if (keys.IsKeyDown(Keys.B))
                return 'B';
            else if (keys.IsKeyDown(Keys.C))
                return 'C';
            else if (keys.IsKeyDown(Keys.D))
                return 'D';
            else if (keys.IsKeyDown(Keys.E))
                return 'E';
            else if (keys.IsKeyDown(Keys.F))
                return 'F';
            else if (keys.IsKeyDown(Keys.G))
                return 'G';
            else if (keys.IsKeyDown(Keys.H))
                return 'H';
            else if (keys.IsKeyDown(Keys.I))
                return 'I';
            else if (keys.IsKeyDown(Keys.J))
                return 'J';
            else if (keys.IsKeyDown(Keys.K))
                return 'K';
            else if (keys.IsKeyDown(Keys.L))
                return 'L';
            else if (keys.IsKeyDown(Keys.M))
                return 'M';
            else if (keys.IsKeyDown(Keys.N))
                return 'N';
            else if (keys.IsKeyDown(Keys.O))
                return 'O';
            else if (keys.IsKeyDown(Keys.P))
                return 'P';
            else if (keys.IsKeyDown(Keys.Q))
                return 'Q';
            else if (keys.IsKeyDown(Keys.R))
                return 'R';
            else if (keys.IsKeyDown(Keys.S))
                return 'S';
            else if (keys.IsKeyDown(Keys.T))
                return 'T';
            else if (keys.IsKeyDown(Keys.U))
                return 'U';
            else if (keys.IsKeyDown(Keys.V))
                return 'V';
            else if (keys.IsKeyDown(Keys.W))
                return 'W';
            else if (keys.IsKeyDown(Keys.X))
                return 'X';
            else if (keys.IsKeyDown(Keys.Y))
                return 'Y';
            else if (keys.IsKeyDown(Keys.Z))
                return 'Z';
            else if (keys.IsKeyDown(Keys.D0))
                return '0';
            else if (keys.IsKeyDown(Keys.D1))
                return '1';
            else if (keys.IsKeyDown(Keys.D2))
                return '2';
            else if (keys.IsKeyDown(Keys.D3))
                return '3';
            else if (keys.IsKeyDown(Keys.D4))
                return '4';
            else if (keys.IsKeyDown(Keys.D5))
                return '5';
            else if (keys.IsKeyDown(Keys.D6))
                return '6';
            else if (keys.IsKeyDown(Keys.D7))
                return '7';
            else if (keys.IsKeyDown(Keys.D8))
                return '8';
            else if (keys.IsKeyDown(Keys.D9))
                return '9';
            else if (keys.IsKeyDown(Keys.Space))
                return ' ';
            else
                return '!';
        }
    }
}

