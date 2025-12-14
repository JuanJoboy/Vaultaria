using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Projectiles.Shields;
using Terraria.DataStructures;
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using Vaultaria.Content.Buffs.MagicEffects;
using Microsoft.CodeAnalysis;
using Vaultaria.Common.Configs;

namespace Vaultaria.Content.Projectiles.Magic
{
    public class PhaselockBubble : ElementalProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(54, 54);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Config
            Projectile.timeLeft = 3600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            base.AI();
            Player player = Main.player[Projectile.owner];
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();
            
            NPC target = FindTarget();

            if (target != null && target.active && !target.friendly)
            {
                Utilities.MoveToPosition(Projectile, target.Center, 4, 1);

                float distance = Vector2.Distance(Projectile.Center, target.Center);

                // Simple collision approximation: when close enough
                if (distance < (Projectile.width + target.width) * 0.5f)
                {
                    if (!target.HasBuff(ModContent.BuffType<Phaselocked>()))
                    {
                        target.AddBuff(ModContent.BuffType<Phaselocked>(), 300);

                        if(config.GetRuinFirst && Main.hardMode)
                        {
                            SetElements(player, target);
                        }
                        else if(!config.GetRuinFirst && Main.hardMode && NPC.downedMoonlord)
                        {
                            SetElements(player, target);
                        }
    
                        if(NPC.downedFishron)
                        {
                            PullInEnemies(target);
                        }
                    }

                    Projectile.Kill();
                }
            }
        }

        private void PullInEnemies(NPC target)
        {
            foreach(NPC npc in Main.ActiveNPCs)
            {
                if(Vector2.Distance(npc.Center, target.Center) < 1000 && !npc.townNPC)
                {
                    Utilities.MoveToPosition(npc, target.Center, 40f, 6f);
                }
            }
        }

        private NPC FindTarget()
        {
            float range = 500f; // 500 pixels
            NPC closest = null;

            // Loops through every NPC in the world
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this)) // Filters to only hostile and valid targets
                {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center);
                    if (dist < range)
                    {
                        closest = npc;
                        range = dist;
                    }
                }
            }

            return closest; // Returns the best valid NPC target, or null if none found
        }
    }
}