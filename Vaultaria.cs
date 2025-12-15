using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Vaultaria.Common.Global.BossKillGlobalNPC;
using System.IO;
using Vaultaria.Common.Networking;

namespace Vaultaria
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class Vaultaria : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);

			ModNetHandler.HandlePacket(reader, whoAmI);
        }
	}
}