namespace Battle
{
    public class OpponentBattleResolution : BattleResolution
    {
        public override void ConcludeBattle(BattleControl battleController)
        {
            battleController.HandlePlayerWinning();
        }
        public override void DetermineImpact(WeaponControl weaponController)
        {
            weaponController.Impact = BattleControl.BattleBootstrapper.OriginalOpponentRank;
        }
    }
}
