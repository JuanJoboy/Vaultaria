using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Vaultaria.Common.Configs
{
    // Define the scope: ServerSide because everything here needs to be in sync with the server and clients
    public class VaultariaConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // --- Optional Boolean Setting ---

        // [ReloadRequired] // Marking it with [ReloadRequired] makes tModLoader force a mod reload if the option is changed. It should be used for things like item toggles, which only take effect during mod loading
        [DefaultValue(false)]
        public bool EnableOldSawbarExplosion;

        [DefaultValue(false)]
        public bool GetRuinFirst;
        
        [DefaultValue(false)]
        public bool KeepMinionSizeTheSameWhenGammaBursting;

        [DefaultValue(false)]
        public bool KeepBossSizeTheSameWhenBossRushing;

        // --- Optional Integer Setting ---
        [DefaultValue(1)] // Default value is 1 (no multiplier)
        [Range(1, 10)] // Optional: Defines the min/max slider range in the UI
        public int EridiumDropRateMultiplier;

        [DefaultValue(1)]
        [Range(1, 3)]
        public int VaultHunterMode;

        // --- Optional Float Setting (e.g., for damage) ---

        [DefaultValue(1.1f)] // Default value is 1.1 (10% increase)
        [Range(1.0f, 3.0f)]
        [Increment(0.1f)] // Optional: Sets the slider step size
        public float SlagDamageMultiplier;
    }
}