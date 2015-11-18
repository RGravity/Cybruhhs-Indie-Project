using UnityEngine;
using System.Collections;

public class BuildingSpawnScript : MonoBehaviour {

    private BaseScript _baseStats;


	// Use this for initialization
	void Start () {
        _baseStats = GameObject.FindObjectOfType<BaseScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (_baseStats.Gold >= 150 && !this.GetComponent<ArrowTowerScript>())
        {
            gameObject.AddComponent<ArrowTowerScript>();
            gameObject.GetComponent<Material>().color = Color.red;
        }
        else
        {
            Debug.Log("not enough gold!");
        }
    }
}
