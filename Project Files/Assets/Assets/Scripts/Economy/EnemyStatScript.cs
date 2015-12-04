using UnityEngine;
using System.Collections;

public class EnemyStatScript : MonoBehaviour {

    [SerializeField]
    private int _health;
    [SerializeField]
    private int _goldCarrying;
    [SerializeField]
    private int _damageToBase;
    [SerializeField]
    private EnemyType _enemyType;
    [SerializeField]
    private Sprite _shadowSprite;

    public EnemyType EnemyType { get { return _enemyType; } }


    private BaseScript _baseStats;
    private AudioSource _heavyBase;
    private AudioSource _paladinBase;
    private AudioSource _flyingBase;
    private AudioSource _gruntBase;

    private AudioSource _heavyDeathArrow;
    private AudioSource _paladinDeathArrow;
    private AudioSource _flyingDeathArrow;
    private AudioSource _gruntDeathArrow;

    private AudioSource _heavyDeathCannon;
    private AudioSource _paladinDeathCannon;
    private AudioSource _flyingDeathCannon;
    private AudioSource _gruntDeathCannon;


    public int Health { get { return _health; } }

    void Start()
    {
        GameObject shadowObject = new GameObject();
        shadowObject.name = "Shadow";
        shadowObject.transform.position = this.transform.position;
        shadowObject.AddComponent<SpriteRenderer>().sprite = _shadowSprite;
        shadowObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        shadowObject.transform.localPosition = new Vector3(shadowObject.transform.localPosition.x+0.09f, shadowObject.transform.localPosition.y - 0.5f, shadowObject.transform.localPosition.z);
        shadowObject.transform.parent = this.transform;
        _baseStats = GameObject.FindObjectOfType<BaseScript>();
        _gruntBase = GameObject.Find("GruntBase").GetComponent<AudioSource>();
        _flyingBase = GameObject.Find("GriffonBase").GetComponent<AudioSource>();
        _paladinBase = GameObject.Find("PaladinBase").GetComponent<AudioSource>();
        _heavyBase = GameObject.Find("HeavyBase").GetComponent<AudioSource>();
        _gruntDeathArrow = GameObject.Find("GruntArrow").GetComponent<AudioSource>();
        _flyingDeathArrow = GameObject.Find("GriffonArrow").GetComponent<AudioSource>();
        _paladinDeathArrow = GameObject.Find("PaladinArrow").GetComponent<AudioSource>();
        _heavyDeathArrow = GameObject.Find("HeavyArrow").GetComponent<AudioSource>();
        _gruntDeathCannon = GameObject.Find("GruntCannon").GetComponent<AudioSource>();
        _flyingDeathCannon = GameObject.Find("GriffonCannon").GetComponent<AudioSource>();
        _paladinDeathCannon = GameObject.Find("PaladinCannon").GetComponent<AudioSource>();
        _heavyDeathCannon = GameObject.Find("HeavyCannon").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        _checkHealth();
        _checkTarget();
	}

    /// <summary>
    /// <para>Check if Health is 0</para>
    /// <para>if Health is 0 then drop Gold</para>
    /// </summary>
    private void _checkHealth()
    {
        //Debug.Log("Current Health of Enemy:" + _health);
        if (_health <= 0)
        {
            //DROP GOLD HERE
            _baseStats.AddGold(_goldCarrying);
            //RemoveGameObject, And Fix Pathfinding.
            this.gameObject.GetComponent<UnitScript>().Map = null;
            if (this.name == "Grunt")
            {
                _gruntDeathArrow.Play();
            }
            if (this.name == "Flying")
            {
               _flyingDeathArrow.Play();
            }
            if (this.name == "Paladin")
            {
                _paladinDeathArrow.Play();
            }
            if (this.name == "Heavy")
            {
                _heavyDeathArrow.Play();
            }
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Lower Health when Enemy reaches base
    /// </summary>
    private void _checkTarget()
    {
        if (this.GetComponent<UnitScript>().CurrentPath == null)
        {
            _baseStats.LowerHealth(_damageToBase);
            _baseStats.LowerGold(_goldCarrying);
            if (this.name == "Grunt")
            { 
                _gruntBase.Play();
            }
            if (this.name == "Flying")
            {
                _flyingBase.Play();
            }
            if (this.name == "Paladin")
            {
                _paladinBase.Play();
            }
            if (this.name == "Heavy")
            {
                _heavyBase.Play();
            }
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// <para>Lower Base Health by x amount</para>
    /// </summary>
    public void LowerHealth(int pAmount)
    {
        _health = _health - pAmount;
    }
}
