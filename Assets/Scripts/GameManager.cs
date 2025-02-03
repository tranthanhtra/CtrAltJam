using System;
using UnityEngine;

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

    [SerializeField] private GameObject readyPanel;
    private GameState currentState = GameState.Ready;

    public GameState CurrentState
    {
        get => currentState;
        private set
        {
            if (currentState == GameState.Playing && value == GameState.Ready)
            {
                readyPanel.SetActive(true);
            }

            if (currentState == GameState.Ready && value == GameState.Playing)
            {
                readyPanel.SetActive(false);
            }
            currentState = value;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentState == GameState.Ready)
        {
            CurrentState = GameState.Playing;
        }
    }

    private void KillAnt()
    {
        
    }

    public enum GameState
    {
        Ready,
        Playing,
        Result,
    }

}