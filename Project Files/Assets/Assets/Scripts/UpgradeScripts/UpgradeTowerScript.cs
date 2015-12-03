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
        //checking for all the tower places
       
        _check = FindObjectOfType<CheckForMusicScript>();
        
        _upgradeOne = FindObjectOfType<UpgradeOneButtonScript>();
        _upgradeTwo = FindObjectOfType<UpgradeTwoButtonScript>();
        
        _upgradePanel = GameObject.Find("UpgradePanel");
        

    }

    // Update is called once per frame
    void Update()
    {
        objectsAppear();
        objectsAppear2();
        objectsAppear3();
        objectsDissappear();
        upgradeTowers();
        upgradeTowers2();
       

    }

    private void check()
    {
        //ArrowTowerScript[] tempscripts = FindObjectsOfType<ArrowTowerScript>();
        //for (int i = 0; i < tempscripts.Length; i++)
        //{
        //    if (tempscripts[i].TileX <= gameObject.transform.position.x + 1 ||
        //        tempscripts[i].TileX >= gameObject.transform.position.x - 1 ||
        //        tempscripts[i].TileY <= gameObject.transform.position.y + 1 ||
        //        tempscripts[i].TileY >= gameObject.transform.position.y - 1 ||
        //        (tempscripts[i].TileX)
        //    {
                
        //    }
        //}
        _arrowTower = FindObjectOfType<ArrowTowerScript>();
        _cannonTower = FindObjectOfType<CannonTowerScript>();
        _slowTower = FindObjectOfType<SlowTowerScript>();
        //Collider[] tower = new Collider[0];
        //switch (gameObject.GetComponent<BuildPlacementTilesScript>().TowerPlaceNr)
        //{

        //    case TowerNoneNumbers.Tower1:
        //        tower = Physics.OverlapSphere(new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y - 0.5f), 0.5f);
        //        break;
        //    case TowerNoneNumbers.Tower2:
        //        tower = Physics.OverlapSphere(new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.5f), 0.5f);
        //        break;
        //    case TowerNoneNumbers.Tower3:

        //        tower = Physics.OverlapSphere(new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f), 0.5f);
        //        break;
        //    case TowerNoneNumbers.Tower4:

        //        tower = Physics.OverlapSphere(new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.5f), 0.5f);
        //        break;
        //    default:
        //        break;
        //}
        ////put the correct script in the correct variable
        //for (int i = 0; i < tower.Length; i++)
        //{
        //    if (tower[i].gameObject.GetComponent<ArrowTowerScript>())
        //    {
        //        _arrowTower = tower[i].gameObject.GetComponent<ArrowTowerScript>();
        //    }
        //    if (tower[i].gameObject.GetComponent<CannonTowerScript>())
        //    {
        //        _cannonTower = tower[i].gameObject.GetComponent<CannonTowerScript>();
        //    }
        //    if (tower[i].gameObject.GetComponent<SlowTowerScript>())
        //    {
        //        _slowTower = tower[i].GetComponent<SlowTowerScript>();
        //    }
        //}

        //Debug.Log(_arrowTower.gameObject.transform.localPosition.x);
        // Debug.Log(_arrowTower.gameObject.transform.localPosition.y);
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
            _upgradeTrollAppear = false;
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
        if (_upgradeSpiderAppear2 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeSpiderAppear2 = false;
           
        }

        else if (_upgradeTrollAppear2 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTrollAppear2 = false;
        }
        else if (_upgradeTreeAppear2 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade2");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear2 = false;
        }

    }
    private void objectsAppear3()
    {
        if (_upgradeSpiderAppear3 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpiderUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeSpiderAppear3 = false;

        }

        else if (_upgradeTrollAppear3 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TrollUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTrollAppear3 = false;
        }
        else if (_upgradeTreeAppear3 == true)
        {
            _upgradePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            _upgradeOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade1Locked");
            _upgradeTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("TreeUpgrade2Locked");
            _upgradePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _upgradeTreeAppear3 = false;
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
       
        if (gameObject.GetComponent<ArrowTowerScript>())
        {
            
            if (gameObject.GetComponent<ArrowTowerScript>().Tier == 1)
            {
              
                _upgradeTreeAppear = true;
            }
            if (gameObject.GetComponent<ArrowTowerScript>().Tier == 2)
            {
               
                _upgradeTreeAppear2 = true;
            }
            if (gameObject.GetComponent<ArrowTowerScript>().Tier >= 3)
            {
             
                _upgradeTreeAppear3 = true;
            }
        }
        if (gameObject.GetComponent<CannonTowerScript>())
        {
           
            if (gameObject.GetComponent<CannonTowerScript>().Tier == 1) _upgradeTrollAppear = true;

            if (gameObject.GetComponent<CannonTowerScript>().Tier == 2) _upgradeTrollAppear2 = true;

            if (gameObject.GetComponent<CannonTowerScript>().Tier >= 3) _upgradeTrollAppear3 = true;
        }
        if (gameObject.GetComponent<SlowTowerScript>())
        {
           
            if (gameObject.GetComponent<SlowTowerScript>().Tier == 1) _upgradeSpiderAppear = true;

            if (gameObject.GetComponent<SlowTowerScript>().Tier == 2) _upgradeSpiderAppear2 = true;

            if (gameObject.GetComponent<SlowTowerScript>().Tier >= 3) _upgradeSpiderAppear3 = true;
        }
    }
}
