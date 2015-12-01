using UnityEngine;
using System.Collections;

public class BuildingSpawnScript : MonoBehaviour {

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

    //TOWERS
    [SerializeField]
    private Texture[] _turretTextures;


    private int _offsetForRadialWheel;



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
    void Start ()
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
                if (vHit.collider.gameObject.GetComponent<BuildPlacementTilesScript>())
                {
                    Debug.Log("THIS WORKS");
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
                    if (_tileMap.TileTypes[indexofSelectedTile].BuildingAllowed && !vHit.collider.gameObject.GetComponent<ArrowTowerScript>() && !vHit.collider.gameObject.GetComponent<CannonTowerScript>() && !vHit.collider.gameObject.GetComponent<SlowTowerScript>())
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
        Event currentEvent = Event.current;

        //if mousedown then show the menu
        if (currentEvent.type == EventType.MouseDown && _centerRect.Contains(currentEvent.mousePosition))
        {
            _showButtons = true;
            _index = -1;
        }

        //if mouseup then get the index and go to ResolveButtonPressed
        if (currentEvent.type == EventType.MouseUp)
        {
            if (_showButtons)
            {
                Debug.Log("User selected #" + _index);
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
            Debug.Log(_index);
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
        //build the right turret if money is available
        switch (_index)
        {
            //CannonTower
            case 0:
                if (_baseScript.Gold >= 250)
                {
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        _selectedTile[i].GetComponent<Renderer>().material.mainTexture = _turretTextures[i];
                        _selectedTile[3].AddComponent<CannonTowerScript>();
                        _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    //_baseScript.LowerGold(200);
                    if (_buy != null)
                    {
                        _buy.Play();
                    }
                }
                break;
            //SlowTower            
            case 1:
                break;
            //ArrowTower
            case 2:
                if (_baseScript.Gold >= 200)
                {
                    for (int i = 0; i < _selectedTile.Length; i++)
                    {
                        _selectedTile[i].GetComponent<Renderer>().material.mainTexture = _turretTextures[i];
                        _selectedTile[3].AddComponent<ArrowTowerScript>();
                        _selectedTile[i].AddComponent<UpgradeTowerScript>();
                        _selectedTile[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    }
                    //_baseScript.LowerGold(200);
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
