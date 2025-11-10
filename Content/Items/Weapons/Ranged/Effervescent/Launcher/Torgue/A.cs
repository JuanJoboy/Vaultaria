using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Effervescent.Launcher.Torgue;
using Vaultaria.Common.Systems;

namespace Vaultaria.Content.Items.Weapons.Ranged.Effervescent.Launcher.Torgue
{
    public class A : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.rare = ItemRarityID.Expert;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 45;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Item.UseSound = SoundID.Item62;
        }

        public override bool? UseItem(Player player)
        {
            // Toggle the visibility of the UI when the item is used

            if (CustomUISystem.Visible == false)
            {
                CustomUISystem.Visible = true;

                if(Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.gamePaused = true;
                }
            }
            else
            {
                CustomUISystem.Visible = false;
                
                if(Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.gamePaused = false;
                }
            }

            // Return true to signal the item use was successful.
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-60f, 0f);
        }
    }
}