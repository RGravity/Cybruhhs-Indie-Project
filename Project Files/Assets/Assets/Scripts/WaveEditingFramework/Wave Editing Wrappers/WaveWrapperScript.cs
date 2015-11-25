using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveWrapperScript{

    [SerializeField]
    private string WaveNr = "Wave ";
    [SerializeField]
    private List<WaveTemplateScript> WaveParts;
}
