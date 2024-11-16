using Microsoft.Xna.Framework;
using Terraria;

namespace tRW.Content.BugSwarms.Bugs
{
    public class FireFly
    {
        public float heading;
        public int speed;
        public Vector2 flyPosition;
        public Vector2 prevPosition;
        public Vector2 velocity;
        public Vector2 direction;

        public int awayTimer;

        public int oldPosLength = 3;
        public Vector2[] oldPositions = new Vector2[3];

        public void SetInitials(Vector2 position)
        {
            speed = Main.rand.Next(2, 4);
            heading += Main.rand.NextFloat(MathHelper.ToRadians(-20), MathHelper.ToRadians(20));
            direction = heading.ToRotationVector2();
            velocity = direction * speed;
            Vector2 initPosVariation = Main.rand.NextVector2Circular(64, 64);
            flyPosition = position + initPosVariation;
        }

        public void Update(bool collisionCheck, int timer, Vector2 swarmPosition)
        {
            if (collisionCheck)
            {
                //heading += (headingDir == 0) ? MathHelper.ToRadians(-20) : MathHelper.ToRadians(20);
                heading += MathHelper.ToRadians(20);
                direction = heading.ToRotationVector2();
                velocity = direction * speed;
            }


            if (timer % 4 == 0)
            {
                heading += Main.rand.NextFloat(MathHelper.ToRadians(-20), MathHelper.ToRadians(20));
                direction = heading.ToRotationVector2();
                velocity = direction * speed;
            }

            if (timer % 60 == 0)
            {
                heading += Main.rand.NextFloat(MathHelper.ToRadians(-40), MathHelper.ToRadians(40));
                direction = heading.ToRotationVector2();
                speed = Main.rand.Next(2, 4);
                velocity = direction * speed;
            }

            if (flyPosition.DistanceSQ(swarmPosition) > (256 * 256))
            {
                //direction = flyPosition.DirectionTo(swarmPosition);

                awayTimer++;

                //heading += MathHelper.ToRadians(5);
                //direction = heading.ToRotationVector2();
                //velocity = direction * speed;


                if (awayTimer >= 30)
                {
                    direction = flyPosition.DirectionTo(swarmPosition);
                    velocity = direction * 3;
                }
                else
                {
                    heading += MathHelper.ToRadians(5);
                    direction = heading.ToRotationVector2();
                    velocity = direction * speed;
                }
            }
            else
            {
                awayTimer = 0;
            }

            //for (int i = 0; i < oldPositions.Length; i++) oldPositions = new Vector2[5];

            Lighting.AddLight(flyPosition, new Vector3(0.4f, 0.4f, 0));
        }

        public void UpdatePosition()
        {
            flyPosition += velocity;
        }

        public void UpdateOldPositions()
        {
            for (int i = oldPositions.Length - 1; i > 0; i--)
            {
                oldPositions[i] = oldPositions[i - 1];
            }
            oldPositions[0] = flyPosition;
        }
    }
}
