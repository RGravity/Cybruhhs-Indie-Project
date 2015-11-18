using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TiledWorldScript : MonoBehaviour {

    private int _columns;
    private int _rows;
    private int _tileSize;
    private bool[,] _tileData;
    //private bool[,] _startNode;
    //private bool[,] _endNode;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private NodeScript _startNode;
    private NodeScript _endNode;
    private GameObject _enemy;

    private NodeWorldScript _nodeWorld = new NodeWorldScript();
    private NodeScript[,] _nodeCache;

    private int[,] _tiledWorld =
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},// bottom row
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}//top row
    };
	// Use this for initialization
	void Start () {
        //_tileWorld(30, 18, 32);
        _tileWorld(_tiledWorld);
        _makeConnections();
        _startNode = _nodeWorld.GetNodeAtPosition(_startPosition);
        _endNode = _nodeWorld.GetNodeAtPosition(_endPosition);
        _enemy.GetComponent<AIWalkingScript>().GetPath(_startNode, _endNode);
    }
    private void _tileWorld(int pColumns, int pRows, int pTileSize)
		{
        //store base values
        _columns = pColumns;
        _rows = pRows;
        _tileSize = pTileSize;

        //create underlying data structure
        _tileData = new bool[_columns, _rows];

        //generate the data in our world
        _initializeWorld();
        //draw our world
        //drawWorld();
        _nodeCache = new NodeScript[_columns, _rows];
        ////draw a grid over our world
        //_gridView = new Canvas(width + 1, height + 1);
        //AddChild(_gridView);
        //drawGrid();
    }
    private void _tileWorld(int[,] pTileData)
    {
        _columns = pTileData.GetLength(1);
        _rows = pTileData.GetLength(0);
        _tileData = new bool[_columns, _rows];
        _initializeWorld();
        _nodeCache = new NodeScript[_columns, _rows];
    //    //store base values
    //    _columns = pColumns;
    //    _rows = pRows;
    //    _tileSize = pTileSize;

    //    //create underlying data structure
    //    _tileData = new bool[_columns, _rows];

    //    //generate the data in our world
    //    _initializeWorld();
    //    //draw our world
    //    //drawWorld();
    //    _nodeCache = new NodeScript[_columns, _rows];
    //    ////draw a grid over our world
    //    //_gridView = new Canvas(width + 1, height + 1);
    //    //AddChild(_gridView);
    //    //drawGrid();
    }
    private void _initializeWorld()
    {
        for (int column = 0; column < _columns; column++)
        {
            for (int row = 0; row < _rows; row++)
            {
                switch (_tiledWorld[row, column])
                {
                    case 1:
                        _tileData[column, row] = true;
                        GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        cube1.transform.position = new Vector3(column, row, -1);
                        cube1.name = "Enemy";
                        cube1.AddComponent<AIWalkingScript>();
                        cube1.AddComponent<Rigidbody2D>();
                        _startPosition = new Vector3(column, row, 1);
                        _enemy = cube1;
                        break;
                    case 2:
                        _tileData[column, row] = true;
                        GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube2.transform.position = new Vector3(column, row, -1);
                        cube2.name = "EndCube";
                        _endPosition = new Vector3(column, row, 1);
                        break;
                    default:
                        break;
                }
                _tileData[column, row] = true;
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(column, row, 0);
            }
        }
    }

    private void _findConnections()
    {
        List<NodeScript> Nodes = new List<NodeScript>();
        for (int i = 0; i < _columns; i++)
        {
            for (int y = 0; y < _rows; y++)
            {
                GameObject overlapCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                overlapCube.transform.position = new Vector3(i, y, -1);
                overlapCube.transform.localScale = new Vector3(2, 2, 1);
                overlapCube.GetComponent<MeshRenderer>().enabled = false;
                Collider[] hits = Physics.OverlapSphere(overlapCube.transform.position,1);
                if (hits.Length > 1)
                {
                    foreach (Collider hit in hits)
                    {
                        if (hit.gameObject.name == "Node")
                        {
                            Nodes.Add(hit.gameObject.GetComponent<NodeScript>());
                        }
                    }
                    //_makeConnections(Nodes);
                }
            }
        }
    }

    private void _makeConnections()
    {

        NodeScript tmpNode = null;
        //int index = 1;
        for (int column = 0; column < _columns; column++)
        {
            for (int row = 0; row < _rows; row++)
            {
                if (this.IsWalkable(column, row))
                {
                    //tmpNode = new Node(new Vector3((column * _tileSize) + _tileSize / 2, (row * _tileSize) + _tileSize / 2));
                    tmpNode = new NodeScript(new Vector3(column, row, 1));

                    _nodeCache[column, row] = tmpNode;
                    _nodeWorld.addNode(tmpNode);
                }
            }
        }
        for (int column = 0; column < _columns; column++)
        {
            for (int row = 0; row < _rows; row++)
            {
                if (!this.IsWalkable(column, row)) continue;

                tmpNode = _nodeCache[column, row];

                int minNodeColumn = Math.Max(0, column - 1);
                int maxNodeColumn = Math.Min(_columns - 1, column + 1);
                int minNodeRow = Math.Max(0, row - 1);
                int maxNodeRow = Math.Min(_rows - 1, row + 1);

                for (int nodeColumn = minNodeColumn; nodeColumn <= maxNodeColumn; nodeColumn++)
                {
                    for (int nodeRow = minNodeRow; nodeRow <= maxNodeRow; nodeRow++)
                    {
                        if (nodeColumn == column && nodeRow == row) continue;
                        //if (nodeColumn == 9) Console.WriteLine(nodeRow);

                        if ((Math.Abs(nodeColumn - column) + Math.Abs(nodeRow - row) == 2) && !(this.IsWalkable(nodeColumn, row) && this.IsWalkable(column, nodeRow))) continue;

                        if (_nodeCache[nodeColumn, nodeRow] != null)
                        {
                            _nodeWorld.addConnection(tmpNode, _nodeCache[nodeColumn, nodeRow]);
                            //Console.WriteLine(tmpNode.Index + " , " + _nodeCache[nodeColumn, nodeRow].Index);
                        }
                    }
                }
            }
        }
        
    }


    public bool IsWalkable(int pColumn, int pRow)
    {
        return _tileData[pColumn, pRow];
    }
    
    // Update is called once per frame
    void Update () {
	}
}
