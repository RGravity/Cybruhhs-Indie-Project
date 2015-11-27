using UnityEngine;
using System.Collections;

public class CheckForMusicScript : MonoBehaviour {

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
