using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Vaultaria.Common.Configs;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using System;

public class EridiumRule : IItemDropRule
{
    // Not used
    List<IItemDropRuleChainAttempt> IItemDropRule.ChainedRules => new List<IItemDropRuleChainAttempt>();

    public int min;
    public int max;

    // Allows for the drop to be dynamically run on the server so that it isn't just loaded once when the player loads the world on the client side
    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
        VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

        int min = this.min * config.EridiumDropRateMultiplier;
        int max = this.max * config.EridiumDropRateMultiplier;

        int amount = Main.rand.Next(min, max + 1);

        CommonCode.DropItem(info, ModContent.ItemType<Eridium>(), amount);

        return new ItemDropAttemptResult { State = ItemDropAttemptResultState.Success };
    }

    // Not Used
    public bool CanDrop(DropAttemptInfo info) { return true; }

    // Not Used
    public void ReportDroprates(List<DropRateInfo> a, DropRateInfoChainFeed b) {}
}