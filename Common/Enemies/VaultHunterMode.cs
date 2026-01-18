using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Common.Utilities;
using Vaultaria.Common.Configs;

public class VaultHunterMode : GlobalNPC
{
    public override void SetDefaults(NPC entity)
    {
        VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();
        
        base.SetDefaults(entity);

        if(entity.type != NPCID.DungeonGuardian)
        {
            entity.lifeMax *= config.VaultHunterMode;
        }
    }
}