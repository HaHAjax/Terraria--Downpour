using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ReLogic.Content;

namespace tRW.Content.NPCs;

public class BugFly : ModNPC
{
    public override string Texture => "tRW/Assets/NPCs/Fly";

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 1; // Copy animation frames
        //Main.npcCatchable[Type] = true; // I'm not sure if I want this to be catchable just yet

        // These three are typical critter values
        NPCID.Sets.CountsAsCritter[Type] = true;

        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;

        NPCID.Sets.TrailingMode[Type] = 1;
        NPCID.Sets.TrailCacheLength[Type] = 5;
    }

    public override void SetDefaults()
    {
        NPC.noGravity = true;
        NPC.npcSlots = 0.05f;
        NPC.chaseable = false;
        NPC.damage = 0;
        //NPC.width = 24;
        //NPC.height = 24;
        NPC.Hitbox = new Rectangle(0, 0, 24, 24);
        NPC.defense = 0;
        NPC.lifeMax = 5;
        NPC.knockBackResist = 1f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath9;
        NPC.scale = 0.4f;
        NPC.Opacity = 1f;
        NPC.gfxOffY = -6;

        //NPC.CloneDefaults(NPCID.Butterfly);

        NPC.aiStyle = -1;
        AIType = -1; // Setting it to the butterfly for now just to make things easier
        AnimationType = Type;

        //base.SetDefaults();
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Texture2D baseTexture = TextureAssets.Npc[Type].Value;
        Texture2D outlineTexture = ModContent.Request<Texture2D>(Texture + "_Outline", AssetRequestMode.ImmediateLoad).Value;

        if (!Main.dayTime)
        {
            Vector2 drawOrigin = outlineTexture.Size() / 2;
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = Color.White;
                color.A = 0;
                Main.EntitySpriteDraw(outlineTexture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale * 2f, SpriteEffects.None, 0);
            }
        }
        if (NPC.velocity != Vector2.Zero)
        {
            Vector2 drawOrigin = baseTexture.Size() / 2;
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(drawColor);
                Main.EntitySpriteDraw(baseTexture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }
        }

        return true;
    }

    public float Heading
    {
        get => NPC.ai[0];
        set => NPC.ai[0] = value;
    }

    public int Timer
    {
        get => (int)NPC.ai[1];
        set => NPC.ai[1] = value;
    }

    public int Speed
    {
        get => (int)NPC.ai[2];
        set => NPC.ai[2] = value;
    }

    public override void AI()
    {
        Timer++;

        if (!Collision.CanHitLine(NPC.Center, 1, 1, NPC.Center + Heading.ToRotationVector2() * 4 * 16, 16, 16))
        {
            Heading += MathHelper.ToRadians(30);
        }

        if (Timer % 4 == 0)
        {
            Heading += Main.rand.NextFloat(MathHelper.ToRadians(-20), MathHelper.ToRadians(20));
            Vector2 direction = Heading.ToRotationVector2();
            NPC.velocity = direction * Speed;
        }

        if (Timer % 60 == 0)
        {
            Heading += Main.rand.NextFloat(MathHelper.ToRadians(-90), MathHelper.ToRadians(90));
            Vector2 direction = Heading.ToRotationVector2();
            Speed = Main.rand.Next(2, 6);
            NPC.velocity = direction * Speed;
        }

        if (NPC.velocity == Vector2.Zero)
        {
            Speed = Main.rand.Next(2, 6);
        }

        //Timer -= 1;
        //if (Timer <= 0)
        //{
        //    Timer = 8;
        //    if (Main.netMode != NetmodeID.MultiplayerClient)
        //    {
        //        Heading += Main.rand.Next(-5, 5);
        //        NPC.netUpdate = true;
        //    }
        //}

        //NPC.velocity = Heading.ToRotationVector2() * 5;

    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Copper, 0, 0, 0, new Color(134f, 63f, 8f, 0.73f));
            }
        }

        //base.HitEffect(hit);
    }

    //public override Color? GetAlpha(Color drawColor)
    //{
    //    // GetAlpha gives the fly a white glow.
    //    return drawColor with
    //    {
    //        R = 255,
    //        G = 255,
    //        B = 255,
    //        A = 150
    //    };
    //}

    //public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //{
    //    if (SpawnCondition.OverworldDay.Active)
    //    {
    //        return SpawnCondition.OverworldDay.Chance * 0.1f;
    //    }
    //    else
    //    {
    //        return 0f;
    //    }

    //    //return base.SpawnChance(spawnInfo);
    //}

    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter = 1f;
    }
}