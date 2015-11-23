using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelectionScript : MonoBehaviour {

    // Use this for initialization

    private Button _level1;
    private Button _level2;
    private Button _level3;
    private Button _level4;
    private Button _level5;
    private Button _level6;
    private Button _level7;
    private Button _level8;
    private Button _level9;
    private Button _level10;

    void Start ()
    {
        _level1 = GameObject.Find("Level1").GetComponent<Button>();
        _level2 = GameObject.Find("Level2").GetComponent<Button>();
        _level3 = GameObject.Find("Level3").GetComponent<Button>();
        _level4 = GameObject.Find("Level4").GetComponent<Button>();
        _level5 = GameObject.Find("Level5").GetComponent<Button>();
        _level6 = GameObject.Find("Level6").GetComponent<Button>();
        _level7 = GameObject.Find("Level7").GetComponent<Button>();
        _level8 = GameObject.Find("Level8").GetComponent<Button>();
        _level9 = GameObject.Find("Level9").GetComponent<Button>();
        _level10 = GameObject.Find("Level10").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void LevelOne()
    {
        Debug.Log("Load Level One");
    }

    public void LevelTwo()
    {
        Debug.Log("Load Level Two");
    }
    public void LevelThree()
    {
        Debug.Log("Load Level Three");
    }

    public void LevelFour()
    {
        Debug.Log("Load Level Four");
    }
    public void LevelFive()
    {
        Debug.Log("Load Level Five");
    }

    public void LevelSix()
    {
        Debug.Log("Load Level Six");
    }
    public void LevelSeven()
    {
        Debug.Log("Load Level Seven");
    }

    public void LevelEight()
    {
        Debug.Log("Load Level Eight");
    }
    public void LevelNine()
    {
        Debug.Log("Load Level Nine");
    }

    public void LevelTen()
    {
        Debug.Log("Load Level Ten");
    }
}
