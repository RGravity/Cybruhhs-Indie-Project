using UnityEngine;
using System.Collections;

public class EnemyStatScript : MonoBehaviour {

    [SerializeField]
    private int _health;
    [SerializeField]
    private int _goldCarrying;
    private BaseScript _baseStats;

    public int Health { get { return _health; } }

    void Start()
    {
        _baseStats = GameObject.FindObjectOfType<BaseScript>();
    }

	// Update is called once per frame
	void Update () {
        _checkHealth();
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
            Destroy(this.gameObject);
            Debug.Log("Enemy Died");
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
