using System;
using Actions;

namespace Utility.CardUtility
{
    public abstract class WinningResolution
    {
        public abstract void Resolve(BattleOutcomeHandling currentPawnBattleOutcomeHandler, Action postAction);
    }
}
