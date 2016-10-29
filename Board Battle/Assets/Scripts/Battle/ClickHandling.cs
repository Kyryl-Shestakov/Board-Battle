using UnityEngine;

namespace Battle
{
    public class ClickHandling : MonoBehaviour
    {
        public WeaponControl WeaponController;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                WeaponController.Shoot();
            }
        }
    }
}
