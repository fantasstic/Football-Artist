using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreCounter;

    private void Start()
    {
        _scoreCounter.text = PlayerPrefs.GetInt("Best Score").ToString();
    }
}
