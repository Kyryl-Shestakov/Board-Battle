using UnityEngine;
// ReSharper disable PossibleNullReferenceException

namespace Battle
{
    public class ShotTriggering : MonoBehaviour
    {
        public WeaponControl WeaponController { get; private set; }

        void Start()
        {
            WeaponController = GetComponent<WeaponControl>();
        }

        

        
    }
}
