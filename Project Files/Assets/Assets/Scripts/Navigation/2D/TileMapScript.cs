using UnityEngine;
using System.Collections.Generic;

public class TileMapScript : MonoBehaviour {

    [SerializeField]
	private GameObject _selectedUnit;

    [SerializeField]
	private TileTypeScript[] _tileTypes;

    private int[,] _tiles;
	private NodeScript[,] _graph;

    private Vector3 _endPosition;
    public Vector3 EndPosition { get { return _endPosition; } set { _endPosition = value; } }

    private int[,] _tileData =
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,3,0,0,0,2},// bottom row
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,3,3,3},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,3,0,0,0},
        {0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,3,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,3,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,3,0,0,3,0,0,3,0,0,0,0,0,0,3,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0},
        {0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,3,0,0,0,0,0,0,0,0},
        {0,0,0,3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,3,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {1,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}//top row
    };


    private int _mapSizeX = 0 ;
	private int _mapSizeY = 0;

	void Start() {
		// Setup the selectedUnit's variable

        _mapSizeX = _tileData.GetLength(1);
        _mapSizeY = _tileData.GetLength(0);

        GenerateMapData();
		GeneratePathfindingGraph();
		GenerateMapVisual();
        GeneratePathTo((int)_endPosition.x, (int)_endPosition.y);
	}

	void GenerateMapData() {
        // Allocate our map tiles
        _tiles = new int[_mapSizeX,_mapSizeY];
		
		int x,y;
		
		// Initialize our map tiles to be grass
		for(x=0; x < _mapSizeX; x++) {
			for(y=0; y < _mapSizeY; y++) {
                switch (_tileData[y, x])
                {
                    case 1:
                        _selectedUnit.GetComponent<UnitScript>().TileX = x;
                        _selectedUnit.GetComponent<UnitScript>().TileY = y;
                        _selectedUnit.GetComponent<UnitScript>().Map = this;
                        _selectedUnit.GetComponentInChildren<MeshRenderer>().enabled = true;
                        break;
                    case 2:
                        GameObject endCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        endCube.transform.position = new Vector3(x, y, -1);
                        _endPosition = endCube.transform.position;
                        break;
                    case 3:
                        //GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        //block.transform.position = new Vector3(x, y, -1);
                        _tiles[x, y] = 1;
                        _tileTypes[_tiles[x, y]].IsWalkable = false;
                        break;
                    default:
                        _tiles[x, y] = 0;
                        break;
                }
			}
		}

	}

	public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY) {

        TileTypeScript tt = _tileTypes[_tiles[targetX,targetY] ];

		if(UnitCanEnterTile(targetX, targetY) == false)
			return Mathf.Infinity;

		float cost = tt.MovementCost;

		if( sourceX!=targetX && sourceY!=targetY) {
			// We are moving diagonally!  Fudge the cost for tie-breaking
			// Purely a cosmetic thing!
			cost += 0.001f;
		}

		return cost;

	}

	void GeneratePathfindingGraph() {
        // Initialize the array
        _graph = new NodeScript[_mapSizeX,_mapSizeY];

		// Initialize a Node for each spot in the array
		for(int x=0; x < _mapSizeX; x++) {
			for(int y=0; y < _mapSizeY; y++) {
                _graph[x,y] = new NodeScript();
                _graph[x,y].X = x;
                _graph[x,y].Y = y;
                //GameObject node = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //node.transform.position = new Vector3(x, y, -1);
                //node.transform.localScale = new Vector3(node.transform.localScale.x/2, node.transform.localScale.y/2, node.transform.localScale.z/2);
            }
		}

		// Now that all the nodes exist, calculate their neighbours
		for(int x=0; x < _mapSizeX; x++) {
			for(int y=0; y < _mapSizeY; y++) {

				// This is the 4-way connection version:
				if(x > 0)
                    _graph[x,y].Neighbours.Add(_graph[x-1, y] );
				if(x < _mapSizeX-1)
                    _graph[x,y].Neighbours.Add(_graph[x+1, y] );
				if(y > 0)
                    _graph[x,y].Neighbours.Add(_graph[x, y-1] );
				if(y < _mapSizeY-1)
                    _graph[x,y].Neighbours.Add(_graph[x, y+1] );


				// This is the 8-way connection version (allows diagonal movement)
				//// Try left
				//if(x > 0) {
				//	graph[x,y].neighbours.Add( graph[x-1, y] );
				//	if(y > 0)
				//		graph[x,y].neighbours.Add( graph[x-1, y-1] );
				//	if(y < mapSizeY-1)
				//		graph[x,y].neighbours.Add( graph[x-1, y+1] );
				//}

				//// Try Right
				//if(x < mapSizeX-1) {
				//	graph[x,y].neighbours.Add( graph[x+1, y] );
				//	if(y > 0)
				//		graph[x,y].neighbours.Add( graph[x+1, y-1] );
				//	if(y < mapSizeY-1)
				//		graph[x,y].neighbours.Add( graph[x+1, y+1] );
				//}

				//// Try straight up and down
				//if(y > 0)
				//	graph[x,y].neighbours.Add( graph[x, y-1] );
				//if(y < mapSizeY-1)
				//	graph[x,y].neighbours.Add( graph[x, y+1] );

				// This also works with 6-way hexes and n-way variable areas (like EU4)
			}
		}
	}

	void GenerateMapVisual() {
		for(int x=0; x < _mapSizeX; x++) {
			for(int y=0; y < _mapSizeY; y++) {
				TileTypeScript tt = _tileTypes[_tiles[x,y]];
				GameObject go = (GameObject)Instantiate( tt.TileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity );

				ClickableTileScript ct = go.GetComponent<ClickableTileScript>();
				ct.tileX = x;
				ct.tileY = y;
				ct.map = this;

			}
		}
	}

	public Vector3 TileCoordToWorldCoord(int x, int y) {
		return new Vector3(x, y, 0);
	}

	public bool UnitCanEnterTile(int x, int y) {

		// We could test the unit's walk/hover/fly type against various
		// terrain flags here to see if they are allowed to enter the tile.

		return _tileTypes[_tiles[x,y] ].IsWalkable;
	}

    public void SetWalkable(int x, int y , bool pPsetWalkable)
    {

        // We could test the unit's walk/hover/fly type against various
        // terrain flags here to see if they are allowed to enter the tile.

        _tileTypes[_tiles[x, y]].IsWalkable = pPsetWalkable;
        _tiles[x, y] = 1;
        GeneratePathTo((int)_endPosition.x, (int)_endPosition.y);
    }

    public void GeneratePathTo(int x, int y) {
        // Clear out our unit's old path.
        _selectedUnit.GetComponent<UnitScript>().CurrentPath = null;

		if( UnitCanEnterTile(x,y) == false ) {
			// We probably clicked on a mountain or something, so just quit out.
			return;
		}

		Dictionary<NodeScript, float> dist = new Dictionary<NodeScript, float>();
		Dictionary<NodeScript, NodeScript> prev = new Dictionary<NodeScript, NodeScript>();

		// Setup the "Q" -- the list of nodes we haven't checked yet.
		List<NodeScript> unvisited = new List<NodeScript>();

        NodeScript source = _graph[_selectedUnit.GetComponent<UnitScript>().TileX, _selectedUnit.GetComponent<UnitScript>().TileY];

        NodeScript target = _graph[x, y];
		
		dist[source] = 0;
		prev[source] = null;

		// Initialize everything to have INFINITY distance, since
		// we don't know any better right now. Also, it's possible
		// that some nodes CAN'T be reached from the source,
		// which would make INFINITY a reasonable value
		foreach(NodeScript v in _graph) {
			if(v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}

			unvisited.Add(v);
		}

		while(unvisited.Count > 0) {
            // "u" is going to be the unvisited node with the smallest distance.
            NodeScript u = null;

			foreach(NodeScript possibleU in unvisited) {
				if(u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}

			if(u == target) {
				break;	// Exit the while loop!
			}

			unvisited.Remove(u);

			foreach(NodeScript v in u.Neighbours) {
				//float alt = dist[u] + u.DistanceTo(v);
				float alt = dist[u] + CostToEnterTile(u.X, u.Y, v.X, v.Y);
				if( alt < dist[v] ) {
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}

		// If we get there, the either we found the shortest route
		// to our target, or there is no route at ALL to our target.

		if(prev[target] == null) {
			// No route between our target and the source
			return;
		}

		List<NodeScript> currentPath = new List<NodeScript>();

        NodeScript curr = target;

		// Step through the "prev" chain and add it to our path
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}

		// Right now, currentPath describes a route from out target to our source
		// So we need to invert it!

		currentPath.Reverse();

        _selectedUnit.GetComponent<UnitScript>().CurrentPath = currentPath;
	}

}
