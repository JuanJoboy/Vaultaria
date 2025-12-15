using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Common.Networking
{
	internal class ModNetHandler 
	{
		// When a lot of handlers are added, it might be wise to automate the creation of them
		public const byte MyBossNpcType = 1;

		internal static BossPacketHandler myBossNpc = new BossPacketHandler (MyBossNpcType);

		public static void HandlePacket(BinaryReader r, int fromWho)
		{
			switch (r.ReadByte())
			{
				case MyBossNpcType:
					myBossNpc.HandlePacket(r, fromWho);
					break;
			}
		}
	}  
}