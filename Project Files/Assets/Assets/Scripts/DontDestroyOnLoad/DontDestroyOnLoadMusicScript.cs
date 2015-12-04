using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadMusicScript : MonoBehaviour
{

    private static GameObject _instance; //empty static gameobject
    private AudioSource[] _music; //array audiosource from music
    private int _level = 0; ///Default level for the levelscript
    private bool _play = false; //bool to play the buildingmusic
    private bool _play2 = false; //bool to play the ingamemusic
    private bool _play3 = false; //bool to play the skipwavemusic
    public int Level { get { return _level; } set { _level = value; }}

    public bool Play { get { return _play; } set { _play = value; } }
    public bool Play2 { get { return _play2; } set { _play2 = value; } }
    public bool Play3 { get { return _play3; } set { _play3 = value; } }

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
        _music = gameObject.GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        _playNextMusic();
        _backToMenu();
        _playMusicAtSkip();
    }

    /// <summary>
    /// <para>If play is set to true, Stop the PlayNextIngameMusic first then Start it again,</para>
    /// <para>this will make sure that it will play build music won't play twice or more.</para>
    /// <para>After that stop the first music, which is the menu music, and play the first build wavemusic.</para>
    /// <para>after that set the bool Play to false.</para>
    /// </summary>
    private void _playNextMusic()
    {
        if (_play == true)
        {
            StopCoroutine("PlayNextInGameMusic");
            StartCoroutine("PlayNextInGameMusic");
            _music[0].Stop();
            _music[2].Stop();
            _music[1].Play();
            _play = false;
        }

    }

    private void _playMusicAtSkip()
    {
        if (_play3 == true)
        {
            StopCoroutine("PlayNextInGameMusic");
            _music[1].Stop();
            _music[2].Play();
            _play3 = false;
        }
    }
    /// <summary>
    /// <para>If Play2 is set to true and stop all the music and stop the Coroutine PlayNextInGameMusic</para>
    /// <para>Set the level back to 0 and application 0</para>
    /// </summary>
    private void _backToMenu()
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