using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : Singleton<UIManager>
{
    public Dictionary<GameState,Canvas> dictStateCanvas = new Dictionary<GameState, Canvas>();
    public Dictionary<GameState,GameObject> dictStateGameObject = new Dictionary<GameState, GameObject>();

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
        mainMenu = GameObject.Find("MainMenu").GetComponent<Canvas>();
        pauseGame = GameObject.Find("PauseGame").GetComponent<Canvas>();
        mainMenu = GameObject.Find("MainMenu").GetComponent<Canvas>();
        inGame = GameObject.Find("InGame").GetComponent<Canvas>();
        shopSkinMenu = GameObject.Find("ShopSkinMenu").GetComponent<Canvas>();
        shopWeaponMenu = GameObject.Find("ShopWeaponMenu").GetComponent<Canvas>();
        endGame = GameObject.Find("EndGame").GetComponent<Canvas>();
        question = GameObject.Find("Question").GetComponent<Canvas>();

        mainMenuContainer = GameObject.Find("MainMenu");
        pauseGameContainer = GameObject.Find("PauseGame");
        mainMenuContainer = GameObject.Find("MainMenu");
        inGameContainer = GameObject.Find("InGame");
        shopSkinMenuContainer = GameObject.Find("ShopSkinMenu");
        shopWeaponMenuContainer = GameObject.Find("ShopWeaponMenu");
        endGameContainer = GameObject.Find("EndGame");
        questionContainer = GameObject.Find("Question");
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
