using UnityEngine;
using System.Collections;

public class BuildingSpawnScript : MonoBehaviour {

    [SerializeField]
    private Vector2 _center; //Position of the center Button
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


    private int _offsetForRadialWheel;
    

    //TOWERS
    [SerializeField]
    private Texture[] _turretTextures;

    private CheckForMusicScript _check;
    private int _ringCount;
    private Rect _centerRect;
    private Rect[] _ringRects;
    private float _angle;
    private bool _showButtons;
    private int _index;
    private GameObject[] _selectedTile;
    private BaseScript _baseScript;
    private TileMapScript _tileMap;
    private int[] _savedTileIndexes;
    private AudioSource _buy;
    private int[] _indexofSelectedTile;

    // Use this for initialization

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
        _ringCount = _normalButtons.Length;
        _angle = 360.0f / _ringCount;

        //offset button, making sure center of button is at the mouse cusor
        _centerRect.x = _center.x - _centerButton.width * 0.5f;
        _centerRect.y = _offsetForRadialWheel - _center.y - _centerButton.height * 0.5f;

        //For Creating the Texture;
        _centerRect.width = _centerButton.width;
        _centerRect.height = _centerButton.height;


        _ringRects = new Rect[_ringCount];

        float width = _normalButtons[0].width;
        float height = _normalButtons[0].height;
        Rect rect = new Rect(0, 0, width, height);

        Vector2 vector = new Vector2(_radius, 0);

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
        _offsetForRadialWheel = Screen.height;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit vHit = new RaycastHit();
            Ray vRay = _myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                int[,] tiles = _tileMap.Tiles;
                _indexofSelectedTile = new int[4];
                _indexofSelectedTile[0] = tiles[(int)vHit.collider.transform.position.x, (int)vHit.collider.transform.position.y];

                Collider[] tower = new Collider[0];
                switch (vHit.collider.gameObject.GetComponent<BuildPlacementTilesScript>().TowerPlaceNr)
                {
                    case TowerNoneNumbers.Tower1:
                        tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x - 0.5f, vHit.collider.transform.position.y - 0.5f), 0.5f);
                        break;
                    case TowerNoneNumbers.Tower2:
                        tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x + 0.5f, vHit.collider.transform.position.y - 0.5f), 0.5f);
                        break;
                    case TowerNoneNumbers.Tower3:
                        tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x - 0.5f, vHit.collider.transform.position.y + 0.5f), 0.5f);
                        break;
                    case TowerNoneNumbers.Tower4:
                        tower = Physics.OverlapSphere(new Vector3(vHit.collider.transform.position.x + 0.5f, vHit.collider.transform.position.y + 0.5f), 0.5f);
                        break;
                    default:
                        break;
                }

                if (_tileMap.TileTypes[_indexofSelectedTile[0]].BuildingAllowed && !vHit.collider.gameObject.GetComponent<ArrowTowerScript>() && !vHit.collider.gameObject.GetComponent<CannonTowerScript>() && !vHit.collider.gameObject.GetComponent<SlowTowerScript>())
                {
                    Vector2 tempPos = _myCam.WorldToScreenPoint(vHit.transform.position);
                    _center = new Vector2(tempPos.x, tempPos.y);
                    _selectedTile = new GameObject[tower.Length];
                    for (int i = 0; i < tower.Length; i++)
                    {
                        _selectedTile[i] = tower[i].gameObject;
                    }
                    _calculateEverything();
                }
                else
                {
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

        if (currentEvent.type == EventType.MouseDown && _centerRect.Contains(currentEvent.mousePosition))
        {
            _showButtons = true;
            _index = -1;
        }

        if (currentEvent.type == EventType.MouseUp)
        {
            if (_showButtons)
            {
                Debug.Log("User selected #" + _index);
                _resolveButtonPressed();
            }
            _showButtons = false;
        }

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
    /// Place the right Turret
    /// </summary>
    private void _resolveButtonPressed()
    {
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
