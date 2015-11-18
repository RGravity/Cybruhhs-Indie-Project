using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeWorldScript {

    private List<NodeScript> _nodeList = new List<NodeScript>();
    public NodeScript GetNodeAt(int i) { return _nodeList[i]; }

    public List<NodeScript> NodeList { get { return _nodeList; } }
    public void addNode(NodeScript pNode)
    {
        _nodeList.Add(pNode);
    }
    public void addConnection(NodeScript PNode1, NodeScript pNode2)
    {
        if (_nodeList.Count > 1)
        {
            PNode1.addConnection(pNode2);
            pNode2.addConnection(PNode1);
        }
    }
    public NodeScript GetNodeAtPosition(Vector3 pPosition)
    {
        for (int i = 0; i < _nodeList.Count; i++)
        {
            if (_nodeList[i].Position == pPosition)
            {
                return _nodeList[i];
            }
        }
        return null;
    }

}
