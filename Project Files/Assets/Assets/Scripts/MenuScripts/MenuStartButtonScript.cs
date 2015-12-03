using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuStartButtonScript : MonoBehaviour {

    // Use this for initialization
    
    private GameObject _startButton;
    private Animator _selectionAnimator;
    private DontDestroyOnLoadMusicScript _map;

    private AudioSource _click;
    void Start ()
    {
        _startButton = GameObject.Find("Start Button");
        _selectionAnimator = GameObject.Find("SelectionMenu").GetComponent<Animator>(); 
        _click = GameObject.Find("Click").GetComponent<AudioSource>();
        _map = FindObjectOfType<DontDestroyOnLoadMusicScript>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    /// <summary>
    /// <para>With this you go to the game scene</para>
    /// </summary>
    public void ClickOn()
    {
        _selectionAnimator.Play("SelectionFadeIn");
        _map.Level = 0;
        _click.Play();
    }

    public void PointEnter()
    {
        _startButton.GetComponent<RectTransform>().sizeDelta = new Vector2(828, 500);
        _click.Play();
    }
    public void PointExit()
    {
        _startButton.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 384);
        _click.Play();
    }
}
