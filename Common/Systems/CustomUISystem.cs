using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Vaultaria.Common.UI;
using Terraria; // Reference your UI state

namespace Vaultaria.Common.Systems
{
    public class CustomUISystem : ModSystem
    {
        // Holds the visual UI component
        public UserInterface? CustomInterface;
        public SkillTree? SkillTree;

        // Tracks visibility
        public static bool Visible = false;

        public override void Load()
        {
            SkillTree = new SkillTree();
            SkillTree.Activate(); 
            CustomInterface = new UserInterface();
            CustomInterface.SetState(SkillTree); // Ensure the interface is set to draw the state
        }

        public override void UpdateUI(GameTime gameTime)
        {
            // Only update the interface if it is visible
            if (Visible)
            {
                CustomInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryLayerIndex != -1)
            {
                // Insert the custom layer just before the inventory
                layers.Insert(inventoryLayerIndex, new LegacyGameInterfaceLayer(
                    "Vaultaria: Skill Tree",
                    delegate
                    {
                        if (Visible)
                        {
                            CustomInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}