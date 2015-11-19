using UnityEngine;
using System.Collections.Generic;

public class UnitScript : MonoBehaviour {

	// tileX and tileY represent the correct map-tile position
	// for this piece.  Note that this doesn't necessarily mean
	// the world-space coordinates, because our map might be scaled
	// or offset or something of that nature.  Also, during movement
	// animations, we are going to be somewhere in between tiles.
	private int _tileX;
    private int _tileY;

    public int TileX { get { return _tileX; } set { _tileX = value; } }
    public int TileY { get { return _tileY; } set { _tileY = value; } }

    private TileMapScript _map;

    public TileMapScript Map { get { return _map; } set { _map = value; } }

    // Our pathfinding info.  Null if we have no destination ordered.
    private List<NodeScript> _currentPath = null;

    public List<NodeScript> CurrentPath { get { return _currentPath; } set { _currentPath = value; } }

	void Update() {
		// Draw our debug line showing the pathfinding!
		// NOTE: This won't appear in the actual game view.
		if(_currentPath != null) {
			int currNode = 0;

			while( currNode < _currentPath.Count-1 ) {

				Vector3 start = _map.TileCoordToWorldCoord(_currentPath[currNode].X, _currentPath[currNode].Y ) + 
					new Vector3(0, 0, -0.5f) ;
				Vector3 end   = _map.TileCoordToWorldCoord(_currentPath[currNode+1].X, _currentPath[currNode+1].Y)  + 
					new Vector3(0, 0, -0.5f) ;

				Debug.DrawLine(start, end, Color.red);

				currNode++;
			}
		}

		// Have we moved our visible piece close enough to the target tile that we can
		// advance to the next step in our pathfinding?
		if(Vector3.Distance(transform.position, _map.TileCoordToWorldCoord(_tileX,_tileY )) < 0.1f)
			AdvancePathing();

		// Smoothly animate towards the correct map tile.
		transform.position = Vector3.Lerp(transform.position, _map.TileCoordToWorldCoord(_tileX, _tileY), 5f * Time.deltaTime);
	}

	// Advances our pathfinding progress by one tile.
	void AdvancePathing() {
		if(_currentPath == null)
			return;

		// Teleport us to our correct "current" position, in case we
		// haven't finished the animation yet.
		transform.position = _map.TileCoordToWorldCoord(_tileX, _tileY);

        // Get cost from current tile to next tile
        //remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y );

        // Move us to the next tile in the sequence
        _tileX = _currentPath[1].X;
        _tileY = _currentPath[1].Y;

        // Remove the old "current" tile from the pathfinding list
        _currentPath.RemoveAt(0);
		
		if(_currentPath.Count == 1) {
            // We only have one tile left in the path, and that tile MUST be our ultimate
            // destination -- and we are standing on it!
            // So let's just clear our pathfinding info.
            _currentPath = null;
		}
	}

	// The "Next Turn" button calls this.
	public void NextTurn() {
		// Make sure to wrap-up any outstanding movement left over.
		while(_currentPath != null) {
			AdvancePathing();
		}

		// Reset our available movement points.
	}
}
