using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveScript : MonoBehaviour
{
    
    private int _gruntSize;
    private int _flyingSize;
    private int _heavySize;
    private int _paladinSize;
    private List<GameObject> _gruntList;
    private List<GameObject> _heavyList;
    private List<GameObject> _flyingList;
    private List<GameObject> _paladinList;

    private TileMapScript _map;
    private Vector3 _endPosition;
    private Vector3 _waveStartPosition;

    public int GruntSize { get { return _gruntSize; } set { _gruntSize = value; } }
    public int FlyingSize { get { return _flyingSize; } set { _flyingSize = value; } }
    public int HeavySize { get { return _heavySize; } set { _heavySize = value; } }
    public int PaladinSize { get { return _paladinSize; } set { _paladinSize = value; } }
    public List<GameObject> GruntList { get { return _gruntList; } set { _gruntList = value; } }
    public TileMapScript Map { get { return _map; } set { _map = value; } }

    // Use this for initialization
    void Start()
    {
        _map = Map;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> CreateMonstersGrunt(int pGruntAmount)
    {
        List<GameObject> gruntList = new List<GameObject>();
        for (int i = 0; i < pGruntAmount; i++)
        {
            GameObject gruntObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            gruntObject.name = "Grunt";
            gruntObject.transform.localScale = new Vector3(gruntObject.transform.localScale.x / 3, gruntObject.transform.localScale.y / 3, gruntObject.transform.localScale.z / 3);
            gruntObject.AddComponent<UnitScript>();
            gruntObject.AddComponent<GruntScript>();
            gruntObject.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -1);
            gruntList.Add(gruntObject);
        }
        return gruntList;
    }
    public List<GameObject> CreateMonstersHeavy(int pHeavyAmount)
    {
        List<GameObject> heavyList = new List<GameObject>();
        for (int i = 0; i < pHeavyAmount; i++)
        {
            GameObject heavyObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            heavyObject.name = "Heavy";
            heavyObject.transform.localScale = new Vector3(heavyObject.transform.localScale.x / 3, heavyObject.transform.localScale.y / 3, heavyObject.transform.localScale.z / 3);

            heavyObject.AddComponent<UnitScript>();
            //heavyObject.AddComponent<GruntScript>();
            heavyObject.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -1);
            heavyList.Add(heavyObject);
        }
        return heavyList;
    }
    public List<GameObject> CreateMonstersFlying(int pFlyingAmount)
    {
        List<GameObject> flyingList = new List<GameObject>();
        for (int i = 0; i < pFlyingAmount; i++)
        {
            GameObject flyingObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            flyingObject.name = "Flying";
            flyingObject.transform.localScale = new Vector3(flyingObject.transform.localScale.x / 3, flyingObject.transform.localScale.y / 3, flyingObject.transform.localScale.z / 3);
            flyingObject.AddComponent<UnitScript>();
            //flyingObject.AddComponent<GruntScript>();
            flyingObject.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -1);
            flyingList.Add(flyingObject);
        }
        return flyingList;
    }
    public List<GameObject> CreateMonstersPaladin(int pPaladinAmount)
    {
        List<GameObject> paladinList = new List<GameObject>();
        for (int i = 0; i < pPaladinAmount; i++)
        {
            GameObject paladinObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
            paladinObject.name = "Paladin";
            paladinObject.transform.localScale = new Vector3(paladinObject.transform.localScale.x / 3, paladinObject.transform.localScale.y / 3, paladinObject.transform.localScale.z / 3);
            paladinObject.AddComponent<UnitScript>();
            //paladinObject.AddComponent<GruntScript>();
            paladinObject.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -1);
            paladinList.Add(paladinObject);
        }
        return paladinList;
    }

}
