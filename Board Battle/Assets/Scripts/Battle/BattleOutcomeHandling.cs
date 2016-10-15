using System;
using UnityEngine;

namespace Battle
{
    public abstract class BattleOutcomeHandling : MonoBehaviour
    {
        public abstract void HandlePlayerWinning(int forwardStepCount, int backwardStepCount, Action postAction);
        public abstract void HandleOpponentWinning(int forwardStepCount, int backwardStepCount, Action postAction);
        public abstract void HandleTie(Action postAction);
    }
}
