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

    public EnemyType EnemyType { get { return _enemyType; } }


    private BaseScript _baseStats;
    private AudioSource _heavy;
    private AudioSource _paladin;
    private AudioSource _flying;
    private AudioSource _grunt;


    public int Health { get { return _health; } }

    void Start()
    {
        _baseStats = GameObject.FindObjectOfType<BaseScript>();
        //_grunt = GameObject.Find("GruntBase").GetComponent<AudioSource>();
        //_flying = GameObject.Find("GriffonBase").GetComponent<AudioSource>();
        //_paladin = GameObject.Find("PaladinBase").GetComponent<AudioSource>();
        //_heavy = GameObject.Find("HeavyBase").GetComponent<AudioSource>();
        //this.GetComponent<MeshCollider>().isTrigger = true;
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
            Destroy(this.gameObject);
            //Debug.Log("Enemy Died");
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
            //if (this.name == "Grunt")
            //{
            //    _grunt.Play();
            //}
            //if (this.name == "Flying")
            //{
            //    _flying.Play();
            //}
            //if (this.name == "Paladin")
            //{
            //    _paladin.Play();
            //}
            //if (this.name == "Heavy")
            //{
            //    _heavy.Play();
            //}
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
