using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    private bool _pauseGame = false;
    private DontDestroyOnLoadMusicScript _map;
    private GameObject _resumeButton;
    private GameObject _quitButton;
    private GameObject _backGround;
    private GameObject _pauseButton;
    private GameObject _radialMenu;

    public bool PauseGame { get { return _pauseGame; } set { _pauseGame = value; } }

	// Use this for initialization
	void Start () {
        _resumeButton = GameObject.Find("ResumeButton");
        _quitButton = GameObject.Find("QuitButton");
        _backGround = GameObject.Find("OverlayPause");
        _pauseButton = GameObject.Find("PauseButton");
        _radialMenu = GameObject.FindObjectOfType<BuildingSpawnScript>().gameObject;
        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
	}
	
	// Update is called once per frame
	void Update () {
        _checkInput();
        _checkPause();
	}

    private void _checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseGame = !_pauseGame;
        }
    }

    /// <summary>
    /// Pauses the game if Key is pressed or button is clicked
    /// </summary>
    private void _checkPause()
    {
        if (_pauseGame)
	    {
            Time.timeScale = 0;
            _resumeButton.SetActive(true);
            _quitButton.SetActive(true);
            _backGround.SetActive(true);
            _pauseButton.SetActive(false);
            _radialMenu.SetActive(false);
	    }
        else
        {
            Time.timeScale = 1;
            _resumeButton.SetActive(false);
            _quitButton.SetActive(false);
            _backGround.SetActive(false);
            _pauseButton.SetActive(true);
            _radialMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        _map.Play2 = true;
        Application.LoadLevel(0);
    }
}
