﻿using UnityEngine;
using System.Collections;

public class MenuQuitButtonScript : MonoBehaviour {

    private AudioSource _click;
	// Use this for initialization
	void Start ()
    {
        _click = GameObject.Find("Click").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// <para>Quits the game</para>
    /// </summary>
    public void ClickOn()
    {
        _click.Play();
        Application.Quit();
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
