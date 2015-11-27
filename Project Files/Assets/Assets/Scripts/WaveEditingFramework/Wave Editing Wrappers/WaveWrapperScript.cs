using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveWrapperScript{

    [SerializeField]
    private string _waveNr = "Wave ";
    [SerializeField]
    private List<WaveTemplateScript> _waveParts;

    public List<WaveTemplateScript> WaveParts { get { return _waveParts; } }
}
