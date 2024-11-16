using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using tRW.Content.BugSwarms.Bugs;

namespace tRW.Content.BugSwarms.Swarms;

public class BigFlySwarm : ModNPC
{
    public override string Texture => "tRW/Assets/NPCs/FlySwarm";

    private readonly int maxFlies = 85;
    private readonly Fly[] flies = new Fly[85];

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
        if (Main.dayTime && spawnInfo.Player.ZoneForest)
        {
            return SpawnCondition.Overworld.Chance * 0.1f;
        }
        else
        {
            return 0f;
        }
        

        //return base.SpawnChance(spawnInfo);
    }

    public override void OnSpawn(IEntitySource source)
    {


        base.OnSpawn(source);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        for (int i = 0; i < flies.Length; i++)
        {
            // Update the drawing for the flies
            Texture2D baseTexture = ModContent.Request<Texture2D>("tRW/Assets/NPCs/Fly", AssetRequestMode.ImmediateLoad).Value;

            Vector2 drawOrigin = baseTexture.Size() / 2;
            for (int k = 0; k < flies[i].oldPositions.Length; k++)
            {
                // Shoutout to jioumu (IronTristonia) on the tModLoader Discord for making the flies look much smoother!!!!
                Vector2 drawPos = flies[i].oldPositions[k] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = new(0, 0, 0, 255);
                Vector2 scale = new((flies[i].flyPosition - flies[i].oldPositions[k]).Length() / baseTexture.Width, 0.225f);
                float rotation = (flies[i].flyPosition - flies[i].oldPositions[k]).ToRotation();
                Main.EntitySpriteDraw(baseTexture, drawPos, null, color, rotation, drawOrigin, scale, SpriteEffects.None, 0);
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

        for (int i = 0; i < maxFlies; i++)
        {
            //fliesVelocity[i] = flies[i].velocity;

            if (flies[i] == null)
            {
                flies[i] = new Fly();
            }

            if (flies[i].velocity == Vector2.Zero)
            {
                flies[i].SetInitials(NPC.position);

                //fliesPosition = flies[i].flyPosition;
                //fliesVelocity[i] = flies[i].velocity;
            }

            flies[i].Update(!Collision.CanHitLine(flies[i].flyPosition, 1, 1, flies[i].direction * 4 * 16 + flies[i].flyPosition, 32, 32), Timer, NPC.position);
            flies[i].UpdatePosition();
            flies[i].UpdateOldPositions();
            //fliesPosition = flies[i].flyPosition;
            //fliesHeadingToV2 = flies[i].heading.ToRotationVector2();
            //fliesVelocity = flies[i].velocity;
        }

        //base.AI();
    }
}
