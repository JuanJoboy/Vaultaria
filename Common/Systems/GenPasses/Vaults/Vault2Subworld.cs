using SubworldLibrary;
using Terraria;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Magic;

namespace Vaultaria.Common.Systems.GenPasses.Vaults
{
	public class Vault2Subworld : Subworld
	{
		public override int Width => 1000;
		public override int Height => 1000;

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => true;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new Vault2GenPass()
		};

        public override void Update()
        {
            base.Update();

			Main.dayTime = false;
			Main.time = Main.nightLength;
			Main.dayRate = 0;
        }
	}
}