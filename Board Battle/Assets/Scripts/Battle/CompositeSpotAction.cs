﻿using System;
using UnityEngine;

namespace Battle
{
    public class CompositeSpotAction : SpotAction
    {
        public SpotAction FirstSpotActionContainer;
        public SpotAction SecondSpotActionContainer;
        public override void PerformAction(Action postAction)
        {
            FirstSpotActionContainer.PerformAction(() =>
            {
                SecondSpotActionContainer.PerformAction(postAction);
            });
        }
    }
}
