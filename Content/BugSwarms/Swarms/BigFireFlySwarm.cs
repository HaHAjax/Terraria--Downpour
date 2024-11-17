using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using tRW.Content.BugSwarms.Bugs;

namespace tRW.Content.BugSwarms.Swarms;

public class BigFireFlySwarm : ModNPC
{
    public override string Texture => "tRW/Assets/NPCs/FlySwarm";

    private readonly int maxFireFlies = 30;
    private readonly FireFly[] fireFlies = new FireFly[30];

    public override void SetDefaults()
    {
        NPC.noGravity = true;

        NPC.chaseable = false;
        NPC.npcSlots = 5;
        NPC.damage = 0;
        NPC.lifeMax = 5;
        NPC.dontTakeDamage = true;
        NPC.knockBackResist = 0;
        NPC.Hitbox = new Rectangle(0, 0, 16, 16);

        NPC.Opacity = 0;

        //base.SetDefaults();
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (!Main.dayTime && (spawnInfo.Player.ZoneForest || spawnInfo.Player.ZoneNormalCaverns || spawnInfo.Player.ZoneGemCave))
        {
            return SpawnCondition.Overworld.Chance * 0.1f;
        }
        else
        {
            return 0f;
        }

        //return base.SpawnChance(spawnInfo);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (fireFlies != null)
        {
            for (int i = 0; i < fireFlies.Length; i++)
            {
                // Update the drawing for the flies
                Texture2D baseTexture = ModContent.Request<Texture2D>("tRW/Assets/NPCs/FireFly", AssetRequestMode.ImmediateLoad).Value;

                Vector2 drawOrigin = baseTexture.Size() / 2;
                for (int k = 0; k < fireFlies[i].oldPositions.Length; k++)
                {
                    // Shoutout to jioumu (IronTristonia) on the tModLoader Discord for making the fireflies look much smoother!!!!
                    Vector2 drawPos = fireFlies[i].oldPositions[k] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                    Color color = new(255, 255, 0, 255);
                    Vector2 scale = new((fireFlies[i].flyPosition - fireFlies[i].oldPositions[k]).Length() / baseTexture.Width, 0.225f);
                    float rotation = (fireFlies[i].flyPosition - fireFlies[i].oldPositions[k]).ToRotation();
                    Main.EntitySpriteDraw(baseTexture, drawPos, null, color, rotation, drawOrigin, scale, SpriteEffects.None, 0);
                }
            }
        }

        return true;
        //return base.PreDraw(spriteBatch, screenPos, drawColor);
    }

    public int Timer
    {
        get => (int)NPC.ai[1];
        set => NPC.ai[1] = value;
    }

    public override void AI()
    {
        Timer++;

        //Vector2 fliesPosition; // For debugging
        //Vector2 fliesVelocity; // For debugging
        //Vector2 fliesHeadingToV2; // For debugging

        for (int i = 0; i < maxFireFlies; i++)
        {
            //fliesVelocity[i] = flies[i].velocity;

            if (fireFlies[i] == null)
            {
                fireFlies[i] = new FireFly();
            }

            if (fireFlies[i].velocity == Vector2.Zero)
            {
                fireFlies[i].SetInitials(NPC.position);

                //fliesPosition = flies[i].flyPosition;
                //fliesVelocity[i] = flies[i].velocity;
            }

            fireFlies[i].Update(!Collision.CanHitLine(fireFlies[i].flyPosition, 1, 1, fireFlies[i].direction * 4 * 16 + fireFlies[i].flyPosition, 32, 32), Timer, NPC.position);
            fireFlies[i].UpdatePosition();
            fireFlies[i].UpdateOldPositions();
            //fliesPosition = flies[i].flyPosition;
            //fliesHeadingToV2 = flies[i].heading.ToRotationVector2();
            //fliesVelocity = flies[i].velocity;
        }

        //base.AI();
    }
}
