using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class TileMapScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _selectedUnits = new List<GameObject>();
    [SerializeField]
    private TileTypeScript[] _tileTypes;

    private int[,] _tiles;
    private NodeScript[,] _graph;
    private Vector3 _endPosition;
    private Vector3 _waveStartposition;
    private List<Vector3> _listWaveStartPositions;
    private List<Vector3> _listWaveEndPositions;
    private List<List<NodeScript>> _possibleRoutes;
    private DontDestroyOnLoadMusicScript _map;

    public List<GameObject> SelectedUnits { get { return _selectedUnits; } set { _selectedUnits = value; } }
    public Vector3 EndPosition { get { return _endPosition; } set { _endPosition = value; } }
    public Vector3 WaveStartPosition { get { return _waveStartposition; } set { _waveStartposition = value; } }

    public int[,] Tiles { get { return _tiles; } }
    public TileTypeScript[] TileTypes { get { return _tileTypes; } }

    private int _level = 1;
    //Levels in XML presented as Objects
    private Object[] _xmlLevels;
    private Object[] _lvlBGs;
    //TileData reads the XML Needs to be change with Tiled.
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

    //Sizes of the map
    private int _mapSizeX = 0;
    private int _mapSizeY = 0;

    void Start()
    {
        if (FindObjectOfType<WaveMainScript>().DebugLevel != 0)
        {
            _level = FindObjectOfType<WaveMainScript>().DebugLevel;
        }

        _listWaveStartPositions = new List<Vector3>();
        _listWaveEndPositions = new List<Vector3>();
        _map = GameObject.FindObjectOfType<DontDestroyOnLoadMusicScript>();
        //temporary: for loading level 1 when the game starts(REMOVE WHEN LEVEL SELECTION IS IMPLEMENTED)
        if (_map != null)
        {
            StartLevel(_map.Level);
        }
        else
        {
            StartLevel(_level);
        }

    }

    /// <summary>
    /// <para>Start level</para>
    /// <para>Parameter pLevel, put in a int for the requested level</para>
    /// </summary>
    public void StartLevel(int pLevel)
    {
       
        //Load all the levels in an array
        if (_xmlLevels == null)
        {
            _xmlLevels = Resources.LoadAll("Tiled/Levels");
            if (_xmlLevels != null && _xmlLevels.Length > 0)
            {
                _lvlBGs = Resources.LoadAll("Tiled/LevelBGs/1");
            }
        }
        //Parse Level to _tileData
        if (pLevel > 0 && pLevel <= _xmlLevels.Length)
        {
            _importLevel(pLevel);

            _possibleRoutes = new List<List<NodeScript>>();

            //Generate Map, Nodes and Visuals
            _generateMapData();
            _generatePathfindingGraph();
            _generateMapVisual();

            GameObject.FindObjectOfType<WaveMainScript>().StartSpawning(_listWaveStartPositions, _listWaveEndPositions, this, _graph, pLevel);
        }
        else
        {
            Debug.Log("Could not load level " + pLevel);
        }
        

        
    }

    /// <summary>
    /// <para>Import the Level and put in TileData</para>
    /// <para>To Generate the level ingame</para>
    /// </summary>
    private void _importLevel(int pLevel)
    {
        XmlReader Reader = XmlReader.Create(new System.IO.StringReader(_xmlLevels[pLevel - 1].ToString()));

        //Read XML for Width and Height and make the array _tileData the appropriate size
        Reader.ReadToFollowing("layer");
        Reader.MoveToAttribute("width");
        _mapSizeX = System.Convert.ToInt16(Reader.Value);
        Reader.MoveToNextAttribute();
        _mapSizeY = System.Convert.ToInt16(Reader.Value);
        _tileData = new int[_mapSizeY, _mapSizeX];

        //Read XML for level data
        Reader.ReadToFollowing("data");
        string levelData = Reader.ReadInnerXml();
        //remove unwanted characters
        levelData = levelData.Replace("\n", "");
        levelData = levelData.Replace("\r", "");

        //Split string to String array
        string[] tileStringArray = levelData.Split(',');

        //Convert String array to Int Array
        int[] tileIntArray = new int[tileStringArray.Length];
        for (int i = 0; i < tileStringArray.Length - 1; i++)
        {
            tileIntArray[i] = System.Convert.ToInt16(tileStringArray[i]);
        }

        //Put the 1D array into a 2D array
        int index = 0;
        for (int y = 0; y < _mapSizeY; y++)
        {
            for (int x = 0; x < _mapSizeX; x++)
            {
                _tileData[y, x] = tileIntArray[index];
                if (index == tileIntArray.Length-1)
                {
                    _tileData[y, x] = tileIntArray[index - 1];
                }
                index++;
            }
        }

        //flipping the array to match the tiled file and the ingame level
        for (int y = 0; y < Mathf.FloorToInt(_mapSizeY / 2); y++)
        {
            for (int x = 0; x < _mapSizeX; x++)
            {
                int temp = _tileData[y, x];
                _tileData[y, x] = _tileData[_mapSizeY - 1 - y, x];
                _tileData[_mapSizeY - 1 - y, x] = temp;
            }
        }





    }


    /// <summary>
    /// <para>Create a map based on the TileData</para>
    /// <para>Use case to set the tile</para>
    /// </summary>
    private void _generateMapData()
    {
        // Allocate our map tiles
        _tiles = new int[_mapSizeX, _mapSizeY];

        int x, y;

        // Initialize our map tiles to be grass
        for (x = 0; x < _mapSizeX; x++)
        {
            for (y = 0; y < _mapSizeY; y++)
            {
                switch (_tileData[y, x])
                {
                    case 1://BASE
                        _tiles[x, y] = 0;
                        _listWaveEndPositions.Add(new Vector3(x, y, -1));
                        break;
                    case 2://Non-walkable ?grass?
                        _tiles[x, y] = 2;
                        break;
                    case 3://Border Outside Camera
                        _tiles[x, y] = 1;
                        break;
                    case 4://Road
                        _tiles[x, y] = 3;
                        break;
                    case 5://Monster spawn tiles
                        _tiles[x, y] = 4;
                        _listWaveStartPositions.Add(new Vector3(x, y, -1));
                        break;
                    case 6://bridge
                        _tiles[x, y] = 9;
                        break;
                    case 7://tower placement tile 1
                        _tiles[x, y] = 5;
                        break;
                    case 8://tower placement tile 2
                        _tiles[x, y] = 6;
                        break;
                    case 9://tower placement tile 3
                        _tiles[x, y] = 7;
                        break;
                    case 10://tower placement tile 4
                        _tiles[x, y] = 8;
                        break;
                    default:
                        _tiles[x, y] = 2;
                        break;
                }
            }
        }

    }
    /// <summary>
    /// <para> Properly not going to need. It's a cost to change to the pawn.</para>
    /// <para></para>
    /// </summary>
    public float CostToEnterTile(int pSourceX, int pSourceY, int pTargetX, int pTargetY)
    {

        TileTypeScript tt = _tileTypes[_tiles[pTargetX, pTargetY]];

        if (UnitCanEnterTile(pTargetX, pTargetY) == false)
            return Mathf.Infinity;

        float cost = tt.MovementCost;

        if (pSourceX != pTargetX && pSourceY != pTargetY)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost += 0.001f;
        }

        return cost;

    }

    /// <summary>
    /// <para>Create the nodes and the way the connections are made.</para>
    /// <para></para>
    /// </summary>
    private void _generatePathfindingGraph()
    {
        // Initialize the array
        _graph = GenerateNodeMap.GeneratePathfindingGraph(_mapSizeX, _mapSizeY);
        #region MyRegion
        /*_graph = new NodeScript[_mapSizeX, _mapSizeY];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < _mapSizeX; x++)
        {
            for (int y = 0; y < _mapSizeY; y++)
            {
                _graph[x, y] = new NodeScript();
                _graph[x, y].X = x;
                _graph[x, y].Y = y;
                #region create the node
                //GameObject node = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //node.transform.position = new Vector3(x, y, -1);
                //node.transform.localScale = new Vector3(node.transform.localScale.x/2, node.transform.localScale.y/2, node.transform.localScale.z/2);
                #endregion
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < _mapSizeX; x++)
        {
            for (int y = 0; y < _mapSizeY; y++)
            {

                // This is the 4-way connection version:
                if (x > 0)
                    _graph[x, y].Neighbours.Add(_graph[x - 1, y]);
                if (x < _mapSizeX - 1)
                    _graph[x, y].Neighbours.Add(_graph[x + 1, y]);
                if (y > 0)
                    _graph[x, y].Neighbours.Add(_graph[x, y - 1]);
                if (y < _mapSizeY - 1)
                    _graph[x, y].Neighbours.Add(_graph[x, y + 1]);

                #region diagonal
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
                #endregion
            }
        } */
        #endregion
    }
    /// <summary>
    /// <para>Create the tiles based on tiles that you make in the _generateMapData</para>
    /// <para></para>
    /// </summary>
    private void _generateMapVisual()
    {
        int lvlBGindex = 0;
        for (int x = 0; x < _mapSizeX; x++)
        {
            for (int y = 0; y < _mapSizeY; y++)
            {
                TileTypeScript tt = _tileTypes[_tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.TileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                if (tt.BuildingAllowed)
                {
                    go.AddComponent<ClickableTileScript>();
                    go.GetComponent<Material>().mainTexture = _lvlBGs[lvlBGindex] as Texture;
                    ClickableTileScript ct = go.GetComponent<ClickableTileScript>();
                    ct.tileX = x;
                    ct.tileY = y;
                    ct.map = this;
                }
            }
        }
    }
    /// <summary>
    /// <para>Make a tileCoord to World</para>
    /// <para></para>
    /// </summary>
    public Vector3 TileCoordToWorldCoord(int pX, int pY)
    {
        return new Vector3(pX, pY, -1);
    }
    /// <summary>
    /// <para>IsWalkable to see if you can enter the tile</para>
    /// <para></para>
    /// </summary>
    public bool UnitCanEnterTile(int pX, int pY)
    {

        // We could test the unit's walk/hover/fly type against various
        // terrain flags here to see if they are allowed to enter the tile.

        return _tileTypes[_tiles[pX, pY]].IsWalkable;
    }

    public void SetWalkable(int pX, int pY, bool pPsetWalkable)
    {

        // We could test the unit's walk/hover/fly type against various
        // terrain flags here to see if they are allowed to enter the tile.

        _tileTypes[_tiles[pX, pY]].IsWalkable = pPsetWalkable;
        _tiles[pX, pY] = 1;
        //GeneratePathTo((int)_endPosition.x, (int)_endPosition.y);
    }
    /// <summary>
    /// <para>Create the Path from start to end</para>
    /// <para></para>
    /// </summary>
    
    public void GeneratePathTo(int pX, int pY)
    {
        #region Old Path(Do not delete, maybe we need it if the new path fails)
        // Clear out our unit's old path.
        //foreach (GameObject unit in _selectedUnits)
        //{
        //    unit.GetComponent<UnitScript>().CurrentPath = null;
        //}
        //foreach (GameObject unit in _selectedUnits)
        //{
        //    if (UnitCanEnterTile(pX, pY) == false)
        //    {
        //        // We probably clicked on a mountain or something, so just quit out.
        //        return;
        //    }

        //    Dictionary<NodeScript, float> dist = new Dictionary<NodeScript, float>();
        //    Dictionary<NodeScript, NodeScript> prev = new Dictionary<NodeScript, NodeScript>();

        //    // Setup the "Q" -- the list of nodes we haven't checked yet.
        //    List<NodeScript> unvisited = new List<NodeScript>();

        //    NodeScript source = _graph[unit.GetComponent<UnitScript>().TileX, unit.GetComponent<UnitScript>().TileY];

        //    NodeScript target = _graph[pX, pY];

        //    dist[source] = 0;
        //    prev[source] = null;

        //    // Initialize everything to have INFINITY distance, since
        //    // we don't know any better right now. Also, it's possible
        //    // that some nodes CAN'T be reached from the source,
        //    // which would make INFINITY a reasonable value
        //    foreach (NodeScript v in _graph)
        //    {
        //        if (v != source)
        //        {
        //            dist[v] = Mathf.Infinity;
        //            prev[v] = null;
        //        }

        //        unvisited.Add(v);
        //    }

        //    while (unvisited.Count > 0)
        //    {
        //        // "u" is going to be the unvisited node with the smallest distance.
        //        NodeScript u = null;

        //        foreach (NodeScript possibleU in unvisited)
        //        {
        //            if (u == null || dist[possibleU] < dist[u])
        //            {
        //                u = possibleU;
        //            }
        //        }

        //        if (u == target)
        //        {
        //            break;  // Exit the while loop!
        //        }

        //        unvisited.Remove(u);

        //        foreach (NodeScript v in u.Neighbours)
        //        {
        //            //float alt = dist[u] + u.DistanceTo(v);
        //            float alt = dist[u] + CostToEnterTile(u.X, u.Y, v.X, v.Y);
        //            if (alt < dist[v])
        //            {
        //                dist[v] = alt;
        //                prev[v] = u;
        //            }
        //        }
        //    }

        //    // If we get there, the either we found the shortest route
        //    // to our target, or there is no route at ALL to our target.

        //    if (prev[target] == null)
        //    {
        //        // No route between our target and the source
        //        return;
        //    }

        //    List<NodeScript> currentPath = new List<NodeScript>();

        //    NodeScript curr = target;

        //    // Step through the "prev" chain and add it to our path
        //    while (curr != null)
        //    {
        //        currentPath.Add(curr);
        //        curr = prev[curr];
        //    }

        //    // Right now, currentPath describes a route from out target to our source
        //    // So we need to invert it!

        //    currentPath.Reverse();


        //    #region Michael is Working here
        //    //foreach (List<NodeScript> nodeListRoute in _possibleRoutes)
        //    //{
        //    //    if (!nodeListRoute.Equals(currentPath))
        //    //    {
        //    //        _possibleRoutes.Add(currentPath);
        //    //        break;
        //    //    }
        //    //}
        //    //List<NodeScript> route = this.GeneratePathTo((int)_endPosition.x, (int)_endPosition.y, currentPath);
        //    #endregion
        //    unit.GetComponent<UnitScript>().CurrentPath = currentPath;

        //    //unit.GetComponent<UnitScript>().CurrentPath = _possibleRoutes[1];
        //}
        #endregion
    }

}
