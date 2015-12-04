using UnityEngine;
using System.Collections;
using System.Linq;

public class BuildingSpawnScript : MonoBehaviour
{

    [SerializeField]
    private int _radius = 125; // pixels radius to centor of all the radial buttons
    [SerializeField]
    private Texture _centerButton; //The CenterButtton
    [SerializeField]
    private Texture[] _normalButtons; //The unselected state of the Buttons
    [SerializeField]
    private Texture[] _selectedButtons; //The selected state of the Buttons
    [SerializeField]
    private Camera _myCam;
    [SerializeField]
    private Texture[] _arrowTurretTextures; //TOWERS (Arrow)
    [SerializeField]
    private Texture[] _cannonTurretTextures; //Cannon
    [SerializeField]
    private Texture[] _slowTurretTextures; //Slow Turret


    private int _offsetForRadialWheel; //The Vertical Offset of the Wheel
    private Vector2 _center; //Position of the center Button
    private CheckForMusicScript _check; //Check for Music
    private int _ringCount; //Amount of Objects in Ring
    private Rect _centerRect; //Rectangle of the CenterButton
    private Rect[] _ringRects; //Recranles of the RingButtons
    private float _angle; //Angle of the mouse
    private bool _showButtons; //Bool to show the buttons
    private int _index; //index of which button Selected using angle
    private GameObject[] _selectedTile; //The Current Tower Tile selected for the menu
    private BaseScript _baseScript; //Base Script to lower gold and such.
    private TileMapScript _tileMap; //Tilemap to see if buildable
  
    private AudioSource _troll1; //Troll voice one
    private AudioSource _troll2; //Troll voice two
    private AudioSource _spider1; //Spider voice one
    private AudioSource _spider2; //Spider voice two
    private AudioSource _tree1; //Tree voice one
    private AudioSource _tree2; //Tree voice one

    //Towers Level 1
    private GameObject _towerArrowIdle;
    private GameObject _towerCannonIdle;
    private GameObject _towerSpiderIdle;

