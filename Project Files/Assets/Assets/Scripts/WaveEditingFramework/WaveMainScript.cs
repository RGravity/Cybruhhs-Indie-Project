using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveMainScript : MonoBehaviour {

    private GameObject _grunt;
    private GameObject _heavy;
    private GameObject _flying;
    private GameObject _paladin;

    private GameObject _enemyParent;
    private GameObject _gruntParent;
    private GameObject _heavyParent;
    private GameObject _flyingParent;
    private GameObject _paladinParent;

    private TileMapScript _map;
    private List<Vector3> _listWaveStartPositions;
    private NodeScript[,] _graph;
    private Vector3 _endPosition;

    // Use this for initialization
    void Start () {
        _grunt = (GameObject)Resources.Load("Enemies/Grunt");
        _heavy = (GameObject)Resources.Load("Enemies/Heavy");
        _flying = (GameObject)Resources.Load("Enemies/Flying");
        _paladin = (GameObject)Resources.Load("Enemies/Paladin");

        _enemyParent = new GameObject();
        _enemyParent.name = "Enemies";

        _gruntParent = new GameObject();
        _gruntParent.name = "Grunts";
        _gruntParent.transform.parent = _enemyParent.transform;
        _heavyParent = new GameObject();
        _heavyParent.name = "Heavys";
        _heavyParent.transform.parent = _enemyParent.transform;
        _flyingParent = new GameObject();
        _flyingParent.name = "Flyings";
        _flyingParent.transform.parent = _enemyParent.transform;
        _paladinParent = new GameObject();
        _paladinParent.name = "Paladins";
        _paladinParent.transform.parent = _enemyParent.transform;
    }

    public void StartSpawning(List<Vector3> pListWaveStartPositions, TileMapScript pMap, NodeScript[,] pGraph, Vector3 pEndPosition)
    {
        _listWaveStartPositions = pListWaveStartPositions;
        _map = pMap;
        _graph = pGraph;
        _endPosition = pEndPosition;

        _spawnGrunt(pListWaveStartPositions[Random.Range(0, pListWaveStartPositions.Count-1)]);
        _spawnHeavy(pListWaveStartPositions[Random.Range(0, pListWaveStartPositions.Count - 1)]);
        _spawnFlying(pListWaveStartPositions[Random.Range(0, pListWaveStartPositions.Count - 1)]);
        _spawnPaladin(pListWaveStartPositions[Random.Range(0, pListWaveStartPositions.Count - 1)]);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    /// <summary>
    /// <para>Spawn 1 grunt at selected spawn</para>
    /// </summary>
    private void _spawnGrunt(Vector3 pTileSpawnPoint)
    {
        GameObject gruntObject = (GameObject)Instantiate(_grunt, new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1), Quaternion.identity);
        gruntObject.name = "TEST Grunt";
        gruntObject.transform.parent = _gruntParent.transform;
        gruntObject.AddComponent<UnitScript>();
        gruntObject.GetComponent<UnitScript>().Speed = 1.5f;
        gruntObject.GetComponent<UnitScript>().TileX = (int)pTileSpawnPoint.x;
        gruntObject.GetComponent<UnitScript>().TileY = (int)pTileSpawnPoint.y;
        gruntObject.GetComponent<UnitScript>().Map = _map;
        //gruntObject.AddComponent<GruntScript>();
        gruntObject.transform.localPosition = new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1);
        _setPath(gruntObject);
    }

    /// <summary>
    /// <para>Spawn 1 heavy at selected spawn</para>
    /// </summary>
    private void _spawnHeavy(Vector3 pTileSpawnPoint)
    {
        GameObject heavyObject = (GameObject)Instantiate(_heavy, new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1), Quaternion.identity);
        heavyObject.name = "TEST Heavy";
        heavyObject.transform.parent = _heavyParent.transform;
        heavyObject.AddComponent<UnitScript>();
        heavyObject.GetComponent<UnitScript>().Speed = 0.3f;
        heavyObject.GetComponent<UnitScript>().TileX = (int)pTileSpawnPoint.x;
        heavyObject.GetComponent<UnitScript>().TileY = (int)pTileSpawnPoint.y;
        heavyObject.GetComponent<UnitScript>().Map = _map;
        //gruntObject.AddComponent<GruntScript>();
        heavyObject.transform.localPosition = new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1);
        _setPath(heavyObject);
    }

    /// <summary>
    /// <para>Spawn 1 flying at selected spawn</para>
    /// </summary>
    private void _spawnFlying(Vector3 pTileSpawnPoint)
    {
        GameObject flyingObject = (GameObject)Instantiate(_flying, new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1), Quaternion.identity);
        flyingObject.name = "TEST Flying";
        flyingObject.transform.parent = _flyingParent.transform;
        flyingObject.AddComponent<UnitScript>();
        flyingObject.GetComponent<UnitScript>().Speed = 2f;
        flyingObject.GetComponent<UnitScript>().TileX = (int)pTileSpawnPoint.x;
        flyingObject.GetComponent<UnitScript>().TileY = (int)pTileSpawnPoint.y;
        flyingObject.GetComponent<UnitScript>().Map = _map;
        //gruntObject.AddComponent<GruntScript>();
        flyingObject.transform.localPosition = new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1);
        _setPath(flyingObject);
    }

    /// <summary>
    /// <para>Spawn 1 paladin at selected spawn</para>
    /// </summary>
    private void _spawnPaladin(Vector3 pTileSpawnPoint)
    {
        GameObject paladinObject = (GameObject)Instantiate(_paladin, new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1), Quaternion.identity);
        paladinObject.name = "TEST Paladin";
        paladinObject.transform.parent = _paladinParent.transform;
        paladinObject.AddComponent<UnitScript>();
        paladinObject.GetComponent<UnitScript>().Speed = 0.8f;
        paladinObject.GetComponent<UnitScript>().TileX = (int)pTileSpawnPoint.x;
        paladinObject.GetComponent<UnitScript>().TileY = (int)pTileSpawnPoint.y;
        paladinObject.GetComponent<UnitScript>().Map = _map;
        //gruntObject.AddComponent<GruntScript>();
        paladinObject.transform.localPosition = new Vector3((int)pTileSpawnPoint.x, (int)pTileSpawnPoint.y, -1);
        _setPath(paladinObject);
    }

    private void _setPath(GameObject pUnit)
    {
        List<List<NodeScript>> possibleRoutes = new List<List<NodeScript>>();
        SearchPathScript search = new SearchPathScript(_map);
        if (pUnit.GetComponent<UnitScript>().CurrentPath != null)
        {
            pUnit.GetComponent<UnitScript>().CurrentPath = null;
        }
        possibleRoutes = search.SearchPaths(_graph[pUnit.GetComponent<UnitScript>().TileX, pUnit.GetComponent<UnitScript>().TileY], _graph[(int)_endPosition.x, (int)_endPosition.y]);
        pUnit.GetComponent<UnitScript>().CurrentPath = possibleRoutes[1];
    }
}
