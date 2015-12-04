using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeTwoButtonScript : MonoBehaviour
{

    private UpgradeTowerScript _upgrade;
    // Use this for initialization
    void Start()
    {
        _upgrade = FindObjectOfType<UpgradeTowerScript>();
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void OnClickSpider()
    {
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("SpiderUpgrade2"))
        {
            _upgrade.SlowTower.UpdateTowerSlow();
            _upgrade.Disappear = true;
        }
    }

    public void OnClickTree()
    {
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("TreeUpgrade2"))
        {

            _upgrade.ArrowTower.UpdateTowerArrow();
            _upgrade.Disappear = true;
        }
    }

    public void OnClickTroll()
    {
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("TrollUpgrade2"))
        {
            _upgrade.CannonTower.UpdateTowerCannon();
            _upgrade.Disappear = true;
        }
    }
}