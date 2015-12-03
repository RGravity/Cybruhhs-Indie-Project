using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {

    [SerializeField]
    private int _health;
    [SerializeField]
    private int _gold;

    private bool _isDead;

    public int Gold { get { return _gold; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    void Start()
    {
        FindObjectOfType<GoldTextScript>().UpdateGold(_gold);
        FindObjectOfType<LivesTextScript>().UpdateLives(_health);
    }

    void Update()
    {
        //Debug.Log("Current Gold amount is: " + _gold);
        _checkHealth();
    }

    /// <summary>
    /// <para>Check if Health is 0</para>
    /// <para>if Health is 0 then Game Over</para>
    /// </summary>
    private void _checkHealth()
    {
        if (_health <= 0)
        {
            //TEMP; HERE COMES GAME OVER
            Time.timeScale = 0;
            _isDead = true;
        }
    }

    /// <summary>
    /// <para>Lower Base Health by x amount</para>
    /// </summary>
    public void LowerHealth(int pAmount)
    {
        _health = _health - pAmount;
        FindObjectOfType<LivesTextScript>().UpdateLives(_health);
    }

    /// <summary>
    /// <para>Add Gold by x amount</para>
    /// </summary>
    public void AddGold(int pAmount)
    {
        _gold = _gold + pAmount;
        FindObjectOfType<GoldTextScript>().UpdateGold(_gold);
    }

    /// <summary>
    /// <para>Lower Gold by x amount</para>
    /// </summary>
    public void LowerGold(int pAmount)
    {
        _gold = _gold - pAmount;
        FindObjectOfType<GoldTextScript>().UpdateGold(_gold);
    }


}
