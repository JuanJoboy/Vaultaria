using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using static Vaultaria.Common.Utilities.Utilities;

namespace Vaultaria.Common.Utilities
{
    public abstract class ElementalItem : ModItem
    {
        // virtual: This is a "permission" keyword. It tells the compiler: "This property has a default value (null), but child classes are allowed to change it."
        // => null: This is an expression-bodied getter. It is shorthand for get { return null; }. It ensures that by default, an item has no randomized sounds.
        protected virtual Sounds[]? ItemSounds => null;

        public override bool? UseItem(Player player)
        {
            if(ItemSounds != null) // Needs a null check cause the array itself is null
            {
                SoundVariator(Item, ItemSounds);
            }

            return base.UseItem(player);
        }

        private void SoundVariator(Item item, Sounds[] sounds, Sounds fallBackSound = Sounds.BanditPistol, int instances = 60)
        {
            if(sounds == null || sounds.Length == 0) // If the array doesn't exist at all (which technically shouldn't happen since its initialized as null immediately above) OR if the array exists and is empty
            {
                if(item.UseSound != null) // If the item's native sound isn't null, then use that sound as the fallback
                {
                    SetItemSound(item, item.UseSound);
                }
                else // If the array is null and the item doesn't have a native sound, then use a fallback sound. The fallback defaults to BanditPistol if it isn't defined
                {
                    Utilities.SetItemSound(item, fallBackSound, instances);
                }

                return; // Immediately return to not do the next stuff
            }

            int chosenSound = Main.rand.Next(sounds.Length); // If the array does have values, then get a random sound from the array
            Utilities.SetItemSound(item, sounds[chosenSound], instances); // Now set the item's sound to that random value whenever the item is used
        }

        // This method acts as a dedicated setter (Encapsulation). 
        // By passing the Item and SoundStyle through this "pipeline," I create a formal interface for modifying audio state. 
        // This makes the code more maintainable and readable than a direct self-assignment, ensuring all sound updates follow the same standardized path.
        private void SetItemSound(Item item, SoundStyle? sound)
        {
            item.UseSound = sound;
        }
    }
}