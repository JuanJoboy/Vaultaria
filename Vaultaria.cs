using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Vaultaria.Common.Networking;

namespace Vaultaria
{
	public class Vaultaria : Mod // The main class for your tModLoader mod.
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI) // This is the hook that executes whenever your mod receives a packet from another client or the server.
        {
            base.HandlePacket(reader, whoAmI);

			ModNetHandler.HandlePacket(reader, whoAmI); // Takes the incoming packet (reader) and the sender ID (whoAmI) and immediately passes it to your central router class (ModNetHandler), which takes over the task of reading the first byte and routing the packet to the correct handler.
        }
	}
}