using UnityEngine;
using System.Collections;
using System.Linq;

public class SlowTowerScript : MonoBehaviour {

    private BuildingType _buildingType = BuildingType.Both;
    private GameObject _bullet;
    [SerializeField]
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;

    #region SlowTime
    private float _slowTime = 0;
    [SerializeField]
    private float _slowTimeTier1 = 0;
    [SerializeField]
    private float _slowTimeTier2 = 0;
    [SerializeField]
    private float _slowTimeTier3 = 0;
    #endregion

    #region SlowAmount
    private float _slowAmount = 0;
    [SerializeField]
    private float _slowAmountTier1 = 0;
    [SerializeField]
    private float _slowAmountTier2 = 0;
    [SerializeField]
    private float _slowAmountTier3 = 0;
    #endregion

    #region Rate of fire
    private float _rateOfFire = 0;
    [SerializeField]
    private float _rateOfFireTier1 = 0;
    [SerializeField]
    private float _rateOfFireTier2 = 0;
    [SerializeField]
    private float _rateOfFireTier3 = 0;
    #endregion

    #region Range of fire
    private float _range = 0;
    [SerializeField]
    private float _rangeTier1 = 0;
    [SerializeField]
    private float _rangeTier2 = 0;
    [SerializeField]
    private float _rangeTier3 = 0;
    #endregion

    private bool _isNextEnemy = false;
    private int _indexForEnemy = 0;

    private bool _allowShoot = true;
    [SerializeField]
    private float _countdownTime;
    [SerializeField]
    private float _speedProjectile = 0;

    private GameObject _enemyInRange;
    private AudioSource _shoot;
    private AudioSource _spider3;
    private AudioSource _spider4;
    private AudioSource _spider5;
    private AudioSource _spider6;
    private AudioSource _spider7;
  

    private CheckForMusicScript _check;

    private int _nextEnemy = 1;
    public int Tier { get { return _tier; } set { _tier = value; } }

    private Vector3 _tile1;
    private Vector3 _tile2;
    private Vector3 _tile3;
    private Vector3 _tile4;
    public Vector3 Tile1 { get { return _tile1; } set { _tile1 = value; } }
    public Vector3 Tile2 { get { return _tile2; } set { _tile2 = value; } }
    public Vector3 Tile3 { get { return _tile3; } set { _tile3 = value; } }
    public Vector3 Tile4 { get { return _tile4; } set { _tile4 = value; } }

    private GameObject _towerSlowIdleLevel1;
    public GameObject TowerSlowIdleLevel1 { get { return _towerSlowIdleLevel1; } set { _towerSlowIdleLevel1 = value; } }

