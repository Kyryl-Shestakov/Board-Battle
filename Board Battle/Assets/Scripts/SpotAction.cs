using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility.CardUtility;

public abstract class SpotAction : MonoBehaviour
{
    protected ActorControl ActorController; //TODO: figure out why is it null

    //void Awake()
    //{
    //    ActorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
    //}
    public abstract void PerformAction(Action postAction);

    public void PickCardsForTheBattle(string element, Func<CardManagement, CardManagement, WinningResolution> winningDetermination, Action postAction)
    {
        var actorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
        var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
        var button = GameObject.FindGameObjectWithTag("Interface")
                    .transform.FindChild("Pick Button").GetComponent<Button>();
        var opponentHandManager = GameObject.Find("Opponent Hand").GetComponent<CardHoldingManagement>();
        var playerHandManager = GameObject.Find("Player Hand").GetComponent<CardHoldingManagement>();

        opponentHandManager.PickRandomCard();
        playerHandManager.PickTheFirstCard();
        statusText.text = "Pick the card for " + element + " battle";
        PickHandling.CardPicker = new BattleCardPicking();

        UnityAction buttonClickListener = () =>
        {
            PickHandling.CardPicker = null;
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
            statusText.text = "The cards are being revealed";
            actorController.RevealPickedCards(winningDetermination, postAction);
        };
        button.onClick.AddListener(buttonClickListener);
        button.gameObject.SetActive(true);
    }
}
