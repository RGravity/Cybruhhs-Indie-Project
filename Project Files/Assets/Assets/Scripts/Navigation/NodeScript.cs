using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NodeScript {
    private int _radius;
    private Vector3 _position;
    private List<NodeScript> _connectionList = new List<NodeScript>();
    private List<GameObject> _connectionLines = new List<GameObject>();
    private string _DEBUGletter;
    private int _woodCount;

    private Vector3 _pointAlongLine;
    private GameObject _node;

    private NodeScript _parentNode;
    private float _distanceToEnd = 0;
    private float _estimatedDistanceToEnd = 0;
    private float _distance = 0;

    public int GetConnctionCount { get { return _connectionList.Count; } }
    public List<NodeScript> ConnectionList { get { return _connectionList; } }
    public Vector3 Position { get { return _position; } }
    public float DistanceFromStart { get { return _distanceToEnd; } set { _distanceToEnd = value; } }
    public float EstimatedDistanceToEnd { get { return _estimatedDistanceToEnd; } set { _estimatedDistanceToEnd = value; } }

    public NodeScript ParentNode { get { return _parentNode; } set { _parentNode = value; } }

    public NodeScript GetConnectionAt(int i) { return _connectionList[i]; }

    public NodeScript(Vector3 position)
    {
        _connectionList = new List<NodeScript>();
        
        _position = position;

        _node = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _node.name = "Node";
        //_node.AddComponent<NodeScript>();
        _node.transform.position = new Vector3(position.x, position.y, -1);
        _node.transform.localScale = new Vector3(_node.transform.localScale.x / 2, _node.transform.localScale.y / 2, _node.transform.localScale.z / 2);
        //_ball = new Ball(radius, _DEBUGletter, position, Vec2.zero, color);
        //AddChild(_ball);
    }
    // Use this for initialization
    public void addConnection(NodeScript node2)
    {
        //LineSegment connection = new LineSegment(this.Position, node2.Position, 0xff666666, 4);
        //GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        if (this._position.x < node2._position.x)
        {
            //line.transform.position = new Vector3((node2._position.x - this._position.x / 2), this._position.y, 1);
        }
        if (this._position.x > node2._position.x)
        {
            //line.transform.position = new Vector3((this._position.x + node2._position.x / 2),this._position.y,1);
        }
        if (this._position.y < node2._position.y)
        {
            //line.transform.position = new Vector3(this._position.x, (this._position.y + node2._position.y / 2), 1);
        }
        if (this._position.y > node2._position.y)
        {
            //line.transform.position = new Vector3(this._position.x, (node2._position.y - this._position.y / 2), 1);
        }
        //_connectionLines.Add(line);
        _connectionList.Add(node2);
       // parent.AddChild(connection);
    }
    void Update()
    {
    }
    public List<NodeScript> getParentNodes(int iteration = 0)
    {
        iteration++;
        List<NodeScript> ParentsList = new List<NodeScript>();
        ParentsList.Add(this);
        if (_parentNode != null)
        {
            ParentsList.AddRange(_parentNode.getParentNodes(iteration));
        }
        return ParentsList;
    }
    public void resetParentNode()
    {
        _parentNode = null;
    }
    //public int CompareTo(NodeScript other)
    //{
    //    return _estimatedDistanceToEnd.CompareTo(other._estimatedDistanceToEnd);
    //}

    //public int Compare(NodeScript x, NodeScript y)
    //{
    //    return x._estimatedDistanceToEnd.CompareTo(y._estimatedDistanceToEnd);
    //}
}
