using System;
using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour
{
    public static float ProximityThreshold = 0.1f;

    public float CardSpeed;

    public static bool AreNear(Vector3 a, Vector3 b)
    {
        var distance = Vector3.Distance(a, b);

        return distance < ProximityThreshold;
    }

    public IEnumerator Move(Vector3 destinationPosition, Action<Transform> postAction)
    {
        var sourcePosition = transform.position;
        var currentInterpolant = Time.deltaTime;

        do
        {
            var step = Vector3.Lerp(sourcePosition, destinationPosition, CardSpeed*currentInterpolant);

            transform.position = step;

            yield return step;
            
            currentInterpolant += Time.deltaTime;
        }
        while (!AreNear(transform.position, destinationPosition));
        
        transform.position = destinationPosition;
        yield return destinationPosition;

        postAction(transform);
    }
}
