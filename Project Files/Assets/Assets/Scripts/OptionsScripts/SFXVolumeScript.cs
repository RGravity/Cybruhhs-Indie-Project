using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SFXVolumeScript : MonoBehaviour {

    private Slider _soundSlider;
    private AudioSource[] _sound;
    private AudioSource _click;
    // Use this for initialization

    void Start () {
        _soundSlider = gameObject.GetComponent<Slider>();
        _sound = GameObject.FindObjectOfType <DontDestroyOnLoadSoundScript>().GetComponentsInChildren<AudioSource>();
        _soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        PlayerPrefs.SetFloat("SoundVolume", _soundSlider.value);
        _click = GameObject.Find("Click").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// <para>Changes the volume of the sound in the whole game</para>
    /// </summary>
    public void SoundVolume()
    {
        for (int i = 0; i < _sound.Length; i++)
        {
            _sound[i].volume = _soundSlider.value;
            PlayerPrefs.SetFloat("SoundVolume", _soundSlider.value);
            
        }
       
    }

    public void TikSound()
    {
        _click.Play();
    }

    public void ResetVolume()
    {

        for (int i = 0; i < _sound.Length; i++)
        {
            _soundSlider.value = 0.5f;
            _sound[i].volume = _soundSlider.value;
            PlayerPrefs.SetFloat("SoundVolume", _soundSlider.value);
            _click.Play();
        }
    }

}
