using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class MenuSelectionScript : MonoBehaviour {

    // Use this for initialization

   
    private string _strxml = "<SavedData></SavedData>";
    private GameObject _level1;
    private GameObject _level2;
    private GameObject _level3;
    private GameObject _level4;
    private GameObject _level5;
    private GameObject _level6;
    private GameObject _level7;
    private GameObject _level8;
    private GameObject _level9;
    private GameObject _level10;

    private bool _level2Unlocked;
    private bool _level3Unlocked;
    private bool _level4Unlocked;
    private bool _level5Unlocked;
    private bool _level6Unlocked;
    private bool _level7Unlocked;
    private bool _level8Unlocked;
    private bool _level9Unlocked;
    private bool _level10Unlocked;

    private DontDestroyOnLoadMusicScript _map;
    private AudioSource _click;

    private bool _resave;

    public bool Level2Unlocked {get { return _level2Unlocked; } set { _level2Unlocked = value; }}
    public bool Level3Unlocked { get { return _level3Unlocked; } set { _level3Unlocked = value; }}
    public bool Level4Unlocked { get { return _level4Unlocked; } set { _level4Unlocked = value; }}
    public bool Level5Unlocked { get { return _level5Unlocked; } set { _level5Unlocked = value; }}
    public bool Level6Unlocked { get { return _level6Unlocked; } set { _level6Unlocked = value; }}
    public bool Level7Unlocked { get { return _level7Unlocked; } set { _level7Unlocked = value; }}
    public bool Level8Unlocked { get { return _level8Unlocked; } set { _level8Unlocked = value; }}
    public bool Level9Unlocked { get { return _level9Unlocked; } set { _level9Unlocked = value; }}
    public bool Level10Unlocked { get { return _level10Unlocked; } set { _level10Unlocked = value; }}

  


    void Start()
    {
        _level1 = GameObject.Find("Level1");
        _level2 = GameObject.Find("Level2");
        _level3 = GameObject.Find("Level3");
        _level4 = GameObject.Find("Level4");
        _level5 = GameObject.Find("Level5");
        _level6 = GameObject.Find("Level6");
        _level7 = GameObject.Find("Level7");
        _level8 = GameObject.Find("Level8");
        _level9 = GameObject.Find("Level9");
        _level10 = GameObject.Find("Level10");

        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
        _click = GameObject.Find("Click").GetComponent<AudioSource>();

        saveAndLoad();
}

    // Update is called once per frame
    void Update ()
    {
        unlockLeveled();

        if (Input.GetKey (KeyCode.Space))
        {
            _level3Unlocked = true;
            saveAndLoad();
        }

        if (Input.GetKey(KeyCode.A))
        {
            _level4Unlocked = true;
            saveAndLoad();
        }

    }

    private void saveAndLoad()
    {
        XmlDocument _xml = new XmlDocument();
        _xml.LoadXml(_strxml);
        if (!File.Exists("SavedData.xml") || _resave == true )
        {

            XmlNode Elem = _xml.CreateElement("Levels");
            XmlNode ElemLvl2 = _xml.CreateElement("Level2");
            ElemLvl2.InnerText = XmlConvert.ToString (_level2Unlocked);
            XmlNode ElemLvl3 = _xml.CreateElement("Level3");
            ElemLvl3.InnerText = XmlConvert.ToString(_level3Unlocked);
            XmlNode ElemLvl4 = _xml.CreateElement("Level4");
            ElemLvl4.InnerText = XmlConvert.ToString(_level4Unlocked);
            XmlNode ElemLvl5 = _xml.CreateElement("Level5");
            ElemLvl5.InnerText = XmlConvert.ToString(_level5Unlocked);
            XmlNode ElemLvl6 = _xml.CreateElement("Level6");
            ElemLvl6.InnerText = XmlConvert.ToString(_level6Unlocked);
            XmlNode ElemLvl7 = _xml.CreateElement("Level7");
            ElemLvl7.InnerText = XmlConvert.ToString(_level7Unlocked);
            XmlNode ElemLvl8 = _xml.CreateElement("Level8");
            ElemLvl8.InnerText = XmlConvert.ToString(_level8Unlocked);
            XmlNode ElemLvl9 = _xml.CreateElement("Level9");
            ElemLvl9.InnerText = XmlConvert.ToString(_level9Unlocked);
            XmlNode ElemLvl10 = _xml.CreateElement("Level10");
            ElemLvl10.InnerText = XmlConvert.ToString(_level10Unlocked);

            
            Elem.AppendChild(ElemLvl2);
            Elem.AppendChild(ElemLvl3);
            Elem.AppendChild(ElemLvl4);
            Elem.AppendChild(ElemLvl5);
            Elem.AppendChild(ElemLvl6);
            Elem.AppendChild(ElemLvl7);
            Elem.AppendChild(ElemLvl8);
            Elem.AppendChild(ElemLvl9);
            Elem.AppendChild(ElemLvl10);
            _xml.DocumentElement.AppendChild(Elem);

            _resave = false;

            if (_resave == false)
            {
                XmlWriterSettings ssettings = new XmlWriterSettings();
                ssettings.Indent = true;

                XmlWriter sw = XmlWriter.Create("SavedData.xml", ssettings);
                _xml.Save(sw);
                sw.Close();
            }

            

        }
        else
        {
            _xml.Load("SavedData.xml");
        }

        XmlNode node = _xml.DocumentElement;

        string lvl2 = "";
        string lvl3 = "";
        string lvl4 = "";
        string lvl5 = "";
        string lvl6 = "";
        string lvl7 = "";
        string lvl8 = "";
        string lvl9 = "";
        string lvl10 = "";



        foreach (XmlNode node1 in node.ChildNodes)
        {
            foreach (XmlNode node2 in node1.ChildNodes)
            {
               
                if (node2.Name == "Level2") lvl2 = node2.InnerText;
                if (node2.Name == "Level3") lvl3 = node2.InnerText;
                if (node2.Name == "Level4") lvl4 = node2.InnerText;
                if (node2.Name == "Level5") lvl5 = node2.InnerText;
                if (node2.Name == "Level6") lvl6 = node2.InnerText;
                if (node2.Name == "Level7") lvl7 = node2.InnerText;
                if (node2.Name == "Level8") lvl8 = node2.InnerText;
                if (node2.Name == "Level9") lvl9 = node2.InnerText;
                if (node2.Name == "Level10") lvl10 = node2.InnerText;
               
                
            }
            _level2Unlocked = XmlConvert.ToBoolean(lvl2);
            _level3Unlocked = XmlConvert.ToBoolean(lvl3);
            _level4Unlocked = XmlConvert.ToBoolean(lvl4);
            _level5Unlocked = XmlConvert.ToBoolean(lvl5);
            _level6Unlocked = XmlConvert.ToBoolean(lvl6);
            _level7Unlocked = XmlConvert.ToBoolean(lvl7);
            _level8Unlocked = XmlConvert.ToBoolean(lvl8);
            _level9Unlocked = XmlConvert.ToBoolean(lvl9);
            _level10Unlocked = XmlConvert.ToBoolean(lvl10);

            lvl2 = XmlConvert.ToString(_level2Unlocked);
            lvl3 = XmlConvert.ToString(_level3Unlocked);
            lvl4 = XmlConvert.ToString(_level4Unlocked);
            lvl5 = XmlConvert.ToString(_level5Unlocked);
            lvl6 = XmlConvert.ToString(_level6Unlocked);
            lvl7 = XmlConvert.ToString(_level7Unlocked);
            lvl8 = XmlConvert.ToString(_level8Unlocked);
            lvl9 = XmlConvert.ToString(_level9Unlocked);
            lvl10 = XmlConvert.ToString(_level10Unlocked);
         
        }
        _resave = true;

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        
        XmlWriter w = XmlWriter.Create("SavedData.xml", settings);
        _xml.Save(w);
        w.Close();
       


    }



    private void unlockLeveled()
    {
        if (_level2Unlocked == true)
        {
            _level2.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level3Unlocked == true)
        {
            _level3.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level4Unlocked == true)
        {
            _level4.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level5Unlocked == true)
        {
            _level5.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level6Unlocked == true)
        {
            _level6.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level7Unlocked == true)
        {
            _level7.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level8Unlocked == true)
        {
            _level8.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level9Unlocked == true)
        {
            _level9.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }
        if (_level10Unlocked == true)
        {
            _level10.GetComponent<Image>().sprite = Resources.Load<Sprite>("level select unlocked");
            saveAndLoad();
        }

    }

    public void LevelOne()
    {
        _map.Level = 1;
        _click.Play();
    }
    public void LevelTwo()
    {
        if (_level2Unlocked == true)
        {
            _map.Level = 2;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelThree()
    {
        if (_level3Unlocked == true)
        {
            _map.Level = 3;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelFour()
    {
        if (_level4Unlocked == true)
        {
            _map.Level = 4;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelFive()
    {
        if (_level5Unlocked == true)
        {
            _map.Level = 5;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelSix()
    {
        if (_level6Unlocked == true)
        {
            _map.Level = 6;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelSeven()
    {
        if (_level7Unlocked == true)
        {
            _map.Level = 7;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelEight()
    {
        if (_level8Unlocked == true)
        {
            _map.Level = 8;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelNine()
    {
        if (_level9Unlocked == true)
        {
            _map.Level = 9;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }
    public void LevelTen()
    {
        if (_level10Unlocked == true)
        {
            _map.Level = 10;
            _click.Play();
        }
        else
        {
            _map.Level = 0;
        }
    }

    public void StartGame()
    {
        if (_map.Level > 0)
        {
            _click.Play();
            Application.LoadLevel(1);
        }
    }

    public void Enter()
    {
        _click.Play();
    }

    public void Exit()
    {
        _click.Play();
    }
}
