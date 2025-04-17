using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "GameManager";
                    _instance = go.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] private Timer timer;
    [SerializeField] private GameObject bug;
    [SerializeField] private UI uiPanel;
    private GameState currentState = GameState.Ready;

    public GameState CurrentState
    {
        get => currentState;
        private set
        {
            uiPanel.Setup(value);
            if (value == GameState.Playing)
            {
                timer.SetTimer(60);
                timer.gameObject.SetActive(true);
                timer.StartTimer();
            }
            else timer.gameObject.SetActive(false);

            currentState = value;
        }
    }

    private void Start()
    {
        CurrentState = GameState.Ready;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentState == GameState.Ready) CurrentState = GameState.Playing;
        }

        if (Input.anyKeyDown && currentState is GameState.ResultVictory or GameState.ResultDefeat)
        {
            CurrentState = GameState.Ready;
        }

        if (bug.transform.position.y < -20 && currentState == GameState.Playing)
        {
            CurrentState = GameState.ResultVictory;
        }

        if (!timer.IsRunning && currentState == GameState.Playing) CurrentState = GameState.ResultDefeat;
    }

    public enum GameState
    {
        Ready,
        Playing,
        ResultVictory,
        ResultDefeat
    }
}