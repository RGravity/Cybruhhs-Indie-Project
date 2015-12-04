using UnityEngine;
using System.Collections;
using System.Linq;

public class ArrowTowerScript : MonoBehaviour {

    private BuildingType _buildingType = BuildingType.Both;
    private GameObject _bullet;
    [SerializeField]
    private int _tier = 1;
    private Vector3 _thisPosition;
    private float _timeLastShot;
    private int _tileX;
    private int _tileY;
    public int TileX { get { return _tileX; } }
    public int TileY { get { return _tileY; } }
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
    private float _range = 0f;
    [SerializeField]
    private float _rangeTier1 = 0;
    [SerializeField]
    private float _rangeTier2 = 0;
    [SerializeField]
    private float _rangeTier3 = 0;
    #endregion

    private bool _allowShoot = true;
    [SerializeField]
    private float _countdownTime;

    [SerializeField]
    private float _speedProjectile = 0; //this needs to be changed
    
    private GameObject _enemyInRange;
    private AudioSource _shoot1;
    private AudioSource _shoot2;
    private AudioSource _shoot3;
    private AudioSource _tree3;
    private AudioSource _tree4;
    private AudioSource _tree5;
    private AudioSource _tree6;
    private AudioSource _tree7;

    private bool _isNextEnemy = false;
    private int _indexEnemy = 0;

    private CheckForMusicScript _check;
    public int Tier { get { return _tier; } set { _tier = value; }  }

    private GameObject _towerArrowIdleLevel1;
    public GameObject TowerArrowIdleLevel1 { get { return _towerArrowIdleLevel1; } set { _towerArrowIdleLevel1 = value; } }

    private Vector3 _tile1;
    private Vector3 _tile2;
    private Vector3 _tile3;
    private Vector3 _tile4;
    public Vector3 Tile1 { get { return _tile1; } set { _tile1 = value; } }
    public Vector3 Tile2 { get { return _tile2; } set { _tile2 = value; } }
    public Vector3 Tile3 { get { return _tile3; } set { _tile3 = value; } }
    public Vector3 Tile4 { get { return _tile4; } set { _tile4 = value; } }

    // Use this for initializations
    void Start()
    {
        _tileX = (int)gameObject.transform.position.x;
        _tileY = (int)gameObject.transform.position.y;
        _thisPosition = this.gameObject.transform.position;
        _bullet = (GameObject)Resources.Load("ArrowBullet");
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        _thisPosition = this.gameObject.transform.position;
        if (_check.Check == true)
        {
            _shoot1 = GameObject.Find("ArrowShoot1").GetComponent<AudioSource>();
            _shoot2 = GameObject.Find("ArrowShoot2").GetComponent<AudioSource>();
            _shoot3 = GameObject.Find("ArrowShoot3").GetComponent<AudioSource>();

            _tree3 = GameObject.Find("Tree3").GetComponent<AudioSource>();
            _tree4 = GameObject.Find("Tree4").GetComponent<AudioSource>();
            _tree5 = GameObject.Find("Tree5").GetComponent<AudioSource>();
            _tree6 = GameObject.Find("Tree6").GetComponent<AudioSource>();
            _tree7 = GameObject.Find("Tree7").GetComponent<AudioSource>();
        }
        _rateOfFire = _rateOfFireTier1;
        _damage = _damageTier1;
        _range = _rangeTier1;
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
            if (_isNextEnemy)
            {
                _indexEnemy++;
                if (_indexEnemy >= enemies.Length)
                {
                    _indexEnemy = 0;
                }
                _isNextEnemy = false;
            }
            if ((enemies[_indexEnemy].transform.position - _thisPosition).magnitude < _range)
            {
                _enemyInRange = enemies[_indexEnemy].gameObject;
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
            _countdownTime = CountTimerScript.AddSeconds(_rateOfFire);
            if ((_enemyInRange.transform.position - _thisPosition).magnitude > _range)
            {
                _enemyInRange = null;
            }

            GameObject bulletObject = Instantiate(_bullet);
            bulletObject.transform.position =new Vector3(this._thisPosition.x,this._thisPosition.y,-1);
            bulletObject.GetComponent<ArrowBulletScript>().ShootEnemy(_enemyInRange, _damage,_speedProjectile);
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

    private void _playIdleAnimation()
    {
        _towerArrowIdleLevel1.transform.position = _thisPosition;
        Transform[] towerParts = _towerArrowIdleLevel1.GetComponentsInChildren<Transform>();
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

        if (_tier == 1)
        {
            leftTop.GetComponent<Animator>().Play("LeftTopTreeAnimation");
            rightTop.GetComponent<Animator>().Play("LeftTopTreeAnimation");
            leftBottom.GetComponent<Animator>().Play("LeftTopTreeAnimation");
            rightBottom.GetComponent<Animator>().Play("LeftTopTreeAnimation");
        }
        else if (_tier == 2)
        {
            leftTop.GetComponent<Animator>().Play("LeftTopTreeLvl2Animation");
            rightTop.GetComponent<Animator>().Play("RightTopTreeLvl2Animation");
            leftBottom.GetComponent<Animator>().Play("LeftBottomTreeLvl2Animation");
            rightBottom.GetComponent<Animator>().Play("RightBottomTreeLvl2Animation");
        }
        else if (_tier == 3)
        {
            leftTop.GetComponent<Animator>().Play("LeftTopTreeLvl3Animation");
            rightTop.GetComponent<Animator>().Play("RightTopTreeLvl3Animation");
            leftBottom.GetComponent<Animator>().Play("LeftBottomTreeLvl3Animation");
            rightBottom.GetComponent<Animator>().Play("RightBottomTreeLvl3Animation");
        }
    }

    /// <summary>
    /// Updates the Tier of the tower
    /// </summary>
    /// <returns></returns>
    public bool UpdateTowerArrow()
    {
        if (_tier > 3)
        {
            _tier = 3;
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
                if (_tree3 != null)
                {
                    _tree3.Play();
                }
            }
            else 
            {
                if (_tree4 != null)
                {
                    _tree4.Play();
                }
            }
            return true;
        }
        if (_tier == 3)
        {
            _damage = _damageTier3;
            _rateOfFire = _rateOfFireTier3;
            _range = _rangeTier3;
            int random = Random.Range(0, 9);
            if (random <= 3)
            {
                if (_tree5!= null)
                {
                    _tree5.Play();
                }
            }
            else if (random >= 4 && random <= 7)
            {
                if (_tree6 != null)
                {
                    _tree6.Play();
                }
            }
            else
            {
                if (_tree7 != null)
                {
                    _tree7.Play();
                }
            }
            return true;
        }
        return true;
    }
}
