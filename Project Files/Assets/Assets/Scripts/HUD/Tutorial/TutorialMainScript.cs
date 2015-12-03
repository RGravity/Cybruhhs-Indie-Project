using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialMainScript : MonoBehaviour {

    private GameObject _tutorialBG;
    private GameObject _tutorialNextButton;
    private GameObject _tutorialSkipButton;
    private GameObject _tutorialImage;

    private List<Sprite> _tutorialSprites;
    private int _spriteIndex = 0;
    private bool _loadNextImage = false;
    private bool _skipTutorial = false;
    private bool _tutorialActive = false;

    // Use this for initialization
    void Start () {
        _tutorialBG = FindObjectOfType<TutorialBackgroundScript>().gameObject;
        _tutorialNextButton = FindObjectOfType<TutorialNextButtonScript>().gameObject;
        _tutorialSkipButton = FindObjectOfType<TutorialSkipButtonScript>().gameObject;
        _tutorialImage = FindObjectOfType<TutorialImageScript>().gameObject;
        _tutorialBG.SetActive(false);
        _tutorialNextButton.SetActive(false);
        _tutorialSkipButton.SetActive(false);
        _tutorialImage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (_loadNextImage && _tutorialSprites.Count > _spriteIndex)
        {
            _spriteIndex++;
            if (_tutorialSprites.Count == _spriteIndex)
            {
                _loadNextImage = false;
                _loadImage();
            }
            else
            {
                _tutorialBG.SetActive(false);
                _tutorialNextButton.SetActive(false);
                _tutorialSkipButton.SetActive(false);
                _tutorialImage.SetActive(false);
            }

        }

        if (_skipTutorial)
        {
            _tutorialSprites = null;
            _spriteIndex = 0;
            _loadNextImage = false;
            _skipTutorial = false;
            _tutorialActive = false;
            _tutorialBG.SetActive(false);
            _tutorialNextButton.SetActive(false);
            _tutorialSkipButton.SetActive(false);
            _tutorialImage.SetActive(false);

        }
    }

    public void NextImage()
    {
        _loadNextImage = true;
    }

    public void ShowTutorial(List<Sprite> pTutorialSprite)
    {
        _tutorialSprites = pTutorialSprite;
        _tutorialBG.SetActive(true);
        _tutorialImage.SetActive(true);
        _tutorialNextButton.SetActive(true);
        _tutorialSkipButton.SetActive(true);
        _loadImage();
    }

    private void _loadImage()
    {
        _tutorialImage.GetComponent<Image>().sprite = _tutorialSprites[_spriteIndex];
    }
}
