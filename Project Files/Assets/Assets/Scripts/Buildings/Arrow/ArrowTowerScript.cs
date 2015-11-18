using UnityEngine;
using System.Collections;


public class ArrowTowerScript : MonoBehaviour {

    private BuildingType _buildingType = BuildingType.Both;
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;
    private GameObject _bullet;
    
    private GameObject EnemyInRange;

    // Use this for initialization
    void Start()
    {
        _thisPosition = this.gameObject.transform.position;
        _bullet = (GameObject)Resources.Load("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyInRange == null)
        {
            _checkForEnemies();
        }
        else
        {
            _shootEnemy();
        }
        
    }

    private void _checkForEnemies()
    {
        Object[] enemies = GameObject.FindObjectsOfType<EnemyTestScript>();
        foreach (GameObject enemy in enemies)
        {
            if ((enemy.transform.position - _thisPosition).magnitude < 10)
            {
                EnemyInRange = enemy;
                break;
            }
        }
    }

    private void _shootEnemy()
    {
        
    }
}
