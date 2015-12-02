using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveIndicatorScript : MonoBehaviour {
    
	public void SetWaveIndicator(int pCurrentWave, int pTotalWaves)
    {
        GetComponent<Text>().text = pCurrentWave + "/" + pTotalWaves;
    }
}
