using Terraria;
using Terraria.ModLoader;

public abstract class ElementalProjectile : ModProjectile
{
    public static bool SetElementalChance(float chance)
    {
        if (Main.rand.Next(1, 101) <= chance)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Set the chance and damage that an element will do.
    /// <br/> To use chance, put in a float from 1 - 100. So if you put in 23.5, there would be a 23.5% elemental chance.
    /// <br/> To use multiplier, put in a float that you want to use to multiply the amount of damage that hits an opponent.
    /// </summary>
    /// <param name="chance">The chance percentage</param>
    /// <param name="multiplier">The amount of damage the element will be multiplied by</param>
    public static void SetElementalDamage(float damage, float multiplier, out int elementalDamage)
    {
        elementalDamage = (int)(damage * multiplier);
    }
}