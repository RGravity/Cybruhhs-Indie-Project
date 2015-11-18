using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class AIWalkingScript : MonoBehaviour {
    
    private NodeScript _startNode;
    private NodeScript _currentNode;
    private NodeScript _targetNode;
    private NodeScript _nextNode;
    private bool _pathFound = true;
    private List<NodeScript> _finalPath = null;
    private int _finalPathCurrentNodeIndex = 1;
    private NodeWorldScript _nodeWorld = new NodeWorldScript();
    private int _index = 0;
    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x == _startNode.Position.x && this.gameObject.transform.position.y == _startNode.Position.y)
        {
            _nextNode = _findNextNode(_finalPath, _index);
            _startNode = _nextNode;
            _index++;
        }

        //Vector3.MoveTowards(this.gameObject.transform.position, _nextNode.Position, 10);
        Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(10,10, -1),10);
    }
    private NodeScript _findNextNode(List<NodeScript> pPath, int pIndex)
    {
        return pPath[pIndex];
    }

        //if (_finalPathCurrentNodeIndex < _finalPath.Count)
        //{
        //    if (_targetNode != null)
        //    {
        //        _position = _targetNode.Position.Clone();
        //        _currentNode = _targetNode;
        //    }
        //    _targetNode = _finalPath[_finalPathCurrentNodeIndex];
        //    _finalPathCurrentNodeIndex++;

        //    _velocity = _targetNode.Position.Clone().Sub(_currentNode.Position).Normalize();
        //    rotation = _velocity.GetAngleDegrees();

        //    Console.WriteLine("-------" + " Going to Node '" + _targetNode.DEBUGLETTER + "' now.");
        //}
        //else
        //{
        //    _velocity = null;

        //    if (_finalPath.Count == 1)
        //        _startNode = _targetNode;
        //    else
        //        _startNode = _targetNode;
        //}

    public List<NodeScript> GetPath(NodeScript pStartNode, NodeScript pTargetNode)
    {
        _startNode = pStartNode;
        _targetNode = pTargetNode;

        List<NodeScript> todoList = new List<NodeScript>();
        List<NodeScript> doneList = new List<NodeScript>();
        List<NodeScript> shortestPathList = new List<NodeScript>();

        todoList.Add(_startNode);


        //int iterations = 0;
        do
        {
            // todoList.Sort();
            _currentNode = todoList[0];
            todoList.Remove(_currentNode);
            doneList.Add(_currentNode);
            if (_currentNode == _targetNode)
            {
                _pathFound = true;
                break;
            }
            else
            {
                for (int i = 0; i < _currentNode.GetConnctionCount; i++)
                {
                    todoList = checkAndAddTodoNode(_currentNode.GetConnectionAt(i), todoList, doneList);
                }
            }


        } while (todoList.Count != 0);

        if (!_pathFound)
        {
            resetNodesData(todoList);
            resetNodesData(doneList);

            return null;
        }
        else
        {
            shortestPathList = _targetNode.getParentNodes();
            shortestPathList.Reverse();

            resetNodesData(todoList);
            resetNodesData(doneList);

            _finalPath = shortestPathList;

            return shortestPathList;
        }
    }

    private void resetNodesData(List<NodeScript> nodesToResetList)
    {
        foreach (NodeScript n in nodesToResetList)
        {
            n.DistanceFromStart = 0;
            n.EstimatedDistanceToEnd = 0;
            n.resetParentNode();
        }
    }

    private List<NodeScript> checkAndAddTodoNode(NodeScript n, List<NodeScript> todoList, List<NodeScript> doneList)
    {
        if (doneList.Contains(n)) return todoList;

        if (todoList.Contains(n))
        {
            if (n.EstimatedDistanceToEnd <= _currentNode.DistanceFromStart + GetCostEstimate(_currentNode, n) + GetCostEstimate(n, _targetNode)) return todoList;
            todoList.Remove(n);
            n.resetParentNode();
        }

        if (n != _startNode) n.ParentNode = _currentNode;
        if (n.ParentNode != null) n.DistanceFromStart = n.ParentNode.DistanceFromStart + GetCostEstimate(n.ParentNode, n);
        else n.DistanceFromStart = GetCostEstimate(_startNode, n);

        n.EstimatedDistanceToEnd = n.DistanceFromStart + GetCostEstimate(n, _targetNode);
        todoList.Add(n);
        return todoList;
    }

    private float GetCostEstimate(NodeScript startNode, NodeScript endNode)
    {
        float distance = Vector3.Distance(endNode.Position, startNode.Position);
        return Math.Abs(distance);
    }
}       
