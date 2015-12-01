using UnityEngine;
using System.Collections;

public class BuildingWaveScript : MonoBehaviour {

    [SerializeField]
    private int _SecondsToBuild = 30;
    private float _milliSecondsRemaining;
    private bool _buildingWaveActive = false;
    private WaveMainScript _waveScript;
    private NextWaveTimerScript _waveTimer;
    private bool _startNextWave = false;

    public bool BuildingWaveActive { get { return _buildingWaveActive; } }
    public bool StartNextWave { set { _startNextWave = value; } }

	// Use this for initialization
	void Start () {
        _waveScript = FindObjectOfType<WaveMainScript>();
        _waveTimer = FindObjectOfType<NextWaveTimerScript>();
        _milliSecondsRemaining = _SecondsToBuild;
    }
	
	// Update is called once per frame
	void Update () {
        if (_buildingWaveActive)
        {
            if (_startNextWave)
            {
                _milliSecondsRemaining = 0;
            }

            _milliSecondsRemaining -= Time.deltaTime;
            int SecondsRemaining = (int)Mathf.Ceil(_milliSecondsRemaining);

            // ------ replace with code to update the HUD Timer ------
            _waveTimer.SetNextWaveTimer(SecondsRemaining);
            // -------------------------------------------------------

            if (_milliSecondsRemaining <= 0)
            {
                _buildingWaveActive = false;
                _waveScript.BuildWaveStarted = false;
                _waveScript.BuildWaveEnded = true;
            }
        }
	}

    /// <summary>
    /// <para>Method to start the timer for the Build time between waves</para>
    /// </summary>
    public void StartBuildingWave()
    {
        _buildingWaveActive = true;
        _milliSecondsRemaining = _SecondsToBuild;

        _waveScript.BuildWaveStarted = true;
        _waveScript.BuildWaveEnded = false;
    }
}