    // Use this for initialization
    //Load all the Variables and Prepare the Radial Menu
    void Start()
    {
        //Get all the prefabs of the Idle towers level 1
        _towerArrowIdle = (GameObject)Resources.Load("Towers/TreeIdleLevel1");
        _towerCannonIdle = (GameObject)Resources.Load("Towers/TrollIdleLevel1");
        _towerSpiderIdle = (GameObject)Resources.Load("Towers/SpiderIdleLevel1");

        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        _tileMap = FindObjectOfType<TileMapScript>();
        _baseScript = GameObject.FindObjectOfType<BaseScript>();
        _calculateEverything();
        //Check if there is audio source and get all the sources
        if (_check.Check == true)
        {
           
            _troll1 = GameObject.Find("Troll1").GetComponent<AudioSource>();
            _troll2 = GameObject.Find("Troll2").GetComponent<AudioSource>();
            _spider1 = GameObject.Find("Spider1").GetComponent<AudioSource>();
            _spider2 = GameObject.Find("Spider2").GetComponent<AudioSource>();
            _tree1 = GameObject.Find("Tree1").GetComponent<AudioSource>();
            _tree2 = GameObject.Find("Tree2").GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Calculating everything for the Radial Wheel to put everything in the right place.
    /// </summary>
    private void _calculateEverything()
    {
        //First we get the The count of the amount of Buttons that are gonna be in the Ring
        _ringCount = _normalButtons.Length;
        //Then we place the buttons equally around the middle button
        _angle = 360.0f / _ringCount;

        //offset button, making sure center of button is at the mouse cusor
        _centerRect.x = _center.x - _centerButton.width * 0.5f;
        _centerRect.y = _offsetForRadialWheel - _center.y - _centerButton.height * 0.5f;

        //For Creating the Texture;
        _centerRect.width = _centerButton.width;
        _centerRect.height = _centerButton.height;

        //Initializing the Ring Rectangles with the RingCount
        _ringRects = new Rect[_ringCount];

        //Setting the Width/Height of the buttons
        float width = _normalButtons[0].width;
        float height = _normalButtons[0].height;
        //Making a rectangle with the width and height
        Rect rect = new Rect(0, 0, width, height);

        Vector2 vector = new Vector2(_radius, 0);

        //Put the rectangles on the right positions
        for (int i = 0; i < _ringCount; i++)
        {
            rect.x = _center.x + vector.x - width * 0.5f;
            rect.y = _offsetForRadialWheel - _center.y + vector.y - height * 0.5f;
            _ringRects[i] = rect;
            vector = Quaternion.AngleAxis(_angle, Vector3.forward) * vector;
        }
    }

    void Update()
    {
        //Get the Y offset for the Menu
        _offsetForRadialWheel = Screen.height;

        _onMouseClickMoveRadialMenu();
    }

    /// <summary>
    /// Move Radial Menu to right spot when clicking on a Turret
    /// </summary>
    private void _onMouseClickMoveRadialMenu()
    {
        //IF player clicks on a Tower Tile move the Menu to the right position
        if (Input.GetMouseButtonDown(0))
        {
            //Raycasting the position clicked
            RaycastHit vHit = new RaycastHit();
            Ray vRay = _myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                //Finding the right tile in the TileMap using the position of the object that was raycasted
                int[,] tiles = _tileMap.Tiles;
                int indexofSelectedTile = tiles[(int)vHit.collider.transform.position.x, (int)vHit.collider.transform.position.y];

                Collider[] tower = new Collider[0];
                //If Gameobject has BuildPlacementTilesScript make sure to get all the Cubes for the turret, which is 4 tiles
                if (vHit.collider.gameObject.GetComponent<BuildPlacementTilesScript>() && !vHit.collider.gameObject.GetComponent<UpgradeTowerScript>())
                {
                    switch (vHit.collider.gameObject.GetComponent<BuildPlacementTilesScript>().TowerPlaceNr)
                    {
                        case TowerNoneNumbers.Tower1:
                            tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x + 0.5f, vHit.collider.transform.position.y - 0.5f), 0.5f);
                            break;
                        case TowerNoneNumbers.Tower2:
                            tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x - 0.5f, vHit.collider.transform.position.y - 0.5f), 0.5f);
                            break;
                        case TowerNoneNumbers.Tower3:
                            tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x + 0.5f, vHit.collider.transform.position.y + 0.5f), 0.5f);
                            break;
                        case TowerNoneNumbers.Tower4:
                            tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x - 0.5f, vHit.collider.transform.position.y + 0.5f), 0.5f);
                            break;
                        default:
                            break;
                    }

                    //If im allowed to build a tower there and theres not another tower in place there, then start building
                    if (_tileMap.TileTypes[indexofSelectedTile].BuildingAllowed)
                    {
                        //Get the Screenpoint of the object
                        Vector2 tempPos = _myCam.WorldToScreenPoint(vHit.transform.position);
                        //Save the screenpoint to know where to place the centerbutton of the menu
                        _center = new Vector2(tempPos.x, tempPos.y);
                        //Setting the tile where you will place the tower
                        _selectedTile = new GameObject[tower.Length];
                        for (int i = 0; i < tower.Length; i++)
                        {
                            _selectedTile[i] = tower[i].gameObject;
                        }
                        _calculateEverything();
                    }
                }
                else
                {
                    //Moving the Menu away
                    _center = new Vector2(-100, -100);
                    _selectedTile = null;
                    _calculateEverything();
                }
            }
        }
    }

    void OnGUI()
    {
        _radialFunctionality();
    }

    /// <summary>
    /// The functionality of the Radial Menu
    /// </summary>
    private void _radialFunctionality()
    {
        Event currentEvent = Event.current;

        //if mousedown then show the menu
        if (currentEvent.type == EventType.MouseDown && _centerRect.Contains(currentEvent.mousePosition))
        {
            _showButtons = true;
            _index = -1;
        }

        //if mouseup then go to ResolveButtonPressed
        if (currentEvent.type == EventType.MouseUp)
        {
            if (_showButtons)
            {
                _resolveButtonPressed();
            }
            _showButtons = false;
        }

        //Here i calculate which button he currently is by using the angle from the center to find out which button he currently is hovering on.
        if (currentEvent.type == EventType.MouseDrag)
        {
            Vector2 mouseOffset = currentEvent.mousePosition - new Vector2(_center.x, _offsetForRadialWheel - _center.y);
            float angle = Mathf.Atan2(mouseOffset.y, mouseOffset.x) * Mathf.Rad2Deg;
            angle += _angle / 2.0f;
            if (angle < 0)
            {
                angle = angle + 360.0f;
            }

            _index = (int)(angle / _angle);
        }

        //Draw the Textures if you need to show the button
        if (_showButtons)
        {
            GUI.DrawTexture(_centerRect, _centerButton);
            for (int i = 0; i < _normalButtons.Length; i++)
            {
                if (i != _index)
                {
                    GUI.DrawTexture(_ringRects[i], _normalButtons[i]);
                }
                else
                {
                    GUI.DrawTexture(_ringRects[i], _selectedButtons[i]);
                }
            }
        }
    }

    /// <summary>
    /// Build the right turret if money is available
    /// Creates the prefabs and set the positions of the towers
    /// </summary>
    private void _resolveButtonPressed()
    {
        //build the right turret if money is available with the Index
        switch (_index)
        {
            //CannonTower
            case 0:
                if (_baseScript.Gold >= 250)
                {
                    //create on 1 tile the script that is needed.
                    CannonTowerScript tempTower = _selectedTile[1].AddComponent<CannonTowerScript>();
                    //Get the prefab of the Cannon and create the prefab.
                    //Get all the children but remove the parent so only the 4 tiles are in the parts and can be adjusted to the world coordinates.
                    _towerCannonIdle = Instantiate(_towerCannonIdle);
                    _towerCannonIdle.transform.position = new Vector3(_selectedTile[1].gameObject.transform.position.x, _selectedTile[1].gameObject.transform.position.y, -0.5f);
                    _selectedTile[1].GetComponent<CannonTowerScript>().TowerCannonIdleLevel1 = _towerCannonIdle;
                    Transform[] towerParts =_towerCannonIdle.GetComponentsInChildren<Transform>();
                    towerParts = towerParts.Except(new Transform[] { towerParts[0].transform }).ToArray();
                    //Save the 4 positions
                    GameObject leftTop = null;
                    GameObject rightTop = null;
                    GameObject leftBottom = null;
                    GameObject rightBottom = null;
                    foreach (Transform part in towerParts)
                    {
                        if (part.name.Contains("LeftTop"))
                        {
                            leftTop = part.gameObject;
                        }
                        if (part.name.Contains("RightTop"))
                        {
                            rightTop = part.gameObject;
                        }
                        if (part.name.Contains("LeftBottom"))
                        {
                            leftBottom = part.gameObject;
                        }
                        if (part.name.Contains("RightBottom"))
                        {
                            rightBottom = part.gameObject;
                        }
                    }
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower1)
                        {
                            //set on ID 1 LeftTop tile of the prefab
                            leftTop.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            leftTop.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<CannonTowerScript>().Tile1 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower2)
                        {
                            //set on ID 2 RightTop tile of the prefab
                            rightTop.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            rightTop.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<CannonTowerScript>().Tile2 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower3)
                        {
                            //set on ID 3 LeftBottom tile of the prefab
                            leftBottom.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            leftBottom.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<CannonTowerScript>().Tile3 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower4)
                        {
                            //set on ID 4 RightBottom tile of the prefab
                            rightBottom.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            rightBottom.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<CannonTowerScript>().Tile4 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f); // Set position in the script for next upgrades and changes
                        }
                        UpgradeTowerScript upgradeTower = _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        upgradeTower.CannonTower = tempTower;
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    _baseScript.LowerGold(250);
                    int random = Random.Range(0, 2);
                    switch (random)
                    {
                        case 0:
                            if (_troll1 != null)
                            {
                                _troll1.Play();
                            }
                            break;
                        case 1:
                            if (_troll2 != null)
                            {
                                _troll2.Play();
                            }
                            break;
                    }
                }
                break;
            //quitTower          
            case 1:
                break;
            //Slow
            case 2:
                if (_baseScript.Gold >= 150)
                {
                    //create on 1 tile the script that is needed.
                    SlowTowerScript tempTower = _selectedTile[1].AddComponent<SlowTowerScript>();
                    //Get the prefab of the Slow Tower and create the prefab.
                    //Get all the children but remove the parent so only the 4 tiles are in the parts and can be adjusted to the world coordinates.
                    _towerSpiderIdle = Instantiate(_towerSpiderIdle);
                    _towerSpiderIdle.transform.position = new Vector3(_selectedTile[1].gameObject.transform.position.x, _selectedTile[1].gameObject.transform.position.y, -0.5f);
                    _selectedTile[1].GetComponent<SlowTowerScript>().TowerSlowIdleLevel1 = _towerSpiderIdle;
                    Transform[] towerParts = _towerSpiderIdle.GetComponentsInChildren<Transform>();
                    towerParts = towerParts.Except(new Transform[] { towerParts[0].transform }).ToArray();
                    //Save the 4 positions
                    GameObject leftTop = null;
                    GameObject rightTop = null;
                    GameObject leftBottom = null;
                    GameObject rightBottom = null;
                    foreach (Transform part in towerParts)
                    {
                        if (part.name.Contains("LeftTop"))
                        {
                            leftTop = part.gameObject;
                        }
                        if (part.name.Contains("RightTop"))
                        {
                            rightTop = part.gameObject;
                        }
                        if (part.name.Contains("LeftBottom"))
                        {
                            leftBottom = part.gameObject;
                        }
                        if (part.name.Contains("RightBottom"))
                        {
                            rightBottom = part.gameObject;
                        }
                    }
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower1)
                        {
                            //set on ID 1 LeftTop tile of the prefab
                            leftTop.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            leftTop.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<SlowTowerScript>().Tile1 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower2)
                        {
                            //set on ID 2 RightTop tile of the prefab
                            rightTop.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            rightTop.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<SlowTowerScript>().Tile2 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower3)
                        {
                            //set on ID 3 LeftBottom tile of the prefab
                            leftBottom.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            leftBottom.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<SlowTowerScript>().Tile3 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower4)
                        {
                            //set on ID 4 RightBottom tile of the prefab
                            rightBottom.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            rightBottom.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                            _selectedTile[1].GetComponent<SlowTowerScript>().Tile4 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        UpgradeTowerScript upgradeTower = _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        upgradeTower.SlowTower = tempTower;
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    _baseScript.LowerGold(150);
                    int random = Random.Range(0, 2);
                    switch (random)
                    {
                        case 0:
                            if (_spider1 != null)
                            {
                                _spider1.Play();
                            }
                            break;
                        case 1:
                            if (_spider2 != null)
                            {
                                _spider2.Play();
                            }
                            break;
                    }


                }
                break;
            case 3:
                if (_baseScript.Gold >= 200)
                {
                    //create on 1 tile the script that is needed.
                    ArrowTowerScript tempTower =_selectedTile[1].AddComponent<ArrowTowerScript>();
                    //Get the prefab of the Slow Tower and create the prefab.
                    //Get all the children but remove the parent so only the 4 tiles are in the parts and can be adjusted to the world coordinates.
                    _towerArrowIdle = Instantiate(_towerArrowIdle);
                    _towerArrowIdle.transform.position = new Vector3(_selectedTile[1].gameObject.transform.position.x, _selectedTile[1].gameObject.transform.position.y, -0.5f);
                    _selectedTile[1].GetComponent<ArrowTowerScript>().TowerArrowIdleLevel1 = _towerArrowIdle;
                    Transform[] towerParts = _towerArrowIdle.GetComponentsInChildren<Transform>();
                    towerParts = towerParts.Except(new Transform[] { towerParts[0].transform }).ToArray();
                    //Save the 4 positions
                    GameObject leftTop = null;
                    GameObject rightTop = null;
                    GameObject leftBottom = null;
                    GameObject rightBottom = null;
                    foreach (Transform part in towerParts)
                    {
                        if (part.name.Contains("LeftTop"))
                        {
                            leftTop = part.gameObject;
                        }
                        if (part.name.Contains("RightTop"))
                        {
                            rightTop = part.gameObject;
                        }
                        if (part.name.Contains("LeftBottom"))
                        {
                            leftBottom = part.gameObject;
                        }
                        if (part.name.Contains("RightBottom"))
                        {
                            rightBottom = part.gameObject;
                        }
                    }
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower1)
                        {
                            //set on ID 1 LeftTop tile of the prefab
                            leftTop.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            leftTop.transform.localScale = new Vector3(1, 1, 1);
                            _selectedTile[1].GetComponent<ArrowTowerScript>().Tile1 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower2)
                        {
                            //set on ID 2 RightTop tile of the prefab
                            rightTop.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            rightTop.transform.localScale = new Vector3(1, 1, 1);
                            _selectedTile[1].GetComponent<ArrowTowerScript>().Tile2 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower3)
                        {
                            //set on ID 3 LeftBottom tile of the prefab
                            leftBottom.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            leftBottom.transform.localScale = new Vector3(1, 1, 1);
                            _selectedTile[1].GetComponent<ArrowTowerScript>().Tile3 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        if (_selectedTile[i].GetComponent<BuildPlacementTilesScript>().TowerPlaceNr == TowerNoneNumbers.Tower4)
                        {
                            //set on ID 4 RightBottom tile of the prefab
                            rightBottom.transform.position = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);
                            rightBottom.transform.localScale = new Vector3(1, 1, 1);
                            _selectedTile[1].GetComponent<ArrowTowerScript>().Tile4 = new Vector3(_selectedTile[i].transform.position.x, _selectedTile[i].transform.position.y, -0.5f);// Set position in the script for next upgrades and changes
                        }
                        UpgradeTowerScript upgradeTower = _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        upgradeTower.ArrowTower = tempTower;
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    _baseScript.LowerGold(200);
                    int random = Random.Range(0, 2);
                    switch (random)
                    {
                        case 0:
                            if (_tree1 != null)
                            {
                                _tree1.Play();
                            }
                            break;
                        case 1:
                            if (_tree2 != null)
                            {
                                _tree2.Play();
                            }
                            break;
                    }
                }
                break;
            default:
                break;
        }
    }
}
