using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadMusicScript : MonoBehaviour
{
    private static GameObject _instance;
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
    { }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel(0);
    }
}