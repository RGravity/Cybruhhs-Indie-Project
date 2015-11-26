using UnityEngine;
using System.Collections;

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
    private int _path;
    [SerializeField]
    private float _timeBetweenEnemies;
    [SerializeField]
    private float _secToWaitForNextPart;


    public string WavePartNr { get { return _WavePartNr; } }
    public int AmountOfGrunt { get { return _amountOfGrunt; } }
    public int AmountOfHeavy { get { return _amountOfHeavy; } }
    public int AmountOfFlying { get { return _amountOfFlying; } }
    public int AmountofPaladin { get { return _amountOfPaladin; } }
}
