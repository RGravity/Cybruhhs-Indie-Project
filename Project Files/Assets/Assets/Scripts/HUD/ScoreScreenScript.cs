using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreenScript : MonoBehaviour {

    private bool _endLevel = false;
    private GameObject _score;
    private BaseScript _baseScript;
    private DontDestroyOnLoadMusicScript _map;

    #region StarVariables
    //Stars
    private Image _0Stars;
    private Image _1Star;
    private Image _2Stars;
    private Image _3Stars;
    #endregion

    public bool EndLevel { get { return _endLevel; } set { _endLevel = value; } }

    // Use this for initialization
    void Start () {
        _score = GameObject.Find("BackgroundScore");
        _baseScript = GameObject.FindObjectOfType<BaseScript>();
        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
        _0Stars = GameObject.Find("BigStarsEmpty").GetComponent<Image>();
        _1Star = GameObject.Find("BigStarsFill1").GetComponent<Image>();
        _2Stars = GameObject.Find("BigStarsFill2").GetComponent<Image>();
        _3Stars = GameObject.Find("BigStarsFill3").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_endLevel)
        {
            _score.SetActive(true);
            _checkCriteria();
        }
        else
        {
            _score.SetActive(false);
        }
	}

    /// <summary>
    /// <para>Check criteria and show right amount of Stars</para>
    /// </summary>
    private void _checkCriteria()
    {
        if (_baseScript.Health == 30)
        {
            _0Stars.enabled = false;
            _1Star.enabled = false;
            _2Stars.enabled = false;
            _3Stars.enabled = true;
        }
        else if (_baseScript.Health < 30 && _baseScript.Health >= 20)
        {
            _0Stars.enabled = false;
            _1Star.enabled = false;
            _2Stars.enabled = true;
            _3Stars.enabled = true;
        }
        else if (_baseScript.Health < 20 && _baseScript.Health >= 10)
        {
            _0Stars.enabled = false;
            _1Star.enabled = true;
            _2Stars.enabled = false;
            _3Stars.enabled = false;
        }
        else if (_baseScript.Health < 10)
        {
            _0Stars.enabled = true;
            _1Star.enabled = false;
            _2Stars.enabled = false;
            _3Stars.enabled = false; 
        }
    }

    public void QuitGame()
    {
        _baseScript.IsDead = false;
        GameObject.FindObjectOfType<PauseScript>().PauseGame = false;
        Time.timeScale = 1;
        _map.Play2 = true;
    }

    public void RetryLevel()
    {
        Application.LoadLevel(1);
        _map.Play = true;
    }

    public void NextLevel()
    {
        _map.Level = _map.Level + 1;
        Application.LoadLevel(1);
        _map.Play = true;
    }
}
