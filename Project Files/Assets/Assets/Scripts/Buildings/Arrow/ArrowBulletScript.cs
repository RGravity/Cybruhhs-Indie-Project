using UnityEngine;
using System.Collections;

public class ArrowBulletScript : MonoBehaviour {

    private GameObject _enemy;
    private int _damage;
    private float _speed;
    private AudioSource _arrowHit;
    private CheckForMusicScript _check;
    private Vector3 _thisStartPosition;

    private AudioSource _heavy;
    private AudioSource _flying;
    private AudioSource _grunt;
    private AudioSource _paladin;
    // Use this for initialization
    void Start()
    {
        _thisStartPosition = this.gameObject.transform.position;
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        if (_check.Check == true)
        {
            _arrowHit = GameObject.Find("ArrowHit").GetComponent<AudioSource>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemy != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _enemy.transform.position, _speed * Time.deltaTime);
            
            //Rotation of the projectile
            Vector3 arrowVectorDir = _thisStartPosition - _enemy.transform.position;
            float angle = Mathf.Atan2(arrowVectorDir.y, arrowVectorDir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Vector3.Distance(transform.position, _enemy.transform.position) < 0.3f)
            {
                EnemyStatScript stats = _enemy.GetComponent<EnemyStatScript>();
                stats.LowerHealth(_damage);
                if (_arrowHit != null)
                {
                    _arrowHit.Play();

                }
                Destroy(this.gameObject);
            }
            else if (_enemy == null)
            {
                Destroy(this.gameObject);
            }
        }
        else if (_enemy == null)
        {
            Destroy(this.gameObject);
        }

	}

    public void ShootEnemy(GameObject pEnemy, int pDamage, float pSpeedProjectile)
    {
        _speed = pSpeedProjectile;
        _enemy = pEnemy;
        _damage = pDamage;
    }

}
