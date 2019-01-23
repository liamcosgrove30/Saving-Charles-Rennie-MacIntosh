using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavingCharlesRennieMacintosh
{
    class Boss : GameSprite
    {
        public bool ySwitch = false;
        public bool xSwitch = false;
        public bool bossIdle = true;
        public bool bossAttack = false;
        public bool bossMelee = false;
        public bool bossGun = false;
        Random rng = new Random();


        public void Initialize(int x, int y, ContentManager content)
        {
            AddAnimation(content.Load<Texture2D>("bossFlyingNormal"), 23, 500);
            AddAnimation(content.Load<Texture2D>("bossFlyingMeleeAttack"), 23, 400);
            AddAnimation(content.Load<Texture2D>("bossFlyingBulletAttack"), 23, 400);
            AddAnimation(content.Load<Texture2D>("bossFlyingDeath"), 23, 1000);


            position = new Vector2(x, y);
        }

        public void Update(int milliseconds, int bossHealth, bool playerHit, bool playerHit2)
        {
            base.Update(milliseconds);

            if (bossHealth <= 0) //the boss dies 
            {
                currentAnimation = 3;
                position.Y += 5;
            }

            if (bossHealth > 0)
            {
                //boss not attacking 
                if (bossIdle && !bossAttack)
                {
                    currentAnimation = 0;

                    if (position.X == 1500)
                    {


                        if (ySwitch) position.Y -= 3;
                        if (!ySwitch) position.Y += 3;
                        if (position.Y > 680) ySwitch = true;
                        if (position.Y < 0) ySwitch = false;
                    }

                    if (position.X > 1500) position.X -= 2;

                    //boss attack 
                    if (rng.Next(0, 1000) < 10f && position.X <= 1502)
                    {
                        bossAttack = true;
                    }

                }










                //decides what attack to do
                if (bossAttack)
                {

                    if (rng.Next(0, 10) < 5)
                    {
                        bossMelee = true;

                        bossIdle = false;
                        bossAttack = false;
                    }
                    else
                    {
                        bossGun = true;

                        bossIdle = false;
                        bossAttack = false;
                    }




                }

                //if the boss attacks
                if (bossMelee)
                {
                    currentAnimation = 1;

                    if (!xSwitch) position.X -= 10;

                    if (position.X < 0 || playerHit || playerHit2) xSwitch = true;

                    if (xSwitch) position.X += 10;

                    if (position.X >= 1510)
                    {
                        bossMelee = false;
                        bossAttack = false;
                        xSwitch = false;
                        playerHit = false;
                        bossIdle = true;
                    }
                }

                //boss throwing a bullet
                if (bossGun)
                {
                    currentAnimation = 2;

                    if (animations[currentAnimation].currentAnimationFrame == 22)
                    {
                        bossGun = false;
                        bossAttack = false;
                        bossIdle = true;
                    }
                }

            }

        }


    }
}
