using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    private bool _pauseGame = false;
    private DontDestroyOnLoadMusicScript _map;
    private CheckForMusicScript _check;
    private GameObject _resumeButton;
    private GameObject _quitButton;
    private GameObject _backGround;
    private GameObject _pauseButton;
    private GameObject _radialMenu;
    private BaseScript _baseScript;
    private GameObject _hud;
    private ScoreScreenScript _ScoreScreenScript;

    public bool PauseGame { get { return _pauseGame; } set { _pauseGame = value; } }

	// Use this for initialization
	void Start () {
        _baseScript = GameObject.FindObjectOfType<BaseScript>();
        _resumeButton = GameObject.Find("ResumeButton");
        _quitButton = GameObject.Find("QuitButton");
        _backGround = GameObject.Find("OverlayPause");
        _pauseButton = GameObject.Find("PauseButton");
        _ScoreScreenScript = GameObject.Find("ScoreScreen").GetComponent<ScoreScreenScript>();
        _radialMenu = GameObject.FindObjectOfType<BuildingSpawnScript>().gameObject;
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        _hud = GameObject.Find("HUD");
        if (_check.Check == true)_map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
	
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
            //Show the Pause Screen
            _resumeButton.SetActive(true);
            _quitButton.SetActive(true);
            _backGround.SetActive(true);
            _pauseButton.SetActive(false);
            _radialMenu.SetActive(false);
            _hud.SetActive(false);
        }
        else
        {
            if (!_baseScript.IsDead && !_ScoreScreenScript.EndLevel)
            {
                Time.timeScale = 1;
                _backGround.SetActive(false);
                _hud.SetActive(true);
            }
            else
            {
                Time.timeScale = 0;
                _backGround.SetActive(true);
                _hud.SetActive(false);                
            }
            //Hide the Pause Screen
            _resumeButton.SetActive(false);
            _quitButton.SetActive(false);
            _pauseButton.SetActive(true);
            _radialMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        _baseScript.IsDead = false;
        _pauseGame = false;
        Time.timeScale = 1;
        _map.Play2 = true;
    }
}
