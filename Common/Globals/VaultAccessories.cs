using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Globals
{
    public class VaultAccessories : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if(SubworldLibrary.SubworldSystem.AnyActive())
            {
                // No Teleporting
                if(item.type == ItemID.RodofDiscord || item.type == ItemID.RodOfHarmony)
                {
                    return false;
                }

                // No External Boss Summoning
                if(item.type == ItemID.SlimeCrown || item.type == ItemID.SuspiciousLookingEye || item.type == ItemID.Abeemination || item.type == ItemID.DeerThing || item.type == ItemID.QueenSlimeCrystal || item.type == ItemID.MechanicalEye || item.type == ItemID.MechanicalWorm || item.type == ItemID.MechanicalSkull || item.type == ItemID.MechdusaSummon || item.type == ItemID.LihzahrdPowerCell || item.type == ItemID.CelestialSigil)
                {
                    return false;
                }
            }

            return base.CanUseItem(item, player);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            base.UpdateAccessory(item, player, hideVisual);

            FindArea(player);
        }

        private void FindArea(Player player)
        {
            if(SubworldLibrary.SubworldSystem.AnyActive())
            {
                int chlorophyteX = -1;
                int chlorophyteY = -1;
                int titaniumX = -1;
                int titaniumY = -1;

                for(int i = 0; i < Main.maxTilesX; i++)
                {
                    for(int j = 0; j < Main.maxTilesY; j++)
                    {
                        Tile tile = Main.tile[i, j];

                        if(tile.TileType == TileID.Chlorophyte)
                        {
                            chlorophyteX = i;
                            chlorophyteY = j;
                        }

                        if(tile.TileType == TileID.Titanium)
                        {
                            titaniumX = i;
                            titaniumY = j;
                        }
                    }
                }

                Vector2 chlorophytePosition = new Vector2(chlorophyteX * 16f, chlorophyteY * 16f);
                Vector2 titaniumPosition = new Vector2(titaniumX * 16f, titaniumY * 16f);

                if(chlorophytePosition.X < player.Center.X && player.Center.Y > titaniumPosition.Y)
                {
                    DeactivateAccessories(player);
                }
            }
        }

        private void DeactivateAccessories(Player player)
        {
            player.canRocket = false;
            player.canCarpet = false;
            player.rocketBoots = -1;
            player.rocketTime = 0;
            player.tryKeepingHoveringDown = false;
            player.tryKeepingHoveringUp = false;
            player.empressBrooch = false;
            player.wingTime = 0;
            player.sandStorm = false;

            player.waterWalk = false;
            player.waterWalk2 = false;
            player.fireWalk = false;
            player.iceSkate = false;
            player.accFlipper = false;

            // 1. Force the player to drop the current hook line (if one is deployed).
            player.grappling[0] = -1; 
        }
    }
}