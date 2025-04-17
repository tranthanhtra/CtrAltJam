using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image stateImage;

    [SerializeField] private Sprite victorySprite;

    [SerializeField] private Sprite defeatSprite;

    [SerializeField] private Sprite readySprite;

    [SerializeField] private TMP_Text text;

    public void Setup(GameManager.GameState gameState)
    {
        gameObject.SetActive(true);
        switch (gameState)
        {
            case GameManager.GameState.ResultVictory:
                text.text = "You Win!! Press Space to check if there is any more bugs";
                stateImage.sprite = victorySprite;
                break;
            case GameManager.GameState.ResultDefeat:
                text.text = "You Lose!! The bug got away... Press Space to check where the bug is";
                stateImage.sprite = defeatSprite;
                break;
            case GameManager.GameState.Ready:
                stateImage.sprite = readySprite;
                text.text = "The bug is hiding under Space key!!";
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }
}