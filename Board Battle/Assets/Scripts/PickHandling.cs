using UnityEngine;
using Utility.CardUtility;

public class PickHandling : MonoBehaviour
{
    public static CardPicking CardPicker;

    public void OnMouseDown()
    {
        if (CardPicker != null) CardPicker.PickTheCard(gameObject, GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>().CurrentHandManager);
    }
}




