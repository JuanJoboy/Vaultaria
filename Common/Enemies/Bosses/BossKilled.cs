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
        public void DisplayStatusMessage(Vector2 position, Color colour, string msg)
        {
            // Display the text at the position
            CombatText.NewText(
                new Rectangle((int)position.X, (int)position.Y, 1, 1), 
                colour, // The color of the text (e.g., gold)
                msg, // The message you want to display
                dramatic: true, // Optional: Makes the text larger and appear more impactful
                dot: false
            );
        }

        public override void OnKill(NPC npc)
        {
            // Ensure this logic only runs on the server in multiplayer, or locally in singleplayer.
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // if (npc.type == NPCID.SkeletronHead)
                // {
                //     if (BossDownedSystem.skeletron == false)
                //     {
                //         DisplayStatusMessage(npc.Center, Color.Gold, "Masher Prefix Unlocked!");
                        
                //         // Set the static flag and sync the event flag cleared status
                //         BossDownedSystem.skeletron = true;
                //         NPC.SetEventFlagCleared(ref BossDownedSystem.skeletron, -1);
                //     }
                // }

                if (npc.type == NPCID.Deerclops)
                {
                    if (BossDownedSystem.deerClops == false)
                    {
                        DisplayStatusMessage(npc.Center, Color.Gold, "Double Penetrating Prefix Unlocked!");
                        
                        BossDownedSystem.deerClops = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.deerClops, -1);
                    }
                }

                if (npc.type == NPCID.WallofFlesh)
                {
                    if (BossDownedSystem.wallOfFlesh == false)
                    {
                        DisplayStatusMessage(npc.Center, Color.Violet, "Slag Prefix Unlocked!");
                        
                        BossDownedSystem.wallOfFlesh = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.wallOfFlesh, -1);
                    }
                }

                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (BossDownedSystem.twins == false)
                    {
                        DisplayStatusMessage(npc.Center, Color.YellowGreen, "Radiation Prefix Unlocked!");
                        
                        BossDownedSystem.twins = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.twins, -1);
                    }
                }

                if (npc.type == NPCID.IceGolem)
                {
                    if (BossDownedSystem.iceGolem == false)
                    {
                        DisplayStatusMessage(npc.Center, Color.LightBlue, "Cryo Prefix Unlocked!");
                        
                        BossDownedSystem.iceGolem = true;
                        NPC.SetEventFlagCleared(ref BossDownedSystem.iceGolem, -1);
                    }
                }
            }
        }
    }
}