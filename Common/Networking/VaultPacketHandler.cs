using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using Terraria.ModLoader.IO;
using Vaultaria.Common.Systems;
using SubworldLibrary;
using Vaultaria.Common.Systems.GenPasses;

namespace Vaultaria.Common.Networking
{
	internal class VaultPacketHandler : PacketHandler // Inherits the base PacketHandler, gaining the HandlerType and GetPacket logic.
	{
		public const byte SyncBossRush1 = 1; // Defines a constant byte ID for this specific action: synchronizing the vault's start. This will be the packetType passed to GetPacket.
		public const byte SyncBossRush2 = 2;
		public const byte SyncBossDeaths1 = 3;
		public const byte SyncBossDeaths2 = 4;
		public const byte SyncNoReturn = 5;
		public const byte SyncPedestal1 = 6;
		public const byte SyncPedestal2 = 7;

		public VaultPacketHandler(byte handlerType) : base(handlerType) // Constructor, requires the unique ID assigned in ModNetHandler.
		{
		}

		public override void HandlePacket(BinaryReader reader, int fromWho) // Implements the abstract method from the base class. This is the receiving point for all Boss-related packets.
		{
			switch (reader.ReadByte()) // Reads the second byte of data (the packetType or SyncBossRush ID) to determine what action to take.
			{
				case SyncBossRush1: // If the packet type is SyncBossRush1 (1), it calls the method responsible for reading and acting on that data.
					ReceiveBossRush1(reader, fromWho);
					break;
				case SyncBossRush2:
					ReceiveBossRush2(reader, fromWho);
					break;
				case SyncBossDeaths1:
					ReceiveBossDeath1(reader, fromWho);
					break;
				case SyncBossDeaths2:
					ReceiveBossDeath2(reader, fromWho);
					break;
				case SyncNoReturn:
					ReceiveNoReturn(reader, fromWho);
					break;
				case SyncPedestal1:
					ReceivePedestal1(reader, fromWho);
					break;
				case SyncPedestal2:
					ReceivePedestal2(reader, fromWho);
					break;
			}
		}

		public void ReceiveBossRush1(BinaryReader reader, int fromWho) // This method runs when a SyncBossRush packet is received.
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				reader.ReadByte(); // Consumes the 'fromWho' byte written by GetPacket header
			}

			// Reads the two specific pieces of data sent by SendTarget in the exact same order (FIFO).
			bool bossRushStarted = reader.ReadBoolean();
			Utilities.Utilities.startedVault1BossRush = bossRushStarted; // 1. The "Perfect" Version (Assignment inside ReceiveBossRush). In this version, the assignment line is the very first thing that happens after reading the data. The Server receives the packet -> runs ReceiveBossRush -> sets its variable -> sends to others. The Clients receive the packet -> run ReceiveBossRush -> set their variables. Result: Everyone is synced.

