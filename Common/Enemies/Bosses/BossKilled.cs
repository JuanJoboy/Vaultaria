using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Vaultaria.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Vaultaria.Common.Systems.GenPasses.Vaults;
using Vaultaria.Common.Networking;

namespace Vaultaria.Common.Global
{
    public class BossKillGlobalNPC : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            // Only do manual NPC AI tracking in a subworld
            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                if(Main.netMode != NetmodeID.MultiplayerClient) // Only the server should make the decisions
                {
                    if(npc.boss || npc.type == NPCID.Pumpking || npc.type == NPCID.IceQueen) // idk if pumpking or ice queen count as bosses
                    {
                        int bestTarget = npc.target; // Store the target that the boss is currently fighting
                        float minDistance = float.MaxValue; // This just initially sets the distance to the max possible number, so that everyone is valid for the first check

                        for(int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];

                            if(player.active && !player.dead && !player.ghost) // Go through every player and check if they're valid
                            {
                                float distance = Vector2.Distance(player.Center, npc.Center); // Get the distance of the player and the boss

                                if(distance < minDistance) // If the distance is less than that max value (on the first run this will be true)
                                {
                                    bestTarget = player.whoAmI; // Then set the target to that player
                                    minDistance = distance; // And set the min distance to that distance so that it isn't 2 trillion or whatever that max value float is. Then on every run after, the checks above check if the next player is within this distance, and if they are, reduce it again and assign them as the target. And as players move apart, the distance grows larger as the AI constantly checks what the distance between players is.
                                }
                            }
                        }
                        
                        // Set the actual target here only if it's a different target, you don't really need to do the check but its good, otherwise the network would be spammed
                        if (npc.target != bestTarget)
                        {
                            npc.target = bestTarget;
                            npc.netUpdate = true;
                            npc.netUpdate2 = true;
                        }
                    }
                }
            }
        }

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
                    
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.WorldData); // If in multiplayer, immediately inform all clients of new world state. Uses netSend and netReceive in BossDownedSystem
                    }
                }
            }   
        }

        private void CallAllPrefixUnlocked(NPC npc)
        {
            // PrefixUnlocked(npc, BossDownedSystem.skeletron, NPCID.SkeletronHead, Color.Gold, "Masher");
            PrefixUnlocked(npc, ref BossDownedSystem.eyeOfCthulhu, NPCID.EyeofCthulhu, Color.OrangeRed, "Incendiary");
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

                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        ModNetHandler.vault.SendBossDeath1(Main.myPlayer);
                        ModNetHandler.vault.SendBossDeath2(Main.myPlayer);
                        NetMessage.SendData(MessageID.WorldData); // If in multiplayer, immediately inform all clients of new world state. Uses netSend and netReceive in VaultMonsterSystem
                    }
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
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultKingSlime, NPCID.KingSlime);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultEyeOfCthulhu, NPCID.EyeofCthulhu);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultQueenBee, NPCID.QueenBee);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultDeerClops, NPCID.Deerclops);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultSkeletron, NPCID.SkeletronHead);

            ShowVaultMessage(npc, NPCID.SkeletronHead, Color.OrangeRed, "Vault of the Warrior Raided!");
        }

        private void CallAllVault2BossDowned(NPC npc)
        {
            // Vault 2
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultQueenSlime, NPCID.QueenSlimeBoss);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultSeasonalBosses, NPCID.Pumpking);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultSkeletronPrime, NPCID.SkeletronPrime);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultBetsy, NPCID.DD2Betsy);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultPlantera, NPCID.Plantera);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultGolem, NPCID.Golem);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultDukeFishron, NPCID.DukeFishron);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultEmpress, NPCID.HallowBoss);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultLunaticCultist, NPCID.CultistBoss);
            VaultBossDowned(npc, ref VaultMonsterSystem.vaultMoonLord, NPCID.MoonLordCore);

            ShowVaultMessage(npc, NPCID.MoonLordCore, Color.LightBlue, "Vault of the Destroyer Raided!");
        }
    }
}