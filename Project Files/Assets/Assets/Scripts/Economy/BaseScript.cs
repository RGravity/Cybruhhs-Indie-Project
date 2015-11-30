using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {

    [SerializeField]
    private int _health;
    [SerializeField]
    private int _gold;

    public int Gold { get { return _gold; } }

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
        }
    }

    /// <summary>
    /// <para>Lower Base Health by x amount</para>
    /// </summary>
    public void LowerHealth(int pAmount)
    {
        _health = _health - pAmount;
    }

    /// <summary>
    /// <para>Add Gold by x amount</para>
    /// </summary>
    public void AddGold(int pAmount)
    {
        _gold = _gold + pAmount;
    }

    /// <summary>
    /// <para>Lower Gold by x amount</para>
    /// </summary>
    public void LowerGold(int pAmount)
    {
        _gold = _gold - pAmount;
    }


}
