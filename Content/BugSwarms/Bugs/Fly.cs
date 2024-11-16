using Microsoft.Xna.Framework;
using Terraria;

namespace tRW.Content.BugSwarms.Bugs
{
    public class Fly
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
            speed = Main.rand.Next(3, 8);
            heading += Main.rand.NextFloat(MathHelper.ToRadians(-20), MathHelper.ToRadians(20));
            direction = heading.ToRotationVector2();
            velocity = direction * speed;
            Vector2 initPosVariation = Main.rand.NextVector2Circular(64, 64);
            flyPosition = position + initPosVariation;
        }

        public void Update(bool collisionCheck, int timer, Vector2 swarmPosition)
        {
            Vector2 influence = flyPosition.DirectionTo(swarmPosition) * (flyPosition.DistanceSQ(swarmPosition));
            if (collisionCheck)
            {
                heading += MathHelper.ToRadians(20);
                direction = heading.ToRotationVector2();
                velocity = influence * speed;
            }


            if (timer % 4 == 0)
            {
                heading += Main.rand.NextFloat(MathHelper.ToRadians(-50), MathHelper.ToRadians(50));
                direction = heading.ToRotationVector2();
                velocity = influence * speed;
            }

            if (timer % 15 == 0)
            {
                speed = Main.rand.Next(3, 7);
                velocity = influence * speed;
            }

            if (flyPosition.DistanceSQ(swarmPosition) > (256 * 156))
            {
                //direction = flyPosition.DirectionTo(swarmPosition);

                awayTimer++;

                //heading += MathHelper.ToRadians(5);
                //direction = heading.ToRotationVector2();
                //velocity = direction * speed;


                if (awayTimer >= 15)
                {
                    direction = flyPosition.DirectionTo(swarmPosition);
                    velocity = influence * speed;
                }
                else
                {
                    heading += MathHelper.ToRadians(5);
                    direction = heading.ToRotationVector2();
                    velocity = influence * speed;
                }
            }
            else
            {
                awayTimer = 0;
            }

            //for (int i = 0; i < oldPositions.Length; i++) oldPositions = new Vector2[5];

            
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
