using UnityEngine;

namespace Battle
{
    public class MainCharacterTracking : MonoBehaviour
    {
        /// <summary>
        /// Game object to follow
        /// </summary>
        public GameObject Target;
        private float _distance;
        private float _characterRotation;
        private float _ordinateDifference;

        void Start()
        {
            _ordinateDifference = transform.position.y - Target.transform.position.y;
            var targetPosition = Target.transform.position;
            var lift = new Vector3(targetPosition.x, targetPosition.y + _ordinateDifference, targetPosition.z);
            _distance = Vector3.Distance(transform.position, lift);
            _characterRotation = Target.transform.eulerAngles.y;
        }
        
        void LateUpdate()
        {
            var targetPosition = Target.transform.position;
            float rotationDelta = Target.transform.eulerAngles.y - _characterRotation;
            var lift = new Vector3(targetPosition.x, targetPosition.y + _ordinateDifference, targetPosition.z);
            var currentDistance = Vector3.Distance(transform.position, lift);

            _characterRotation = Target.transform.eulerAngles.y;
            transform.position = new Vector3(transform.position.x, targetPosition.y + _ordinateDifference, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, lift, currentDistance - _distance);
            transform.RotateAround(targetPosition, Vector3.up, rotationDelta);
        }
    }
}
