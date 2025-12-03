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
    public override void OnKill(NPC npc)
    {
        base.OnKill(npc);
        
        int playerWhoKilledNPC = npc.lastInteraction;
        Player player = Main.player[playerWhoKilledNPC];

        if(player != null)
        {
            ActivateKillSkill(player, ModContent.ItemType<FollowThrough>(), ModContent.BuffType<FollowThroughKillSkill>(), 7);
        }
    }

    private void ActivateKillSkill(Player player, int skillAccessory, int buff, int duration)
    {
        if(IsWearing(player, skillAccessory))
        {
            AddBuff(player, buff, duration);
        }
    }

    private void AddBuff(Player player, int buff, int seconds)
    {
        seconds *= 60;
        player.AddBuff(buff, seconds + 60);
    }

    private bool IsWearing(Player player, int accessory)
    {
        // Ignore empty accessory slots and check if the player is wearing the accessory
        for (int i = 0; i < 8 + player.extraAccessorySlots; i++)
        {
            if (player.armor[i].ModItem != null && player.armor[i].ModItem.Type == accessory)
            {
                return true;
            }
        }

        return false;
    }
}