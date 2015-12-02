using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeTowerScript : MonoBehaviour {

    private CheckForMusicScript _check;
    private ArrowTowerScript _arrowTower;
    private CannonTowerScript _cannonTower;
    private SlowTowerScript _slowTower;
    private UpgradeOneButtonScript _upgradeOne;
    private UpgradeTwoButtonScript _upgradeTwo;

    private bool _upgradeSpiderAppear = false;
    private bool _upgradeSpiderAppear2 = false;
    private bool _upgradeTreeAppear = false;
    private bool _upgradeTreeAppear2 = false;
    private bool _upgradeTrollAppear = false;
    private bool _upgradeTrollAppear2 = false;
    private bool _disappear = false;

    public bool Disappear { get { return _disappear; } set { _disappear = value; } }

    private GameObject _upgradePanel;

    void Start()
    {
        _check = FindObjectOfType<CheckForMusicScript>();
        _arrowTower = FindObjectOfType<ArrowTowerScript>();
        _cannonTower = FindObjectOfType<CannonTowerScript>();
        _slowTower = FindObjectOfType<SlowTowerScript>();
        _upgradeOne = FindObjectOfType<UpgradeOneButtonScript>();
        _upgradeTwo = FindObjectOfType<UpgradeTwoButtonScript>();

        _upgradePanel = GameObject.Find("UpgradePanel");
        
    }

    // Update is called once per frame
    void Update()
    {
        objectsAppear();
        objectsAppear2();
        objectsDissappear();
        upgradeTowers();
        upgradeTowers2();
    }
    private void objectsAppear()
    {
        if (_upgradeSpiderAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade1");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeSpiderAppear = false;
        }

        else if (_upgradeTrollAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade1");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear = false;
        }
        else if (_upgradeTreeAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade1");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear = false;
        }

    }

    private void objectsAppear2()
    {
        if (_upgradeSpiderAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeSpiderAppear = false;
        }

        else if (_upgradeTrollAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear = false;
        }
        else if (_upgradeTreeAppear == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear = false;
        }

    }


    private void objectsDissappear()
    {
        if (_disappear == true)
        {
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            _disappear = false;
        }
    }

    private void upgradeTowers()
    {
        if (_upgradeOne.Spider == true)
        {
            _slowTower.UpdateTowerSlow();
            _upgradeOne.Spider = false;
            _disappear = true;
        }
        else if (_upgradeOne.Tree == true)
        {
            _arrowTower.UpdateTowerArrow();
            _upgradeOne.Tree = false;
           
            _disappear = true;
        }
        else if (_upgradeOne.Troll == true)
        {
            _cannonTower.UpdateTowerCannon();
            _upgradeOne.Troll = false;
            
            _disappear = true;
        }

    }

    private void upgradeTowers2()
    {
        if (_upgradeTwo.Spider == true)
        {
            _slowTower.UpdateTowerSlow();
            _upgradeTwo.Spider = false;
            _disappear = true;
        }
        else if (_upgradeTwo.Tree == true)
        {
            _arrowTower.UpdateTowerArrow();
            _upgradeTwo.Tree = false;
            _disappear = true;
        }
        else if (_upgradeTwo.Troll == true)
        {
            _cannonTower.UpdateTowerCannon();
            _upgradeTwo.Troll = false;
            _disappear = true;
        }

    }

    void OnMouseDown()
    {
        if (gameObject.GetComponent<ArrowTowerScript>()) _upgradeTreeAppear = true;
        //else if (gameObject.GetComponent<ArrowBulletScript>()) _upgradeTreeAppear2 = true;
        else if (gameObject.GetComponent<CannonTowerScript>()) _upgradeTrollAppear = true;
       // else if (gameObject.GetComponent<CannonTowerScript>()) _upgradeTrollAppear2 = true;
        else if (gameObject.GetComponent<CannonTowerScript>()) _upgradeSpiderAppear = true;
       // else if (gameObject.GetComponent<SlowTowerScript>()) _upgradeSpiderAppear2 = true;
    }
}
