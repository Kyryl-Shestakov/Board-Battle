using System;
using UnityEngine;
using Utility.CardUtility;

namespace Battle
{
    public class AirSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            Func<CardManagement, int> rankResolver = card => card.CardStats.AirRank;
            Func<CardManagement, CardManagement, WinningResolution> winningDetermination =
                ObtainWinningDetermination(rankResolver);

            //TODO: Determine if something needs to be added to postAction
            PickCardsForTheBattle("Air", winningDetermination, postAction);
        }
    }
}