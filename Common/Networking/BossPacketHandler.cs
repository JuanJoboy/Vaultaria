using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Common.Networking
{
	internal class BossPacketHandler : PacketHandler
	{
		public const byte SyncTarget = 1;

		public BossPacketHandler(byte handlerType) : base(handlerType)
		{
		}

		public override void HandlePacket(BinaryReader reader, int fromWho)
		{
			switch (reader.ReadByte())
			{
				case SyncTarget:
					ReceiveTarget(reader, fromWho);
					break;
			}
		}

		public void SendTarget(int toWho, int fromWho, int npc, int target)
		{
			ModPacket packet = GetPacket(SyncTarget, fromWho);

			packet.Write(npc);
			packet.Write(target);

			packet.Send(toWho, fromWho);
		}

		public void ReceiveTarget(BinaryReader reader, int fromWho)
		{
			int npc = reader.ReadInt32();
			int target = reader.ReadInt32();

			if (Main.netMode == NetmodeID.Server)
			{
				SendTarget(-1, fromWho, npc, target);
			}

			else
			{
				NPC theNpc = Main.npc[npc];
				theNpc.oldTarget = theNpc.target;
				theNpc.target = target;
			}
		}
	} 
}