    //AUDIOSOURCE NEEDS TO BE CHANGED
    // Use this for initializations
    void Start()
    {
        _thisPosition = this.gameObject.transform.position;
        _bullet = (GameObject)Resources.Load("SlowBullet");
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        _thisPosition = this.gameObject.transform.position;
        if (_check.Check == true)
        {
            _shoot = GameObject.Find("SlowShoot").GetComponent<AudioSource>();
            _spider3 = GameObject.Find("Spider3").GetComponent<AudioSource>();
            _spider4 = GameObject.Find("Spider4").GetComponent<AudioSource>();
            _spider5 = GameObject.Find("Spider5").GetComponent<AudioSource>();
            _spider6 = GameObject.Find("Spider6").GetComponent<AudioSource>();
            _spider7 = GameObject.Find("Spider7").GetComponent<AudioSource>();
        }
        _slowTime = _slowTimeTier1;
        _range = _rangeTier1;
        _rateOfFire = _rateOfFireTier1;
        _slowAmount = _slowAmountTier1;
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
                _playIdleAnimation();
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
            _playAttackAnimation();
            _countdownTime = CountTimerScript.AddSeconds(_rateOfFire);
            if ((_enemyInRange.transform.position - _thisPosition).magnitude > _range)
            {
                _enemyInRange = null;
            }

            GameObject bulletObject = Instantiate(_bullet);
            bulletObject.transform.position = new Vector3(this._thisPosition.x, this._thisPosition.y, -1);
            bulletObject.GetComponent<SlowBulletScript>().ShootEnemy(_enemyInRange, _slowTime, _slowAmount, _speedProjectile);
            if (_shoot != null) _shoot.Play();
        }
        else if (Time.time >= _countdownTime)
        {
            _allowShoot = true;
        }
    }

    private void _playAttackAnimation()
    {
        //GameObject towerAttack = Instantiate(_towerCannonAttackLevel1);

        _towerSlowIdleLevel1.transform.position = _thisPosition;
        Transform[] towerParts = _towerSlowIdleLevel1.GetComponentsInChildren<Transform>();
        towerParts = towerParts.Except(new Transform[] { towerParts[0].transform }).ToArray();
        GameObject leftTop = null;
        GameObject rightTop = null;
        GameObject leftBottom = null;
        GameObject rightBottom = null;
        foreach (Transform part in towerParts)
        {
            if (part.name.Contains("LeftTop"))
            {
                leftTop = part.gameObject;
            }
            if (part.name.Contains("RightTop"))
            {
                rightTop = part.gameObject;
            }
            if (part.name.Contains("LeftBottom"))
            {
                leftBottom = part.gameObject;
            }
            if (part.name.Contains("RightBottom"))
            {
                rightBottom = part.gameObject;
            }
        }
        leftTop.transform.position = _tile1;
        rightTop.transform.position = _tile2;
        leftBottom.transform.position = _tile3;
        rightBottom.transform.position = _tile4;
        
        if (_tier == 2)
        {
            leftTop.GetComponent<Animator>().Play("LeftTopSpiderAttacklLvl3Animation");
            rightTop.GetComponent<Animator>().Play("RightTopSpiderAttacklLvl3Animation");
            leftBottom.GetComponent<Animator>().Play("LeftBottomSpiderAttacklLvl3Animation");
            rightBottom.GetComponent<Animator>().Play("RightBottomSpiderAttacklLvl3Animation");
        }
        else
        {
            leftTop.GetComponent<Animator>().Play("LeftTopSpiderAttacklLvl" + _tier + "Animation");
            rightTop.GetComponent<Animator>().Play("RightTopSpiderAttacklLvl" + _tier + "Animation");
            leftBottom.GetComponent<Animator>().Play("LeftBottomSpiderAttacklLvl" + _tier + "Animation");
            rightBottom.GetComponent<Animator>().Play("RightBottomSpiderAttacklLvl" + _tier + "Animation");
        }
        //}
    }
    private void _playIdleAnimation()
    {
        _towerSlowIdleLevel1.transform.position = _thisPosition;
        Transform[] towerParts = _towerSlowIdleLevel1.GetComponentsInChildren<Transform>();
        towerParts = towerParts.Except(new Transform[] { towerParts[0].transform }).ToArray();
        GameObject leftTop = null;
        GameObject rightTop = null;
        GameObject leftBottom = null;
        GameObject rightBottom = null;
        foreach (Transform part in towerParts)
        {
            if (part.name.Contains("LeftTop"))
            {
                leftTop = part.gameObject;
            }
            if (part.name.Contains("RightTop"))
            {
                rightTop = part.gameObject;
            }
            if (part.name.Contains("LeftBottom"))
            {
                leftBottom = part.gameObject;
            }
            if (part.name.Contains("RightBottom"))
            {
                rightBottom = part.gameObject;
            }
        }
        leftTop.transform.position = _tile1;
        rightTop.transform.position = _tile2;
        leftBottom.transform.position = _tile3;
        rightBottom.transform.position = _tile4;

        //if (_tier == 1)
        //{

        //    leftTop.GetComponent<Animator>().Play("LeftTopSpiderIdleAnimation");
        //    rightTop.GetComponent<Animator>().Play("RightTopSpiderIdleAnimation");
        //    leftBottom.GetComponent<Animator>().Play("LeftBottomSpiderIdleAnimation");
        //    rightBottom.GetComponent<Animator>().Play("RightBottomSpiderIdleAnimation");
        //}
        //else
        //{
        if (_tier == 2)
        {
            leftTop.GetComponent<Animator>().Play("LeftTopSpiderIdleLvl3Animation");
            rightTop.GetComponent<Animator>().Play("RightTopSpiderIdleLvl3Animation");
            leftBottom.GetComponent<Animator>().Play("LeftBottomSpiderIdleLvl3Animation");
            rightBottom.GetComponent<Animator>().Play("RightBottomSpiderIdleLvl3Animation");
        }
        else
        {
            leftTop.GetComponent<Animator>().Play("LeftTopSpiderIdleLvl" + _tier + "Animation");
            rightTop.GetComponent<Animator>().Play("RightTopSpiderIdleLvl" + _tier + "Animation");
            leftBottom.GetComponent<Animator>().Play("LeftBottomSpiderIdleLvl" + _tier + "Animation");
            rightBottom.GetComponent<Animator>().Play("RightBottomSpiderIdleLvl" + _tier + "Animation");
        }
        //}
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
            int random = Random.Range(0, 1);
            if (random == 0)
            {
                if (_spider3 != null)
                {
                    _spider3.Play();
                }
            }
            else
            {
                if (_spider4 != null)
                {
                    _spider4.Play();
                }
            }

            return false;
        }
        if (_tier == 3)
        {
            _slowTime = _slowTimeTier3;
            _rateOfFire = _rateOfFireTier3;
            _range = _rangeTier3;
            _slowAmount = _slowAmountTier3;
            int random = Random.Range(0, 9);
            if (random <= 3)
            {
                if (_spider5 != null)
                {
                    _spider5.Play();
                }
            }
            else if (random >= 4 && random <= 7)
            {
                if (_spider6 != null)
                {
                    _spider6.Play();
                }
            }
            else
            {
                if (_spider7 != null)
                {
                    _spider7.Play();
                }
            }
            return false;
        }
        return true;
    }
}
