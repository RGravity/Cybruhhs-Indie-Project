using UnityEngine;
using System.Collections;

public class MenuReturnButtonScript : MonoBehaviour
{

    // Use this for initialization
    private GameObject _creditCanvas;
    private GameObject _menuCanvas;
    private GameObject _optionsCanvas;
    private GameObject _selectionCanvas;
    private DontDestroyOnLoadMusicScript _map;
    private AudioSource _click;
    void Start()
    {
        _menuCanvas = GameObject.Find("MenuCanvas");
        _optionsCanvas = GameObject.Find("OptionsCanvas");
        _creditCanvas = GameObject.Find("CreditsCanvas");
        _selectionCanvas = GameObject.Find("SelectionCanvas");
        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
        _click = GameObject.Find("Click").GetComponent<AudioSource>();

    }
    /// <summary>
    /// <para>With this fuction, when clicking on the button, it enables the canvas needed while disabling the other canvas </para>
    /// </summary>
    public void ClickOn()
    {
        _selectionCanvas.GetComponent<Canvas>().enabled = false;
        _creditCanvas.GetComponent<Canvas>().enabled = false;
        _optionsCanvas.GetComponent<Canvas>().enabled = false;
        _menuCanvas.GetComponent<Canvas>().enabled = true;
        _map.Level = 0;
        _click.Play();
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


