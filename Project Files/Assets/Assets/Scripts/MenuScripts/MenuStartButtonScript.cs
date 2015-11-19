using UnityEngine;
using System.Collections;

public class MenuStartButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
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
        Application.LoadLevel(1);
    }
}
