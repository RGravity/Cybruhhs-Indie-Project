using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
    private int _radius;
    private Vector3 _position;
    private List<Node> _connectionList;
    //private List<LineSegment> _connectionLines = new List<LineSegment>();
    private string _DEBUGletter;
    private int _woodCount;

    private Vector3 _pointAlongLine;
    private GameObject _node;

    private Node _parentNode;
    private float _distanceToEnd = 0;
    private float _estimatedDistanceToEnd = 0;
    private float _distance = 0;
    public Node(Vector3 position)
    {
        _connectionList = new List<Node>();
        
        _position = position;

        _node = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _node.transform.position = new Vector3(position.x, position.y, -1);
        _node.transform.localScale = new Vector3(_node.transform.localScale.x / 2, _node.transform.localScale.y / 2, _node.transform.localScale.z / 2);
        //_ball = new Ball(radius, _DEBUGletter, position, Vec2.zero, color);
        //AddChild(_ball);
    }
    // Use this for initialization
    public void addConnection(Node node2)
    {
        
    }
    void Update()
    {
    }

}
