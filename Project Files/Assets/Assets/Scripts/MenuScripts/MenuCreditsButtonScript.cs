using UnityEngine;
using System.Collections;

public class MenuCreditsButtonScript : MonoBehaviour {

    private GameObject _creditCanvas;
    private GameObject _menuCanvas;
    private GameObject _optionsCanvas;
    // Use this for initialization
    void Start ()
    {
        _menuCanvas = GameObject.FindGameObjectWithTag("MenuCanvas");
        _creditCanvas = GameObject.FindGameObjectWithTag("CreditCanvas");
        _optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas");
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
        _menuCanvas.GetComponent<Canvas>().enabled = false;
    }
}
