using UnityEngine;
using System.Collections;

public class SlowBulletScript : MonoBehaviour {

    private GameObject _enemy;
    private float _speedTime;
    private float _speed;
    private float _amountOfSpeed;
    private AudioSource _hit;

    private CheckForMusicScript _check;
   

    // Use this for initialization
    void Start()
    {
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        if (_check.Check == true)
        {
            _hit = GameObject.Find("SlowHit").GetComponent<AudioSource>();
            
        }
        _speedTime = CountTimerScript.AddSeconds(_speedTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _enemy.transform.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _enemy.transform.position) < 0.3f)
            {
                UnitScript stats = _enemy.GetComponent<UnitScript>();
                if (Time.time < _speedTime)
                {
                    stats.IsSlowed = true;
                    stats.Speed = _amountOfSpeed;
                }
                if (Time.time > _speedTime)
                {
                    stats.IsSlowed = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void ShootEnemy(GameObject pEnemy, float pSpeedTime, float pAmountOfSpeed, float pSpeedProjectile)
    {
        _speed = pSpeedProjectile;
        _enemy = pEnemy;
        _speedTime = pSpeedTime;
        _amountOfSpeed = pAmountOfSpeed;
    }
}
