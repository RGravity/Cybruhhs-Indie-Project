using UnityEngine;
using System.Collections;

public class ArrowBulletScript : MonoBehaviour {

    private GameObject _enemy;
    private int _damage;
    private AudioSource _arrowHit;
    // Use this for initialization
    void Start () {
        _arrowHit = GameObject.Find("ArrowHit").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemy != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _enemy.transform.position,7*Time.deltaTime);
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
        }
	}

    public void ShootEnemy(GameObject pEnemy, int pDamage)
    {
        _enemy = pEnemy;
        _damage = pDamage;
    }

}
