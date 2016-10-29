using UnityEngine;

namespace Battle
{
    public abstract class BattleResolution : MonoBehaviour
    {
        public abstract void ConcludeBattle(BattleControl battleController);
        public abstract void DetermineImpact(WeaponControl weaponController);
    }
}
