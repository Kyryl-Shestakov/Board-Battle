using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility.CardUtility;

// ReSharper disable PossibleNullReferenceException

public class ActorControl : MonoBehaviour
{
    public static GameObject PlayerBattleCard;
    public static GameObject OpponentBattleCard;

    public PawnMovement CurrentPawnMover;
    public CardHoldingManagement CurrentHandManager;
    public DrawingCardDeckManagement DrawingCardDeckManager;

    public Text Status;
    public DiceRolling DiceRoller;

    public event EventHandler CardsRevealed;
    public event EventHandler CardsDiscarded;
    public event EventHandler CardsReplenished;

    protected virtual void OnCardsRevealed()
    {
        CardsRevealed(this, EventArgs.Empty);
    }

    protected virtual void OnCardsDiscarded()
    {
        CardsDiscarded(this, EventArgs.Empty);
    }

    protected virtual void OnCardsReplenished()
    {
        CardsReplenished(this, EventArgs.Empty);
    }

    public void GoForth()
    {
        //GameObject.FindGameObjectWithTag("Interface").transform.FindChild("Roll Button").gameObject.SetActive(false);
        SetStatusText("The pawn is moving");

        int stepCount = DiceRoller.RollTheDie();
        var queue = new Queue<Action>();
        Action pawnMovement = () =>
        {
            if (!(CurrentPawnMover.NextSpotAction is FinishSpotAction))
            {
                StartCoroutine(CurrentPawnMover.Move(spotConnection => spotConnection.NextSpot,
                    () =>
                    {
                        //try
                        //{
                        //    Action postAction = queue.Dequeue();
                        //    postAction();
                        //}
                        //catch (InvalidOperationException)
                        //{
                        //    CurrentPawn.GetComponent<SpotAction>().PerformAction();
                        //}

                        if (queue.Count != 0) //If there are movements left perform them
                        {
                            Action postAction = queue.Dequeue();
                            postAction();
                        }
                        else //Perform actions defined by the landing spot
                        {
                            CurrentPawnMover.CurrentSpotAction.PerformAction(() =>
                            {
                                SwitchCharacter();
                                GameObject.FindGameObjectWithTag("Interface")
                                    .transform.FindChild("Roll Button")
                                    .gameObject.SetActive(true);
                                GameObject.FindGameObjectWithTag("CurrentCharacterText").GetComponent<Text>().text =
                                    CurrentPawnMover.CharacterName;
                                SetStatusText("Roll the dice");
                            });
                        }
                    }));
            }
            else
            {
                CurrentPawnMover.CurrentSpotAction.PerformAction(() =>
                {
                    SwitchCharacter();
                    GameObject.FindGameObjectWithTag("Interface")
                        .transform.FindChild("Roll Button")
                        .gameObject.SetActive(true);
                    GameObject.FindGameObjectWithTag("CurrentCharacterText").GetComponent<Text>().text =
                        CurrentPawnMover.CharacterName;
                    SetStatusText("Roll the dice");
                });
            }
            
        };

        for (int i = 0; i < stepCount; ++i)
        {
            queue.Enqueue(pawnMovement);
        }

        Action action = queue.Dequeue();
        action();
    }

    protected void SetStatusText(string text)
    {
        Status.text = text;
    }

