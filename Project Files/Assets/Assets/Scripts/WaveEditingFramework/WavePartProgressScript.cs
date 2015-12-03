using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavePartProgressScript {

    private float _timeStarted;

    private int _gruntAmountSpawned;
    private int _gruntAmountRemaining;
    private int _heavyAmountSpawned;
    private int _heavyAmountRemaining;
    private int _flyingAmountSpawned;
    private int _flyingAmountRemaining;
    private int _paladinAmountSpawned;
    private int _paladinAmountRemaining;

    private int _path;

    private float _timeBetweenEnemies;
    private float _secToWaitForNextPart;
    
    private int _beginPositionNr;
    private int _endPositionNr;

    private List<Sprite> _tutorialSprites;

    public float TimeStarted { get { return _timeStarted; } set { _timeStarted = value; } }

    public int GruntAmountSpawned { get { return _gruntAmountSpawned; } set { _gruntAmountSpawned = value; } }
    public int GruntAmountRemaining { get { return _gruntAmountRemaining; } set { _gruntAmountRemaining = value; } }
    public int HeavyAmountSpawned { get { return _heavyAmountSpawned; } set { _heavyAmountSpawned = value; } }
    public int HeavyAmountRemaining { get { return _heavyAmountRemaining; } set { _heavyAmountRemaining = value; } }
    public int FlyingAmountSpawned { get { return _flyingAmountSpawned; } set { _flyingAmountSpawned = value; } }
    public int FlyingAmountRemaining { get { return _flyingAmountRemaining; } set { _flyingAmountRemaining = value; } }
    public int PaladinAmountSpawned { get { return _paladinAmountSpawned; } set { _paladinAmountSpawned = value; } }
    public int PaladinAmountRemaining { get { return _paladinAmountRemaining; } set { _paladinAmountRemaining = value; } }

    public int Path { get { return _path; } set { _path = value; } }

    public float TimeBetweenEnemies { get { return _timeBetweenEnemies; } set { _timeBetweenEnemies = value; } }
    public float SecToWaitForNextPart { get { return _secToWaitForNextPart; } set { _secToWaitForNextPart = value; } }

    public int BeginPositionNr { get { return _beginPositionNr; } set { _beginPositionNr = value; } }
    public int EndPositionNr { get { return _endPositionNr; } set { _endPositionNr = value; } }

    public List<Sprite> TutorialSprites { get { return _tutorialSprites; } set { _tutorialSprites = value; } }

}
