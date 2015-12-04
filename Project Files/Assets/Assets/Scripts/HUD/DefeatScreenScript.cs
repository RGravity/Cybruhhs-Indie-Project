using UnityEngine;
using System.Collections;

public class DefeatScreenScript : MonoBehaviour {

    private DontDestroyOnLoadMusicScript _map;
    private BaseScript _baseScript;
    private GameObject _backgroundDefeat;

    // Use this for initialization
    void Start () {
        _baseScript = GameObject.FindObjectOfType<BaseScript>();
        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
        _backgroundDefeat = GameObject.Find("BackgroundDefeat");
    }
	
	// Update is called once per frame
	void Update () {
        if (_baseScript.IsDead)
        {
            _backgroundDefeat.SetActive(true);
        }
        else
        {
            _backgroundDefeat.SetActive(false);
        }
	}

    /// <summary>
    /// <para>Go Back to Menu on Defeat</para>
    /// </summary>
    public void QuitGame()
    {
        _baseScript.IsDead = false;
        GameObject.FindObjectOfType<PauseScript>().PauseGame = false;
        Time.timeScale = 1;
        _map.Play2 = true;
    }

    /// <summary>
    /// <para>Retry Current Level</para>
    /// </summary>
    public void RetryLevel()
    {
        Application.LoadLevel(1);
        _map.Play = true;
    }
}
