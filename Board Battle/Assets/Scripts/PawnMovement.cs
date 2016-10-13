using System;
using UnityEngine;
using System.Collections;
using Linking;
using Utility;

public class PawnMovement : MonoBehaviour
{
    /// <summary>
    /// A spot the pawn resides upon currently
    /// </summary>
    private SpotConnection _currentSpot;
    /// <summary>
    /// Specifies an offset from the center of a spot for the pawn to be placed on it 
    /// </summary>
    public Vector3 PawnSpotOffset;

    public int StepCount;

	/// <summary>
    /// Finds a reference to a starting spot as initialization
    /// </summary>
	protected void Awake()
	{
	    _currentSpot = GameObject.Find("Start Spot").GetComponent<SpotConnection>();
	}

    protected IEnumerator Move(Func<SpotConnection, GameObject> nextSpotResolver)
    {
        var destinationSpot = nextSpotResolver(_currentSpot);

        var sourcePoint = _currentSpot.gameObject.transform.position;
        var destinationPoint = destinationSpot.transform.position;
        //var startingPosition = sourcePoint + PawnSpotOffset;
        var startingPosition = transform.position;

        var directionResolver = _currentSpot.gameObject.GetComponent<StepOrientation>().DetermineDirection();
        var movementInterpolator = new MovementInterpolation(directionResolver, sourcePoint, destinationPoint, StepCount);

        _currentSpot = destinationSpot.GetComponent<SpotConnection>();

        return movementInterpolator.Iterate(p =>
        {
            transform.position = startingPosition + p;
            //transform.Translate(startingPosition + p); Wrong
            //Debug.Log(startingPosition + p);
        });
    }

    public void Handle()
    {
        StartCoroutine(Move(c => c.NextSpot));
    }
}
