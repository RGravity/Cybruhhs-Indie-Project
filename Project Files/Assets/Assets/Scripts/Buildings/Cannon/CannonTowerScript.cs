﻿using UnityEngine;
using System.Collections;

public class CannonTowerScript : MonoBehaviour {
    private BuildingType _buildingType = BuildingType.Ground;
    private GameObject _bullet;
    private GameObject _towerCannonAttack;
    [SerializeField]
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;

    #region Damage
    private int _damage = 2;
    [SerializeField]
    private int _damageTier1 = 2;
    [SerializeField]
    private int _damageTier2 = 3;
    [SerializeField]
    private int _damageTier3 = 4;
    #endregion

    #region Rate of fire
    private float _rateOfFire = 1;
    [SerializeField]
    private float _rateOfFireTier1 = 1;
    [SerializeField]
    private float _rateOfFireTier2 = 1;
    [SerializeField]
    private float _rateOfFireTier3 = 1;
    #endregion

    #region Range of fire
    private float _range = 5;
    [SerializeField]
    private float _rangeTier1 = 5;
    [SerializeField]
    private float _rangeTier2 = 5;
    [SerializeField]
    private float _rangeTier3 = 5;
    #endregion

    private bool _allowShoot = true;
    private float _countdownTime;

    [SerializeField]
    private float _speedProjectile = 4;

    private GameObject _enemyInRange;
    private AudioSource _cannonFire;
    private AudioSource _troll3;// Troll3 voice
    private AudioSource _troll4;//Troll4 voice
    private AudioSource _troll5;// Troll5 voice
    private AudioSource _troll6;//Troll6 voice
    private AudioSource _troll7;// Troll7 voice
    private CheckForMusicScript _check;
    public int Tier { get { return _tier; } set { _tier = value; } }
    // Use this for initialization
    void Start()
    {
        _towerCannonAttack = (GameObject)Resources.Load("Towers/TrollAttackLevel1");
        _thisPosition = this.gameObject.transform.position;
        _bullet = (GameObject)Resources.Load("CannonBullet");
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        if (_check.Check == true)
        {
            _cannonFire = GameObject.Find("CannonHit").GetComponent<AudioSource>();
            _troll3 = GameObject.Find("Troll3").GetComponent<AudioSource>();
            _troll4 = GameObject.Find("Troll4").GetComponent<AudioSource>();
            _troll5 = GameObject.Find("Troll5").GetComponent<AudioSource>();
            _troll6 = GameObject.Find("Troll6").GetComponent<AudioSource>();
            _troll7 = GameObject.Find("Troll7").GetComponent<AudioSource>();

        }
        _damage = _damageTier1;
        _range = _rangeTier1;
        _rateOfFire = _rateOfFireTier1;
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
        if (enemies.Length > 0)
        {
            if (enemies[enemies.Length - 1].gameObject.GetComponent<EnemyStatScript>().EnemyType == EnemyType.Ground)
            {
                if ((enemies[enemies.Length - 1].transform.position - _thisPosition).magnitude < _range)
                {
                    _enemyInRange = enemies[enemies.Length - 1].gameObject;
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
            bulletObject.GetComponent<CannonBulletScript>().ShootEnemy(_enemyInRange, _damage, _speedProjectile);
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
        if (_tier == 2)
        {
            _damage = _damageTier2;
            _rateOfFire = _rateOfFireTier2;
            _range = _rangeTier2;
            int random = Random.Range(0, 1);
            if (random == 0)
            {
                if (_troll3 != null)
                {
                    _troll3.Play();
                }
            }
            else
            {
                if (_troll4 != null)
                {
                    _troll4.Play();
                }
            }
            return false;
        }
        if (_tier == 3)
        {
            _damage = _damageTier3;
            _rateOfFire = _rateOfFireTier3;
            _range = _rangeTier3;

            int random = Random.Range(0, 9);
            if (random <= 3)
            {
                if (_troll5 != null)
                {
                    _troll5.Play();
                }
            }
            else if (random >= 4 && random <= 7)
            {
                if (_troll6 != null)
                {
                    _troll6.Play();
                }
            }
            else
            {
                if (_troll7 != null)
                {
                    _troll7.Play();
                }
            }
            return false;
        }
        return true;
    }
}
