using System;

namespace Battle
{
    public class PlayerBattleOutcomeHandling : BattleOutcomeHandling
    {
        public override void HandlePlayerWinning(int forwardStepCount, int backwardStepCount, Action postAction)
        {
            
        }

        public override void HandleOpponentWinning(int forwardStepCount, int backwardStepCount, Action postAction)
        {
        }

        public override void HandleTie(Action postAction)
        {
        }
    }
}
