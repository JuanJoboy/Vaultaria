using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace Vaultaria.Common.Networking
{
	internal class VaultPacketHandler : PacketHandler // Inherits the base PacketHandler, gaining the HandlerType and GetPacket logic.
	{
		public const byte SyncPlayers = 1; // Defines a constant byte ID for this specific action: synchronizing the vault's start. This will be the packetType passed to GetPacket.

		public VaultPacketHandler(byte handlerType) : base(handlerType) // Constructor, requires the unique ID assigned in ModNetHandler.
		{
		}

		public override void HandlePacket(BinaryReader reader, int fromWho) // Implements the abstract method from the base class. This is the receiving point for all Boss-related packets.
		{
			switch (reader.ReadByte()) // Reads the second byte of data (the packetType or SyncPlayers ID) to determine what action to take.
			{
				case SyncPlayers: // If the packet type is SyncPlayers (1), it calls the method responsible for reading and acting on that data.
					ReceiveTarget(reader, fromWho);
					break;
			}
		}

		public void ReceiveTarget(BinaryReader reader, int fromWho) // This method runs when a SyncPlayers packet is received.
		{
			// Reads the two specific pieces of data sent by SendTarget in the exact same order (FIFO).
			int npc = reader.ReadInt32();
			int target = reader.ReadInt32();

			if (Main.netMode == NetmodeID.Server) // Server Logic (Flow 1: Client -> Server -> Client). If the server receives this packet from a client, it immediately forwards it to all other clients (-1) to synchronize the state.
			{
				SendTarget(-1, fromWho, npc, target);
			}

			else // Client Logic (Flow 1: Server -> Client). If a client receives this packet, it applies the synchronized data: it finds the NPC by index and sets its target to the new value.
			{
				NPC theNpc = Main.npc[npc];
				theNpc.oldTarget = theNpc.target;
				theNpc.target = target;
			}
		}

		public void SendTarget(int toWho, int fromWho, int npc, int target) // This public method is called from your ModNPC or global logic when a boss needs to change targets and sync that change.
		{
			ModPacket packet = GetPacket(SyncPlayers, fromWho); // Uses the base method to start a packet, writing the HandlerType and the SyncPlayers ID.

			// Writes the two required pieces of specific data: the NPC index (int) and the target player index (int).
			packet.Write(npc);
			packet.Write(target);

			packet.Send(toWho, fromWho); // Sends the complete packet. toWho determines the recipient (e.g., -1 for all clients, fromWho for the client that initiated the action).
		}
	} 
}