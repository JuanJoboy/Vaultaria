using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace Vaultaria.Common.Networking
{
	// For in depth info on networking, go to this link
	// https://www.notion.so/Student-Dashboard-6c9fe56edeea48fda51ba893346ca82b?p=2cbe234bbf728045a0e8ea9c1565f9f6&pm=c

	internal abstract class PacketHandler // Defines an abstract class, meaning you can't create an instance of it directly. It must be inherited by specific handlers (like BossPacketHandler). internal restricts access to your mod assembly.
	{
		internal byte HandlerType { get; set; } // This property stores the unique byte ID for this specific type of handler (e.g., BossPacketHandler might be assigned 1). This is the first piece of data written to every packet created by this handler.

		protected PacketHandler(byte handlerType) // This is the constructor. It forces any derived class to set the unique HandlerType when it is instantiated.
		{
			HandlerType = handlerType;
		}

		protected ModPacket GetPacket(byte packetType, int fromWho) // This helper method is called by derived classes (like BossPacketHandler) to start building a new packet.
		{
			ModPacket packet = ModContent.GetInstance<Vaultaria>().GetPacket(); // Gets an empty, writable ModPacket object associated with your mod, Vaultaria.
            
			packet.Write(HandlerType); // Writes the Handler ID (e.g., 1 for Boss). This is the first byte of data. It tells the main ModNetHandler which specific handler class to forward the packet to.
			packet.Write(packetType); // Writes the Packet Type (e.g., SyncTarget = 1). This is the second byte of data. It tells the specific handler (BossPacketHandler) which method (e.g., ReceiveTarget) to run.

			if (Main.netMode == NetmodeID.Server) // If the packet is being sent from the server (Flow 1: Server -> Client), the original sender's ID (fromWho) is written as the third byte.
			{
				packet.Write((byte)fromWho); // This is crucial for clients to know who initiated the original action, as the server's fromWho is always 256.
			}

			return packet; // Returns the partially written ModPacket, ready for the derived class to write its specific data (like NPC index, target ID).
		}

		public abstract void HandlePacket(BinaryReader reader, int fromWho); // This is a required method that must be implemented by any derived class. When a packet is received, the mod's main HandlePacket method will eventually call this, passing the reader and the sender ID (fromWho).
	}   
}