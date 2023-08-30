using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private BallController _ballController;
    [SerializeField] private TMP_Text _scoreCounter, _gameOverScoreCounterEN, _gameOverScoreCounterIT;
    [SerializeField] private GameObject _gameOverPanel, _gameOverPanelIT, _game;

    private int _score;
    private int _userLevel = 1;

    private void Awake()
    {
        _ballController.Init();
        _userLevel = PlayerPrefs.GetInt("User level", 1);
        _score = PlayerPrefs.GetInt("Current Score");
        _scoreCounter.text = PlayerPrefs.GetInt("Current Score").ToString();
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreCounter.text = _score.ToString();
        PlayerPrefs.SetInt("Current Score", _score);
    }

    public void CheckWin(bool win)
    {
        if (win)
        {
            if (_userLevel < 10)
            {
                _userLevel++;
                Debug.Log(_userLevel);
                PlayerPrefs.SetInt("User level", _userLevel);
                SceneManager.LoadScene(_userLevel);
            }
            else
            {
                _userLevel = 1;
                Debug.Log(_userLevel);
                PlayerPrefs.SetInt("User level", _userLevel);
                SceneManager.LoadScene("Menu");

            }
        }
        else
        {
            int currentScore = PlayerPrefs.GetInt("Current Score");
            int bestScore = PlayerPrefs.GetInt("Best Score", 0);

            if(currentScore > bestScore) 
            {
                PlayerPrefs.SetInt("Best Score", currentScore);
                
            }
            _game.SetActive(false);

            if (PlayerPrefs.GetString("Language") == "En")
            {
                _gameOverPanel.SetActive(true);
                _gameOverScoreCounterEN.text = currentScore.ToString();


            }
            else
            {
                _gameOverPanelIT.SetActive(true);
                _gameOverScoreCounterIT.text = currentScore.ToString();
            }
            PlayerPrefs.SetInt("Current Score", 0);

        }
    }
}

