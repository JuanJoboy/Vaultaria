using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Vaultaria.Common.Systems
{
    /// <summary>
    /// A custom drop rule that starts with a base pool of items and dynamically appends 
    /// extra drop options at runtime based on progression-based boolean conditions.
    /// </summary>
    public class ProgressionOneFromOptionsRule : IItemDropRule
    {
        // Required by the IItemDropRule interface for chaining drops (e.g. OnSuccess, OnFailedConditions)
        public List<IItemDropRuleChainAttempt> ChainedRules { get; }

        public int[] BaseItemIds;
        public List<(int itemId, Func<bool> condition)> ConditionalItems;

        public ProgressionOneFromOptionsRule(int[] baseItemIds)
        {
            BaseItemIds = baseItemIds;
            ConditionalItems = new List<(int, Func<bool>)>();
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        /// <summary>
        /// Registers a progressive drop that is only added to the roll pool if its condition evaluates to true.
        /// </summary>
        public void AddConditionalItem(int itemId, Func<bool> condition)
        {
            ConditionalItems.Add((itemId, condition));
        }

        public bool CanDrop(DropAttemptInfo info) => true;

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            // 1. Build our active drop pool at runtime based on currently completed milestones
            List<int> activePool = new List<int>(BaseItemIds);
            foreach (var (itemId, condition) in ConditionalItems)
            {
                if (condition())
                {
                    activePool.Add(itemId);
                }
            }

            // 2. Select exactly ONE item from our dynamically resolved active pool
            if (activePool.Count > 0)
            {
                int selectedItem = activePool[info.rng.Next(activePool.Count)];
                CommonCode.DropItem(info, selectedItem, 1);
                
                return new ItemDropAttemptResult 
                { 
                    State = ItemDropAttemptResultState.Success 
                };
            }

            return new ItemDropAttemptResult 
            { 
                State = ItemDropAttemptResultState.FailedRandomRoll 
            };
        }

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            // Simplistic Bestiary reporting: evenly distribute the chance across all potential items
            float personalChance = 1f; 
            float totalEstimatedOptions = BaseItemIds.Length + ConditionalItems.Count;
            float baseChance = personalChance / totalEstimatedOptions;

            foreach (int itemId in BaseItemIds)
            {
                drops.Add(new DropRateInfo(itemId, 1, 1, baseChance, ratesInfo.conditions));
            }
            foreach (var (itemId, condition) in ConditionalItems)
            {
                drops.Add(new DropRateInfo(itemId, 1, 1, baseChance, ratesInfo.conditions));
            }
            
            Chains.ReportDroprates(ChainedRules, personalChance, drops, ratesInfo);
        }
    }
}