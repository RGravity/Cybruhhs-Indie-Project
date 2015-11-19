using UnityEngine;
using System.Collections;

public class MenuQuitButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// <para>Quits the game</para>
    /// </summary>
    public void ClickOn()
    {
        Application.Quit();
    }
}
