using UnityEngine;
using System.Collections;

public class MenuReturnButtonScript : MonoBehaviour
{

    // Use this for initialization
    private GameObject _optionsCanvas;
    private GameObject _menuCanvas;
    private GameObject _creditCanvas;
    void Start()
    {
        _menuCanvas = GameObject.Find ("MenuCanvas");
        _optionsCanvas = GameObject.Find("OptionsCanvas");
        _creditCanvas = GameObject.Find("CreditsCanvas");
    }

    /// <summary>
    /// <para>With this fuction, when clicking on the button, it enables the canvas needed while disabling the other canvas </para>
    /// </summary>
    public void ClickOn()
    {
        _optionsCanvas.GetComponent<Canvas>().enabled = false;
        _creditCanvas.GetComponent<Canvas>().enabled = false;
        _menuCanvas.GetComponent<Canvas>().enabled = true;
    }
}


