using UnityEngine;
using System.Collections;

public class UpgradeTowerScript : MonoBehaviour {

    private CheckForMusicScript _check;
    private ArrowTowerScript _arrowTower;
    private CannonTowerScript _cannonTower;
    private SlowTowerScript _slowTower;

    void Start()
    {
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
       

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseDown()
    {

        Debug.Log("It's Something!");
    }
}
