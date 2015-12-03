using UnityEngine;
using System.Collections;

public class TutorialNextButtonScript : MonoBehaviour {

    private bool _nextButtonClicked = false;

    public bool NextButtonClicked { get { return _nextButtonClicked; } set { _nextButtonClicked = value; } }

    void Update()
    {
        if (_nextButtonClicked)
        {
            FindObjectOfType<TutorialMainScript>().NextImage();
        }
    }
}
