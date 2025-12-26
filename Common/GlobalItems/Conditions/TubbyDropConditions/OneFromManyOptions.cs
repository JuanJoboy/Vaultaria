using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

public class OneFromManyOptions : IItemDropRule
{
	public int[][] dropIds; // A 2D array of integers containing the Item IDs of the possible drops.
	public int chanceDenominator;
	public int chanceNumerator;

	public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

	public OneFromManyOptions(int chanceDenominator, int chanceNumerator, params int[][] options) // Allows you to pass any number of Item IDs separated by commas as the last argument.
	{
		this.chanceDenominator = chanceDenominator;
		dropIds = options;
		this.chanceNumerator = chanceNumerator;
		ChainedRules = new List<IItemDropRuleChainAttempt>();
	}

	public bool CanDrop(DropAttemptInfo info) => true;

    // The logic executed when the NPC dies.
	public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
	{
		ItemDropAttemptResult result;

        // Rolls a random number between 0 and chanceDenominator - 1. If the result is less than the numerator, the roll succeeds.
		if (info.rng.Next(chanceDenominator) < chanceNumerator)
        {
            int devSetIndex = info.rng.Next(dropIds.Length); // Gets the actual set to drop the items from

            for(int j = 0; j < dropIds[devSetIndex].Length; j++)
            {
                CommonCode.DropItem(info, dropIds[devSetIndex][j], 1); // Spawns each item from the chosen dev set from the array at the NPC's location.
            }

			result = default(ItemDropAttemptResult); // Creates a struct to return the status.
			result.State = ItemDropAttemptResultState.Success; // Signals that the roll passed and an item was dropped.

			return result; // Finalizes and returns the successful attempt.
		}

		result = default(ItemDropAttemptResult);
		result.State = ItemDropAttemptResultState.FailedRandomRoll; // If the initial if statement fails, it returns this status to signal no drop occurred.

		return result;
	}

    // Calculates what the player sees in the Bestiary.
	public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
	{
		float num = (float)chanceNumerator / (float)chanceDenominator; // Calculates the base probability of the rule passing.
		float num2 = num * ratesInfo.parentDroprateChance; // Factors in probabilities from parent rules if this rule is chained.
		float dropRate = 1f / (float)dropIds.Length * num2; // Divides the total pass chance by the number of items in the list to find the individual drop rate for each item.

        // Adds every item ID in the array to the Bestiary UI with its calculated percentage.
		for (int i = 0; i < dropIds.Length; i++)
        {
            for(int j = 0; j < dropIds[i].Length; j++)
            {
                drops.Add(new DropRateInfo(dropIds[i][j], 1, 1, dropRate, ratesInfo.conditions));
            }
		}

		Chains.ReportDroprates(ChainedRules, num, drops, ratesInfo); // Tells the UI to also display any rules chained to this one.
	}
}