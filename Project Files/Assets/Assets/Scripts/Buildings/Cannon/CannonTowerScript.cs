﻿using UnityEngine;
using System.Collections;

public class CannonTowerScript : MonoBehaviour {
    private BuildingType _buildingType = BuildingType.Ground;
    private GameObject _bullet;
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;
    [SerializeField]
    private int _damage = 2;
    [SerializeField]
    private float _rateOfFire = 2;
    [SerializeField]
    private float _range = 2;
    private bool _allowShoot = true;
    private float _countdownTime;

    private GameObject _enemyInRange;
    private AudioSource _cannonFire;
    private CheckForMusicScript _check;
    // Use this for initialization
    void Start()
    {
        _thisPosition = this.gameObject.transform.position;
        _bullet = (GameObject)Resources.Load("CannonBullet");
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        if (_check.Check == true)
        {
            _cannonFire = GameObject.Find("CannonHit").GetComponent<AudioSource>();
        }
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
            if (enemy.gameObject.GetComponent<EnemyStatScript>().EnemyType == EnemyType.Ground)
            {
                if ((enemy.transform.position - _thisPosition).magnitude < _range)
                {
                    _enemyInRange = enemy.gameObject;
                    break;
                }
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

            GameObject bulletObject = Instantiate(_bullet);
            bulletObject.transform.position = new Vector3(this._thisPosition.x, this._thisPosition.y, -1);
            bulletObject.GetComponent<CannonBulletScript>().ShootEnemy(_enemyInRange, _damage);
            if (_cannonFire != null)
            {
                _cannonFire.Play();
            }

            Debug.Log("Enemy Shot");
            
        }
        else if (Time.time >= _countdownTime)
        {
            _allowShoot = true;
        }
    }
    /// <summary>
    /// Updates the Tier of the tower
    /// </summary>
    /// <returns></returns>
    public bool UpdateTowerCannon()
    {
        if (_tier > 3)
        {
            return false;
        }
        _tier++;
        return true;
    }
}
