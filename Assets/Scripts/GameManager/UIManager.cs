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
    public Dictionary<GameState,GameObject> dictStateGameObject = new Dictionary<GameState, GameObject>();
    public TextMeshProUGUI txtCoinCurrent;

    public GameObject mainMenuContainer;
    public GameObject inGameContainer;
    public GameObject shopSkinMenuContainer;
    public GameObject shopWeaponMenuContainer;
    public GameObject endGameContainer;
    public GameObject pauseGameContainer;
    public GameObject questionContainer;
    public GameObject coinDesplayContainer;

    public InGame inGame;
    private void Awake()
    {
        this.AddStates();
        this.AwakeState();
    }
   
    public void EnterStateUI(GameState nextGameState)
    {
        if (GameManager.Instance.currentState == nextGameState) return;
        GameManager.Instance.currentState = nextGameState;
        foreach (var state in dictStateGameObject)
        {
            if (state.Key == GameManager.Instance.currentState)
            {
                state.Value.SetActive(true);
            }
            else
            {
                state.Value.SetActive(false);
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
        foreach (var state in dictStateGameObject)
        {
            if (state.Key == GameManager.Instance.currentState)
            {
                state.Value.SetActive(true);
            }
            else
            {
                state.Value.SetActive(false);
            }
        }
    }
    private void AddStates()
    {

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

        inGame = inGameContainer.GetComponent<InGame>();
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
