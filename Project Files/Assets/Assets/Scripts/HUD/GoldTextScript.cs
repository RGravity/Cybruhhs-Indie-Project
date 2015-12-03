using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldTextScript : MonoBehaviour {
    
    public void UpdateGold(int pGold)
    {
        GetComponent<Text>().text = pGold.ToString();
    }
}
