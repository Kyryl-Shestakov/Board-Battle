using System;
using Actions;

namespace Utility.CardUtility
{
    public class TieWinningResolution : WinningResolution
    {

        public override void Resolve(BattleOutcomeHandling currentPawnBattleOutcomeHandler, Action postAction)
        {
            currentPawnBattleOutcomeHandler.HandleTie(postAction);
        }
    }
}
