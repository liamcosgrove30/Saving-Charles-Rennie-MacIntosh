using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavingCharlesRennieMacintosh
{
    class PlayerFlying2 : GameSprite
    {
        
        float friction = 0.85f;
        GamePadState currentButton2, prevButton2;
        public float playerFlyingHealth2 = 2;

        public void Initialize(int x, int y, ContentManager content)
        {
            AddAnimation(content.Load<Texture2D>("bruFlyingLevel2"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("cooFlyingLevel2"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("nessieFlyingLevel2"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("tamFlyingLevel2"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("thistleFlyingLevel2"), 13, 1000);

            AddAnimation(content.Load<Texture2D>("bruFlyingAttack"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("cooFlyingAttack"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("nessieFlyingAttack"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("tamFlyingAttack"), 13, 1000);
            AddAnimation(content.Load<Texture2D>("thistleFlyingAttack"), 13, 1000);

            position = new Vector2(x, y);

        }

        public void Update(int milliseconds, int characterChoice2)
        {
            base.Update(milliseconds);
            currentButton2 = GamePad.GetState(PlayerIndex.Two);

            velocity *= friction;

            //character movements 
            if (currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) || currentButton2.IsButtonDown(Buttons.DPadRight))
            {
                if (playerFlyingHealth2 > 0)
                {
                    velocity.X += 1.5f;
                    flipHorizontally = false;
                }

                if (characterChoice2 == 0) currentAnimation = 0;
                if (characterChoice2 == 1) currentAnimation = 1;
                if (characterChoice2 == 2) currentAnimation = 2;
                if (characterChoice2 == 3) currentAnimation = 3;
                if (characterChoice2 == 4) currentAnimation = 4;


            }

            if (currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) || currentButton2.IsButtonDown(Buttons.DPadLeft))
            {
                if (playerFlyingHealth2 > 0)
                {
                    velocity.X -= 1.5f;
                    flipHorizontally = true;
                }

                if (characterChoice2 == 0) currentAnimation = 0;
                if (characterChoice2 == 1) currentAnimation = 1;
                if (characterChoice2 == 2) currentAnimation = 2;
                if (characterChoice2 == 3) currentAnimation = 3;
                if (characterChoice2 == 4) currentAnimation = 4;

            }

            if (currentButton2.IsButtonDown(Buttons.LeftThumbstickDown) || currentButton2.IsButtonDown(Buttons.DPadDown))
            {
                if (playerFlyingHealth2 > 0)
                {
                    velocity.Y += 1.5f;
                }

                if (characterChoice2 == 0) currentAnimation = 0;
                if (characterChoice2 == 1) currentAnimation = 1;
                if (characterChoice2 == 2) currentAnimation = 2;
                if (characterChoice2 == 3) currentAnimation = 3;
                if (characterChoice2 == 4) currentAnimation = 4;

            }

            if (currentButton2.IsButtonDown(Buttons.LeftThumbstickUp) || currentButton2.IsButtonDown(Buttons.DPadUp))
            {
                if (playerFlyingHealth2 > 0)
                {
                    velocity.Y -= 1.5f;
                }

                if (characterChoice2 == 0) currentAnimation = 0;
                if (characterChoice2 == 1) currentAnimation = 1;
                if (characterChoice2 == 2) currentAnimation = 2;
                if (characterChoice2 == 3) currentAnimation = 3;
                if (characterChoice2 == 4) currentAnimation = 4;

            }

            //character attacking 

            if (currentButton2.IsButtonDown(Buttons.B) && flipHorizontally == false)
            {
                if (characterChoice2 == 0) currentAnimation = 5;
                if (characterChoice2 == 1) currentAnimation = 6;
                if (characterChoice2 == 2) currentAnimation = 7;
                if (characterChoice2 == 3) currentAnimation = 8;
                if (characterChoice2 == 4) currentAnimation = 9;


            }
            else
            {
                if (characterChoice2 == 0) currentAnimation = 0;
                if (characterChoice2 == 1) currentAnimation = 1;
                if (characterChoice2 == 2) currentAnimation = 2;
                if (characterChoice2 == 3) currentAnimation = 3;
                if (characterChoice2 == 4) currentAnimation = 4;

            }

            //the player has died 
            if (playerFlyingHealth2 <= 0) position.Y += 7;

            prevButton2 = currentButton2;
        }

    }
}
    

