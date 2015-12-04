using UnityEngine;
using System.Collections;

public class TutorialSkipButtonScript : MonoBehaviour {

    // Use this for initialization
    private bool _skipButtonClicked = false;

    public bool SkipButtonClicked { get { return _skipButtonClicked; } set { _skipButtonClicked = value; } }

    void Update()
    {
        if (_skipButtonClicked)
        {
            _skipButtonClicked = false;
            FindObjectOfType<TutorialMainScript>().SkipTutorial();
        }
    }
}
