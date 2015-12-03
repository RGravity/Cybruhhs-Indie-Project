using UnityEngine;
using System.Collections.Generic;

public class UnitScript : MonoBehaviour
{

    // tileX and tileY represent the correct map-tile position
    // for this piece.  Note that this doesn't necessarily mean
    // the world-space coordinates, because our map might be scaled
    // or offset or something of that nature.  Also, during movement
    // animations, we are going to be somewhere in between tiles.
    private int _tileX;
    private int _tileY;
    private float _speed = 0;
    private TileMapScript _map;
    // Our pathfinding info.  Null if we have no destination ordered.
    private List<NodeScript> _currentPath = null;

    //Tile the unit is on.
    public int TileX { get { return _tileX; } set { _tileX = value; } }
    public int TileY { get { return _tileY; } set { _tileY = value; } }
    public TileMapScript Map { get { return _map; } set { _map = value; } }
    //Path the unit is walking.
    public List<NodeScript> CurrentPath { get { return _currentPath; } set { _currentPath = value; } }

    //Speed of the unit
    public float Speed { get { return _speed; } set { _speed = value; } }

    //If the unit is slowed down by the Spider
    private bool _isSlowed = false;
    public bool IsSlowed { get { return _isSlowed; } set { _isSlowed = value; } }

    //Direction of the Animation
    private bool _isDirectionRight = false;
    private bool _isDirectionLeft = false;
    private bool _isDirectionUp = false;
    private bool _isDirectionDown = false;

    void Update()
    {
        // Draw our debug line showing the pathfinding!
        // NOTE: This won't appear in the actual game view.
        if (_currentPath != null)
        {
            int currNode = 0;

            while (currNode < _currentPath.Count - 1)
            {

                Vector3 start = _map.TileCoordToWorldCoord(_currentPath[currNode].X, _currentPath[currNode].Y) +
                    new Vector3(0, 0, -0.5f);
                Vector3 end = _map.TileCoordToWorldCoord(_currentPath[currNode + 1].X, _currentPath[currNode + 1].Y) +
                    new Vector3(0, 0, -0.5f);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }

        // Have we moved our Unit close enough to the target tile that we can
        // advance to the next step in our pathfinding?
        if (this != null)
        {
            if (Vector3.Distance(transform.position, new Vector3(_tileX, _tileY,-1)) < 0.1f)
            {
                _advancePathing();
            }
            // Smoothly animate towards the correct map tile.
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_tileX, _tileY, -1), _speed * Time.deltaTime);
            
                //Animation of left and right
                if (transform.position.x - _tileX < 0 && _isDirectionLeft == false)
            {
                this.GetComponent<Animator>().Play("Walking");

                if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
                _isDirectionLeft = true;
                _isDirectionRight = false;
                _isDirectionDown = false;
                _isDirectionUp = false;
            }
            else if (transform.position.x - _tileX > 0 && _isDirectionRight == false)
            {
                this.GetComponent<Animator>().Play("Walking");

                if (transform.localScale.x == -1)
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }
                else if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
                _isDirectionRight = true;
                _isDirectionLeft = false;
                _isDirectionDown = false;
                _isDirectionUp = false;
            }
            //Animation of up and Down
            else if (transform.position.y - _tileY > 0)
            {
                _isDirectionLeft = false;
                _isDirectionRight = false;
                _isDirectionDown = true;
                _isDirectionUp = false;
                this.GetComponent<Animator>().Play("WalkingDown");
            }
            else if (transform.position.y - _tileY < 0)
            {
                _isDirectionLeft = false;
                _isDirectionRight = false;
                _isDirectionDown = false;
                _isDirectionUp = true;
                this.GetComponent<Animator>().Play("WalkingUp");
            }
        }
    }

    /// <summary>
    /// <para>Move according to the pathfinding on TileMapScript</para>
    /// <para></para>
    /// </summary>
    private void _advancePathing()
    {
        if (_currentPath == null)
            return;

        // Teleport us to our correct "current" position, in case we
        // haven't finished walking there yet.
        transform.position = _map.TileCoordToWorldCoord(_tileX, _tileY);
        
        // Move us to the next tile in the sequence
        _tileX = _currentPath[1].X;
        _tileY = _currentPath[1].Y;

        // Remove the old "current" tile from the pathfinding list
        _currentPath.RemoveAt(0);

        if (_currentPath.Count == 1)
        {
            // We only have one tile left in the path, and that tile MUST be our ultimate
            // destination -- and we are standing on it!
            // So let's just clear our pathfinding info.
            _currentPath = null;
        }
    }
}
