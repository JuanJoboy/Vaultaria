using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Common.Utilities;
using Vaultaria.Common.Configs;
using Terraria.GameContent.ItemDropRules;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue;
using Terraria.DataStructures;
using Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.Shotgun.Hyperion;
using Terraria.ModLoader.IO;
using System.IO;

public class TubbyNPC : GlobalNPC
{
    public bool isTubby = false;
    public bool isChubby = false;
    public override bool InstancePerEntity => true;

    public override void SetDefaults(NPC npc)
    {
        base.SetDefaults(npc);

        if(npc.type != NPCID.TargetDummy && !npc.townNPC && npc.lifeMax > 10 && !NPCID.Sets.CountsAsCritter[npc.type])
        {
            if(Utilities.Randomizer(50)) // Make 0.7f;
            {
                if(Main.hardMode)
                {
                    SetTubbyDefaults(npc, ref isTubby, 3, 2, 2, 1.3f);
                }
                else
                {
                    SetTubbyDefaults(npc, ref isChubby, 2, 2, 2, 1.3f);
                }
            }   
        }
    }

    private void SetTubbyDefaults(NPC npc, ref bool tubVariant, int life, int damage, int defense, float scaler)
    {
        tubVariant = true;

        npc.lifeMax *= life;
        npc.damage *= damage;
        npc.defense *= defense;

        npc.scale *= scaler;
        npc.width *= (int) scaler;
        npc.height *= (int) scaler;

        npc.netUpdate = true;
    }

    public override void ModifyTypeName(NPC npc, ref string typeName)
    {
        base.ModifyTypeName(npc, ref typeName);

        if(isTubby)
        {
            typeName = $"Tubby {typeName}";
        }
        else if(isChubby)
        {
            typeName = $"Chubby {typeName}";
        }
    }

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        base.ModifyNPCLoot(npc, npcLoot);

        npcLoot.Add(ItemDropRule.ByCondition(new TubbyCondition(), ModContent.ItemType<Butcher>(), 1, 1, 1));

        npcLoot.Add(ItemDropRule.ByCondition(new ChubbyCondition(), ModContent.ItemType<UnkemptHarold>(), 1, 1, 1));
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        base.DrawEffects(npc, ref drawColor);

        if(isTubby || isChubby)
        {
            drawColor = Color.Gold;
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.IceTorch);
        }
    }

    public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter writer)
    {
        writer.Write(isTubby);
        writer.Write(isChubby);

        writer.Write(npc.lifeMax);
        writer.Write(npc.damage);
        writer.Write(npc.defense);

        writer.Write(npc.scale);
        writer.Write(npc.width);
        writer.Write(npc.height);
    }

    public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader reader)
    {
        isTubby = reader.ReadBoolean();
        isChubby = reader.ReadBoolean();

        npc.lifeMax = reader.ReadInt32();
        npc.damage = reader.ReadInt32();
        npc.defense = reader.ReadInt32();

        npc.scale = reader.ReadSingle();
        npc.width = reader.ReadInt32();
        npc.height = reader.ReadInt32();
    }
}