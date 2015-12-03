using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovingLoading1 : MonoBehaviour {

    [SerializeField]
    private Sprite[] _sprites;
    private int _index = 0;
    private float _indexChanger = 0;

    void Start()
    {
    }

    void Update()
    {
        if (_sprites.Length == 0)
            return;

        _indexChanger += 0.15f;
        _index = (int)_indexChanger;
        if (_index >= _sprites.Length)
        {
            _indexChanger = 0;
            _index = 0;
        }
        GetComponent<Image>().sprite = _sprites[_index];
    }
}
