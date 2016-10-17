using UnityEngine;

namespace Utility.CardUtility
{
    public class BackCardRotationResolution : CardRotationResolution
    {
        public Vector3 Rotation;

        public override void Rotate(Transform cardTransform)
        {
            cardTransform.eulerAngles = Rotation;
        }
    }
}
