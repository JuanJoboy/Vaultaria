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
                // if (npc.type == NPCID.SkeletronHead)
                // {
                //     if (BossDownedSystem.skeletron == false)
                //     {
                //         Utilities.Utilities.DisplayStatusMessage(npc.Center, Color.Gold, "Masher Prefix Unlocked!");
                        
                //         // Set the static flag and sync the event flag cleared status
                //         BossDownedSystem.skeletron = true;
                //         NPC.SetEventFlagCleared(ref BossDownedSystem.skeletron, -1);
                //     }
                // }

                if (npc.type == NPCID.Deerclops)
                {
                    if (BossDownedSystem.deerClops == false)
                    {
                        Utilities.Utilities.DisplayStatusMessage(npc.Center, Color.Gold, "Double Penetrating Prefix Unlocked!");
                        
                        BossDownedSystem.deerClops = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.deerClops, -1);
                    }
                }

                if (npc.type == NPCID.WallofFlesh)
                {
                    if (BossDownedSystem.wallOfFlesh == false)
                    {
                        Utilities.Utilities.DisplayStatusMessage(npc.Center, Color.Violet, "Slag Prefix Unlocked!");
                        
                        BossDownedSystem.wallOfFlesh = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.wallOfFlesh, -1);
                    }
                }

                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (BossDownedSystem.twins == false)
                    {
                        Utilities.Utilities.DisplayStatusMessage(npc.Center, Color.YellowGreen, "Radiation Prefix Unlocked!");
                        
                        BossDownedSystem.twins = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.twins, -1);
                    }
                }

                if (npc.type == NPCID.IceGolem)
                {
                    if (BossDownedSystem.iceGolem == false)
                    {
                        Utilities.Utilities.DisplayStatusMessage(npc.Center, Color.LightBlue, "Cryo Prefix Unlocked!");
                        
                        BossDownedSystem.iceGolem = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.iceGolem, -1);
                    }
                }
            }
        }
    }
}