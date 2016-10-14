using UnityEngine;

namespace Utility.CardUtility
{
    public class FrontCardRotationResolution : CardRotationResolution
    {
        public override void Rotate(Transform cardTransform)
        {
            cardTransform.eulerAngles = Vector3.zero;
        }
    }
}
