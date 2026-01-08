using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Systems.GenPasses.Vaults;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Summoner.Sentry;
using System.Collections;
using System.Collections.Generic;

namespace Vaultaria.Common.Globals
{
    public class VaultTiles : GlobalTile
    {
        public override bool CanExplode(int i, int j, int type)
        {
            if(IsInVault(i, j))
            {
                return false;
            }

            return base.CanExplode(i, j, type);
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if(IsInVault(i, j))
            {
                return false;
            }

            return base.CanKillTile(i, j, type, ref blockDamaged);
        }

        public override bool CanPlace(int i, int j, int type)
        {
            if(IsInVault(i, j))
            {
                return false;
            }

            return base.CanPlace(i, j, type);
        }

        public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
        {
            if(IsInVault(i, j))
            {
                return false;
            }

            return base.CanReplace(i, j, type, tileTypeBeingPlaced);
        }

        private bool IsInVault(int i, int j)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                return true;
            }

            if(Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j))
            {
                return true;
            }

            if(Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i + 2, j - 2))
            {
                return true;
            }

            return false;
        }
    }

    public class VaultWalls : GlobalWall
    {
        public override bool CanExplode(int i, int j, int type)
        {
            if(IsInVault(i, j))
            {
                return false;
            }

            return base.CanExplode(i, j, type);
        }

        public override void KillWall(int i, int j, int type, ref bool fail)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                fail = true;
            }

            Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j, ref fail);
            Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i, j, ref fail);
        }

        public override bool CanPlace(int i, int j, int type)
        {
            if(IsInVault(i, j))
            {
                return false;
            }

            return base.CanPlace(i, j, type);
        }

        private bool IsInVault(int i, int j)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                return true;
            }

            if(Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j))
            {
                return true;
            }

            if(Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i, j))
            {
                return true;
            }

            return false;
        }
    }

    public class VaultAccessories : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                if(item.type == ItemID.RodofDiscord || item.type == ItemID.RodOfHarmony) // No Teleporting
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

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                if(item.type == ItemID.RodofDiscord || item.type == ItemID.RodOfHarmony)
                {
                    Utilities.Utilities.Text(tooltips, Mod, "vault", "This item is unusable when inside either Vault");
                }
            }
        }
        
        // public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        // {
        //     base.UpdateAccessory(item, player, hideVisual);

        //     FindArea(player);
        // }

        // private void FindArea(Player player)
        // {
        //     if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
        //     {
        //         int chlorophyteX = -1;
        //         int chlorophyteY = -1;
        //         int titaniumX = -1;
        //         int titaniumY = -1;

        //         for(int i = 0; i < Main.maxTilesX; i++)
        //         {
        //             for(int j = 0; j < Main.maxTilesY; j++)
        //             {
        //                 Tile tile = Main.tile[i, j];

        //                 if(tile.TileType == TileID.Chlorophyte)
        //                 {
        //                     chlorophyteX = i;
        //                     chlorophyteY = j;
        //                 }

        //                 if(tile.TileType == TileID.Titanium)
        //                 {
        //                     titaniumX = i;
        //                     titaniumY = j;
        //                 }
        //             }
        //         }

        //         Vector2 chlorophytePosition = new Vector2(chlorophyteX * 16f, chlorophyteY * 16f);
        //         Vector2 titaniumPosition = new Vector2(titaniumX * 16f, titaniumY * 16f);

        //         if(chlorophytePosition.X < player.Center.X && player.Center.Y > titaniumPosition.Y)
        //         {
        //             DeactivateAccessories(player);
        //         }
        //     }
        // }

        // private void DeactivateAccessories(Player player)
        // {
        //     player.canRocket = false;
        //     player.canCarpet = false;
        //     player.rocketBoots = -1;
        //     player.rocketTime = 0;
        //     player.tryKeepingHoveringDown = false;
        //     player.tryKeepingHoveringUp = false;
        //     player.empressBrooch = false;
        //     player.wingTime = 0;
        //     player.sandStorm = false;

        //     player.waterWalk = false;
        //     player.waterWalk2 = false;
        //     player.fireWalk = false;
        //     player.iceSkate = false;
        //     player.accFlipper = false;

        //     // 1. Force the player to drop the current hook line (if one is deployed).
        //     player.grappling[0] = -1; 
        // }
    }
}