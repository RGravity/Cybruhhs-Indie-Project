using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesTextScript : MonoBehaviour {

    public void UpdateLives(int pLives)
    {
        GetComponent<Text>().text = pLives.ToString();
    }
}
