using UnityEngine;
using System.Collections;

public class BuildArrowTowerScript : MonoBehaviour
{

    // Use this for initialization
    private BaseScript _baseStats;
    [SerializeField]
    private Material _arrowTowerMaterial;
    [SerializeField]
    private int _arrowTowerPrice;
    private int _amountOftiles;

    private bool _buildAllowed = false;

    private BuildingSpawnScript[] _spawn;
    void Start()
    {
        _spawn = GameObject.FindObjectsOfType<BuildingSpawnScript>();
        _baseStats = GameObject.FindObjectOfType<BaseScript>();
        for (int i = 0; i < _spawn.Length; i++)
        {
            _amountOftiles = i;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
    //void OnMouseEnter()
    //{ 
    //        if (_spawn[_amountOftiles].MenuTowers == true && Input.GetMouseButtonUp(0))
    //        {
                
    //            if (_baseStats.Gold >= _arrowTowerPrice && !this.GetComponent<ArrowTowerScript>())
    //            {
    //                gameObject.AddComponent<ArrowTowerScript>();
    //                gameObject.GetComponent<Renderer>().material = _arrowTowerMaterial;
    //                _baseStats.LowerGold(_arrowTowerPrice);
    //                _spawn[_amountOftiles].MenuTowers = false;
    //            }

    //            else
    //            {
    //                Debug.Log("not enough gold or already a turret there!");
    //                _spawn[_amountOftiles].MenuTowers = false;

    //            }
    //        }

    //    }
    //}

    //void OnMouseExit()
    //{
    //    for (int i = 0; i < _spawn.Length; i++)
    //    {
    //        _spawn[i].MenuTowers = false;
    //        Debug.Log(true);
    //    }
    //}
