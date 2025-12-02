using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Vaultaria.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Vaultaria.Common.Global
{
    public class BossKillGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            // Ensure this logic only runs on the server in multiplayer, or locally in singleplayer.
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                CallAllPrefixUnlocked(npc);
                
                CallAllVaultBossDowned(npc);
            }
        }

        private void PrefixUnlocked(NPC npc, ref bool npcDowned, int npcID, Color color, string prefixUnlocked)
        {
            if (npc.type == npcID)
            {
                if (npcDowned == false)
                {
                    Utilities.Utilities.DisplayStatusMessage(npc.Center, color, $"{prefixUnlocked} Prefix Unlocked!");

                    npcDowned = true;
                    NPC.SetEventFlagCleared(ref npcDowned, -1);
                }
            }   
        }

        private void CallAllPrefixUnlocked(NPC npc)
        {
            // PrefixUnlocked(npc, BossDownedSystem.skeletron, NPCID.SkeletronHead, Color.Gold, "Masher");
            PrefixUnlocked(npc, ref BossDownedSystem.torchGod, NPCID.TorchGod, Color.OrangeRed, "Incendiary");
            PrefixUnlocked(npc, ref BossDownedSystem.evilBoss, NPCID.BrainofCthulhu, Color.LightGreen, "Corrosive");
            PrefixUnlocked(npc, ref BossDownedSystem.evilBoss, NPCID.EaterofWorldsHead, Color.LightGreen, "Corrosive");
            PrefixUnlocked(npc, ref BossDownedSystem.deerClops, NPCID.Deerclops, Color.Gold, "Double Penetrating");
            PrefixUnlocked(npc, ref BossDownedSystem.wallOfFlesh, NPCID.WallofFlesh, Color.Violet, "Slag");
            PrefixUnlocked(npc, ref BossDownedSystem.pirateShip, NPCID.PirateShip, Color.Yellow, "Explosive");
            PrefixUnlocked(npc, ref BossDownedSystem.twins, NPCID.Retinazer, Color.YellowGreen, "Radiation");
            PrefixUnlocked(npc, ref BossDownedSystem.twins, NPCID.Spazmatism, Color.YellowGreen, "Radiation");
            PrefixUnlocked(npc, ref BossDownedSystem.iceGolem, NPCID.IceGolem, Color.LightBlue, "Cryo");
            PrefixUnlocked(npc, ref BossDownedSystem.martianSaucerCore, NPCID.MartianSaucerCore, Color.DeepSkyBlue, "Shock");
        }

        private void VaultBossDowned(NPC npc, ref bool npcDowned, ref bool dontRespawn, int npcID)
        {
            if(SubworldLibrary.SubworldSystem.AnyActive())
            {
                if (npc.type == npcID)
                {
                    if (npcDowned == false)
                    {
                        npcDowned = true;
                        NPC.SetEventFlagCleared(ref npcDowned, -1);
                    }
                    if(dontRespawn == false)
                    {
                        dontRespawn = true;
                        NPC.SetEventFlagCleared(ref dontRespawn, -1);
                    }
                }
            }
        }

        private void CallAllVaultBossDowned(NPC npc)
        {
            VaultBossDowned(npc, ref BossDownedSystem.vaultKingSlime, ref BossDownedSystem.vaultKingSlimeDR, NPCID.KingSlime);
            VaultBossDowned(npc, ref BossDownedSystem.vaultEyeOfCthulhu, ref BossDownedSystem.vaultEyeOfCthulhuDR, NPCID.EyeofCthulhu);
        }
    }
}