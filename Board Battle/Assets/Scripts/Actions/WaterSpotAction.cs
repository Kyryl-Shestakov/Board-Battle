using System;
using UnityEngine;
using Utility.CardUtility;

namespace Actions
{
    public class WaterSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            Func<CardManagement, int> rankResolver = card => card.CardStats.WaterRank;
            //Func<CardManagement, CardManagement, WinningResolution> winningDetermination =
            //    ObtainWinningDetermination(rankResolver);
            Color color = Color.blue;
            string material = "Water Material";

            PickCardsForTheBattle("Water", color, material, rankResolver, postAction);
        }
    }
}