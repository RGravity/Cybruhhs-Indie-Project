using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicVolumeScript : MonoBehaviour {

    private Slider _musicSlider;
    private AudioSource[] _music;
    private AudioSource _click;
   
    void Start()
    {
        _musicSlider = gameObject.GetComponent <Slider>();
        _music = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>().GetComponentsInChildren<AudioSource>();
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        _click = GameObject.Find("Click").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
    }
    /// <summary>
    /// <para>Changes the volume of the music in the whole game</para>
    /// </summary>
    public void MusicVolume()
    {

        for (int i = 0; i < _music.Length; i++)
        {
            _music[i].volume = _musicSlider.value;
            PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        }
    }

    public void ResetVolume()
    {
        for (int i = 0; i < _music.Length; i++)
        {
            _musicSlider.value = 0.5f;
            _music[i].volume = _musicSlider.value;
            PlayerPrefs.SetFloat("SoundVolume", _musicSlider.value);
        }
    }

    public void Enter()
    {
        _click.Play();
    }

    public void Exit()
    {
        _click.Play();
    }
}
