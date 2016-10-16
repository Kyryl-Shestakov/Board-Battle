using System;
using UnityEngine;
using Utility.CardUtility;

namespace Battle
{
    public class FireSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            Func<CardManagement, int> rankResolver = card => card.CardStats.FireRank;
            Func<CardManagement, CardManagement, WinningResolution> winningDetermination =
                ObtainWinningDetermination(rankResolver);
            
            PickCardsForTheBattle("Fire", winningDetermination, postAction);
        }
    }
}