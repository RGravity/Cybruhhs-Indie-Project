using UnityEngine;
using System.Collections;

public class SlowTowerScript : MonoBehaviour {

    private BuildingType _buildingType = BuildingType.Both;
    private GameObject _bullet;
    [SerializeField]
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;

    #region SlowTime
    private float _slowTime = 2;
    [SerializeField]
    private float _slowTimeTier1 = 2;
    [SerializeField]
    private float _slowTimeTier2 = 5;
    [SerializeField]
    private float _slowTimeTier3 = 8;
    #endregion

    #region SlowAmount
    private float _slowAmount = 0.5f;
    [SerializeField]
    private float _slowAmountTier1 = 0.5f;
    [SerializeField]
    private float _slowAmountTier2 = 5;
    [SerializeField]
    private float _slowAmountTier3 = 8;
    #endregion

    #region Rate of fire
    private float _rateOfFire = 0;
    [SerializeField]
    private float _rateOfFireTier1 = 2;
    [SerializeField]
    private float _rateOfFireTier2 = 1.5f;
    [SerializeField]
    private float _rateOfFireTier3 = 0.5f;
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

    private bool _isNextEnemy = false;
    private int _indexForEnemy = 0;

    private bool _allowShoot = true;
    [SerializeField]
    private float _countdownTime;
    [SerializeField]
    private float _speedProjectile = 6;

    private GameObject _enemyInRange;
    private AudioSource _shoot1;
    private AudioSource _shoot2;
    private AudioSource _shoot3;

    private CheckForMusicScript _check;

    private int _nextEnemy = 1;
    public int Tier { get { return _tier; } set { _tier = value; } }

    //AUDIOSOURCE NEEDS TO BE CHANGED
    // Use this for initializations
    void Start()
    {
        _thisPosition = this.gameObject.transform.position;
        _bullet = (GameObject)Resources.Load("SlowBullet");
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        if (_check.Check == true)
        {
            _shoot1 = GameObject.Find("ArrowShoot1").GetComponent<AudioSource>();
            _shoot2 = GameObject.Find("ArrowShoot2").GetComponent<AudioSource>();
            _shoot3 = GameObject.Find("ArrowShoot3").GetComponent<AudioSource>();
        }
        _slowTime = _slowTimeTier1;
        _range = _rangeTier1;
        _rateOfFire = _rateOfFireTier1;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_enemyInRange == null)
        //{
        _checkForEnemies();
        //}
        //else
        //{
        if (_enemyInRange != null)
        {
            _shootEnemy();

        }
        //}

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
            if (_isNextEnemy || enemies[_indexForEnemy].IsSlowed)
            {
                _indexForEnemy++;
                if (_indexForEnemy >= enemies.Length)
                {
                    _indexForEnemy = 0;
                }
                _isNextEnemy = false;
            }
            if ((enemies[_indexForEnemy].transform.position - _thisPosition).magnitude < _range)
            {
                _enemyInRange = enemies[_indexForEnemy].gameObject;
            }
            else
            {
                _isNextEnemy = true;
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
            bulletObject.GetComponent<SlowBulletScript>().ShootEnemy(_enemyInRange, _slowTime, _slowAmount, _speedProjectile);
            int random = Random.Range(0, 2);

            switch (random)
            {
                case 0:
                    if (_shoot1 != null) _shoot1.Play();
                    break;
                case 1:
                    if (_shoot1 != null) _shoot2.Play();
                    break;
                case 2:
                    if (_shoot1 != null) _shoot3.Play();
                    break;

            }


            //Debug.Log("Enemy Shot");
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
    public bool UpdateTowerSlow()
    {
        if (_tier > 3)
        {
            return false;
        }
        _tier++;
        if (_tier == 2)
        {
            _slowTime = _slowTimeTier2;
            _rateOfFire = _rateOfFireTier2;
            _range = _rangeTier2;
            _slowAmount = _slowAmountTier2;
            return false;
        }
        if (_tier == 3)
        {
            _slowTime = _slowTimeTier3;
            _rateOfFire = _rateOfFireTier3;
            _range = _rangeTier3;
            _slowAmount = _slowAmountTier3;
            return false;
        }
        return true;
    }
}
