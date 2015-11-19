using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicVolumeScript : MonoBehaviour {

    private Slider _musicSlider;
    private AudioSource[] _music;
   
    void Start()
    {
        _musicSlider = gameObject.GetComponent <Slider>();
        _music = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>().GetComponentsInChildren<AudioSource>();
        _musicSlider.value = _music[0].volume;
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
        }
       
    }
}
