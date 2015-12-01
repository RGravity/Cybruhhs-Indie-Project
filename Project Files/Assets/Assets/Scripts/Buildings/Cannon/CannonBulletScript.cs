using UnityEngine;
using System.Collections;

public class CannonBulletScript : MonoBehaviour {

    private GameObject _enemy;
    private int _damage;
    private float _speed;
    private AudioSource _hit1;
    private AudioSource _hit2;
    private AudioSource _hit3;

    private CheckForMusicScript _check;

    // Use this for initialization
    void Start()
    {
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        if (_check.Check == true)
        {
            _hit1 = GameObject.Find("CannonShoot1").GetComponent<AudioSource>();
            _hit2 = GameObject.Find("CannonShoot2").GetComponent<AudioSource>();
            _hit3 = GameObject.Find("CannonShoot3").GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _enemy.transform.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _enemy.transform.position) < 0.3f)
            {
                EnemyStatScript stats = _enemy.GetComponent<EnemyStatScript>();
                stats.LowerHealth(_damage);
                
                int random = Random.Range(0, 2);

                switch (random)
                {
                    case 0:
                        if (_hit1 != null) _hit1.Play();
                        break;
                    case 1:
                        if (_hit1 != null) _hit2.Play();
                        break;
                    case 2:
                        if (_hit1 != null) _hit3.Play();
                        break;

                }
                Destroy(this.gameObject);
            }
        }
    }

    public void ShootEnemy(GameObject pEnemy, int pDamage, float pSpeedProjectile)
    {
        _speed = pSpeedProjectile;
        _enemy = pEnemy;
        _damage = pDamage;
    }
}
