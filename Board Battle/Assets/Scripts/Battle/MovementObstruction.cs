using UnityEngine;

namespace Battle
{
    public class MovementObstruction : MonoBehaviour {

        public void OnTriggerEnter(Collider enteringCollider)
        {
            var body = enteringCollider.GetComponent<Rigidbody>();

            if (body != null)
            {
                body.useGravity = false;
            }
        }

        public void OnTriggerExit(Collider exitingCollider)
        {
            var body = exitingCollider.GetComponent<Rigidbody>();

            if (body != null)
            {
                body.useGravity = true;
            }
        }
    }
}
