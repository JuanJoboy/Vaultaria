using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Vaultaria.Common.Configs;
using Vaultaria.Common.Networking;
using Vaultaria.Common.Systems.GenPasses.Vaults;
using Vaultaria.Content.Items.Placeables.Vaults;

namespace Vaultaria.Common.Systems
{
	public class VaultMonsterSystem : ModSystem
	{
        // A timer that counts down from 60 to 0 and then spawns the next boss
        private static int countdown = 600;
        private static int bossTimer = countdown;

        public static bool vaultKingSlime = false;
        public static bool vaultKingSlimeDR = false;
        public static bool vaultEyeOfCthulhu = false;
        public static bool vaultEyeOfCthulhuDR = false;
        public static bool vaultQueenBee = false;
        public static bool vaultQueenBeeDR = false;
        public static bool vaultDeerClops = false;
        public static bool vaultDeerClopsDR = false;
        public static bool vaultSkeletron = false;
        public static bool vaultSkeletronDR = false;

        public static bool vaultQueenSlime = false;
        public static bool vaultQueenSlimeDR = false;
        public static bool vaultSeasonalBosses = false;
        public static bool vaultSeasonalBossesDR = false;
        public static bool vaultSkeletronPrime = false;
        public static bool vaultSkeletronPrimeDR = false;
        public static bool vaultBetsy = false;
        public static bool vaultBetsyDR = false;
        public static bool vaultPlantera = false;
        public static bool vaultPlanteraDR = false;
        public static bool vaultGolem = false;
        public static bool vaultGolemDR = false;
        public static bool vaultDukeFishron = false;
        public static bool vaultDukeFishronDR = false;
        public static bool vaultEmpress = false;
        public static bool vaultEmpressDR = false;
        public static bool vaultLunaticCultist = false;
        public static bool vaultLunaticCultistDR = false;
        public static bool vaultMoonLord = false;
        public static bool vaultMoonLordDR = false;

        public override void PostUpdateWorld()
        {
            base.PostUpdateWorld();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = null;
                foreach(Player p in Main.ActivePlayers)
                {
                    player = p;
                    break;
                }

                if(player != null)
                {
                    if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>())
                    {
                        if(Utilities.Utilities.startedVault1BossRush)
                        {
                            if(vaultKingSlimeDR == false)
                            {
                                SpawnBoss(player, NPCID.KingSlime);
                                vaultKingSlimeDR = true;
                                
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    ModNetHandler.vault.SendBossDeath1(Main.myPlayer);
                                }
                            }
                            
                            SpawnPreHardmodeBosses(player);
                        }
                    }  

