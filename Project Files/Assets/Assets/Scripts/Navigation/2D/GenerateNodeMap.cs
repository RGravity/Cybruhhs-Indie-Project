using UnityEngine;
using System.Collections;

public static class GenerateNodeMap
{
    private static NodeScript[,] _graph;
    /// <summary>
    /// <para>Create nodes on the tiles</para>
    /// <para>MapsizeX and MapSizeY are needed.</para>
    /// <para>Returns a NodeScript[,] for the Graph</para>
    /// </summary>
    public static NodeScript[,] GeneratePathfindingGraph(int pMapSizeX, int pMapSizeY)
    {
        // Initialize the array
        _graph = new NodeScript[pMapSizeX, pMapSizeY];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < pMapSizeX; x++)
        {
            for (int y = 0; y < pMapSizeY; y++)
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
        for (int x = 0; x < pMapSizeX; x++)
        {
            for (int y = 0; y < pMapSizeY; y++)
            {

                // This is the 4-way connection version:
                if (x > 0)
                    _graph[x, y].Neighbours.Add(_graph[x - 1, y]);
                if (x < pMapSizeX - 1)
                    _graph[x, y].Neighbours.Add(_graph[x + 1, y]);
                if (y > 0)
                    _graph[x, y].Neighbours.Add(_graph[x, y - 1]);
                if (y < pMapSizeY - 1)
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
        }
        return _graph;
    }
}
