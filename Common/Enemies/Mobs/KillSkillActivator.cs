using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Content.Buffs.PotionEffects;
using Vaultaria.Content.Buffs.SkillEffects;
using Vaultaria.Content.Items.Accessories.Skills;

public class KillSkillActivator : GlobalNPC
{
    public int lastRealHitPlayer = -1;

    public override bool InstancePerEntity => true;

    public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitByItem(npc, player, item, hit, damageDone);

        lastRealHitPlayer = player.whoAmI;
    }

    public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitByProjectile(npc, projectile, hit, damageDone);

        if(projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
        {
            lastRealHitPlayer = projectile.owner;
        }
    }

    public override void HitEffect(NPC npc, NPC.HitInfo hit)
    {
        base.HitEffect(npc, hit);

        Player? player = GetPlayer();

        if(player != null && npc.life <= hit.Damage)
        {
            ActivateAllKillSkills(player);
        }
    }

    private Player? GetPlayer()
    {
        if(lastRealHitPlayer >= 0 && lastRealHitPlayer < Main.maxPlayers)
        {
            Player player = Main.player[lastRealHitPlayer];

            if(player != null && player.active && !player.dead)
            {
                return player;
            }
        }

        return null;
    }

    private void ActivateAllKillSkills(Player player)
    {
        ActivateKillSkill(player, ModContent.ItemType<FollowThrough>(), ModContent.BuffType<FollowThroughKillSkill>(), 7);
        ActivateKillSkill(player, ModContent.ItemType<LegendaryNinja>(), ModContent.BuffType<FollowThroughKillSkill>(), 7);

        ActivateKillSkill(player, ModContent.ItemType<Killer>(), ModContent.BuffType<KillerKillSkill>(), 7);
        ActivateKillSkill(player, ModContent.ItemType<LegendaryKiller>(), ModContent.BuffType<KillerKillSkill>(), 7);

        ActivateKillSkill(player, ModContent.ItemType<Salvation>(), ModContent.BuffType<SalvationKillSkill>(), 5);

        ActivateKillSkill(player, ModContent.ItemType<ViolentSpeed>(), ModContent.BuffType<ViolentSpeedKillSkill>(), 8);
        ActivateKillSkill(player, ModContent.ItemType<Antifreeze>(), ModContent.BuffType<ViolentSpeedKillSkill>(), 8);

        ActivateKillSkill(player, ModContent.ItemType<QuickCharge>(), ModContent.BuffType<QuickChargeKillSkill>(), 7);
        ActivateKillSkill(player, ModContent.ItemType<LegendaryRanger>(), ModContent.BuffType<QuickChargeKillSkill>(), 7);

        ActivateKillSkill(player, ModContent.ItemType<MetalStorm>(), ModContent.BuffType<MetalStormKillSkill>(), 7);
        ActivateKillSkill(player, ModContent.ItemType<LegendaryRanger>(), ModContent.BuffType<MetalStormKillSkill>(), 7);

        ActivateKillSkill(player, ModContent.ItemType<Onslaught>(), ModContent.BuffType<OnslaughtKillSkill>(), 7);
        ActivateKillSkill(player, ModContent.ItemType<LegendaryRanger>(), ModContent.BuffType<OnslaughtKillSkill>(), 7);
    }

    private void ActivateKillSkill(Player player, int skillAccessory, int buff, int duration)
    {
        if(Utilities.IsWearing(player, skillAccessory))
        {
            AddBuff(player, buff, duration);
        }
    }

    private void AddBuff(Player player, int buff, int seconds)
    {
        seconds *= 60;
        player.AddBuff(buff, seconds + 60);
    }
}