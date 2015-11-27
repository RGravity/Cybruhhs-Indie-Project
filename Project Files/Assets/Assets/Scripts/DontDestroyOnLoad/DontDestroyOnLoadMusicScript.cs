using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadMusicScript : MonoBehaviour
{
    private static GameObject _instance;
    private GameObject _creditCanvas;
    private GameObject _menuCanvas;
    private GameObject _optionsCanvas;
    private GameObject _selectionCanvas;
    private int _level;
    public int Level { get { return _level; } set { _level = value; }}

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!_instance)
        {
            _instance = this.gameObject;

        }
        //otherwise, if we do, kill this thing
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        _menuCanvas = GameObject.Find("MenuCanvas");
        _optionsCanvas = GameObject.Find("OptionsCanvas");
        _creditCanvas = GameObject.Find("CreditsCanvas");
        _selectionCanvas = GameObject.Find("SelectionCanvas");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
            _level = 0;
        }
    }
}