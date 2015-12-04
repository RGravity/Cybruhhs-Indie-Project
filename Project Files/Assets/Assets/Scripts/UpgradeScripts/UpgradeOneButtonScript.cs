using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeOneButtonScript : MonoBehaviour {

    private int _intUpdate;
    public int IntUpdate { get { return _intUpdate; } set { _intUpdate = value; } }
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnClickSpider()
    {
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("SpiderUpgrade1"))
        {
          //  _upgrade.TempSlow.UpdateTowerSlow();
           // _upgrade.Disappear = true;
        }
    }

    public void OnClickTree()
    {
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("TreeUpgrade1"))
        {
          //  _upgrade.TempArrow.UpdateTowerArrow();
          //  _upgrade.Disappear = true;
        }
    }

    public void OnClickTroll()
    {
       
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("TrollUpgrade1"))
        {
            IntUpdate = 1;
            //_cannon = pTower;
            //_cannon.UpdateTowerCannon();
        }
    }

}
