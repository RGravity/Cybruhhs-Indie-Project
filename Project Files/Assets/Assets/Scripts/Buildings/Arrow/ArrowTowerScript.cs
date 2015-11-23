using UnityEngine;
using System.Collections;


public class ArrowTowerScript : MonoBehaviour {

    private BuildingType _buildingType = BuildingType.Both;
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;
    private GameObject _bullet;
    [SerializeField]
    private int _damage = 2;
    [SerializeField]
    private float _rateOfFire = 2;
    [SerializeField]
    private float _range = 10;
    private bool _allowShoot = true;
    private float _countdownTime;
    
    private GameObject _enemyInRange;

    // Use this for initialization
    void Start()
    {
        _thisPosition = this.gameObject.transform.position;
        _bullet = (GameObject)Resources.Load("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyInRange == null)
        {
            _checkForEnemies();
        }
        else
        {
            _shootEnemy();
        }
        
    }

    /// <summary>
    /// <para>Check all the heroes, and find one that is in range of the turret</para>
    /// <para>Then make that one the target</para>
    /// </summary>
    private void _checkForEnemies()
    {
        UnitScript[] enemies = GameObject.FindObjectsOfType<UnitScript>();
        foreach (UnitScript enemy in enemies)
        {
            if ((enemy.transform.position - _thisPosition).magnitude < _range)
            {
                _enemyInRange = enemy.gameObject;
                break;
            }
        }
    }

    /// <summary>
    /// <para>Shoot bullet and only attack once every x seconds, when in range</para>
    /// </summary>
    private void _shootEnemy()
    {
        if (_allowShoot)
        {
            //_enemyInRange.GetComponent<EnemyStatScript>().LowerHealth(_damage);
            _allowShoot = false;
            _countdownTime = CountTimerScript.AddSeconds(_rateOfFire);
            if ((_enemyInRange.transform.position - _thisPosition).magnitude > _range)
            {
                _enemyInRange = null;
            }

            Instantiate(_bullet);

            Debug.Log("Enemy Shot");
        }
        else if (Time.time >= _countdownTime)
        {
            _allowShoot = true;
        }
    }
}
