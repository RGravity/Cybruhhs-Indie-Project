using UnityEngine;
using System.Collections;

public class CancelButtonScript : MonoBehaviour {

    // Use this for initialization
    private UpgradeTowerScript [] _upgrade;
    private bool _check = false;


    void Start ()
    {
        _upgrade = GameObject.FindObjectsOfType<UpgradeTowerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        check ();
    }


    private void check()
    {
        if (_check == true)
        {
            _upgrade = GameObject.FindObjectsOfType<UpgradeTowerScript>();
            _check = false;
        }
    }

    public void ClickOn()
    {
        _check = true;
        for (int i = 0; i < _upgrade.Length; i++)
        {
            _upgrade[i].Disappear = true;
        }
       
    }
}
