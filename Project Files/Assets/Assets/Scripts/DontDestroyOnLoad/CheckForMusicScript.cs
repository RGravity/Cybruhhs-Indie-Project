using UnityEngine;
using System.Collections;

public class CheckForMusicScript : MonoBehaviour {
    /// <summary>
    /// This Script is a checkscript for the audio, if the audio is here it will give out a true,
    /// if there is no audio it will give a false, so no audio will be played.
    /// </summary>
    private DontDestroyOnLoadMusicScript _music;
    private bool _check = true;

    public bool Check { get { return _check; } set { _check = value; }}

    void Awake()
    {
        _music = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
    }
    // Use this for initialization
    void Start ()
    {
        if (_music == null)
        {
            _check = false;
        }
        else
        {
            _check = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	
	}
}
