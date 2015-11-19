using UnityEngine;
using System.Collections;

public class BuildingSpawnScript : MonoBehaviour {

    private BaseScript _baseStats;
    [SerializeField]
    private Material _arrowTowerMaterial;
    [SerializeField]
    private int _arrowTowerPrice;


	// Use this for initialization
	void Start () {
        _baseStats = GameObject.FindObjectOfType<BaseScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// <para>Check if Gold is Sufficient</para>
    /// <para>If so build Arrow Tower</para>
    /// </summary>
    void OnMouseDown()
    {
        if (_baseStats.Gold >= _arrowTowerPrice && !this.GetComponent<ArrowTowerScript>())
        {
            gameObject.AddComponent<ArrowTowerScript>();
            gameObject.GetComponent<Renderer>().material = _arrowTowerMaterial;
        }
        else
        {
            Debug.Log("not enough gold!");
        }
    }
}