                    if(SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
                    {
                        if(Utilities.Utilities.startedVault2BossRush)
                        {
                            if(vaultQueenSlimeDR == false)
                            {
                                SpawnBoss(player, NPCID.QueenSlimeBoss);
                                vaultQueenSlimeDR = true;
                                
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    ModNetHandler.vault.SendBossDeath2(Main.myPlayer);
                                }
                            }
                            
                            SpawnHardmodeBosses(player);
                        }
                    }   
                }
            }
        }

		private void SpawnPreHardmodeBosses(Player player)
        {
            ContinueBossRush(player, ref vaultKingSlime, ref vaultEyeOfCthulhu, ref vaultEyeOfCthulhuDR, NPCID.EyeofCthulhu);
            ContinueBossRush(player, ref vaultEyeOfCthulhu, ref vaultQueenBee, ref vaultQueenBeeDR, NPCID.QueenBee);
            ContinueBossRush(player, ref vaultQueenBee, ref vaultDeerClops, ref vaultDeerClopsDR, NPCID.Deerclops);
            ContinueBossRush(player, ref vaultDeerClops, ref vaultSkeletron, ref vaultSkeletronDR, NPCID.SkeletronHead);
        }

		private void SpawnHardmodeBosses(Player player)
        {
            ContinueBossRush(player, ref vaultQueenSlime, ref vaultSeasonalBosses, ref vaultSeasonalBossesDR, NPCID.Pumpking);
            ContinueBossRush(player, ref vaultSeasonalBosses, ref vaultSkeletronPrime, ref vaultSkeletronPrimeDR, NPCID.SkeletronPrime);
            ContinueBossRush(player, ref vaultSkeletronPrime, ref vaultBetsy, ref vaultBetsyDR, NPCID.DD2Betsy);
            ContinueBossRush(player, ref vaultBetsy, ref vaultPlantera, ref vaultPlanteraDR, NPCID.Plantera);
            ContinueBossRush(player, ref vaultPlantera, ref vaultGolem, ref vaultGolemDR, NPCID.Golem);
            ContinueBossRush(player, ref vaultGolem, ref vaultDukeFishron, ref vaultDukeFishronDR, NPCID.DukeFishron);
            ContinueBossRush(player, ref vaultDukeFishron, ref vaultEmpress, ref vaultEmpressDR, NPCID.HallowBoss);
            ContinueBossRush(player, ref vaultEmpress, ref vaultLunaticCultist, ref vaultLunaticCultistDR, NPCID.CultistBoss);
            ContinueBossRush(player, ref vaultLunaticCultist, ref vaultMoonLord, ref vaultMoonLordDR, NPCID.MoonLordCore);
        }

		private void ContinueBossRush(Player player, ref bool oldBossIsDead, ref bool newBossHasDied, ref bool newBossHasBeenSpawned, int newBossToSpawn)
        {
            if(BossTimer(player, ref oldBossIsDead, ref newBossHasDied, ref newBossHasBeenSpawned) == true)
            {
                if(newBossToSpawn != NPCID.Pumpking)
                {
                    SpawnBoss(player, newBossToSpawn);
                    newBossHasBeenSpawned = true;                    
                }
                else if(newBossToSpawn == NPCID.Pumpking)
                {
                    SpawnBoss(player, newBossToSpawn);
                    SpawnBoss(player, NPCID.IceQueen);
                    newBossHasBeenSpawned = true;
                }
            }
        }

		private bool BossTimer(Player player, ref bool oldBossIsDead, ref bool newBossHasDied, ref bool newBossHasBeenSpawned)
        {
            foreach(NPC n in Main.ActiveNPCs)
            {
                if(n.type == NPCID.IceQueen || n.type == NPCID.Pumpking)
                {
                    return false;
                }
            }

            if(oldBossIsDead == true && newBossHasDied == false && newBossHasBeenSpawned == false)
			{
                if(bossTimer <= 0)
                {
                    bossTimer = countdown;
                    return true;
                }

                bossTimer--;

                if(bossTimer > 0 && bossTimer % 60 == 0)
                {
                    string seconds = (bossTimer / 60).ToString();
                    Utilities.Utilities.DisplayStatusMessage(player.Center - new Vector2(0, 50), Color.Gold, seconds);
                }

				return false;
            }

			return false;
        }

		private void SpawnBoss(Player player, int newBossToSpawn)
        {
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC boss = NPC.NewNPCDirect(new EntitySource_WorldEvent(), (int) player.Center.X - 300, (int) player.Center.Y - 100, newBossToSpawn);

                SoundEngine.PlaySound(SoundID.Roar, boss.Center);
                // This adds a screen shake (screenshake) similar to Deerclops
                PunchCameraModifier modifier = new PunchCameraModifier(boss.Center, (Main.rand.NextFloat() * ((float)System.Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f);
                Main.instance.CameraModifiers.Add(modifier);

                if(newBossToSpawn != NPCID.Golem)
                {
                    if(newBossToSpawn == NPCID.Plantera || newBossToSpawn == NPCID.DukeFishron)
                    {
                        boss.boss = true;
                        boss.lifeMax = (int) (boss.lifeMax * 1.5f);
                        boss.life = boss.lifeMax;
                        boss.damage = (int) (boss.damage * 1.25f);
                        boss.velocity *= 1.5f;
                    }
                    else
                    {
                        boss.boss = true;
                        boss.lifeMax *= 2;
                        boss.life = boss.lifeMax;
                        boss.damage *= 2;
                        boss.velocity *= 2f;
                    }

                    VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();
                    if(config.KeepBossSizeTheSameWhenBossRushing == false)
                    {
                        boss.scale *= 1.5f;
                    }
                }

                boss.netUpdate = true;

                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: boss.whoAmI);
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }

        // These 2 net methods are good for when players load into the vault and are able to see the bosses, but it doesn't help for when you're actually playing the game and new bosses come and go. Mod Packets are needed for that.
		public override void NetSend(BinaryWriter writer)
        {
            writer.Write(countdown);
            writer.Write(bossTimer);

            writer.WriteFlags(Utilities.Utilities.startedVault1BossRush);

            writer.WriteFlags(vaultKingSlime, vaultKingSlimeDR, vaultEyeOfCthulhu, vaultEyeOfCthulhuDR, vaultQueenBee, vaultQueenBeeDR, vaultDeerClops, vaultDeerClopsDR);
            writer.WriteFlags(vaultSkeletron, vaultSkeletronDR);

            writer.WriteFlags(Utilities.Utilities.startedVault2BossRush);

            writer.WriteFlags(vaultQueenSlime, vaultQueenSlimeDR, vaultSeasonalBosses, vaultSeasonalBossesDR, vaultSkeletronPrime, vaultSkeletronPrimeDR, vaultBetsy, vaultBetsyDR);
            writer.WriteFlags(vaultPlantera, vaultPlanteraDR, vaultGolem, vaultGolemDR, vaultDukeFishron, vaultDukeFishronDR, vaultEmpress, vaultEmpressDR);
            writer.WriteFlags(vaultLunaticCultist, vaultLunaticCultistDR, vaultMoonLord, vaultMoonLordDR);
		}

		public override void NetReceive(BinaryReader reader)
        {
            countdown = reader.ReadInt32();
            bossTimer = reader.ReadInt32();

            reader.ReadFlags(out Utilities.Utilities.startedVault1BossRush);

            reader.ReadFlags(out vaultKingSlime, out vaultKingSlimeDR, out vaultEyeOfCthulhu, out vaultEyeOfCthulhuDR, out vaultQueenBee, out vaultQueenBeeDR, out vaultDeerClops, out vaultDeerClopsDR);
            reader.ReadFlags(out vaultSkeletron, out vaultSkeletronDR);

            reader.ReadFlags(out Utilities.Utilities.startedVault2BossRush);

            reader.ReadFlags(out vaultQueenSlime, out vaultQueenSlimeDR, out vaultSeasonalBosses, out vaultSeasonalBossesDR, out vaultSkeletronPrime, out vaultSkeletronPrimeDR, out vaultBetsy, out vaultBetsyDR);
            reader.ReadFlags(out vaultPlantera, out vaultPlanteraDR, out vaultGolem, out vaultGolemDR, out vaultDukeFishron, out vaultDukeFishronDR, out vaultEmpress, out vaultEmpressDR);
            reader.ReadFlags(out vaultLunaticCultist, out vaultLunaticCultistDR, out vaultMoonLord, out vaultMoonLordDR);
		}

		public override void SaveWorldData(TagCompound tag)
		{
            tag["startedVault1BossRush"] = Utilities.Utilities.startedVault1BossRush;
            tag["startedVault2BossRush"] = Utilities.Utilities.startedVault2BossRush;

            tag["countdown"] = countdown;
            tag["bossTimer"] = bossTimer;

            tag["vaultKingSlime"] = vaultKingSlime;
            tag["vaultKingSlimeDR"] = vaultKingSlimeDR; // DR = Don't Respawn
            tag["vaultEyeOfCthulhu"] = vaultEyeOfCthulhu;
            tag["vaultEyeOfCthulhuDR"] = vaultEyeOfCthulhuDR;
            tag["vaultQueenBee"] = vaultQueenBee;
            tag["vaultQueenBeeDR"] = vaultQueenBeeDR;
            tag["vaultDeerClops"] = vaultDeerClops;
            tag["vaultDeerClopsDR"] = vaultDeerClopsDR;
            tag["vaultSkeletron"] = vaultSkeletron;
            tag["vaultSkeletronDR"] = vaultSkeletronDR;

            tag["vaultQueenSlime"] = vaultQueenSlime;
            tag["vaultQueenSlimeDR"] = vaultQueenSlimeDR;
            tag["vaultSeasonalBosses"] = vaultSeasonalBosses;
            tag["vaultSeasonalBossesDR"] = vaultSeasonalBossesDR;
            tag["vaultSkeletronPrime"] = vaultSkeletronPrime;
            tag["vaultSkeletronPrimeDR"] = vaultSkeletronPrimeDR;
            tag["vaultBetsy"] = vaultBetsy;
            tag["vaultBetsyDR"] = vaultBetsyDR;
            tag["vaultPlantera"] = vaultPlantera;
            tag["vaultPlanteraDR"] = vaultPlanteraDR;
            tag["vaultGolem"] = vaultGolem;
            tag["vaultGolemDR"] = vaultGolemDR;
            tag["vaultDukeFishron"] = vaultDukeFishron;
            tag["vaultDukeFishronDR"] = vaultDukeFishronDR;
            tag["vaultEmpress"] = vaultEmpress;
            tag["vaultEmpressDR"] = vaultEmpressDR;
            tag["vaultLunaticCultist"] = vaultLunaticCultist;
            tag["vaultLunaticCultistDR"] = vaultLunaticCultistDR;
            tag["vaultMoonLord"] = vaultMoonLord;
            tag["vaultMoonLordDR"] = vaultMoonLordDR;
		}

		public override void LoadWorldData(TagCompound tag)
		{
            Utilities.Utilities.startedVault1BossRush = tag.GetBool("startedVault1BossRush");
            Utilities.Utilities.startedVault2BossRush = tag.GetBool("startedVault2BossRush");

            countdown = tag.GetInt("countdown");
            bossTimer = tag.GetInt("bossTimer");

            vaultKingSlime = tag.GetBool("vaultKingSlime");
            vaultKingSlimeDR = tag.GetBool("vaultKingSlimeDR");
            vaultEyeOfCthulhu = tag.GetBool("vaultEyeOfCthulhu");
            vaultEyeOfCthulhuDR = tag.GetBool("vaultEyeOfCthulhuDR");
            vaultQueenBee = tag.GetBool("vaultQueenBee");
            vaultQueenBeeDR = tag.GetBool("vaultQueenBeeDR");
            vaultDeerClops = tag.GetBool("vaultDeerClops");
            vaultDeerClopsDR = tag.GetBool("vaultDeerClopsDR");
            vaultSkeletron = tag.GetBool("vaultSkeletron");
            vaultSkeletronDR = tag.GetBool("vaultSkeletronDR");

            vaultQueenSlime = tag.GetBool("vaultQueenSlime");
            vaultQueenSlimeDR = tag.GetBool("vaultQueenSlimeDR");
            vaultSeasonalBosses = tag.GetBool("vaultSeasonalBosses");
            vaultSeasonalBossesDR = tag.GetBool("vaultSeasonalBossesDR");
            vaultSkeletronPrime = tag.GetBool("vaultSkeletronPrime");
            vaultSkeletronPrimeDR = tag.GetBool("vaultSkeletronPrimeDR");
            vaultBetsy = tag.GetBool("vaultBetsy");
            vaultBetsyDR = tag.GetBool("vaultBetsyDR");
            vaultPlantera = tag.GetBool("vaultPlantera");
            vaultPlanteraDR = tag.GetBool("vaultPlanteraDR");
            vaultGolem = tag.GetBool("vaultGolem");
            vaultGolemDR = tag.GetBool("vaultGolemDR");
            vaultDukeFishron = tag.GetBool("vaultDukeFishron");
            vaultDukeFishronDR = tag.GetBool("vaultDukeFishronDR");
            vaultEmpress = tag.GetBool("vaultEmpress");
            vaultEmpressDR = tag.GetBool("vaultEmpressDR");
            vaultLunaticCultist = tag.GetBool("vaultLunaticCultist");
            vaultLunaticCultistDR = tag.GetBool("vaultLunaticCultistDR");
            vaultMoonLord = tag.GetBool("vaultMoonLord");
            vaultMoonLordDR = tag.GetBool("vaultMoonLordDR");
		}

		public override void ClearWorld()
		{
            Utilities.Utilities.startedVault1BossRush = false;
            Utilities.Utilities.startedVault2BossRush = false;
            
            countdown = 600;
            bossTimer = countdown;

            vaultKingSlime = false;
            vaultKingSlimeDR = false;
            vaultEyeOfCthulhu = false;
            vaultEyeOfCthulhuDR = false;
            vaultQueenBee = false;
            vaultQueenBeeDR = false;
            vaultDeerClops = false;
            vaultDeerClopsDR = false;
            vaultSkeletron = false;
            vaultSkeletronDR = false;

            vaultQueenSlime = false;
            vaultQueenSlimeDR = false;
            vaultSeasonalBosses = false;
            vaultSeasonalBossesDR = false;
            vaultSkeletronPrime = false;
            vaultSkeletronPrimeDR = false;
            vaultBetsy = false;
            vaultBetsyDR = false;
            vaultPlantera = false;
            vaultPlanteraDR = false;
            vaultGolem = false;
            vaultGolemDR = false;
            vaultDukeFishron = false;
            vaultDukeFishronDR = false;
            vaultEmpress = false;
            vaultEmpressDR = false;
            vaultLunaticCultist = false;
            vaultLunaticCultistDR = false;
            vaultMoonLord = false;
            vaultMoonLordDR = false;
		}
	}
}