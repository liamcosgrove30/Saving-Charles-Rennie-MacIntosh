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
    class PlayerFlying : GameSprite
    {
        float friction = 0.85f;
        GamePadState currentButton, prevButton;
        public float playerFlyingHealth = 24;

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

        public void Update(int milliseconds, int characterChoice)
        {
            base.Update(milliseconds);
            currentButton = GamePad.GetState(PlayerIndex.One);
          
            velocity *= friction;

            //character movements 
            if (currentButton.IsButtonDown(Buttons.LeftThumbstickRight) || currentButton.IsButtonDown(Buttons.DPadRight))
            {
                if (playerFlyingHealth > 0)
                {
                    velocity.X += 1.5f;
                    flipHorizontally = false;
                }

                if (characterChoice == 0) currentAnimation = 0;
                if (characterChoice == 1) currentAnimation = 1;
                if (characterChoice == 2) currentAnimation = 2;
                if (characterChoice == 3) currentAnimation = 3;
                if (characterChoice == 4) currentAnimation = 4;


            }

            if (currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) || currentButton.IsButtonDown(Buttons.DPadLeft))
            {
                if (playerFlyingHealth > 0)
                {
                    velocity.X -= 1.5f;
                    flipHorizontally = true;
                }

                if (characterChoice == 0) currentAnimation = 0;
                if (characterChoice == 1) currentAnimation = 1;
                if (characterChoice == 2) currentAnimation = 2;
                if (characterChoice == 3) currentAnimation = 3;
                if (characterChoice == 4) currentAnimation = 4;

            }

            if (currentButton.IsButtonDown(Buttons.LeftThumbstickDown) || currentButton.IsButtonDown(Buttons.DPadDown))
            {
                if (playerFlyingHealth > 0)
                {
                    velocity.Y += 1.5f;
                }

                if (characterChoice == 0) currentAnimation = 0;
                if (characterChoice == 1) currentAnimation = 1;
                if (characterChoice == 2) currentAnimation = 2;
                if (characterChoice == 3) currentAnimation = 3;
                if (characterChoice == 4) currentAnimation = 4;

            }

            if (currentButton.IsButtonDown(Buttons.LeftThumbstickUp) || currentButton.IsButtonDown(Buttons.DPadUp))
            {
                if (playerFlyingHealth > 0)
                {
                    velocity.Y -= 1.5f;
                }

                if (characterChoice == 0) currentAnimation = 0;
                if (characterChoice == 1) currentAnimation = 1;
                if (characterChoice == 2) currentAnimation = 2;
                if (characterChoice == 3) currentAnimation = 3;
                if (characterChoice == 4) currentAnimation = 4;

            }

            //character attacking 

            if (currentButton.IsButtonDown(Buttons.B) && flipHorizontally == false)
            {
                if (characterChoice == 0) currentAnimation = 5;
                if (characterChoice == 1) currentAnimation = 6;
                if (characterChoice == 2) currentAnimation = 7;
                if (characterChoice == 3) currentAnimation = 8;
                if (characterChoice == 4) currentAnimation = 9;

               
            }
            else
            {
                if (characterChoice == 0) currentAnimation = 0;
                if (characterChoice == 1) currentAnimation = 1;
                if (characterChoice == 2) currentAnimation = 2;
                if (characterChoice == 3) currentAnimation = 3;
                if (characterChoice == 4) currentAnimation = 4;

            }

            //the player has died 
            if (playerFlyingHealth <= 0) position.Y += 7;

            prevButton = currentButton;
        }
        
    }
}
