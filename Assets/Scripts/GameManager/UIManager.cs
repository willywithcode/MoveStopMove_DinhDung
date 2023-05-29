using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : Singleton<UIManager>
{
    public Dictionary<GameState,Canvas> dictStateCanvas = new Dictionary<GameState, Canvas>();
    public Dictionary<GameState,GameObject> dictStateGameObject = new Dictionary<GameState, GameObject>();
    public TextMeshProUGUI txtCoinCurrent;

    public Canvas mainMenu;
    public Canvas inGame;
    public Canvas shopSkinMenu;
    public Canvas shopWeaponMenu;
    public Canvas endGame;
    public Canvas pauseGame;
    public Canvas question;

    public GameObject mainMenuContainer;
    public GameObject inGameContainer;
    public GameObject shopSkinMenuContainer;
    public GameObject shopWeaponMenuContainer;
    public GameObject endGameContainer;
    public GameObject pauseGameContainer;
    public GameObject questionContainer;
    public GameObject coinDesplayContainer;
    private void Awake()
    {
        this.AddStates();
        this.AwakeState();
    }
   
    public void EnterStateUI(GameState nextGameState)
    {
        if (GameManager.Instance.currentState == nextGameState) return;
        GameManager.Instance.currentState = nextGameState;
        foreach (var state in dictStateCanvas)
        {
            if (state.Key == GameManager.Instance.currentState)
            {
                state.Value.enabled = true;
                dictStateGameObject[state.Key].SetActive(true);
            }
            else
            {
                state.Value.enabled = false;
                dictStateGameObject[state.Key].SetActive(false);
            }
        }
    }
    public void UpdateCoinCurrent()
    {
        txtCoinCurrent.text = GameManager.Instance.currentCoin.ToString();
    }
    public void AwakeState()
    {
        GameManager.Instance.currentState = GameState.MainMenu;
        foreach (var state in dictStateCanvas)
        {
            if (state.Key == GameManager.Instance.currentState)
            {
                state.Value.enabled = true;
                dictStateGameObject[state.Key].SetActive(true);
            }
            else
            {
                state.Value.enabled = false;
                dictStateGameObject[state.Key].SetActive(false);
            }
        }
    }
    private void AddStates()
    {
        dictStateCanvas.Add(GameState.MainMenu, mainMenu);
        dictStateCanvas.Add(GameState.InGame, inGame);
        dictStateCanvas.Add(GameState.ShopSkinMenu, shopSkinMenu);
        dictStateCanvas.Add(GameState.ShopWeaponMenu, shopWeaponMenu);
        dictStateCanvas.Add(GameState.Pause, pauseGame);
        dictStateCanvas.Add(GameState.EndGame, endGame);
        dictStateCanvas.Add(GameState.Question, question);

        dictStateGameObject.Add(GameState.MainMenu, mainMenuContainer);
        dictStateGameObject.Add(GameState.InGame, inGameContainer);
        dictStateGameObject.Add(GameState.ShopSkinMenu, shopSkinMenuContainer);
        dictStateGameObject.Add(GameState.ShopWeaponMenu, shopWeaponMenuContainer);
        dictStateGameObject.Add(GameState.Pause, pauseGameContainer);
        dictStateGameObject.Add(GameState.EndGame, endGameContainer);
        dictStateGameObject.Add(GameState.Question, questionContainer);


    }
    public void FillCanvas()
    {
        mainMenuContainer = GameObject.Find("MainMenu");
        pauseGameContainer = GameObject.Find("PauseGame");
        mainMenuContainer = GameObject.Find("MainMenu");
        inGameContainer = GameObject.Find("InGame");
        shopSkinMenuContainer = GameObject.Find("ShopSkinMenu");
        shopWeaponMenuContainer = GameObject.Find("ShopWeaponMenu");
        endGameContainer = GameObject.Find("EndGame");
        questionContainer = GameObject.Find("Question");
        coinDesplayContainer = GameObject.Find("CoinCurrentDesplay");

        mainMenu = mainMenuContainer.GetComponent<Canvas>();
        pauseGame = pauseGameContainer.GetComponent<Canvas>();
        mainMenu = mainMenuContainer.GetComponent<Canvas>();
        inGame = inGameContainer.GetComponent<Canvas>();
        shopSkinMenu = shopSkinMenuContainer.GetComponent<Canvas>();
        shopWeaponMenu = shopWeaponMenuContainer.GetComponent<Canvas>();
        endGame = endGameContainer.GetComponent<Canvas>();
        question = questionContainer.GetComponent<Canvas>();

    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UIManager fillCanvas = (UIManager)target;

        if (GUILayout.Button("Fill Canvas"))
        {
            fillCanvas.FillCanvas();
        }
    }
}
#endif
