using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility.CardUtility;

namespace Battle
{
    public class CardTheftSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            var actorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
            var currentHandManager = actorController.CurrentHandManager;
            var oppositeHandManager = actorController.CurrentHandManager.OpposingCardHoldingManager;

            if (oppositeHandManager.CardCount > 1)
            {
                if (oppositeHandManager.name.StartsWith("Opponent"))
                {
                    PickHandling.CardPicker = new TheftCardPicking();
                    oppositeHandManager.PickTheFirstCard();
                    var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
                    statusText.text = "Pick a card to steal";

                    var button = GameObject.FindGameObjectWithTag("Interface")
                        .transform.FindChild("Pick Button").GetComponent<Button>();
                    UnityAction buttonClickListener = () =>
                    {
                        PickHandling.CardPicker = null;
                        button.gameObject.SetActive(false);
                        button.onClick.RemoveAllListeners();
                        statusText.text = "The card is being handed over";
                        var card = oppositeHandManager.HandOverPickedCard(); //.DiscardThePickedCard(postAction);
                        currentHandManager.TakeTheCard(card, postAction);
                    };
                    button.onClick.AddListener(buttonClickListener);
                    button.gameObject.SetActive(true);
                }
                else
                {
                    oppositeHandManager.PickRandomCard();
                    var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
                    statusText.text = "Pick a card to return to the deck";

                    var button = GameObject.FindGameObjectWithTag("Interface")
                        .transform.FindChild("Pick Button").GetComponent<Button>();
                    UnityAction buttonClickListener = () =>
                    {
                        button.gameObject.SetActive(false);
                        button.onClick.RemoveAllListeners();
                        statusText.text = "The card is being handed over";
                        var card = oppositeHandManager.HandOverPickedCard();
                        currentHandManager.TakeTheCard(card, postAction);
                    };
                    button.onClick.AddListener(buttonClickListener);
                    button.gameObject.SetActive(true);
                }
            }
            else
            {
                postAction();
            }
        }
    }
}
