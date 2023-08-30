using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelsLG : MonoBehaviour
{
    [SerializeField] private GameObject _enText, _itText;

    public void ChangeLevelText()
    {
        if (PlayerPrefs.GetString("Language") == "En")
        {
            _enText.SetActive(true);
            _itText.SetActive(false);
        }
        else
        {
            _enText.SetActive(false);
            _itText.SetActive(true);
        }
    }
}
