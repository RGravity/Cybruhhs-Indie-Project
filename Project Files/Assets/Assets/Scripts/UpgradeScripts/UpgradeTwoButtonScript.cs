using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeTwoButtonScript : MonoBehaviour
{

    private bool _spider = false;
    private bool _tree = false;
    private bool _troll = false;

    public bool Spider { get { return _spider; } set { _spider = value; } }
    public bool Tree { get { return _tree; } set { _tree = value; } }
    public bool Troll { get { return _troll; } set { _troll = value; } }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnClickSpider()
    {
        
       // if (gameObject.GetComponent <Image>().sprite == Resources.Load<Sprite>("SpiderUpgrade2"))_spider = true;
    }

    public void OnClickTree()
    {
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("SpiderUpgrade2"))_tree = true;
    }

    public void OnClickTroll()
    {
        if (gameObject.GetComponent<Image>().sprite == Resources.Load<Sprite>("TrollUpgrade2"))_troll = true;
    }
}