using UnityEngine;

namespace Battle
{
    public class ShotBallDestruction : MonoBehaviour
    {
        public int Impact;

        public void OnCollisionEnter()
        {
            Destroy(gameObject);
        }

        public void HandleHit(WeaponControl character)
        {
            character.HandleHit(Impact);
        }
    }
}
