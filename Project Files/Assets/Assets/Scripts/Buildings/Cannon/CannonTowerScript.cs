using UnityEngine;
using System.Collections;
using System.Linq;

public class CannonTowerScript : MonoBehaviour {
    private BuildingType _buildingType = BuildingType.Ground;
    private GameObject _bullet;
    [SerializeField]
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;

    #region Damage
    private int _damage = 0;
    [SerializeField]
    private int _damageTier1 = 0;
    [SerializeField]
    private int _damageTier2 = 0;
    [SerializeField]
    private int _damageTier3 = 0;
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

    private bool _allowShoot = true;
    private float _countdownTime;

    [SerializeField]
    private float _speedProjectile = 0;

    private GameObject _enemyInRange;
    private AudioSource _cannonFire;
    private AudioSource _troll3;// Troll3 voice
    private AudioSource _troll4;//Troll4 voice
    private AudioSource _troll5;// Troll5 voice
    private AudioSource _troll6;//Troll6 voice
    private AudioSource _troll7;// Troll7 voice
    private CheckForMusicScript _check;
    public int Tier { get { return _tier; } set { _tier = value; } }

    //Cannon towers prefabs except Level 1 Idle
    private GameObject _towerCannonAttackLevel1;
    private GameObject _towerCannonAttackLevel2;
    private GameObject _towerCannonAttackLevel3;
    private GameObject _towerCannonIdleLevel1;
    private GameObject _towerCannonIdleLevel2;
    private GameObject _towerCannonIdleLevel3;

    //Properties of the Towers so it can swap positions in BuildingSpawnScript;
    public GameObject TowerCannonIdleLevel2 { get { return _towerCannonIdleLevel2; } }
    public GameObject TowerCannonIdleLevel3 { get { return _towerCannonIdleLevel3; } }
    public GameObject TowerCannonIdleLevel1 { get { return _towerCannonIdleLevel1; } set { _towerCannonIdleLevel1 = value; } }

    public int testId = 1;

    //Positions of the tiles
    private Vector3 _tile1;
    private Vector3 _tile2;
    private Vector3 _tile3;
    private Vector3 _tile4;
    public Vector3 Tile1 { set { _tile1 = value; } }
    public Vector3 Tile2 { set { _tile2 = value; } }
    public Vector3 Tile3 { set { _tile3 = value; } }
    public Vector3 Tile4 { set { _tile4 = value; } }

    private int _nextEnemy=0;

