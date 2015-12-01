using UnityEngine;
using System.Collections;

public class NextWaveButtonScript : MonoBehaviour {

    private bool _startNextWave = false;

    public bool StartNextWave { get { return _startNextWave; } set { _startNextWave = value; } }

    void Update()
    {
        if (_startNextWave)
        {
            FindObjectOfType<BuildingWaveScript>().StartNextWave = true;
        }
    }
}