    public void RevealPickedCards(Func<CardManagement, CardManagement, WinningResolution> winningDetermination, Action postAction)
    {
        var playerHandManager = GameObject.Find("Player Hand").GetComponent<CardHoldingManagement>();
        var opponentHandManager = GameObject.Find("Opponent Hand").GetComponent<CardHoldingManagement>();

        CardsRevealed += (sender1, args1) =>
        {
            Delegate[] handlers1 = CardsRevealed.GetInvocationList();
            handlers1.ToList().ForEach(d =>
            {
                CardsRevealed -= d as EventHandler;
            });
            CardsRevealed += (sender2, args2) =>
            {
                Delegate[] handlers2 = CardsRevealed.GetInvocationList();
                handlers2.ToList().ForEach(d =>
                {
                    CardsRevealed -= d as EventHandler;
                });
                var button = GameObject.FindGameObjectWithTag("Interface")
                    .transform.FindChild("Battle Button").GetComponent<Button>();
                UnityAction buttonClickListener = () =>
                {
                    button.gameObject.SetActive(false);
                    button.onClick.RemoveAllListeners();
                    var winningResolver = winningDetermination(PlayerBattleCard.GetComponent<CardManagement>(),
                        OpponentBattleCard.GetComponent<CardManagement>());

                    DiscardBattleCards(() =>
                    {
                        CardsReplenished += (sender3, args3) =>
                        {
                            Delegate[] handlers3 = CardsReplenished.GetInvocationList();
                            handlers3.ToList().ForEach(d =>
                            {
                                CardsReplenished -= d as EventHandler;
                            });
                            CardsReplenished += (o1, eventArgs1) =>
                            {
                                Delegate[] handlers4 = CardsReplenished.GetInvocationList();
                                handlers4.ToList().ForEach(d =>
                                {
                                    CardsReplenished -= d as EventHandler;
                                });

                                winningResolver.Resolve(CurrentPawnMover.GetComponent<BattleOutcomeHandling>(), postAction);
                            };
                        };

                        SetStatusText("The hands are being replenished");
                        DrawingCardDeckManager.DealCardTo(CurrentHandManager, OnCardsReplenished);
                        DrawingCardDeckManager.DealCardTo(CurrentHandManager.OpposingCardHoldingManager, OnCardsReplenished);
                    });
                };
                button.onClick.AddListener(buttonClickListener);
                button.gameObject.SetActive(true);
                SetStatusText("Start the battle");
            };
        };

        //TODO: Change the determination of vectors based on the card transform
        var newPositionForPlayerCard = new Vector3(17.0f, 37.0f, -12.5f);
        var newRotationForPlayerCard = new Vector3(-30.0f, 0.0f, 0.0f);
        var playerCard = playerHandManager.ShowPickedCard(newPositionForPlayerCard, newRotationForPlayerCard, OnCardsRevealed);

        var newPositionForOpponentCard = new Vector3(27.0f, 37.0f, -12.5f);
        var newRotationForOpponentCard = new Vector3(-30.0f, 0.0f, 0.0f);
        var opponentCard = opponentHandManager.ShowPickedCard(newPositionForOpponentCard, newRotationForOpponentCard, OnCardsRevealed);

        PlayerBattleCard = playerCard;
        OpponentBattleCard = opponentCard;
    }

    public void DiscardBattleCards(Action postAction)
    {
        var playerCard = PlayerBattleCard;
        var opponentCard = OpponentBattleCard;
        PlayerBattleCard = null;
        OpponentBattleCard = null;

        var discardedCardDeckManagement =
            GameObject.Find("Discarded Card Deck").GetComponent<DiscardedCardDeckManagement>();
        var rotationForDiscardedCardDeck = new Vector3(0.0f, 0.0f, 180.0f);
        var positionOverDiscardedCardDeck = new Vector3(discardedCardDeckManagement.transform.position.x,
            discardedCardDeckManagement.transform.position.y + discardedCardDeckManagement.CardElevation,
            discardedCardDeckManagement.transform.position.z);

        CardsDiscarded += (sender, args) =>
        {
            Delegate[] handlers1 = CardsDiscarded.GetInvocationList();
            handlers1.ToList().ForEach(d => { CardsDiscarded -= d as EventHandler; });
            CardsDiscarded += (o, eventArgs) =>
            {
                Delegate[] handlers2 = CardsDiscarded.GetInvocationList();
                handlers2.ToList().ForEach(d => { CardsDiscarded -= d as EventHandler; });
                postAction();
                //SetStatusText("Start the battle");
            };
        };

        playerCard.transform.eulerAngles = rotationForDiscardedCardDeck;
        opponentCard.transform.eulerAngles = rotationForDiscardedCardDeck;

        SetStatusText("The cards are being discarded");

        StartCoroutine(playerCard.GetComponent<CardMovement>()
            .Move(positionOverDiscardedCardDeck,
                t => { discardedCardDeckManagement.ReceiveCard(playerCard, OnCardsDiscarded); }));
        StartCoroutine(opponentCard.GetComponent<CardMovement>()
            .Move(positionOverDiscardedCardDeck,
                t => { discardedCardDeckManagement.ReceiveCard(opponentCard, OnCardsDiscarded); }));
    }

    public void SwitchCharacter()
    {
        CurrentPawnMover = CurrentPawnMover.OpposingPawnMover;
        CurrentHandManager = CurrentHandManager.OpposingCardHoldingManager;
    }
}