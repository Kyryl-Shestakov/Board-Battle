using System;
using UnityEngine;
using Utility.CardUtility;

namespace Actions
{
    public class FireSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            Func<CardManagement, int> rankResolver = card => card.CardStats.FireRank;
            //Func<CardManagement, CardManagement, WinningResolution> winningDetermination =
            //    ObtainWinningDetermination(rankResolver);
            Color color = Color.red;
            string material = "Fire Material";

            PickCardsForTheBattle("Fire", color, material, rankResolver, postAction);
        }
    }
}