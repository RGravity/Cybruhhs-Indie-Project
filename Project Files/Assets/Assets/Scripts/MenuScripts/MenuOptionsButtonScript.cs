using UnityEngine;
using System.Collections;

public class MenuOptionsButtonScript : MonoBehaviour {

    private GameObject _optionsCanvas;
    private GameObject _menuCanvas;
    private GameObject _creditCanvas;
    // Use this for initialization
    void Start ()
    {
        _menuCanvas = GameObject.Find("MenuCanvas");
        _optionsCanvas = GameObject.Find("OptionsCanvas");
        _creditCanvas = GameObject.Find("CreditsCanvas");
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    /// <summary>
    /// <para>With this fuction, when clicking on the button, it enables the canvas needed while disabling the other canvas </para>
    /// </summary>
    public void ClickOn()
    {
        _optionsCanvas.GetComponent<Canvas>().enabled = true;
        _menuCanvas.GetComponent<Canvas>().enabled = false;
        _creditCanvas.GetComponent<Canvas>().enabled = false;
    }
}
