using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace tRW
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class tRW : Mod
	{
        private string coinDir = "tRW/Assets/Misc/Currency/";

        public override void Load()
        {
            TextureAssets.Tile[TileID.CopperCoinPile] = ModContent.Request<Texture2D>(coinDir + "CopperPearlTile");
            TextureAssets.Tile[TileID.SilverCoinPile] = ModContent.Request<Texture2D>(coinDir + "SilverPearlTile");
            TextureAssets.Tile[TileID.GoldCoinPile] = ModContent.Request<Texture2D>(coinDir + "GoldPearlTile");
            TextureAssets.Tile[TileID.PlatinumCoinPile] = ModContent.Request<Texture2D>(coinDir + "PlatinumPearlTile");

            TextureAssets.Item[ItemID.CopperCoin] = ModContent.Request<Texture2D>(coinDir + "CopperPearlItem");
            TextureAssets.Item[ItemID.SilverCoin] = ModContent.Request<Texture2D>(coinDir + "SilverPearlItem");
            TextureAssets.Item[ItemID.GoldCoin] = ModContent.Request<Texture2D>(coinDir + "GoldPearlItem");
            TextureAssets.Item[ItemID.PlatinumCoin] = ModContent.Request<Texture2D>(coinDir + "PlatinumPearlItem");

            TextureAssets.Coin[0] = ModContent.Request<Texture2D>(coinDir + "CopperPearl");
            TextureAssets.Coin[1] = ModContent.Request<Texture2D>(coinDir + "SilverPearl");
            TextureAssets.Coin[2] = ModContent.Request<Texture2D>(coinDir + "GoldPearl");
            TextureAssets.Coin[3] = ModContent.Request<Texture2D>(coinDir + "PlatinumPearl");

            base.Load();
        }

        public override void Unload()
        {
            TextureAssets.Tile[TileID.CopperCoinPile] = ModContent.Request<Texture2D>($"Terraria/Images/Tiles_{TileID.CopperCoinPile}");
            TextureAssets.Tile[TileID.SilverCoinPile] = ModContent.Request<Texture2D>($"Terraria/Images/Tiles_{TileID.SilverCoinPile}");
            TextureAssets.Tile[TileID.GoldCoinPile] = ModContent.Request<Texture2D>($"Terraria/Images/Tiles_{TileID.GoldCoinPile}");
            TextureAssets.Tile[TileID.PlatinumCoinPile] = ModContent.Request<Texture2D>($"Terraria/Images/Tiles_{TileID.PlatinumCoinPile}");

            TextureAssets.Item[ItemID.CopperCoin] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.CopperCoin}");
            TextureAssets.Item[ItemID.SilverCoin] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.SilverCoin}");
            TextureAssets.Item[ItemID.GoldCoin] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.GoldCoin}");
            TextureAssets.Item[ItemID.PlatinumCoin] = ModContent.Request<Texture2D>($"Terraria/Images/Item_{ItemID.PlatinumCoin}");

            TextureAssets.Coin[0] = ModContent.Request<Texture2D>("Terraria/Images/Coin_0"); // Setting copper coin back
            TextureAssets.Coin[1] = ModContent.Request<Texture2D>("Terraria/Images/Coin_1"); // Setting silver coin back
            TextureAssets.Coin[2] = ModContent.Request<Texture2D>("Terraria/Images/Coin_2"); // Setting gold coin back
            TextureAssets.Coin[3] = ModContent.Request<Texture2D>("Terraria/Images/Coin_3"); // Setting platinum coin back

            base.Unload();
        }
    }
}
