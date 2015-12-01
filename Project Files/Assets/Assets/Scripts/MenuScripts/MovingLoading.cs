using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovingLoading : MonoBehaviour {

    public Sprite[] Sprites;
    int index = 0;
    float indexChanger = 0;

    void Start()
    {
    }

    void Update()
    {
        if (Sprites.Length == 0)
            return;

        indexChanger += 0.6f;
        index = (int)indexChanger;
        if (index >= Sprites.Length)
        {
            indexChanger = 0;
            index = 0;
        }
        GetComponent<Image>().sprite = Sprites[index];
    }
}
