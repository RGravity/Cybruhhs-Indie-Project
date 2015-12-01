using UnityEngine;
using System.Collections;

public class UpgradeTowerScript : BuildingSpawnScript {


    [SerializeField]
    private Camera _myCam;

    private CheckForMusicScript _check;
    private TileMapScript _tileMap;
    private GameObject _selectedTile;

    void Start ()
    {
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        _tileMap = FindObjectOfType<TileMapScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void Upgrade()
    {

    }
}
