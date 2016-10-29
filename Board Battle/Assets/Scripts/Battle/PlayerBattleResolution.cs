namespace Battle
{
    public class PlayerBattleResolution : BattleResolution
    {
        public override void ConcludeBattle(BattleControl battleController)
        {
            battleController.HandleOpponentWinning();
        }

        public override void DetermineImpact(WeaponControl weaponController)
        {
            weaponController.Impact = BattleControl.BattleBootstrapper.OriginalPlayerRank;
        }
    }
}