			if (Main.netMode == NetmodeID.Server) // Server Logic (Flow 1: Client -> Server -> Client). If the server receives this packet from a client, it immediately forwards it to all other clients (-1) to synchronize the state.
			{
				SendBossRushStatus1(bossRushStarted, fromWho);
			}
		}

		public void SendBossRushStatus1(bool bossRushStarted, int fromWho) // This public method is called from your ModNPC or global logic when a sync needs to occur
		{
			ModPacket packet = GetPacket(SyncBossRush1, fromWho); // Uses the base method to start a packet, writing the HandlerType and the SyncBossRush ID.

			// 2. "Forceful" Version (Assignment inside SendBossRushStatus). In this version, you moved the assignment logic into the sending method.The Server receives the packet -> runs ReceiveBossRush. It reads the boolean. It does not set the variable here.The Server then calls SendBossRushStatus. Now the Server sets its variable and sends the packet to Clients. The Clients receive the packet -> they run ReceiveBossRush. They read the boolean. And then they stop.Result: The Server updated its variable, but all other Clients stayed at false because they never ran a line of code that said Utilities... = bossRushStarted.
			// Utilities.Utilities.startedVault2BossRush = bossRushStarted;

			// Writes the required piece of data
			packet.Write(bossRushStarted);
			packet.Send(-1, fromWho);
		}

		public void ReceiveBossRush2(BinaryReader reader, int fromWho) // This method runs when a SyncBossRush packet is received.
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				reader.ReadByte(); // Consumes the 'fromWho' byte written by GetPacket header
			}

			bool bossRushStarted = reader.ReadBoolean();
			Utilities.Utilities.startedVault2BossRush = bossRushStarted;

			if (Main.netMode == NetmodeID.Server)
			{
				SendBossRushStatus2(bossRushStarted, fromWho);
			}
		}

		public void SendBossRushStatus2(bool bossRushStarted, int fromWho) // This public method is called from your ModNPC or global logic when a sync needs to occur
		{
			ModPacket packet = GetPacket(SyncBossRush2, fromWho); // Uses the base method to start a packet, writing the HandlerType and the SyncBossRush ID.

			// 2. "Forceful" Version (Assignment inside SendBossRushStatus). In this version, you moved the assignment logic into the sending method.The Server receives the packet -> runs ReceiveBossRush. It reads the boolean. It does not set the variable here.The Server then calls SendBossRushStatus. Now the Server sets its variable and sends the packet to Clients. The Clients receive the packet -> they run ReceiveBossRush. They read the boolean. And then they stop.Result: The Server updated its variable, but all other Clients stayed at false because they never ran a line of code that said Utilities... = bossRushStarted.
			// Utilities.Utilities.startedVault2BossRush = bossRushStarted;

			// Writes the required piece of data
			packet.Write(bossRushStarted);
			packet.Send(-1, fromWho);
		}

		public void ReceiveBossDeath1(BinaryReader reader, int fromWho)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				reader.ReadByte(); // Consumes the 'fromWho' byte written by GetPacket header
			}

			// Read every single boolean currently in the system
			reader.ReadFlags(out VaultMonsterSystem.vaultKingSlime, out VaultMonsterSystem.vaultKingSlimeDR, out VaultMonsterSystem.vaultEyeOfCthulhu, out VaultMonsterSystem.vaultEyeOfCthulhuDR, out VaultMonsterSystem.vaultQueenBee, out VaultMonsterSystem.vaultQueenBeeDR, out VaultMonsterSystem.vaultDeerClops, out VaultMonsterSystem.vaultDeerClopsDR);
			
			reader.ReadFlags(out VaultMonsterSystem.vaultSkeletron, out VaultMonsterSystem.vaultSkeletronDR);

			if (Main.netMode == NetmodeID.Server)
			{
				SendBossDeath1(fromWho);
			}
		}

		public void SendBossDeath1(int fromWho)
		{
			ModPacket packet = GetPacket(SyncBossDeaths1, fromWho);

			// Write every single boolean currently in the system
			packet.WriteFlags(VaultMonsterSystem.vaultKingSlime, VaultMonsterSystem.vaultKingSlimeDR, VaultMonsterSystem.vaultEyeOfCthulhu, VaultMonsterSystem.vaultEyeOfCthulhuDR, VaultMonsterSystem.vaultQueenBee, VaultMonsterSystem.vaultQueenBeeDR, VaultMonsterSystem.vaultDeerClops, VaultMonsterSystem.vaultDeerClopsDR);

			packet.WriteFlags(VaultMonsterSystem.vaultSkeletron, VaultMonsterSystem.vaultSkeletronDR);

			packet.Send(-1, fromWho);
		}

		public void ReceiveBossDeath2(BinaryReader reader, int fromWho)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				reader.ReadByte(); // Consumes the 'fromWho' byte written by GetPacket header
			}

			// Read every single boolean currently in the system
			reader.ReadFlags(out VaultMonsterSystem.vaultQueenSlime, out VaultMonsterSystem.vaultQueenSlimeDR, out VaultMonsterSystem.vaultSeasonalBosses, out VaultMonsterSystem.vaultSeasonalBossesDR, out VaultMonsterSystem.vaultSkeletronPrime, out VaultMonsterSystem.vaultSkeletronPrimeDR, out VaultMonsterSystem.vaultBetsy, out VaultMonsterSystem.vaultBetsyDR);
			
			reader.ReadFlags(out VaultMonsterSystem.vaultPlantera, out VaultMonsterSystem.vaultPlanteraDR, out VaultMonsterSystem.vaultGolem, out VaultMonsterSystem.vaultGolemDR, out VaultMonsterSystem.vaultDukeFishron, out VaultMonsterSystem.vaultDukeFishronDR, out VaultMonsterSystem.vaultEmpress, out VaultMonsterSystem.vaultEmpressDR);
			
			reader.ReadFlags(out VaultMonsterSystem.vaultLunaticCultist, out VaultMonsterSystem.vaultLunaticCultistDR, out VaultMonsterSystem.vaultMoonLord, out VaultMonsterSystem.vaultMoonLordDR);

			if (Main.netMode == NetmodeID.Server)
			{
				SendBossDeath2(fromWho);
			}
		}

		public void SendBossDeath2(int fromWho)
		{
			ModPacket packet = GetPacket(SyncBossDeaths2, fromWho);

			// Write every single boolean currently in the system
			packet.WriteFlags(VaultMonsterSystem.vaultQueenSlime, VaultMonsterSystem.vaultQueenSlimeDR, VaultMonsterSystem.vaultSeasonalBosses, VaultMonsterSystem.vaultSeasonalBossesDR, VaultMonsterSystem.vaultSkeletronPrime, VaultMonsterSystem.vaultSkeletronPrimeDR, VaultMonsterSystem.vaultBetsy, VaultMonsterSystem.vaultBetsyDR);

			packet.WriteFlags(VaultMonsterSystem.vaultPlantera, VaultMonsterSystem.vaultPlanteraDR, VaultMonsterSystem.vaultGolem, VaultMonsterSystem.vaultGolemDR, VaultMonsterSystem.vaultDukeFishron, VaultMonsterSystem.vaultDukeFishronDR, VaultMonsterSystem.vaultEmpress, VaultMonsterSystem.vaultEmpressDR);

			packet.WriteFlags(VaultMonsterSystem.vaultLunaticCultist, VaultMonsterSystem.vaultLunaticCultistDR, VaultMonsterSystem.vaultMoonLord, VaultMonsterSystem.vaultMoonLordDR);

			packet.Send(-1, fromWho);
		}

		public void ReceiveNoReturn(BinaryReader reader, int fromWho)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				reader.ReadByte(); // Consumes the 'fromWho' byte written by GetPacket header
			}

			SubworldSystem.noReturn = reader.ReadBoolean();

			if (Main.netMode == NetmodeID.Server)
			{
				SendNoReturn(SubworldSystem.noReturn, fromWho);
			}
		}

		public void SendNoReturn(bool noReturn, int fromWho)
		{
			ModPacket packet = GetPacket(SyncNoReturn, fromWho);

			packet.Write(noReturn);
			packet.Send(-1, fromWho);
		}

		public void ReceivePedestal1(BinaryReader reader, int fromWho)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				reader.ReadByte(); // Consumes the 'fromWho' byte written by GetPacket header
			}

			WorldGenerator.pedestalInVault1 = reader.ReadBoolean();

			if (Main.netMode == NetmodeID.Server)
			{
				SendPedestal1(WorldGenerator.pedestalInVault1, fromWho);
			}
		}

		public void SendPedestal1(bool pedestal, int fromWho)
		{
			ModPacket packet = GetPacket(SyncPedestal1, fromWho);

			packet.Write(pedestal);
			packet.Send(-1, fromWho);
		}

		public void ReceivePedestal2(BinaryReader reader, int fromWho)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				reader.ReadByte(); // Consumes the 'fromWho' byte written by GetPacket header
			}

			WorldGenerator.pedestalInVault2 = reader.ReadBoolean();

			if (Main.netMode == NetmodeID.Server)
			{
				SendPedestal2(WorldGenerator.pedestalInVault2, fromWho);
			}
		}

		public void SendPedestal2(bool pedestal, int fromWho)
		{
			ModPacket packet = GetPacket(SyncPedestal2, fromWho);

			packet.Write(pedestal);
			packet.Send(-1, fromWho);
		}
	} 
}