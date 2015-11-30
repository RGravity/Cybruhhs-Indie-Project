using UnityEngine;
using System.Collections;

public enum enemyType
{
    Ground,
    Air,
    Both,
}

public class EnemyTestScript : MonoBehaviour {

    [SerializeField]
    private int _speed = 1;
    [SerializeField]
    private enemyType Type = enemyType.Ground;
    [SerializeField]
    private int _monsterKill = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Translate(new Vector3(_speed/40f, 0, 0));

        if(CountTimerScript.IsTimerDown(Time.time, 0, 0, 5))
        {
            int test = 0;
        }
	}
}
