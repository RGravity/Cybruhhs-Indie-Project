using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextWaveTimerScript : MonoBehaviour {



	public void SetNextWaveTimer(int pSeconds)
    {
        GetComponent<Text>().text = pSeconds.ToString();
    }
}
