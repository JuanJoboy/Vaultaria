using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Vaultaria.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Vaultaria.Common.Systems.GenPasses.Vaults;

namespace Vaultaria.Common.Global
{
    public class BossKillGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            // Ensure this logic only runs on the server in multiplayer, or locally in single player.
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                if(!SubworldLibrary.SubworldSystem.AnyActive())
                {
                    CallAllPrefixUnlocked(npc);
                }
                if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>())
                {
                    CallAllVault1BossDowned(npc);
                }
                if(SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
                {
                    CallAllVault2BossDowned(npc);
                }
            }
        }

        private void PrefixUnlocked(NPC npc, ref bool npcDowned, int npcID, Color color, string prefixUnlocked)
        {
            if (npc.type == npcID)
            {
                if (npcDowned == false)
                {
                    npcDowned = true;
                    NPC.SetEventFlagCleared(ref npcDowned, -1);

                    Utilities.Utilities.DisplayStatusMessage(npc.Center, color, $"{prefixUnlocked} Prefix Unlocked!");
                    
                    NetMessage.SendData(MessageID.WorldData); // If in multiplayer, immediately inform all clients of new world state. Uses netSend and netReceive in BossDownedSystem
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

        private void VaultBossDowned(NPC npc, ref bool npcDowned, int npcID)
        {
            if (npc.type == npcID)
            {
                if (npcDowned == false)
                {
                    npcDowned = true;
                    NPC.SetEventFlagCleared(ref npcDowned, -1);

                    NetMessage.SendData(MessageID.WorldData); // If in multiplayer, immediately inform all clients of new world state. Uses netSend and netReceive in BossDownedSystem
                }
            }
        }

        private void ShowVaultMessage(NPC npc, int npcID, Color color, string msg)
        {
            if(npc.type == npcID)
            {
                Utilities.Utilities.DisplayStatusMessage(npc.Center, color, msg);
            }
        }

        private void CallAllVault1BossDowned(NPC npc)
        {
            // Vault 1
            VaultBossDowned(npc, ref BossDownedSystem.vaultKingSlime, NPCID.KingSlime);
            VaultBossDowned(npc, ref BossDownedSystem.vaultEyeOfCthulhu, NPCID.EyeofCthulhu);
            VaultBossDowned(npc, ref BossDownedSystem.vaultQueenBee, NPCID.QueenBee);
            VaultBossDowned(npc, ref BossDownedSystem.vaultDeerClops, NPCID.Deerclops);
            VaultBossDowned(npc, ref BossDownedSystem.vaultSkeletron, NPCID.SkeletronHead);

            ShowVaultMessage(npc, NPCID.SkeletronHead, Color.OrangeRed, "Vault of the Warrior Raided!");
        }

        private void CallAllVault2BossDowned(NPC npc)
        {
            // Vault 2
            VaultBossDowned(npc, ref BossDownedSystem.vaultQueenSlime, NPCID.QueenSlimeBoss);
            VaultBossDowned(npc, ref BossDownedSystem.vaultTwins, NPCID.Retinazer);
            VaultBossDowned(npc, ref BossDownedSystem.vaultSkeletronPrime, NPCID.SkeletronPrime);
            VaultBossDowned(npc, ref BossDownedSystem.vaultBetsy, NPCID.DD2Betsy);
            VaultBossDowned(npc, ref BossDownedSystem.vaultPlantera, NPCID.Plantera);
            VaultBossDowned(npc, ref BossDownedSystem.vaultGolem, NPCID.Golem);
            VaultBossDowned(npc, ref BossDownedSystem.vaultDukeFishron, NPCID.DukeFishron);
            VaultBossDowned(npc, ref BossDownedSystem.vaultEmpress, NPCID.HallowBoss);
            VaultBossDowned(npc, ref BossDownedSystem.vaultLunaticCultist, NPCID.CultistBoss);
            VaultBossDowned(npc, ref BossDownedSystem.vaultMoonLord, NPCID.MoonLordCore);

            ShowVaultMessage(npc, NPCID.MoonLordCore, Color.LightBlue, "Vault of the Destroyer Raided!");
        }
    }
}