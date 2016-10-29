using System;
using UnityEngine;
using Utility.CardUtility;

namespace Actions
{
    public class EarthSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            Func<CardManagement, int> rankResolver = card => card.CardStats.EarthRank;
            //Func<CardManagement, CardManagement, WinningResolution> winningDetermination =
            //    ObtainWinningDetermination(rankResolver);
            Color color = Color.green;
            string material = "Earth Material";

            PickCardsForTheBattle("Earth", color, material, rankResolver, postAction);
        }
    }
}