    // Use this for initialization
    void Start()
    {
        //Get the prefabs
        _towerCannonAttackLevel1 = (GameObject)Resources.Load("Towers/TrollAttackLevel1");
        _towerCannonAttackLevel2 = (GameObject)Resources.Load("Towers/TrollAttackLevel2");
        _towerCannonAttackLevel3 = (GameObject)Resources.Load("Towers/TrollAttackLevel3");
        //_towerCannonIdleLevel1 = (GameObject)Resources.Load("Towers/TrollIdleLevel1");
        _towerCannonIdleLevel2 = (GameObject)Resources.Load("Towers/TrollIdleLevel2");
        _towerCannonIdleLevel3 = (GameObject)Resources.Load("Towers/TrollIdleLevel3");

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
        _checkForEnemies();
        _shootEnemy();
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
            foreach (UnitScript Unit in enemies)
            {
                if ((Unit.transform.position - _thisPosition).magnitude < _range && Unit.gameObject.GetComponent<EnemyStatScript>().EnemyType == EnemyType.Ground)
                {
                    _enemyInRange = Unit.gameObject;
                }
            }
            //if (enemies[enemies.Length - 1 - _nextEnemy].gameObject.GetComponent<EnemyStatScript>().EnemyType == EnemyType.Ground)
            //{
            //    if ((enemies[enemies.Length - 1 - _nextEnemy].transform.position - _thisPosition).magnitude < _range)
            //    {
            //        _enemyInRange = enemies[enemies.Length - 1].gameObject;
            //    }
            //    else
            //    {
            //        _nextEnemy++;
            //        if (_nextEnemy >= enemies.Length)
            //        {
            //            _nextEnemy = 0;
            //        }
            //        _playIdleAnimation();
            //    }
            //}
        }
        if (_enemyInRange == null)
        {
            _playIdleAnimation();
        }
    }

    /// <summary>
    /// <para>Shoot bullet and only attack once every x seconds, when in range</para>
    /// </summary>
    private void _shootEnemy()
    {
        if (_allowShoot && _enemyInRange != null)
        {
            _playAttackAnimation();
            _allowShoot = false;
            _countdownTime = CountTimerScript.AddSeconds(_rateOfFire);
            if ((_enemyInRange.transform.position - _thisPosition).magnitude > _range)
            {
                _enemyInRange = null;
            }
            if (_enemyInRange != null)
            {
                GameObject bulletObject = Instantiate(_bullet);
                bulletObject.transform.position = new Vector3(this._thisPosition.x, this._thisPosition.y, -1);
                bulletObject.GetComponent<CannonBulletScript>().ShootEnemy(_enemyInRange, _damage, _speedProjectile);
                if (_cannonFire != null)
                {
                    _cannonFire.Play();
                }
            }
            
            
        }
        else if (Time.time >= _countdownTime)
        {
            _allowShoot = true;
        }
    }


    private void _playAttackAnimation()
    {
        // GameObject towerAttack = Instantiate(_towerCannonAttackLevel1);
        _towerCannonIdleLevel1.transform.position = _thisPosition;
        Transform child1 = _towerCannonIdleLevel1.transform.GetChild(0);
        Transform child2 = _towerCannonIdleLevel1.transform.GetChild(1);
        Transform child3 = _towerCannonIdleLevel1.transform.GetChild(2);
        Transform child4 = _towerCannonIdleLevel1.transform.GetChild(3);
        Transform[] towerParts = new Transform[] { child1, child2, child3, child4 };
        //Transform[] towerParts = _towerCannonIdleLevel1.gameObject.GetComponentsInChildren<Transform>();
        //towerParts = towerParts.Except(new Transform[] { towerParts[0].transform }).ToArray();
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


        leftTop.GetComponent<Animator>().Play("LeftTopTrollAttackLvl"+_tier+"Animation");
        rightTop.GetComponent<Animator>().Play("RightTopTrollAttackLvl" + _tier + "Animation");
        leftBottom.GetComponent<Animator>().Play("LeftBottomTrollAttackLvl" + _tier + "Animation");
        rightBottom.GetComponent<Animator>().Play("RightBottomTrollAttackLvl" + _tier + "Animation");
    }
    private void _playIdleAnimation()
    {
        _towerCannonIdleLevel1.transform.position = _thisPosition;
        Transform child1 = _towerCannonIdleLevel1.transform.GetChild(0);
        Transform child2 = _towerCannonIdleLevel1.transform.GetChild(1);
        Transform child3 = _towerCannonIdleLevel1.transform.GetChild(2);
        Transform child4 = _towerCannonIdleLevel1.transform.GetChild(3);
        Transform[] towerParts = new Transform[] { child1, child2, child3, child4 };
        //Transform[] towerParts = _towerCannonIdleLevel1.gameObject.GetComponentsInChildren<Transform>();
        //towerParts = towerParts.Except(new Transform[] { towerParts[0].transform }).ToArray();
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


        leftTop.GetComponent<Animator>().Play("LeftTopTrollIdleLvl" + _tier + "Animation");
        rightTop.GetComponent<Animator>().Play("RightTopTrollIdleLvl" + _tier + "Animation");
        leftBottom.GetComponent<Animator>().Play("LeftBottomTrollIdleLvl" + _tier + "Animation");
        rightBottom.GetComponent<Animator>().Play("RightBottomTrollIdleLvl" + _tier + "Animation");
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
