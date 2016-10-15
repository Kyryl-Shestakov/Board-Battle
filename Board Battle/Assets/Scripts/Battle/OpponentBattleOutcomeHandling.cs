using System;

namespace Battle
{
    public class OpponentBattleOutcomeHandling : BattleOutcomeHandling
    {
        public override void HandlePlayerWinning(int forwardStepCount, int backwardStepCount, Action postAction)
        {
            throw new NotImplementedException();
        }

        public override void HandleOpponentWinning(int forwardStepCount, int backwardStepCount, Action postAction)
        {
            throw new NotImplementedException();
        }

        public override void HandleTie(Action postAction)
        {
            throw new NotImplementedException();
        }
    }
}
