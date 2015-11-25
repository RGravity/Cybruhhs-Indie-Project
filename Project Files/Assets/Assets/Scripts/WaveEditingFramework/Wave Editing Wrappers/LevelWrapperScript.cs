using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelWrapperScript{

    [SerializeField]
    private string Levelnr = "Level ";
    [SerializeField]
    private List<WaveWrapperScript> WaveList;
}
