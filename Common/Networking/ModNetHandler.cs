using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace Vaultaria.Common.Networking
{
	internal class ModNetHandler 
	{
		// When a lot of handlers are added, it might be wise to automate the creation of them
		public const byte MyBossNpcType = 1; // Defines the unique ID (1) that will be used to identify and route all Boss-related packets.

		internal static BossPacketHandler myBossNpc = new BossPacketHandler (MyBossNpcType); // Creates a static, single instance of the BossPacketHandler, passing the unique ID (1) to its constructor.

		public static void HandlePacket(BinaryReader reader, int fromWho) // This is the method called directly by the Vaultaria mod class when any packet for the mod is received.
		{
			switch (reader.ReadByte()) // Reads the first byte of data (the HandlerType written in PacketHandler.GetPacket).
			{
				case MyBossNpcType: // If the first byte matches the Boss ID (1), it calls the HandlePacket method on the specific BossPacketHandler instance, passing the remaining packet data (reader) and the sender ID (fromWho).
					myBossNpc.HandlePacket(reader, fromWho);
					break;
			}
		}
	}  
}