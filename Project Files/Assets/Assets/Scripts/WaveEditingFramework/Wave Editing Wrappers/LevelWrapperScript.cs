using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelWrapperScript{

    [SerializeField]
    private string _levelNr = "Level ";
    [SerializeField]
    private List<WaveWrapperScript> _waveList;

    public List<WaveWrapperScript> WaveList { get { return _waveList; } }
}
