using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadMusicScript : MonoBehaviour
{

    private static GameObject _instance;
    private AudioSource[] _music;
    private GameObject _creditCanvas;
    private GameObject _menuCanvas;
    private GameObject _optionsCanvas;
    private GameObject _selectionCanvas;
    private int _level = 1;
    private bool _play = false;
    private bool _play2 = false;
    public int Level { get { return _level; } set { _level = value; }}

    public bool Play { get { return _play; } set { _play = value; } }
    public bool Play2 { get { return _play2; } set { _play2 = value; } }

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!_instance)
        {
            _instance = this.gameObject;

        }
        //otherwise, if we do, kill this thing
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        _menuCanvas = GameObject.Find("MenuCanvas");
        _optionsCanvas = GameObject.Find("OptionsCanvas");
        _creditCanvas = GameObject.Find("CreditsCanvas");
        _selectionCanvas = GameObject.Find("SelectionCanvas");
        _music = gameObject.GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        PlayNextMusic();
        BackToMenu();
    }


    void PlayNextMusic()
    {
        if (_play == true)
        {
            StopCoroutine("PlayNextInGameMusic");
            StartCoroutine("PlayNextInGameMusic");
            _music[0].Stop();
            _music[1].Play();
            _play = false;
        }

    }

    void BackToMenu()
    {
        if (_play2 == true)
        {
            _music[0].Play();
            _music[1].Stop();
            _music[2].Stop();
            _level = 0;
            StopCoroutine("PlayNextInGameMusic");
            _play2 = false;
            Application.LoadLevel(0);
        }

    }

    IEnumerator PlayNextInGameMusic()
    {
        yield return new WaitForSeconds(_music[1].clip.length);
        _music[2].Play();
    }
}