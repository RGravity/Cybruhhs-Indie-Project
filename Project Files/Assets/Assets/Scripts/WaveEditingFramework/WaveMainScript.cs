﻿using UnityEngine;
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

    [SerializeField]
    private int _debugLevel = 0;
    [SerializeField]
    private int _debugWave = 0;
    [SerializeField]
    private List<LevelWrapperScript> _levelList;
    private List<WavePartProgressScript> _waveProgressList;
    private int _currentWavePart = 0;
    private bool _startNextWavePart = true;
    private bool _LastPartDone = false;

    private TileMapScript _map;
    private List<Vector3> _listWaveStartPositions;
    private NodeScript[,] _graph;
    private Vector3 _endPosition;

    private bool _spawningStarted = false;

    public int DebugLevel { get { return _debugLevel; } }

    // Use this for initialization
    void Start () {
        _waveProgressList = new List<WavePartProgressScript>();

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
        _spawningStarted = true;
        
    }
	

    /// <summary>
    /// <para>Wave Spawn Control Method (Wave Control Room)</para>
    /// </summary>
	void Update () {
        if (_waveProgressList.Count > 0)
        {
            if (_waveProgressList[_waveProgressList.Count - 1].SecToWaitForNextPart != 0 && (_waveProgressList[_waveProgressList.Count - 1].TimeStarted + _waveProgressList[_waveProgressList.Count - 1].SecToWaitForNextPart) >= Time.time)
            {
                _startNextWavePart = true;
            }
        }
        


        //Enter when spawning is started and Last wave part is NOT done spawning
        if (_spawningStarted && !_LastPartDone)
        {
            //Enter when Debug level and wave in inspector are bigger than 0
            if (_debugLevel > 0 && _debugWave > 0)
            {
                //Enter when next part should be loaded
                if (_startNextWavePart)
                {
                    WaveTemplateScript wavePart = null;
                    do
                    {
                        if (_currentWavePart <= _levelList[_debugLevel - 1].WaveList[_debugWave - 1].WaveParts.Count - 1)
                        {
                            wavePart = _levelList[_debugLevel - 1].WaveList[_debugWave - 1].WaveParts[_currentWavePart];
                            _createIngameWavePart(wavePart);
                            _currentWavePart++;
                            _startNextWavePart = false;
                            if (_waveProgressList[_waveProgressList.Count-1].SecToWaitForNextPart > 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    } while (true);
                    
                }
                _updateWave();
            }
        }
    }

    private void _updateWave()
    {
        foreach (WavePartProgressScript part in _waveProgressList)
        {
            #region Grunt Wave Spawning
            if (part.GruntAmountSpawned == 0 && part.GruntAmountRemaining > 0)
            {
                _spawnGrunt(_listWaveStartPositions[part.Path], part.Path);
                part.GruntAmountSpawned++;
                part.GruntAmountRemaining--;
            }
            else if (part.TimeStarted + (part.GruntAmountSpawned * part.TimeBetweenEnemies) > Time.time)
            {
                continue;
            }
            else if (part.TimeStarted + (part.GruntAmountSpawned * part.TimeBetweenEnemies) <= Time.time)
            {
                if (part.GruntAmountRemaining > 0)
                {
                    _spawnGrunt(_listWaveStartPositions[part.Path], part.Path);
                    part.GruntAmountSpawned++;
                    part.GruntAmountRemaining--;
                }
            }
            #endregion

            #region Heavy Wave Spawning
            if (part.HeavyAmountSpawned == 0 && part.HeavyAmountRemaining > 0)
            {
                _spawnHeavy(_listWaveStartPositions[part.Path], part.Path);
                part.HeavyAmountSpawned++;
                part.HeavyAmountRemaining--;
            }
            else if (part.TimeStarted + (part.HeavyAmountSpawned * part.TimeBetweenEnemies) > Time.time)
            {
                continue;
            }
            else if (part.TimeStarted + (part.HeavyAmountSpawned * part.TimeBetweenEnemies) <= Time.time)
            {
                if (part.HeavyAmountRemaining > 0)
                {
                    _spawnHeavy(_listWaveStartPositions[part.Path], part.Path);
                    part.HeavyAmountSpawned++;
                    part.HeavyAmountRemaining--;
                }
            }
            #endregion

            #region Flying Wave Spawning
            if (part.FlyingAmountSpawned == 0 && part.FlyingAmountRemaining > 0)
            {
                _spawnFlying(_listWaveStartPositions[part.Path], part.Path);
                part.FlyingAmountSpawned++;
                part.FlyingAmountRemaining--;
            }
            else if (part.TimeStarted + (part.FlyingAmountSpawned * part.TimeBetweenEnemies) > Time.time)
            {
                continue;
            }
            else if (part.TimeStarted + (part.FlyingAmountSpawned * part.TimeBetweenEnemies) <= Time.time)
            {
                if (part.FlyingAmountRemaining > 0)
                {
                    _spawnFlying(_listWaveStartPositions[part.Path], part.Path);
                    part.FlyingAmountSpawned++;
                    part.FlyingAmountRemaining--;
                }
            }
            #endregion

            #region Paladin Wave Spawning
            if (part.PaladinAmountSpawned == 0 && part.PaladinAmountRemaining > 0)
            {
                _spawnPaladin(_listWaveStartPositions[part.Path], part.Path);
                part.PaladinAmountSpawned++;
                part.PaladinAmountRemaining--;
            }
            else if (part.TimeStarted + (part.PaladinAmountSpawned * part.TimeBetweenEnemies) > Time.time)
            {
                continue;
            }
            else if (part.TimeStarted + (part.PaladinAmountSpawned * part.TimeBetweenEnemies) <= Time.time)
            {
                if (part.PaladinAmountRemaining > 0)
                {
                    _spawnPaladin(_listWaveStartPositions[part.Path], part.Path);
                    part.PaladinAmountSpawned++;
                    part.PaladinAmountRemaining--;
                }
            }
            #endregion
        }
    }

    private void _createIngameWavePart(WaveTemplateScript part)
    {
        WavePartProgressScript partProgress = new WavePartProgressScript();
        partProgress.TimeStarted = Time.time;
        partProgress.GruntAmountRemaining = part.AmountOfGrunt;
        partProgress.GruntAmountSpawned = 0;
        partProgress.HeavyAmountRemaining = part.AmountOfHeavy;
        partProgress.HeavyAmountSpawned = 0;
        partProgress.FlyingAmountRemaining = part.AmountOfFlying;
        partProgress.FlyingAmountSpawned = 0;
        partProgress.PaladinAmountRemaining = part.AmountofPaladin;
        partProgress.PaladinAmountSpawned = 0;
        partProgress.TimeBetweenEnemies = part.TimeBetweenEnemies;
        partProgress.SecToWaitForNextPart = part.SecToWaitForNextPart;
        partProgress.Path = part.Path;
        _waveProgressList.Add(partProgress);

        Debug.Log(_waveProgressList.Count);
    }

    /// <summary>
    /// <para>Spawn 1 grunt at selected spawn</para>
    /// </summary>
    private void _spawnGrunt(Vector3 pTileSpawnPoint, int pPath)
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
        _setPath(gruntObject, pPath);
    }

    /// <summary>
    /// <para>Spawn 1 heavy at selected spawn</para>
    /// </summary>
    private void _spawnHeavy(Vector3 pTileSpawnPoint, int pPath)
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
        _setPath(heavyObject, pPath);
    }

    /// <summary>
    /// <para>Spawn 1 flying at selected spawn</para>
    /// </summary>
    private void _spawnFlying(Vector3 pTileSpawnPoint, int pPath)
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
        _setPath(flyingObject, pPath);
    }

    /// <summary>
    /// <para>Spawn 1 paladin at selected spawn</para>
    /// </summary>
    private void _spawnPaladin(Vector3 pTileSpawnPoint, int pPath)
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
        _setPath(paladinObject, pPath);
    }

    private void _setPath(GameObject pUnit, int pPath)
    {
        List<List<NodeScript>> possibleRoutes = new List<List<NodeScript>>();
        SearchPathScript search = new SearchPathScript(_map);
        if (pUnit.GetComponent<UnitScript>().CurrentPath != null)
        {
            pUnit.GetComponent<UnitScript>().CurrentPath = null;
        }
        possibleRoutes = search.SearchPaths(_graph[pUnit.GetComponent<UnitScript>().TileX, pUnit.GetComponent<UnitScript>().TileY], _graph[(int)_endPosition.x, (int)_endPosition.y]);
        if (pPath > 0 && pPath <= possibleRoutes.Count - 1)
        {
            pUnit.GetComponent<UnitScript>().CurrentPath = possibleRoutes[pPath-1];
        }
        else
        {
            Debug.Log("!!! Invalid Path Entered !!!");
        }
    }
}
