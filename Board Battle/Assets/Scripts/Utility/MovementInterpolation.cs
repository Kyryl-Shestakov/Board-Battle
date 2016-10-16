﻿using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Determines how a player will move
    /// </summary>
    public class MovementInterpolation
    {
        /// <summary>
        /// Movement vector
        /// </summary>
        private readonly Vector3 _difference;
        /// <summary>
        /// A relative increment that a player has to move each step of a single movement 
        /// </summary>
        private readonly float _step;
        /// <summary>
        /// Radius of a movement semicircle path
        /// </summary>
        private readonly float _radius;
        /// <summary>
        /// Squared radius of a movement semicircle path precalculated for simplicity and efficiency
        /// </summary>
        private readonly float _squaredRadius;

        public MovementInterpolation(Vector3 source, Vector3 destination, int stepCount)
        {
            _difference = destination - source;
            _step = 1.0f/stepCount;
            //var distance = directionResolver.DetermineDistance(source, destination);
            var distance = _difference.magnitude;
            _radius = distance/2.0f;
            _squaredRadius = Mathf.Pow(_radius, 2.0f);
        }

        /// <summary>
        /// Yields a set of positions on a normalized semicircle path
        /// </summary>
        /// <param name="action">Determines what to do with each position</param>
        /// <param name="postAction">Determines what to do after the iteration</param>
        /// <returns>Vector3 enumerator</returns>
        public IEnumerator Iterate(Action<Vector3> action, Action postAction)
        {
            for (float i = _step; i < 1.0f; i += _step)
            {
                var incrementedPosition = Vector3.Lerp(Vector3.zero, _difference, i);
                var liftedIncrementedPosition = TransformToTheCurve(incrementedPosition);

                action(liftedIncrementedPosition);
                yield return liftedIncrementedPosition;
            }

            action(_difference);
            yield return _difference;
            postAction();
        }

        /// <summary>
        /// Transforms a position towards destination on a straight line to a position lifted to be on a semicircle path
        /// </summary>
        /// <param name="origin">Position on a line</param>
        /// <returns>Position on a semicircle</returns>
        public Vector3 TransformToTheCurve(Vector3 origin)
        {
            //var coordinate = Mathf.Abs(_directionResolver.DetermineLift(origin));
            var increment = Mathf.Sqrt(Mathf.Pow(origin.x, 2.0f) + Mathf.Pow(origin.z, 2.0f));
            return new Vector3(origin.x, origin.y + Mathf.Sqrt(_squaredRadius - Mathf.Pow(increment - _radius, 2.0f)), origin.z);
        }
    }
}
