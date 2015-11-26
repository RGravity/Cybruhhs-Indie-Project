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


    public int Health { get { return _health; } }

    void Start()
    {
        _baseStats = GameObject.FindObjectOfType<BaseScript>();
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
        Debug.Log("Current Health of Enemy:" + _health);
        if (_health <= 0)
        {
            //DROP GOLD HERE
            _baseStats.AddGold(_goldCarrying);
            //RemoveGameObject, And Fix Pathfinding.
            this.gameObject.GetComponent<UnitScript>().Map = null;
            Destroy(this.gameObject);
            Debug.Log("Enemy Died");
        }
    }

    /// <summary>
    /// Lower Health when Enemy reaches base
    /// </summary>
    private void _checkTarget()
    {
        //if (/*ReachedTarget*/ true)
        //{
        //    _baseStats.LowerHealth(_damageToBase);
        //    _baseStats.LowerGold(_goldCarrying);
        //    this.gameObject.GetComponent<UnitScript>().Map = null;
        //    Destroy(this.gameObject);
        //}
    }

    /// <summary>
    /// <para>Lower Base Health by x amount</para>
    /// </summary>
    public void LowerHealth(int pAmount)
    {
        _health = _health - pAmount;
    }
}
