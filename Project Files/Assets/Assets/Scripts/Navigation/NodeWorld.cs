using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeWorld   {

    private List<Node> _nodeList = new List<Node>();
    public List<Node> NodeList { get { return _nodeList; } }
    public void addNode(Node node)
    {
        _nodeList.Add(node);
    }
    public void addConnection(Node node1, Node node2)
    {
        if (_nodeList.Count > 1)
        {
            node1.addConnection(node2);
            //node2.addConnection(node1);
        }
    }
}
