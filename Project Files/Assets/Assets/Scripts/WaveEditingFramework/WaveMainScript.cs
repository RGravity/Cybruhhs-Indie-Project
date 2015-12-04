using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveMainScript : MonoBehaviour {

    //Prefab GameObjects
    private GameObject _grunt;
    private GameObject _heavy;
    private GameObject _flying;
    private GameObject _paladin;

    //Parent GameObjects
    private GameObject _enemyParent;
    private GameObject _gruntParent;
    private GameObject _heavyParent;
    private GameObject _flyingParent;
    private GameObject _paladinParent;

    //TileMapScript variables
    private TileMapScript _map;
    private List<Vector3> _listWaveStartPositions;
    private List<Vector3> _listWaveEndPositions;
    private NodeScript[,] _graph;
    
    // ---- wave editing framework variables ----

    //Level and wave variables
    private int _currentLevel = 1;
    private int _currentWave = 0;
    //debug level and wave
    [SerializeField]
    private int _debugLevel = 0;
    [SerializeField]
    private int _debugWave = 0;
    //waves List
    [SerializeField]
    private List<LevelWrapperScript> _levelList;
    //other variables
    private bool _spawningStarted = false;
    private List<WavePartProgressScript> _waveProgressList;
    private int _currentWavePart = 0;
    private bool _startNextWavePart = true;
    private bool _LastPartDone = true;
    private BuildingWaveScript _buildWaveScript;
    //variable for build wave
    private bool _buildWaveStarted = false;
    private bool _buildWaveEnded = true;
    private bool _spawningDone = true;
    private bool _tutorialShown = false;


    public int DebugLevel { get { return _debugLevel; } }
    public bool BuildWaveStarted { get { return _buildWaveStarted; } set { _buildWaveStarted = value; } }
    public bool BuildWaveEnded { get { return _buildWaveEnded; } set { _buildWaveEnded = value; } }

    // Use this for initialization
    void Start () {
        _waveProgressList = new List<WavePartProgressScript>();
        _buildWaveScript = FindObjectOfType<BuildingWaveScript>();

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

    public void StartSpawning(List<Vector3> pListWaveStartPositions, List<Vector3> pListWaveEndPositions, TileMapScript pMap, NodeScript[,] pGraph, int pLevel)
    {
        _listWaveStartPositions = pListWaveStartPositions;
        _listWaveEndPositions = pListWaveEndPositions;
        _map = pMap;
        _graph = pGraph;
        _spawningStarted = true;
        _currentLevel = pLevel;
        FindObjectOfType<WaveIndicatorScript>().SetWaveIndicator(_currentWave, _levelList[_currentLevel - 1].WaveList.Count);
        
    }
	

    /// <summary>
    /// <para>Wave Spawn Control Method (Wave Control Room)</para>
    /// </summary>
	void Update () {

        //checking if the next wave part is allowed to spawn
        if (_waveProgressList.Count > 0)
        {
            if (_waveProgressList[_waveProgressList.Count - 1].SecToWaitForNextPart != 0 && (_waveProgressList[_waveProgressList.Count - 1].TimeStarted + _waveProgressList[_waveProgressList.Count - 1].SecToWaitForNextPart) <= Time.time)
            {
                _startNextWavePart = true;
            }
        }

        if (!_buildWaveStarted && _buildWaveEnded && _spawningDone)
        {
            UnitScript[] enemiesLeft = FindObjectsOfType<UnitScript>();
            if (enemiesLeft.Length == 0)
            {
                _spawningDone = false;
                _LastPartDone = false;
                _startNextWavePart = true;
                _currentWavePart = 0;
                _currentWave++;
                _tutorialShown = false;
                if (_debugLevel > 0 || _debugWave > 0)
                {
                    _debugWave++;
                }

                if (_levelList[_debugLevel - 1].WaveList.Count < _debugWave || _levelList[_currentLevel - 1].WaveList.Count < _currentWave)
                {
                    GameObject.FindObjectOfType<ScoreScreenScript>().EndLevel = true;
                }

                _buildWaveScript.StartBuildingWave();
            }
        }

        //Enter when spawning is started and Last wave part is NOT done spawning
        if (_spawningStarted && !_LastPartDone && _buildWaveEnded)
        {
            FindObjectOfType<WaveIndicatorScript>().SetWaveIndicator(_currentWave, _levelList[_currentLevel - 1].WaveList.Count);
            //wave spawning
            if (_debugLevel > 0 || _debugWave > 0)
            #region Debug Wave Spawning
            {
                if (_debugWave == 0)
                {
                    _debugWave = 1;
                }
                if (_debugLevel == 0)
                {
                    _debugLevel = 1;
                }
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

            }
            #endregion
            else if (_debugLevel == 0 && _debugWave == 0)
            #region Real Wave Spawning
            {
                //Enter when next part should be loaded
                if (_startNextWavePart)
                {
                    WaveTemplateScript wavePart = null;
                    do
                    {
                        if (_currentWavePart <= _levelList[_currentLevel - 1].WaveList[_currentWave - 1].WaveParts.Count - 1)
                        {
                            wavePart = _levelList[_currentLevel - 1].WaveList[_currentWave - 1].WaveParts[_currentWavePart];
                            _createIngameWavePart(wavePart);
                            _currentWavePart++;
                            _startNextWavePart = false;

                            if (_waveProgressList[_waveProgressList.Count - 1].SecToWaitForNextPart > 0)
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
            }
            #endregion
            
            _updateWave();
        }

        

        
    }

    /// <summary>
    /// <para>Update wave</para>
    /// <para>Spawn monsters with the correct timing and update the wave part variables</para>
    /// </summary>
    private void _updateWave()
    {
        foreach (WavePartProgressScript part in _waveProgressList)
        {
            Vector3 startPosition = _listWaveStartPositions[part.BeginPositionNr - 1];
            Vector3 endPosition = _listWaveEndPositions[part.EndPositionNr - 1];

            if (part.GruntAmountRemaining == 0 && part.HeavyAmountRemaining == 0 && part.FlyingAmountRemaining == 0 && part.PaladinAmountRemaining == 0)
            {
                _spawningDone = true;
            }
            else
            {
                _spawningDone = false;
            }

            #region Grunt Wave Spawning
            if (part.GruntAmountSpawned == 0 && part.GruntAmountRemaining > 0)
            {
                _spawnGrunt(part.Path, startPosition, endPosition);
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
                    _spawnGrunt(part.Path, startPosition, endPosition);
                    part.GruntAmountSpawned++;
                    part.GruntAmountRemaining--;
                }
            }
            #endregion

            #region Heavy Wave Spawning
            if (part.HeavyAmountSpawned == 0 && part.HeavyAmountRemaining > 0)
            {
                _spawnHeavy(part.Path, startPosition, endPosition);
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
                    _spawnHeavy(part.Path, startPosition, endPosition);
                    part.HeavyAmountSpawned++;
                    part.HeavyAmountRemaining--;
                }
            }
            #endregion

            #region Flying Wave Spawning
            if (part.FlyingAmountSpawned == 0 && part.FlyingAmountRemaining > 0)
            {
                _spawnFlying(part.Path, startPosition, endPosition);
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
                    _spawnFlying(part.Path, startPosition, endPosition);
                    part.FlyingAmountSpawned++;
                    part.FlyingAmountRemaining--;
                }
            }
            #endregion

            #region Paladin Wave Spawning
            if (part.PaladinAmountSpawned == 0 && part.PaladinAmountRemaining > 0)
            {
                _spawnPaladin(part.Path, startPosition, endPosition);
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
                    _spawnPaladin(part.Path, startPosition, endPosition);
                    part.PaladinAmountSpawned++;
                    part.PaladinAmountRemaining--;
                }
            }
            #endregion

            if (part.TutorialSprites.Count > 0 && !_tutorialShown)
            {
                FindObjectOfType<TutorialMainScript>().ShowTutorial(part.TutorialSprites);
                _tutorialShown = true;
            }

        }
    }

    /// <summary>
    /// <para>Create wave part inside the code, for correctly spawning enemies</para>
    /// </summary>
    /// <param name="part"></param>
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
        partProgress.TutorialSprites = part.TutorialSprites;

        //if no path is set take the first path
        if (part.Path == 0){
            partProgress.Path = 1;
        } else {
            partProgress.Path = part.Path;
        }
        //if no Begin position is set take the first one.
        if (part.BeginPositionNr == 0) {
            partProgress.BeginPositionNr = 1;
        } else {
            partProgress.BeginPositionNr = part.BeginPositionNr;
        }
        //if no End position is set take the first one.
        if (part.EndPositionNr == 0) {
            partProgress.EndPositionNr = 1;
        } else {
            partProgress.EndPositionNr = part.EndPositionNr;
        }
        
        _waveProgressList.Add(partProgress);

        Debug.Log(_waveProgressList.Count);
    }

    /// <summary>
    /// <para>Spawn 1 grunt at selected spawn</para>
    /// </summary>
    private void _spawnGrunt(int pPath, Vector3 pStartPosition, Vector3 pEndPosition)
    {
        GameObject gruntObject = (GameObject)Instantiate(_grunt, new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1), Quaternion.identity);
        gruntObject.name = "Grunt";
        gruntObject.transform.parent = _gruntParent.transform;
        gruntObject.AddComponent<UnitScript>();
        gruntObject.GetComponent<UnitScript>().Speed = 1.5f;
        gruntObject.GetComponent<UnitScript>().TileX = (int)pStartPosition.x;
        gruntObject.GetComponent<UnitScript>().TileY = (int)pStartPosition.y;
        gruntObject.GetComponent<UnitScript>().Map = _map;
        gruntObject.transform.localPosition = new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1);
        _setPath(gruntObject, pPath, pStartPosition, pEndPosition);
    }

    /// <summary>
    /// <para>Spawn 1 heavy at selected spawn</para>
    /// </summary>
    private void _spawnHeavy(int pPath, Vector3 pStartPosition, Vector3 pEndPosition)
    {
        GameObject heavyObject = (GameObject)Instantiate(_heavy, new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1), Quaternion.identity);
        heavyObject.name = "Heavy";
        heavyObject.transform.parent = _heavyParent.transform;
        heavyObject.AddComponent<UnitScript>();
        heavyObject.GetComponent<UnitScript>().Speed = 0.6f;
        heavyObject.GetComponent<UnitScript>().TileX = (int)pStartPosition.x;
        heavyObject.GetComponent<UnitScript>().TileY = (int)pStartPosition.y;
        heavyObject.GetComponent<UnitScript>().Map = _map;
        heavyObject.transform.localPosition = new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1);
        _setPath(heavyObject, pPath, pStartPosition, pEndPosition);
    }

    /// <summary>
    /// <para>Spawn 1 flying at selected spawn</para>
    /// </summary>
    private void _spawnFlying(int pPath, Vector3 pStartPosition, Vector3 pEndPosition)
    {
        GameObject flyingObject = (GameObject)Instantiate(_flying, new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1), Quaternion.identity);
        flyingObject.name = "Flying";
        flyingObject.transform.parent = _flyingParent.transform;
        flyingObject.AddComponent<UnitScript>();
        flyingObject.GetComponent<UnitScript>().Speed = 2f;
        flyingObject.GetComponent<UnitScript>().TileX = (int)pStartPosition.x;
        flyingObject.GetComponent<UnitScript>().TileY = (int)pStartPosition.y;
        flyingObject.GetComponent<UnitScript>().Map = _map;
        flyingObject.transform.localPosition = new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1);
        _setPath(flyingObject, pPath, pStartPosition, pEndPosition);
    }

    /// <summary>
    /// <para>Spawn 1 paladin at selected spawn</para>
    /// </summary>
    private void _spawnPaladin(int pPath, Vector3 pStartPosition, Vector3 pEndPosition)
    {
        GameObject paladinObject = (GameObject)Instantiate(_paladin, new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1), Quaternion.identity);
        paladinObject.name = "Paladin";
        paladinObject.transform.parent = _paladinParent.transform;
        paladinObject.AddComponent<UnitScript>();
        paladinObject.GetComponent<UnitScript>().Speed = 0.8f;
        paladinObject.GetComponent<UnitScript>().TileX = (int)pStartPosition.x;
        paladinObject.GetComponent<UnitScript>().TileY = (int)pStartPosition.y;
        paladinObject.GetComponent<UnitScript>().Map = _map;
        paladinObject.transform.localPosition = new Vector3((int)pStartPosition.x, (int)pStartPosition.y, -1);
        _setPath(paladinObject, pPath, pStartPosition, pEndPosition);
    }

    private void _setPath(GameObject pUnit, int pPath, Vector3 pStartPosition, Vector3 pEndPosition)
    {
        List<List<NodeScript>> possibleRoutes = new List<List<NodeScript>>();
        SearchPathScript search = new SearchPathScript(_map);
        if (pUnit.GetComponent<UnitScript>().CurrentPath != null)
        {
            pUnit.GetComponent<UnitScript>().CurrentPath = null;
        }
        possibleRoutes = search.SearchPaths(_graph[(int)pStartPosition.x, (int)pStartPosition.y], _graph[(int)pEndPosition.x, (int)pEndPosition.y]);
        //if (pPath > 0 && pPath <= possibleRoutes.Count - 1)
        //{
            pUnit.GetComponent<UnitScript>().CurrentPath = possibleRoutes[pPath-1];
        //}
        //else
        //{
        //    Debug.Log("!!! Invalid Path Entered !!!");
        //}
    }
}
