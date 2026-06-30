// using Terraria;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;
// using Terraria.ID;
// using Microsoft.Xna.Framework.Graphics;
// using Vaultaria.Common.Utilities;
// using Vaultaria.Common.Configs;
// using Terraria.ModLoader.IO;
// using System.IO;

// public class ModifierSettings
// {
//     public required string ModifierName;
//     public bool ColourEnabled;
//     public Color? ModifiedColour;
//     public int? ModifiedDust;
//     public float SpawnChance;
//     public int LifeMult, DamageMult, DefMult;
//     public float ScaleMult;
//     public bool RequiresHardmode;
// }

// public abstract class ModifiedNPC : GlobalNPC
// {
//     protected ModifierSettings Settings;
//     internal bool Modifier { get; set; } = false;
//     public override bool InstancePerEntity => true;

//     public override void SetDefaults(NPC npc)
//     {
//         base.SetDefaults(npc);

//         if(Main.netMode != NetmodeID.MultiplayerClient)
//         {
//             bool boss = npc.boss || npc.type == NPCID.Pumpking || npc.type == NPCID.IceQueen;

//             if(npc.type != NPCID.TargetDummy && boss == false && !npc.townNPC && !NPCID.Sets.CountsAsCritter[npc.type])
//             {
//                 if (Utilities.Randomizer(Settings.SpawnChance))
//                 {
//                     if (!Settings.RequiresHardmode || Main.hardMode)
//                     {
//                         ApplyModifier(npc);
//                     }
//                 }
//             }
//         }
//     }

//     private void ApplyModifier(NPC npc)
//     {
//         Modifier = true; // Set your internal state

//         npc.lifeMax *= Settings.LifeMult;
//         npc.damage *= Settings.DamageMult;
//         npc.defense *= Settings.DefMult;

//         npc.scale *= Settings.ScaleMult;
//         npc.width *= (int) Settings.ScaleMult;
//         npc.height *= (int) Settings.ScaleMult;

//         npc.netUpdate = true;
//     }

//     public override void ModifyTypeName(NPC npc, ref string typeName)
//     {
//         base.ModifyTypeName(npc, ref typeName);

//         typeName = $"{Settings.ModifierName} {typeName}";
//     }

//     public override void DrawEffects(NPC npc, ref Color drawColor)
//     {
//         base.DrawEffects(npc, ref drawColor);

//         if(Modifier && Settings.ColourEnabled)
//         {
//             drawColor = Settings.ModifiedColour ?? Color.Transparent;
//             Dust.NewDust(npc.position, npc.width, npc.height, Settings.ModifiedDust ?? DustID.WhiteTorch);
//         }
//     }

//     public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter writer)
//     {
//         writer.Write(Modifier);

//         writer.Write(npc.lifeMax);
//         writer.Write(npc.damage);
//         writer.Write(npc.defense);

//         writer.Write(npc.scale);
//         writer.Write(npc.width);
//         writer.Write(npc.height);
//     }

//     public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader reader)
//     {
//         Modifier = reader.ReadBoolean();

//         npc.lifeMax = reader.ReadInt32();
//         npc.damage = reader.ReadInt32();
//         npc.defense = reader.ReadInt32();

//         npc.scale = reader.ReadSingle();
//         npc.width = reader.ReadInt32();
//         npc.height = reader.ReadInt32();
//     }
// }