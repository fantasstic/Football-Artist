using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string _gameSceneName;
    [SerializeField] private GameObject _settingsPanel, _mainMenuPanel, _pausePanel, _pausePanelIT, _gameOverScreen;
    [SerializeField] private Button _pauseButton, _musicButton, _soundButton, _enButton, _itButton;
    [SerializeField] private Sprite _closePauseSprite, _openPauseSprite,_musicOnSprite, _musicOffSprite, _soundOnSprite, _soundOffSprite, _lgSelectionOn, _lgSelectionOff;
    [SerializeField] private AudioSource _musicAudio, _soundsAudio;
    [SerializeField] private GameObject[] _enTexts, _itTexts;

    private bool _isPlaying = true;

    public bool InGame;

    private void Start()
    {
        if(!InGame)
        {

            if(!PlayerPrefs.HasKey("Music"))
            {
                PlayerPrefs.SetString("Music", "Yes");
            }

            if (PlayerPrefs.GetString("Music") == "Yes")
            {
                _musicButton.image.sprite = _musicOnSprite;
                _musicAudio.mute = false;
            }
            else
            {
                _musicButton.image.sprite = _musicOffSprite;
                _musicAudio.mute = true;
            }

            if (!PlayerPrefs.HasKey("Sounds"))
            {
                PlayerPrefs.SetString("Sounds", "Yes");
            }

            if (PlayerPrefs.GetString("Sounds") == "Yes")
            {
                _soundButton.image.sprite = _soundOnSprite;
                _soundsAudio.mute = false;
            }
            else
            {
                _soundButton.image.sprite = _soundOffSprite;
                _soundsAudio.mute = true;
            }

            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetString("Language", "En");
            }

            if (PlayerPrefs.GetString("Language") == "En")
            {
                _enButton.image.sprite = _lgSelectionOn;
                foreach(var text in _enTexts)
                {
                    text.SetActive(true);
                }
                foreach (var text in _itTexts)
                {
                    text.SetActive(false);
                }
                _itButton.image.sprite = _lgSelectionOff;
            }
            else
            {
                _itButton.image.sprite = _lgSelectionOn;
                foreach (var text in _enTexts)
                {
                    text.SetActive(false);
                }
                foreach (var text in _itTexts)
                {
                    text.SetActive(true);
                }
                _enButton.image.sprite = _lgSelectionOff;
            }
        }

        if(InGame)
        {
            if (!PlayerPrefs.HasKey("Music"))
            {
                PlayerPrefs.SetString("Music", "Yes");
            }

            if (PlayerPrefs.GetString("Music") == "Yes")
            {
                _musicAudio.mute = false;
            }
            else
            {
                _musicAudio.mute = true;
            }

            if (!PlayerPrefs.HasKey("Sounds"))
            {
                PlayerPrefs.SetString("Sounds", "Yes");
            }

            if (PlayerPrefs.GetString("Sounds") == "Yes")
            {
                _soundsAudio.mute = false;
            }
            else
            {
                _soundsAudio.mute = true;
            }
        }
    }

    public void LanguageSelection(string language)
    {
        PlayerPrefs.SetString ("Language", language);

        if (PlayerPrefs.GetString("Language") == "En")
        {
            _enButton.image.sprite = _lgSelectionOn;
            foreach (var text in _enTexts)
            {
                text.SetActive(true);
            }
            foreach (var text in _itTexts)
            {
                text.SetActive(false);
            }
            _itButton.image.sprite = _lgSelectionOff;
        }
        else
        {
            _itButton.image.sprite = _lgSelectionOn;
            foreach (var text in _enTexts)
            {
                text.SetActive(false);
            }
            foreach (var text in _itTexts)
            {
                text.SetActive(true);
            }
            _enButton.image.sprite = _lgSelectionOff;
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenSettings(bool inGame)
    {
        PlayButtonSound();
        if (!inGame)
        {
            _mainMenuPanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }
        else
        {
            _pausePanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings(bool inGame) 
    {
        PlayButtonSound();
        if (!inGame)
        {
            _mainMenuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }
        else
        {
            _pausePanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }
    } 

    public void MusicSwitch()
    {
        PlayButtonSound();
        if (PlayerPrefs.GetString("Music") == "Yes")
        {
            _musicButton.image.sprite = _musicOffSprite;
            _musicAudio.mute = true;
            PlayerPrefs.SetString("Music", "No");
        }
        else
        {
            _musicButton.image.sprite = _musicOnSprite;
            _musicAudio.mute = false;
            PlayerPrefs.SetString("Music", "Yes");
        }
    }

    public void SoundSwitch()
    {
        PlayButtonSound();
        if (PlayerPrefs.GetString("Sounds") == "Yes")
        {
            _soundButton.image.sprite = _soundOffSprite;
            _soundsAudio.mute = true;
            PlayerPrefs.SetString("Sounds", "No");
        }
        else
        {
            _soundButton.image.sprite = _soundOnSprite;
            _soundsAudio.mute = false;
            PlayerPrefs.SetString("Sounds", "Yes");
        }
    }

    public void GameOverButtons(string sceneToLoad)
    {
        PlayButtonSound();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void PauseButtons()
    {
        PlayButtonSound();
        if (_isPlaying)
        {
            _isPlaying = false;
            _pauseButton.image.sprite = _closePauseSprite;
            if (PlayerPrefs.GetString("Language") == "En")
            {
                _pausePanel.SetActive(true);
            }
            else
            {
                _pausePanelIT.SetActive(true);
            }
            
        }
        else
        {
            _isPlaying = true;
            _pauseButton.image.sprite = _openPauseSprite;
            if (PlayerPrefs.GetString("Language") == "En")
            {
                _pausePanel.SetActive(false);
            }
            else
            {
                _pausePanelIT.SetActive(false);
            }
        }
    }

    private void PlayButtonSound()
    {
        _soundsAudio.Play();
    }

    public bool IsPlaying()
    {
        return _isPlaying;
    }
}
