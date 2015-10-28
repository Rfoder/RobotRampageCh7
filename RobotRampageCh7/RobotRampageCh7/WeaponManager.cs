using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RobotRampageCh7
{
    static class WeaponManager
    {
        //pg. 228
        #region declarations
        static public List<Particle> Shots = new List<Particle>();
        static public Texture2D Texture;
        static public Rectangle shotRectangle =
            new Rectangle(0, 128, 32, 32);
        static public float WeaponSpeed = 600f;

        static private float shotTimer = 0f;
        static private float shotMinTimer = 0.15f;

        //pg. 233
        static private float rocketMinTimer = 0.5f;

        public enum WeaponType { Normal, Triple, Rocket };
        static public WeaponType CurrentWeaponType = WeaponType.Triple;
        static public float WeaponTimeRemaing = 30.0f;
        static private float weapomTimeDefault = 30.0f;
        static private float tripleWeaponSplitAngle = 15;

        #endregion

        #region Properties
        static public float WeaponFireDelay
        {
            get
            {
                if (CurrentWeaponType == WeaponType.Rocket)
                {
                    return rocketMinTimer;
                }
                else
                {
                    return shotMinTimer;
                }
            }
        }
            static public bool CanFireWeapon
            {
                get
                {
                    return (shotTimer >= WeaponFireDelay);
                }
        }
        #endregion

        //pg. 229
        #region Effects Management Methods

        private static void AddShot(
            Vector2 location,
            Vector2 velocity,
            int frame)
        {
            Particle shot = new Particle(
                location,
                Texture,
                shotRectangle,
                velocity,
                Vector2.Zero,
                400f,
                120,
                Color.White,
                Color.White);

            shot.AddFrame(new Rectangle(
                shotRectangle.X + shotRectangle.Width,
                shotRectangle.Y,
                shotRectangle.Width,
                shotRectangle.Height));

            shot.Animate = false;
            shot.Frame = frame;
            shot.RotateTo(velocity);
            Shots.Add(shot);
        }
        #endregion

        
        #region Weapons Management Methods
       //pg. 234
        private static void checkWeaponUpgradeExpire(float elapsed)
        {
            if (CurrentWeaponType != WeaponType.Normal)
            {
                //pg.235
                WeaponTimeRemaing -= 0.0f;
                if (WeaponTimeRemaing <= 0)
                {
                    CurrentWeaponType = WeaponType.Normal;
                }
            }
        }
        //pg. 230
        public static void FireWeapon(Vector2 location, Vector2 velocity)
        {
            switch (CurrentWeaponType)
            {
                case WeaponType.Normal:
                    AddShot(location, velocity, 0);
                    break;

                case WeaponType.Triple:
                    AddShot(location, velocity, 0);

                    float baseAngle = (float)Math.Atan2(
                        velocity.Y,
                        velocity.X);
                    float offset = MathHelper.ToRadians(
                        tripleWeaponSplitAngle);
                    AddShot(
                        location,
                        new Vector2(
                            (float)Math.Cos(baseAngle - offset),
                            (float)Math.Sin(baseAngle - offset)
            ) * velocity.Length(),
            0);
                    AddShot(
                        location,
                        new Vector2(
                            (float)Math.Cos(baseAngle + offset),
                            (float)Math.Sin(baseAngle + offset)
                            ) * velocity.Length(),
                            0);
                    break;

                case WeaponType.Rocket:
                    AddShot(location, velocity, 1);
                    break;
            }
            shotTimer = 0.0f;
        }
        #endregion

        #region Update and Draw
        static public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shotTimer += elapsed;
            //pg. 235
            checkWeaponUpgradeExpire(elapsed);

            for (int x = Shots.Count - 1; x >= 0; x--)
            {
                Shots[x].Update(gameTime);
                if (Shots[x].Expired)
                {
                    Shots.RemoveAt(x);
                }
            }
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle sprite in Shots)
            {
                sprite.Draw(spriteBatch);
            }
        }
        #endregion

      
    }
}
