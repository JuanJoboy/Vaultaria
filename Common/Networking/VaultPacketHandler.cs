using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace Vaultaria.Common.Networking
{
	internal class VaultPacketHandler : PacketHandler // Inherits the base PacketHandler, gaining the HandlerType and GetPacket logic.
	{
		public const byte SyncBossRush = 1; // Defines a constant byte ID for this specific action: synchronizing the vault's start. This will be the packetType passed to GetPacket.
		public const byte SyncBossDeaths = 2;

		public VaultPacketHandler(byte handlerType) : base(handlerType) // Constructor, requires the unique ID assigned in ModNetHandler.
		{
		}

		public override void HandlePacket(BinaryReader reader, int fromWho) // Implements the abstract method from the base class. This is the receiving point for all Boss-related packets.
		{
			switch (reader.ReadByte()) // Reads the second byte of data (the packetType or SyncBossRush ID) to determine what action to take.
			{
				case SyncBossRush: // If the packet type is SyncBossRush (1), it calls the method responsible for reading and acting on that data.
					ReceiveBossRush(reader, fromWho);
					break;
				case SyncBossDeaths:
					ReceiveBossDeath(reader, fromWho);
					break;
			}
		}

		public void ReceiveBossRush(BinaryReader reader, int fromWho) // This method runs when a SyncBossRush packet is received.
		{
			// Reads the two specific pieces of data sent by SendTarget in the exact same order (FIFO).
			bool bossRushStarted = reader.ReadBoolean();
			Utilities.Utilities.startedVault2BossRush = bossRushStarted; // 1. The "Perfect" Version (Assignment inside ReceiveBossRush). In this version, the assignment line is the very first thing that happens after reading the data. The Server receives the packet -> runs ReceiveBossRush -> sets its variable -> sends to others. The Clients receive the packet -> run ReceiveBossRush -> set their variables. Result: Everyone is synced.

			if (Main.netMode == NetmodeID.Server) // Server Logic (Flow 1: Client -> Server -> Client). If the server receives this packet from a client, it immediately forwards it to all other clients (-1) to synchronize the state.
			{
				SendBossRushStatus(bossRushStarted, fromWho);
			}
		}

		public void SendBossRushStatus(bool bossRushStarted, int fromWho) // This public method is called from your ModNPC or global logic when a sync needs to occur
		{
			ModPacket packet = GetPacket(SyncBossRush, fromWho); // Uses the base method to start a packet, writing the HandlerType and the SyncBossRush ID.

			// 2. "Forceful" Version (Assignment inside SendBossRushStatus). In this version, you moved the assignment logic into the sending method.The Server receives the packet -> runs ReceiveBossRush. It reads the boolean. It does not set the variable here.The Server then calls SendBossRushStatus. Now the Server sets its variable and sends the packet to Clients. The Clients receive the packet -> they run ReceiveBossRush. They read the boolean. And then they stop.Result: The Server updated its variable, but all other Clients stayed at false because they never ran a line of code that said Utilities... = bossRushStarted.
			// Utilities.Utilities.startedVault2BossRush = bossRushStarted;

			// Writes the required piece of data
			packet.Write(bossRushStarted);
			packet.Send(-1, fromWho);
		}

		public void ReceiveBossDeath(BinaryReader reader, int fromWho)
		{
			bool bossDeaths = reader.ReadBoolean();
			Utilities.Utilities.startedVault2BossRush = bossDeaths;

			if (Main.netMode == NetmodeID.Server)
			{
				SendBossDeath(bossDeaths, fromWho);
			}
		}

		public void SendBossDeath(bool bossDeaths, int fromWho)
		{
			ModPacket packet = GetPacket(SyncBossDeaths, fromWho);

			packet.Write(bossDeaths);
			packet.Send(-1, fromWho);
		}
	} 
}