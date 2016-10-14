using UnityEngine;

namespace Battle
{
    public class CompositeSpotAction : SpotAction
    {
        public SpotAction FirstSpotActionContainer;
        public SpotAction SecondSpotActionContainer;
        public override void PerformAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
