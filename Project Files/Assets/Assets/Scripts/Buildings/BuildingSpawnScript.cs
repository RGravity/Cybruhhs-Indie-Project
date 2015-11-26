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
    [SerializeField]
    private int _offsetForRadialWheel;
    

    //TOWERS
    [SerializeField]
    private Texture[] _turretTextures;

    private int _ringCount;
    private Rect _centerRect;
    private Rect[] _ringRects;
    private float _angle;
    private bool _showButtons;
    private int _index;
    private GameObject _selectedTile;
    private BaseScript _baseScript;
    private TileMapScript _tileMap;

    // Use this for initialization
    void Start ()
    {
        _tileMap = FindObjectOfType<TileMapScript>();
        _baseScript = GameObject.FindObjectOfType<BaseScript>();
        _calculateEverything();
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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit vHit = new RaycastHit();
            Ray vRay = _myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                int[,] iets = _tileMap.Tiles;
                int i = iets[(int)vHit.collider.transform.position.x, (int)vHit.collider.transform.position.y];

                if (_tileMap.TileTypes[i].BuildingAllowed)
                {
                    Vector2 tempPos = _myCam.WorldToScreenPoint(vHit.transform.position);
                    _center = new Vector2(tempPos.x, tempPos.y);
                    _selectedTile = vHit.collider.gameObject;
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
                break;
            //SlowTower            
            case 1:
                break;
            //ArrowTower
            case 2:
                if (_baseScript.Gold >= 250)
                {
                    _selectedTile.GetComponent<Renderer>().material.mainTexture = _turretTextures[0];
                    _selectedTile.AddComponent<ArrowTowerScript>();
                    _baseScript.LowerGold(250); 
                }
                break;
            default:
                break;
        }
    }

}
