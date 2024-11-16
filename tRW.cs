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
        public override void Load()
        {
            TextureAssets.Item[ItemID.PlatinumCoin] = ModContent.Request<Texture2D>("tRW/Assets/Misc/Currency/PearlPlatinum");
            TextureAssets.Item[ItemID.GoldCoin] = ModContent.Request<Texture2D>("tRW/Assets/Misc/Currency/PearlGold");

            base.Load();
        }
    }
}
