using System;
using Actions;

namespace Utility.CardUtility
{
    public class PlayerWinningResolution : WinningResolution
    {
        public int ForwardStep { get; private set; }
        public int BackwardStep { get; private set; }

        public PlayerWinningResolution(int forwardStep, int backwardStep)
        {
            ForwardStep = forwardStep;
            BackwardStep = backwardStep;
        }

        public override void Resolve(BattleOutcomeHandling currentPawnBattleOutcomeHandler, Action postAction)
        {
            currentPawnBattleOutcomeHandler.HandlePlayerWinning(ForwardStep, BackwardStep, postAction);
        }
    }
}
