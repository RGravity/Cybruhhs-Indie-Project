using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveScript : MonoBehaviour {

    [SerializeField]
    private GameObject _grunt;

    [SerializeField]
    private GameObject _heavy;

    [SerializeField]
    private GameObject _flying;

    [SerializeField]
    private GameObject _paladin;

    //[SerializeField]
    private int _gruntSize;

    public int GruntSize { get { return _gruntSize; } set { _gruntSize = value; } }

    //[SerializeField]
    private int _flyingSize;

    public int FlyingSize { get { return _flyingSize; } set { _flyingSize = value; } }

    //[SerializeField]
    private int _heavySize;

    public int HeavySize { get { return _heavySize; } set { _heavySize = value; } }

    //[SerializeField]
    private int _paladinSize;

    public int PaladinSize { get { return _paladinSize; } set { _paladinSize = value; } }

    private List<GameObject> _gruntList;
    private List<GameObject> _heavyList;
    private List<GameObject> _flyingList;
    private List<GameObject> _paladinList;

    private TileMapScript _map;
    private Vector3 _endPosition;

    public TileMapScript Map { get { return _map; } set { _map = value; } }

    private Vector3 _waveStartPosition;

    public WaveScript(TileMapScript pMap)
    {
        _map = Map;
        if (_map != null)
        {
            for (int i = 0; i < _gruntSize; i++)
            {
                _gruntList.Add(_grunt);
            }
            for (int i = 0; i < _heavySize; i++)
            {
                _heavyList.Add(_heavy);
            }
            for (int i = 0; i < _flyingSize; i++)
            {
                _flyingList.Add(_flying);
            }
            for (int i = 0; i < _paladinSize; i++)
            {
                _paladinList.Add(_paladin);
            }
            _waveStartPosition = _map.WaveStartPosition;
            _createMonstersGrunt();
            _createMonstersHeavy();
            _createMonstersFlying();
            _createMonstersPaladin();
        }
    }


    // Use this for initialization
    void Start () {
        _map = Map;
        if (_map != null)
        {
            //for (int i = 0; i < _gruntSize; i++)
            //{
            //    _gruntList.Add(_grunt);
            //}
            //for (int i = 0; i < _heavySize; i++)
            //{
            //    _heavyList.Add(_heavy);
            //}
            //for (int i = 0; i < _flyingSize; i++)
            //{
            //    _flyingList.Add(_flying);
            //}
            //for (int i = 0; i < _paladinSize; i++)
            //{
            //    _paladinList.Add(_paladin);
            //}
            _endPosition = _map.EndPosition;
            _waveStartPosition = _map.WaveStartPosition;
            _createMonstersGrunt();
            _createMonstersHeavy();
            _createMonstersFlying();
            _createMonstersPaladin();
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void _createMonstersGrunt()
    {
        for (int i = 0; i < _gruntSize; i++)
        {
            GameObject gruntObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            gruntObject.name = "Grunt";
            gruntObject.transform.position = _waveStartPosition;
            gruntObject.AddComponent<UnitScript>();
            gruntObject.GetComponent<UnitScript>().Map = _map;
            gruntObject.GetComponent<UnitScript>().TileX = (int)_map.WaveStartPosition.x;
            gruntObject.GetComponent<UnitScript>().TileY = (int)_map.WaveStartPosition.y;
            _map.GeneratePathTo(gruntObject.GetComponent<UnitScript>().TileX, gruntObject.GetComponent<UnitScript>().TileY);
            //grunt.name = "Grunt";
            //grunt.transform.position = _waveStartPosition;
        }
    }
    private void _createMonstersHeavy()
    {
        for (int i = 0; i < _heavySize; i++)
        {
            GameObject heavyObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            heavyObject.name = "Heavy";
            heavyObject.transform.position = _waveStartPosition;
            heavyObject.AddComponent<UnitScript>();
            heavyObject.GetComponent<UnitScript>().Map = _map;
            //heavy.name = "Heavy";
            //heavy.transform.position = _waveStartPosition;
        }
    }
    private void _createMonstersFlying()
    {
        for (int i = 0; i < _flyingSize; i++)
        {
            GameObject flyingObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            flyingObject.name = "Flying";
            flyingObject.transform.position = _waveStartPosition;
            flyingObject.AddComponent<UnitScript>();
            flyingObject.GetComponent<UnitScript>().Map = _map;
            //flying.name = "Flying";
            //flying.transform.position = _waveStartPosition;
        }
    }
    private void _createMonstersPaladin()
    {
        for (int i = 0; i < _paladinSize; i++)
        {
            GameObject paladinObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
            paladinObject.name = "Paladin";
            paladinObject.transform.position = _waveStartPosition;
            paladinObject.AddComponent<UnitScript>();
            paladinObject.GetComponent<UnitScript>().Map = _map;
            //paladin.name = "Paladin";
            //paladin.transform.position = _waveStartPosition;
        }
    }

}
