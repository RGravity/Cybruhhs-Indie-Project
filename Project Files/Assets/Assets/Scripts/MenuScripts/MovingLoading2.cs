using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovingLoading2 : MonoBehaviour {

    [SerializeField]
    private Sprite[] _sprites;
    private int _index = 0;
    private float _indexChanger = 0;
    private float _countDown;
    private DontDestroyOnLoadMusicScript _map;
    private MenuSelectionScript _start;
    public float CountDown { get { return _countDown; } set { _countDown = value; } }

    void Start()
    {
        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
        _start = FindObjectOfType<MenuSelectionScript>();
        _countDown = Time.time;
    }

    void Update()
    {
        if (_start.PressedStart)
        {
            if (_sprites.Length == 0)
                return;

            _indexChanger += 0.05f;
            _index = (int)_indexChanger;
            if (_index >= _sprites.Length)
            {
                _indexChanger = 0;
                _index = 0;
                _showLoading();
            }
            GetComponent<Image>().sprite = _sprites[_index];
        }
    }

    private void _showLoading()
    {
        if (_countDown > Time.time)
        {
            _map.Play = true;
            _start.PressedStart = false;
            Application.LoadLevel(1);
        }
    }
}
