using UnityEngine;
using System.Collections;

public class MenuCreditsButtonScript : MonoBehaviour {
    
    private Animator _returnCredits;
    private AudioSource _click;
    // Use this for initialization
    void Start ()
    {
        _returnCredits = GameObject.Find("CreditsMenu").GetComponent<Animator>();
        _click = GameObject.Find("Click").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// <para>With this fuction, when clicking on the button, it enables the canvas needed while disabling the other canvas </para>
    /// </summary>
    public void ClickOn()
    {
        _returnCredits.Play("CreditsFadeIn");
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
