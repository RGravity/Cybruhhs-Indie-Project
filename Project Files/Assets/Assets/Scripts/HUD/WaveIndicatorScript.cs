using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveIndicatorScript : MonoBehaviour {

    private string _waveText;

    void start()
    {
        _waveText = GetComponent<Text>().text;
    }

	public void SetWaveIndicator(int pCurrentWave, int pTotalWaves)
    {
        GetComponent<Text>().text = pCurrentWave + "/" + pTotalWaves;
    }
}
