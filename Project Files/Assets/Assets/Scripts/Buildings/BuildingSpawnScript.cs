using UnityEngine;
using System.Collections;

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
    private AudioSource _buy; //Buy Sound

    // Use this for initialization
    //Load all the Variables and Prepare the Radial Menu
    void Start()
    {
        _check = GameObject.FindObjectOfType<CheckForMusicScript>();
        _tileMap = FindObjectOfType<TileMapScript>();
        _baseScript = GameObject.FindObjectOfType<BaseScript>();
        _calculateEverything();
        if (_check.Check == true)
        {
            //_buy = GameObject.Find("SellSound").GetComponent<AudioSource>();
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
                    _selectedTile[1].AddComponent<CannonTowerScript>();
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        _selectedTile[i].GetComponent<Renderer>().material.mainTexture = _cannonTurretTextures[i];
                        _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    _baseScript.LowerGold(250);
                    if (_buy != null)
                    {
                        _buy.Play();
                    }
                }
                break;
            //SlowTower            
            case 1:
                if (_baseScript.Gold >= 150)
                {
                    _selectedTile[1].AddComponent<SlowTowerScript>();
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        _selectedTile[i].GetComponent<Renderer>().material.mainTexture = _cannonTurretTextures[i];
                        _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    _baseScript.LowerGold(150);
                    if (_buy != null)
                    {
                        _buy.Play();
                    }
                }
                break;
            //ArrowTower
            case 2:
                if (_baseScript.Gold >= 200)
                {
                    _selectedTile[1].AddComponent<ArrowTowerScript>();
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        _selectedTile[i].GetComponent<Renderer>().material.mainTexture = _arrowTurretTextures[i];
                        _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    _baseScript.LowerGold(200);
                    if (_buy != null)
                    {
                        _buy.Play();
                    }
                }
                break;
            default:
                break;
        }
    }
}
