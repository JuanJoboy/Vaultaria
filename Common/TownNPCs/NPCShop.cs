using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Ammo;

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
        }
	}
}