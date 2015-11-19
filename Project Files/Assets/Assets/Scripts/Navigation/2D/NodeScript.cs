using UnityEngine;
using System.Collections.Generic;

public class NodeScript {
	private List<NodeScript> _neighbours;
    private int _x;
    private int _y;

    public List<NodeScript> Neighbours { get { return _neighbours; } }
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }

    public NodeScript() {
		_neighbours = new List<NodeScript>();
	}
	
	public float DistanceTo(NodeScript n) {
		if(n == null) {
			Debug.LogError("WTF?");
		}
		
		return Vector2.Distance(
			new Vector2(_x, _y),
			new Vector2(n._x, n._y)
			);
	}
	
}
