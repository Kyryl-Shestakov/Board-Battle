using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility.CardUtility;

namespace Battle
{
    public class BattleControl : MonoBehaviour
    {
        public static BattleBootstrap BattleBootstrapper;
        public static WinningResolution WinningResolver;
        public static Action AfterBattleAction;
        public static GameObject BoardWrapper;

        public Text StatusText;
        public Button ProceedButton;

        void Start()
        {
            var playerWeaponController = GameObject.Find("Player").GetComponent<WeaponControl>();
            var opponentWeaponController = GameObject.Find("Opponent").GetComponent<WeaponControl>();
            playerWeaponController.Impact = BattleBootstrapper.OriginalPlayerRank;
            opponentWeaponController.Impact = BattleBootstrapper.OriginalOpponentRank;
        }

        public void HandlePlayerWinning()
        {
            var cardStats = ActorControl.PlayerBattleCard.GetComponent<CardManagement>().CardStats;
            WinningResolver = new PlayerWinningResolution(cardStats.ForwardStepCount, cardStats.BackwardStepCount);
            ActivateEnding("You won!");
        }

        public void HandleOpponentWinning()
        {
            var cardStats = ActorControl.OpponentBattleCard.GetComponent<CardManagement>().CardStats;
            WinningResolver = new OpponentWinningResolution(cardStats.ForwardStepCount, cardStats.BackwardStepCount);
            ActivateEnding("You lost");
        }

        public void ActivateEnding(string status)
        {
            StatusText.text = status;
            ProceedButton.gameObject.SetActive(true);
            StatusText.gameObject.SetActive(true);
        }

        public void Proceed()
        {
            FindObjectsOfType<GameObject>().Where(g => g.transform.parent == null).ToList().ForEach(Destroy);
            SceneManager.UnloadScene(SceneManager.GetSceneAt(1));
            ActorControl.BoardSceneGameObjects.ForEach(g => g.SetActive(true));
            AfterBattleAction();
        }
    }
}
