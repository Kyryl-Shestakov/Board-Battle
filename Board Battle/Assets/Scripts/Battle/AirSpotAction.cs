using System;
using UnityEngine;
using Utility.CardUtility;

namespace Battle
{
    public class AirSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            Func<CardManagement, CardManagement, WinningResolution> winningDetermination =
                (playerCard, opponentCard) =>
                {
                    WinningResolution winningResolver;


                    if (playerCard.CardStats.AirRank > opponentCard.CardStats.AirRank)
                    {
                        winningResolver = new PlayerWinningResolution(playerCard.CardStats.ForwardStepCount,
                            playerCard.CardStats.BackwardStepCount);
                    }
                    else if (playerCard.CardStats.AirRank < opponentCard.CardStats.AirRank)
                    {
                        winningResolver = new OpponentWinningResolution(playerCard.CardStats.ForwardStepCount,
                            playerCard.CardStats.BackwardStepCount);
                    }
                    else
                    {
                        winningResolver = new TieWinningResolution();
                    }

                    return winningResolver;
                };
            //TODO: Determine if something needs to be added to postAction
            PickCardsForTheBattle("Air", winningDetermination, postAction);
        }
    }
}