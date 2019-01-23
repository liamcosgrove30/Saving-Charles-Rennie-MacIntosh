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
    class PlayerTwo : GameSprite
    {
        float friction2 = 0.85f;
        GamePadState currentButton2, prevButton2;
        public float playerHealth2 = 24;
        bool isPlayerDead2;

        public void Initialize(int x, int y, ContentManager content)
        {
            //character 1 bru
            AddAnimation(content.Load<Texture2D>("bruIdle"), 7, 500); //0
            AddAnimation(content.Load<Texture2D>("bruRunning"), 4, 400);
            AddAnimation(content.Load<Texture2D>("bruJumping"), 14, 400);
            AddAnimation(content.Load<Texture2D>("bruAttack"), 24, 400);
            AddAnimation(content.Load<Texture2D>("bruGunNew"), 10, 700);
            AddAnimation(content.Load<Texture2D>("bruDeathNew"), 19, 2000);

            //character 2 coo
            AddAnimation(content.Load<Texture2D>("cooIdle"), 7, 500); //6
            AddAnimation(content.Load<Texture2D>("cooRunning"), 4, 400);
            AddAnimation(content.Load<Texture2D>("cooJumping"), 14, 400);
            AddAnimation(content.Load<Texture2D>("cooAttackNew"), 24, 400);
            AddAnimation(content.Load<Texture2D>("cooGun"), 10, 700);
            AddAnimation(content.Load<Texture2D>("cooDeath"), 19, 2000);

            //character 3 nessie
            AddAnimation(content.Load<Texture2D>("nessieIdle"), 7, 500); //12
            AddAnimation(content.Load<Texture2D>("nessieRunning"), 4, 400);
            AddAnimation(content.Load<Texture2D>("nessieJumping"), 14, 400);
            AddAnimation(content.Load<Texture2D>("nessieAttack"), 24, 400);
            AddAnimation(content.Load<Texture2D>("nessieGun"), 10, 700);
            AddAnimation(content.Load<Texture2D>("nessieDeath"), 19, 2000);

            //character 4 tam
            AddAnimation(content.Load<Texture2D>("tamIdle"), 7, 500); //18
            AddAnimation(content.Load<Texture2D>("tamRunning"), 4, 400);
            AddAnimation(content.Load<Texture2D>("tamJumping"), 14, 400);
            AddAnimation(content.Load<Texture2D>("tamAttack"), 24, 400);
            AddAnimation(content.Load<Texture2D>("tamGun"), 10, 700);
            AddAnimation(content.Load<Texture2D>("tamDeath"), 19, 2000);

            //character 4 thistle
            AddAnimation(content.Load<Texture2D>("thistleIdle1"), 7, 500); //24
            AddAnimation(content.Load<Texture2D>("thistleRunning1"), 4, 400);
            AddAnimation(content.Load<Texture2D>("thistleJumping1"), 14, 400);
            AddAnimation(content.Load<Texture2D>("thistleAttack1"), 24, 400);
            AddAnimation(content.Load<Texture2D>("thistleGunNew"), 10, 700);
            AddAnimation(content.Load<Texture2D>("thistleDeath1"), 19, 2000);

            AddAnimation(content.Load<Texture2D>("bruFlying"), 13, 1500); //30
            AddAnimation(content.Load<Texture2D>("cooFlying"), 13, 1500);
            AddAnimation(content.Load<Texture2D>("nessieFlying"), 13, 1500);
            AddAnimation(content.Load<Texture2D>("tamFlying"), 13, 1500);
            AddAnimation(content.Load<Texture2D>("thistleFlying"), 13, 1500);

            position = new Vector2(x, y);
        }

        public void Update(int milliseconds, int characterChoice2, bool isPlayerAttacking2, bool isPlayerShootingRight2, int energyNumber2, bool flySwitch2)
        {
            if (playerHealth2 <= 0) isPlayerDead2 = true;
            else
                isPlayerDead2 = false;

            currentButton2 = GamePad.GetState(PlayerIndex.Two);

            base.Update(milliseconds);

            if (onGround && currentButton2.IsButtonUp(Buttons.A)) //player 2 is standing still and is on the ground 
            {
                if (characterChoice2 == 0) currentAnimation = 0; //character 1 standing still
                if (characterChoice2 == 1) currentAnimation = 6; //character 2 standing still
                if (characterChoice2 == 2) currentAnimation = 12; //character 3 standing still
                if (characterChoice2 == 3) currentAnimation = 18; //character 4 standing still
                if (characterChoice2 == 4) currentAnimation = 24; //character 5 standing still



            }


            if (currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) && isPlayerDead2 == false || //stick movement or dpad
                currentButton2.IsButtonDown(Buttons.DPadLeft) && isPlayerDead2 == false)
            {
                if (onGround && characterChoice2 == 0)
                {
                    currentAnimation = 1; //character 1 running 
                }

                if (onGround && characterChoice2 == 1)
                {
                    currentAnimation = 7; //character 2 running
                }

                if (onGround && characterChoice2 == 2)
                {
                    currentAnimation = 13; //character 3 running
                }

                if (onGround && characterChoice2 == 3)
                {
                    currentAnimation = 19; //character 4 running
                }

                if (onGround && characterChoice2 == 4)
                {
                    currentAnimation = 25; //character 5 running
                }

                velocity.X -= 1.5f; //speed the player moves at
                flipHorizontally = true; //player will move to the left
                isPlayerShootingRight2 = true;
            }


            if (currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) && isPlayerDead2 == false || //stick movement or dpad
                currentButton2.IsButtonDown(Buttons.DPadRight) && isPlayerDead2 == false)
            {
                if (onGround && characterChoice2 == 0)
                    currentAnimation = 1; //character 1 running 
                if (onGround && characterChoice2 == 1)
                    currentAnimation = 7; //character 2 running
                if (onGround && characterChoice2 == 2)
                    currentAnimation = 13; //character 3 running
                if (onGround && characterChoice2 == 3)
                    currentAnimation = 19; //character 4 running
                if (onGround && characterChoice2 == 4)
                    currentAnimation = 25; //character 5 running

                velocity.X += 1.5f; //speed the player runs at
                flipHorizontally = false; //player will move to the right 
                isPlayerShootingRight2 = false;
            }


            if (currentButton2.IsButtonDown(Buttons.A) && onGround && isPlayerDead2 == false) //a button being pressed on the controller
            {
                if (characterChoice2 == 0) currentAnimation = 2; //character 1 jump animation
                if (characterChoice2 == 1) currentAnimation = 8; //character 2 jump animation
                if (characterChoice2 == 2) currentAnimation = 14; //character 3 jump animation
                if (characterChoice2 == 3) currentAnimation = 20; //character 4 jump animation
                if (characterChoice2 == 4) currentAnimation = 26; //character 5 jump animation               
                velocity.Y = -100; //how high the player jumps              
                onGround = false; //brings the player back down after a jump
            }

            if (currentButton2.IsButtonDown(Buttons.X) && isPlayerDead2 == false && energyNumber2 > 0)
            {
                if (characterChoice2 == 0) currentAnimation = 3;
                if (characterChoice2 == 1) currentAnimation = 9;
                if (characterChoice2 == 2) currentAnimation = 15;
                if (characterChoice2 == 3) currentAnimation = 21;
                if (characterChoice2 == 4) currentAnimation = 27;

                isPlayerAttacking2 = true;

                if (onGround)
                    velocity.X = 0;
            }
            else isPlayerAttacking2 = false;


            if (currentButton2.IsButtonDown(Buttons.B) && isPlayerDead2 == false)
            {
                if (characterChoice2 == 0) currentAnimation = 4;
                if (characterChoice2 == 1) currentAnimation = 10;
                if (characterChoice2 == 2) currentAnimation = 16;
                if (characterChoice2 == 3) currentAnimation = 22;
                if (characterChoice2 == 4) currentAnimation = 28;

                if (onGround)
                    velocity.X = 0;
            }


            if (onGround == false && characterChoice2 == 0) currentAnimation = 2; //if the player is in the air and character 1 has been selected
            if (onGround == false && characterChoice2 == 1) currentAnimation = 8; //if the player is in the air and character 2 has been selected
            if (onGround == false && characterChoice2 == 2) currentAnimation = 14; //if the player is in the air and character 3 has been selected
            if (onGround == false && characterChoice2 == 3) currentAnimation = 20; //if the player is in the air and character 4 has been selected
            if (onGround == false && characterChoice2 == 4) currentAnimation = 26; //if the player is in the air and character 5 has been selected
            velocity *= friction2;


            if (playerHealth2 <= 0 && onGround)
            {
                if (characterChoice2 == 0) currentAnimation = 5; //if the player dies and character 1 has been chosen 
                if (characterChoice2 == 1) currentAnimation = 11; //if the player dies and character 2 has been chosen 
                if (characterChoice2 == 2) currentAnimation = 17; //if the player dies and character 3 has been chosen 
                if (characterChoice2 == 3) currentAnimation = 23; //if the player dies and character 3 has been chosen 
                if (characterChoice2 == 4) currentAnimation = 29; //if the player dies and character 4 has been chosen 

                velocity.X = 0;
            }

            if (flySwitch2)
            {
                if (characterChoice2 == 0) currentAnimation = 30;
                if (characterChoice2 == 1) currentAnimation = 31;
                if (characterChoice2 == 2) currentAnimation = 32;
                if (characterChoice2 == 3) currentAnimation = 33;
                if (characterChoice2 == 4) currentAnimation = 34;

                velocity.Y = -2.5f;

                if (currentButton2.IsButtonDown(Buttons.LeftThumbstickUp) || currentButton2.IsButtonDown(Buttons.DPadUp)) velocity.Y = -5;
                if (currentButton2.IsButtonDown(Buttons.LeftThumbstickDown) || currentButton2.IsButtonDown(Buttons.DPadDown)) velocity.Y = 0;

            }

            //player 1 controller
            prevButton2 = currentButton2;
        }
    }
}
