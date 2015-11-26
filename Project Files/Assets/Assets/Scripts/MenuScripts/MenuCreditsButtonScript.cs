using UnityEngine;
using System.Collections;

public class MenuCreditsButtonScript : MonoBehaviour {

    private GameObject _creditCanvas;
    private GameObject _menuCanvas;
    private GameObject _optionsCanvas;
    private GameObject _selectionCanvas;

    private AudioSource _click;
    // Use this for initialization
    void Start ()
    {
        _menuCanvas = GameObject.Find("MenuCanvas");
        _optionsCanvas = GameObject.Find("OptionsCanvas");
        _creditCanvas = GameObject.Find("CreditsCanvas");
        _selectionCanvas = GameObject.Find("SelectionCanvas");
        _click = GameObject.Find("Click").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// <para>With this fuction, when clicking on the button, it enables the canvas needed while disabling the other canvas </para>
    /// </summary>
    public void ClickOn()
    {
        _creditCanvas.GetComponent<Canvas>().enabled = true;
        _optionsCanvas.GetComponent<Canvas>().enabled = false;
        _selectionCanvas.GetComponent<Canvas>().enabled = false;
        _menuCanvas.GetComponent<Canvas>().enabled = false;
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
