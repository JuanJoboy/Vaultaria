using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Potions;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Maliwan;

namespace ExampleMod.Common.GlobalNPCs
{
	class NPCShop : GlobalNPC
	{
        public override void ModifyShop(Terraria.ModLoader.NPCShop shop)
        {
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                shop.Add<PistolAmmo>();
                shop.Add<SubmachineGunAmmo>();
                shop.Add<AssaultRifleAmmo>();
                shop.Add<ShotgunAmmo>();
                shop.Add<SniperAmmo>();
                shop.Add<LauncherAmmo>();
            }

            if (shop.NpcType == NPCID.Wizard)
            {
                shop.Add<DeceptionPotion>();
            }

            if (shop.NpcType == NPCID.SkeletonMerchant)
            {
                shop.Add<Volcano>(Condition.Hardmode, Condition.NearLava);
            }
        }
	}
}