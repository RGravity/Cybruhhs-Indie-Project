using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuStartButtonScript : MonoBehaviour {

    // Use this for initialization
    private GameObject _creditCanvas;
    private GameObject _menuCanvas;
    private GameObject _optionsCanvas;
    private GameObject _selectionCanvas;
    private GameObject _startButton;
    void Start ()
    {
        _menuCanvas = GameObject.Find("MenuCanvas");
        _optionsCanvas = GameObject.Find("OptionsCanvas");
        _creditCanvas = GameObject.Find("CreditsCanvas");
        _selectionCanvas = GameObject.Find("SelectionCanvas");
        _startButton = GameObject.Find("Start Button");

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    /// <summary>
    /// <para>With this you go to the game scene</para>
    /// </summary>
    public void ClickOn()
    {
        _selectionCanvas.GetComponent<Canvas>().enabled = true;
        _creditCanvas.GetComponent<Canvas>().enabled = false;
        _optionsCanvas.GetComponent<Canvas>().enabled = false;
        _menuCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void PointEnter()
    {
        _startButton.GetComponent<RectTransform>().sizeDelta = new Vector2(505, 355);
    }
    public void PointExit()
    {
        _startButton.GetComponent<RectTransform>().sizeDelta = new Vector2(286, 251);
    }
}
