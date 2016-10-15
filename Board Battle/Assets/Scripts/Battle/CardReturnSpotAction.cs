using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility.CardUtility;

namespace Battle
{
    public class CardReturnSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            var actorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
            var currentHandManager = actorController.CurrentHandManager;
            if (currentHandManager.CardCount > 1)
            {
                PickHandling.CardPicker = new ReturnCardPicking(); //TODO: Maybe introduce empty game object with script
                currentHandManager.PickTheFirstCard();
                var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
                statusText.text = "Pick a card to return to the deck";

                var button = GameObject.FindGameObjectWithTag("Interface")
                    .transform.FindChild("Pick Button").GetComponent<Button>();
                UnityAction buttonClickListener = () =>
                {
                    PickHandling.CardPicker = null;
                    button.gameObject.SetActive(false);
                    button.onClick.RemoveAllListeners();
                    statusText.text = "The card is being discarded";
                    actorController.CurrentHandManager.DiscardThePickedCard(postAction);
                };
                button.onClick.AddListener(buttonClickListener);
                button.gameObject.SetActive(true);
            }
            else
            {
                postAction();
            }
        }
    }
}
