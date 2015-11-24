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

    private int _ringCount;
    private Rect _centerRect;
    private Rect[] _ringRects;
    private float _angle;
    private bool _showButtons;
    private int _index;


    // Use this for initialization
    void Start ()
    {
        _calculateEverything();
    }

    /// <summary>
    /// Calculating everything for the Radial Wheel to put everything in the right place.
    /// </summary>
    private void _calculateEverything()
    {
        _ringCount = _normalButtons.Length;
        _angle = 360.0f / _ringCount;

        _centerRect.x = _center.x - _centerButton.width * 0.5f;
        _centerRect.y = _center.y - _centerButton.height * 0.5f;
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
            rect.y = _center.y + vector.y - height * 0.5f;
            _ringRects[i] = rect;
            vector = Quaternion.AngleAxis(_angle, Vector3.forward) * vector;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit vHit = new RaycastHit();
            Ray vRay = _myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                if (vHit.collider.gameObject.GetComponent<ClickableTileScript>())
                {
                    Vector2 tempPos = _myCam.WorldToScreenPoint(vHit.transform.position);
                    _center = new Vector2(tempPos.x, tempPos.y);

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
            }
            _showButtons = false;
        }

        if (currentEvent.type == EventType.MouseDrag)
        {
            Vector2 mouseOffset = currentEvent.mousePosition - _center;
            float angle = Mathf.Atan2(mouseOffset.y, mouseOffset.x) * Mathf.Rad2Deg;
            angle += _angle / 2.0f;
            if (angle < 0)
            {
                angle = angle + 360.0f;
            }

            _index = (int)(angle / _angle);
        }
        GUI.DrawTexture(_centerRect, _centerButton);

        if (_showButtons)
        {
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

}
