using UnityEngine;
using System.Collections;

public class MenuReturnButtonScript : MonoBehaviour
{

    // Use this for initialization
    private DontDestroyOnLoadMusicScript _map;
    private AudioSource _click;
    private Animator _returnSelection;
    private Animator _returnOptions;
    private Animator _returnCredits;
    void Start()
    {
        _returnSelection = GameObject.Find("SelectionMenu").GetComponent<Animator>();
        _returnOptions = GameObject.Find("OptionsMenu").GetComponent<Animator>();
        _returnCredits = GameObject.Find("CreditsMenu").GetComponent<Animator>();
        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
        _click = GameObject.Find("Click").GetComponent<AudioSource>();

    }
    /// <summary>
    /// <para>With this fuction, when clicking on the button, it enables the canvas needed while disabling the other canvas </para>
    /// </summary>
    /// 
    public void ReturnFromSelection()
    {
        _returnSelection.Play("SelectionFadeOut");
        _map.Level = 0;
        _click.Play();
    }
    public void ReturnFromCredits()
    {
        _returnCredits.Play("CreditsFadeOut");
        _click.Play();
    }
    public void ReturnFromOptions()
    {
        _returnOptions.Play("OptionsFadeOut");
        _click.Play();
    }

    public void Enter()
    {
        _click.Play();
    }

    public void Exit()
    {
        _click.Play();
    }
}


