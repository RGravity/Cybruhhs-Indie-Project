using UnityEngine;
using System.Collections;
using System;

public class TiledWorld : MonoBehaviour {

    private int _columns;
    private int _rows;
    private int _tileSize;
    private bool[,] _tileData;

    private NodeWorld _nodeWorld = new NodeWorld();
    private Node[,] _nodeCache;

    private int[,] _tiledWorld =
    {
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
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };
	// Use this for initialization
	void Start () {
        _tileWorld(30, 18, 32);
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

        ////draw a grid over our world
        //_gridView = new Canvas(width + 1, height + 1);
        //AddChild(_gridView);
        //drawGrid();
    }
    private void _initializeWorld()
    {
        int i = 0;
        for (int column = 0; column < _columns; column++)
        {
            for (int row = 0; row < _rows; row++)
            {
               GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(column, row, 0);
                if (!IsWalkable(column,row))
                {
                    Node node = new Node(new Vector3(column, row, 0));
                    _nodeWorld.addNode(node);
                    if (_nodeWorld.NodeList.Count > 1)
                    {
                        _nodeWorld.addConnection(node, _nodeWorld.NodeList[i]);
                    }
                i++;
                }
            }
        }
    }

    private void _makeConnections()
    {

    }

    public bool IsWalkable(int pColumn, int pRow)
    {
        return _tileData[pColumn, pRow];
    }

    private void _makeNodes()
    {
        Node tmpNode = null;
        int index = 1;
        for (int column = 0; column < _columns; column++)
        {
            for (int row = 0; row < _rows; row++)
            {
                if (IsWalkable(column, row))
                {
                    tmpNode = new Node(/*5,*/ new Vector3((column * _tileSize) + _tileSize / 2, (row * _tileSize) + _tileSize / 2));
                    index++;
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
                            //_nodeWorld.addConnection(tmpNode.Index, _nodeCache[nodeColumn, nodeRow].Index);
                            //Console.WriteLine(tmpNode.Index + " , " + _nodeCache[nodeColumn, nodeRow].Index);
                        }
                    }
                }
            }
        }

    }
    // Update is called once per frame
    void Update () {
	}
}
