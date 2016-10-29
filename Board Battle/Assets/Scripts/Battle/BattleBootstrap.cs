using UnityEngine;

namespace Battle
{
    public class BattleBootstrap
    {
        public Color BattleColor
        {
            get;
            private set;
        }

        public string ShotBallMaterial { get; private set; }
        public int OriginalPlayerRank { get; private set; }
        public int OriginalOpponentRank { get; private set; }

        public BattleBootstrap(Color color, string shotBallMaterial, int playerRank, int opponentRank)
        {
            BattleColor = color;
            ShotBallMaterial = shotBallMaterial;
            OriginalPlayerRank = playerRank;
            OriginalOpponentRank = opponentRank;
        }
    }
}
