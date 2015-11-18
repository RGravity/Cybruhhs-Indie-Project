using UnityEngine;
using System.Collections;

public enum enemyType
{
    Ground,
    Air,
    Both,
}

public class EnemyTestScript : MonoBehaviour {

    public int Speed = 1;
    public enemyType Type = enemyType.Ground;
    public int Health = 3;
    public int GoldDrop = 100;
    public int MonsterKill = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Translate(new Vector3(Speed/40f, 0, 0));
	}
}
