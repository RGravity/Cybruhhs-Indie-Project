using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveTemplateScript : MonoBehaviour {
    
    [SerializeField]
    private int _amountOfGrunt;
    [SerializeField]
    private int _amountOfHeavy;
    [SerializeField]
    private int _amountOfFlying;
    [SerializeField]
    private int _amountofPaladin;

    public int AmountOfGrunt { get { return _amountOfGrunt; } }
    public int AmountOfHeavy { get { return _amountOfHeavy; } }
    public int AmountOfFlying { get { return _amountOfFlying; } }
    public int AmountofPaladin { get { return _amountofPaladin; } }
}
