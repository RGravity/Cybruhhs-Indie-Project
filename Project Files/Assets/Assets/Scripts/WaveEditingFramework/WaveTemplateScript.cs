using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WaveTemplateScript{

    [SerializeField]
    private string _WavePartNr;
    [SerializeField]
    private int _amountOfGrunt;
    [SerializeField]
    private int _amountOfHeavy;
    [SerializeField]
    private int _amountOfFlying;
    [SerializeField]
    private int _amountOfPaladin;
    [SerializeField]
    private float _timeBetweenEnemies;
    [SerializeField]
    private int _beginPositionNr;
    [SerializeField]
    private int _endPositionNr;
    [SerializeField]
    private int _path;
    [SerializeField]
    private float _secToWaitForNextPart;
    [SerializeField]
    private List<Sprite> _tutorialSprites;


    public string WavePartNr { get { return _WavePartNr; } }
    public int AmountOfGrunt { get { return _amountOfGrunt; } }
    public int AmountOfHeavy { get { return _amountOfHeavy; } }
    public int AmountOfFlying { get { return _amountOfFlying; } }
    public int AmountofPaladin { get { return _amountOfPaladin; } }
    public float TimeBetweenEnemies { get { return _timeBetweenEnemies; } }
    public int BeginPositionNr { get { return _beginPositionNr; } }
    public int EndPositionNr { get { return _endPositionNr; } }
    public int Path { get { return _path; } }
    public float SecToWaitForNextPart { get { return _secToWaitForNextPart; } }
    public List<Sprite> TutorialSprites { get { return _tutorialSprites; } }

}
