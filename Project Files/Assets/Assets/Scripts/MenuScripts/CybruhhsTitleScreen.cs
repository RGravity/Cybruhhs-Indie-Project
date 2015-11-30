using UnityEngine;
using System.Collections;

public class CybruhhsTitleScreen : MonoBehaviour {

	// Use this for initialization
    private float screenTime = 0;
    // Use this for initialization
    void Start()
    {
        screenTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > (screenTime + 7.3f))
        {
            Application.LoadLevel(1);
        }
    }
}
