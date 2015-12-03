using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeTowerScript : MonoBehaviour {

  
    private ArrowTowerScript _arrowTower;
    private ArrowTowerScript _tempTower;
    private CannonTowerScript _cannonTower;
    private CannonTowerScript _tempCannon;
    private SlowTowerScript _slowTower;
    private SlowTowerScript _tempSlow;
    private UpgradeOneButtonScript _upgradeOne;
    private UpgradeTwoButtonScript _upgradeTwo;
    private GameObject _towerObj;

    public ArrowTowerScript ArrowTower { set { _arrowTower = value; } }
    public CannonTowerScript CannonTower { set { _cannonTower = value; } }
    public SlowTowerScript SlowTower { set { _slowTower = value; } }

    public GameObject TowerObj { set { _towerObj = value; } }

    private bool _upgradeSpiderAppear = false;
    private bool _upgradeSpiderAppear2 = false;
    private bool _upgradeSpiderAppear3 = false;
    private bool _upgradeTreeAppear = false;
    private bool _upgradeTreeAppear2 = false;
    private bool _upgradeTreeAppear3 = false;
    private bool _upgradeTrollAppear = false;
    private bool _upgradeTrollAppear2 = false;
    private bool _upgradeTrollAppear3 = false;
    private bool _disappear = false;


    public bool Disappear { get { return _disappear; } set { _disappear = value; } }

    private GameObject _upgradePanel;

    void Start()
    {
        _upgradePanel = GameObject.Find("UpgradePanel"); //gets the panel in the game scene
        _upgradeOne = FindObjectOfType<UpgradeOneButtonScript>(); //the first upgrade button in the panel
        _upgradeTwo = FindObjectOfType<UpgradeTwoButtonScript>(); //the second upgrade button in the panel      
    }

    // Update is called once per frame
    void Update()
    {
        _objectsAppear();
        _objectsAppear2();
        _objectsAppear3();
        _objectsDissappear();
        _upgradeTowers();
        _upgradeTowers2();
    }
    /// <summary>
    /// <para>If one of the bools is true it will set the scale of the panel to the right size</para>
    /// <para>It will also load the right sprites for the button for that tower</para>
    /// </summary>
    private void _objectsAppear()
    {
        if (_upgradeTreeAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade1");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear = false;
        }
        else if (_upgradeTrollAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade1");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTrollAppear = false;
        }
        else if (_upgradeSpiderAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade1");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeSpiderAppear = false;
        }
    }
    /// <summary>
    /// <para>If one of the bools is true it will set the scale of the panel to the right size</para>
    /// <para>It will also load the right sprites for the button for that tower</para>
    /// </summary>

    private void _objectsAppear2()
    {
        if (_upgradeTreeAppear2 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear2 = false;
        }
        else if (_upgradeTrollAppear2 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTrollAppear2 = false;
        }
        else if (_upgradeSpiderAppear2 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeSpiderAppear2 = false;
        }
    }
    /// <summary>
    /// <para>If one of the bools is true it will set the scale of the panel to the right size</para>
    /// <para>It will also load the right sprites for the button for that tower</para>
    /// </summary>
    private void _objectsAppear3()
    {
         if (_upgradeTreeAppear3 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear3 = false;
        }

        else if (_upgradeTrollAppear3 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTrollAppear3 = false;
        }
        else if (_upgradeSpiderAppear3 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeSpiderAppear3 = false;
        }
    }
    /// <summary>
    /// <para>If the boolean is true it will set the upgradepanel size to 0, so it disappears!</para>
    /// </summary>

    private void _objectsDissappear()
    {
        if (_disappear == true)
        {
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            _disappear = false;
        }
    }

    /// <summary>
    /// <para>Gets the boolean from upgrade buttton one, if one of the bools is true then it will run the following:</para>
    /// <para>The UpgradeTower fuction in the right script, it will set the bool in the upgrade one button to false and disappear becomes true</para>
    /// </summary>

    private void _upgradeTowers()
    {
        if (_upgradeOne.Tree == true)
        {
            _tempTower.UpdateTowerArrow();
            _upgradeOne.Tree = false;
            _disappear = true;
        }
        else if (_upgradeOne.Troll == true)
        {
            _tempCannon.UpdateTowerCannon();
            _upgradeOne.Troll = false;
            _disappear = true;
        }
        else if (_upgradeOne.Spider == true)
        {
            _tempSlow.UpdateTowerSlow();
            _upgradeOne.Spider = false;
            _disappear = true;         
        }
    }

    /// <summary>
    /// <para>Gets the boolean from upgrade buttton two, if one of the bools is true then it will run the following:</para>
    /// <para>The UpgradeTower fuction in the right script, it will set the bool in the upgrade one button to false and disappear becomes true</para>
    /// </summary>

    private void _upgradeTowers2()
    {
        if (_upgradeTwo.Tree == true)
        {
            _tempTower.UpdateTowerArrow();
            _upgradeTwo.Tree = false;
            _disappear = true;
        }

        else if (_upgradeTwo.Troll == true)
        {
            _tempCannon.UpdateTowerCannon();
            _upgradeTwo.Troll = false;
            _disappear = true;
        }
        else if (_upgradeTwo.Spider == true)
        {
            _tempSlow.UpdateTowerSlow();
            _upgradeTwo.Spider = false;
            _disappear = true;
        }

    }
    /// <summary>
    /// <para>With On Mouse down it will check which of the following towers it is by,</para>
    /// <para>if the script is indeed there, check the tier of that script and set at the right tier the bool to true.</para>
    /// </summary>
    void OnMouseDown()
    {
        if (_arrowTower)
        {
            _tempTower = _arrowTower;
            if (_arrowTower.Tier == 1)
            {
                _upgradeTreeAppear = true;
            }
            else if (_arrowTower.Tier == 2)
            {
                _upgradeTreeAppear2 = true;
            }
            else if (_arrowTower.Tier >= 3)
            {
                _upgradeTreeAppear3 = true;
            }
        }
        else if (_cannonTower)
        {
            _tempCannon = _cannonTower;
            if (_cannonTower.Tier == 1 )
            {
                _upgradeTrollAppear = true;
            }
            else if (_cannonTower.Tier == 2 )
            {
                _upgradeTrollAppear2 = true;

            }
            else if (_cannonTower.Tier >= 3 )
            {
                _upgradeTrollAppear3 = true;
            }
        }
        else if (_slowTower)
        {
            _tempSlow = _slowTower;
            if (_slowTower.Tier == 1)
            {
                _upgradeSpiderAppear = true;
            }

            else if (_slowTower.Tier == 2)
            {
                _upgradeSpiderAppear2 = true;
            }

            else if (_slowTower.Tier >= 3)
            {
                _upgradeSpiderAppear3 = true;
            }
        }
    }
}
