using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SFXVolumeScript : MonoBehaviour {

    private Slider _soundSlider;
    private AudioSource[] _sound;
    // Use this for initialization

    void Start () {
        _soundSlider = gameObject.GetComponent<Slider>();
        _sound = GameObject.FindObjectOfType <DontDestroyOnLoadSoundScript>().GetComponentsInChildren<AudioSource>();
        _soundSlider.value = _sound[0].volume;
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
        }
    }
}
