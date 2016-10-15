using System;
using Battle;

namespace Utility.CardUtility
{
    public class OpponentWinningResolution : WinningResolution
    {
        public int ForwardStep { get; private set; }
        public int BackwardStep { get; private set; }

        public OpponentWinningResolution(int forwardStep, int backwardStep)
        {
            ForwardStep = forwardStep;
            BackwardStep = backwardStep;
        }

        public override void Resolve(BattleOutcomeHandling currentPawnBattleOutcomeHandler, Action postAction)
        {
            currentPawnBattleOutcomeHandler.HandleOpponentWinning(ForwardStep, BackwardStep, postAction);
        }
    }
}
