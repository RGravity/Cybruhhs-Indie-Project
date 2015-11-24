using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchPathScript
{
    private List<List<NodeScript>> _possibleRoutes; //list of possible routes
    private bool _isAlternative = false; //If there is another possibility for another route in the search path.
    private TileMapScript _map;

    public SearchPathScript(TileMapScript pMap)
    {
        _map = pMap;
        _possibleRoutes = new List<List<NodeScript>>();
    }


    /// <summary>
    /// This method creates a path out of parents nodes.
    /// </summary>
    /// <param name="pNode"> The parent node</param>
    /// <returns></returns>
    private List<NodeScript> _constructPath(NodeScript pNode)
    {
        List<NodeScript> path = new List<NodeScript>();
        while (pNode.PathParent != null)
        {
            path.Add(pNode);
            pNode = pNode.PathParent;
        }
        return path;
    }
    /// <summary>
    /// Search for 2 different paths! More in the next days.
    /// Give backs a list of a list with nodes! 1 list with nodes is a path.
    /// It's a list of possible paths.
    /// Work in progress.
    /// </summary>
    /// <param name="pStartNode">Start of the wave</param>
    /// <param name="pGoalNode">End of the wave</param>
    /// <param name="pPreviousPath">check with older paths</param>
    /// <param name="pMultiple">counts how many possible path are there.</param>
    /// <returns></returns>
    public List<List<NodeScript>> SearchPaths(NodeScript pStartNode, NodeScript pGoalNode, List<NodeScript> pPreviousPath = null, int pMultiple = 0)
    {
        // list of visited nodes
        List<NodeScript> closedList = new List<NodeScript>();
        int multipleRoutes = pMultiple;

        // list of nodes to visit (sorted)
        List<NodeScript> openList = new List<NodeScript>();
        openList.Add(pStartNode);
        pStartNode.PathParent = null;
        int listCount = 0;
        while (openList != null)
        {
            NodeScript node = openList[listCount];
            if (node == pGoalNode)
            {
                // path found!
                foreach (List<NodeScript> nodeList in _possibleRoutes)
                {
                    List<NodeScript> pathList = _constructPath(pGoalNode);
                    pathList.Reverse();
                    bool isEqual = _isEqual(pathList, nodeList);
                    //if (isEqual == false)
                    //{
                    //    multipleRoutes++;
                    //    _possibleRoutes.Add(pathList);
                    //    _searchPaths(_graph[(int)WaveStartPosition.x, (int)WaveStartPosition.y], _graph[(int)_endPosition.x, (int)_endPosition.y], pathList, multipleRoutes);
                    //    break;
                    //}
                    /*else*/
                    if (isEqual)
                    {
                        return _possibleRoutes;
                    }
                    if (pMultiple > 0)
                    {
                        _possibleRoutes.Add(pathList);
                        return _possibleRoutes;
                    }
                    else
                    {
                        if (_possibleRoutes.Count > 1)
                        {
                            return _possibleRoutes;
                        }

                        continue;
                    }

                }
                List<NodeScript> path = _constructPath(pGoalNode);
                path.Reverse();
                if (pMultiple < 1)
                {
                    _possibleRoutes.Add(path);
                    multipleRoutes++;
                    SearchPaths(pStartNode, pGoalNode, path, multipleRoutes);
                }

                return _possibleRoutes;//_searchPaths(_graph[(int)WaveStartPosition.x, (int)WaveStartPosition.y], _graph[(int)_endPosition.x, (int)_endPosition.y], path, ++multipleRoutes);
            }
            else
            {
                closedList.Add(node);

                // add neighbors to the open list
                //..Iterator i = node.Neighbours.iterator();
                for (int i = 0; i < node.Neighbours.Count; i++)
                {
                    NodeScript neighborNode = (NodeScript)node.Neighbours[i];
                    if (_map.UnitCanEnterTile((int)node.Neighbours[i].X, (int)node.Neighbours[i].Y))
                    {
                        //multiple++;
                        if (pPreviousPath == null)
                        {
                            pPreviousPath = new List<NodeScript>();
                        }
                        if (!closedList.Contains(neighborNode) && !openList.Contains(neighborNode) && !pPreviousPath.Contains(neighborNode))
                        {
                            neighborNode.PathParent = node;
                            openList.Add(neighborNode);
                            _isAlternative = true;
                            if (node.Neighbours.Count != i + 1)
                            {
                                break;
                            }
                            //listCount++;
                        }

                        else if (!closedList.Contains(neighborNode) && !openList.Contains(neighborNode) && pPreviousPath.Count < 1)
                        {
                            neighborNode.PathParent = node;
                            openList.Add(neighborNode);
                        }
                    }
                    if (node.Neighbours.Count == i + 1 && pPreviousPath.Count > 0 && _isAlternative == false)
                    {
                        for (int j = 0; j < node.Neighbours.Count; j++)
                        {
                            neighborNode = (NodeScript)node.Neighbours[j];
                            if (_map.UnitCanEnterTile((int)node.Neighbours[j].X, (int)node.Neighbours[j].Y))
                            {
                                if (!closedList.Contains(neighborNode) && !openList.Contains(neighborNode))
                                {
                                    neighborNode.PathParent = node;
                                    openList.Add(neighborNode);
                                }
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                _isAlternative = false;
                listCount++;
            }
        }
        return null;
    }
    /// <summary>
    /// Compares 2 list with each other. When all are the same return true else false
    /// </summary>
    /// <param name="pList1">List 1</param>
    /// <param name="pList2">List 2</param>
    /// <returns></returns>
    private bool _isEqual(List<NodeScript> pList1, List<NodeScript> pList2)
    {
        for (int i = 0; i < pList1.Count; i++)
        {
            if (pList1[i] != pList2[i])
            {
                return false;
            }
        }
        if (pList1.Count != 0 || pList2.Count != 0)
        {
            return true;
        }
        return false;
    }
}
