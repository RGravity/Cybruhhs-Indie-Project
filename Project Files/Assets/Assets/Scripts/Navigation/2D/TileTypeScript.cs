using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileTypeScript {
    [SerializeField]
	private string _name;
    [SerializeField]
    private GameObject _tileVisualPrefab;
    [SerializeField]
    private bool _isWalkable = true;

	private float _movementCost = 0;
    public float MovementCost { get { return _movementCost; } }
    public GameObject TileVisualPrefab { get { return _tileVisualPrefab; } }
    public string Name { get { return _name; } }
    public bool IsWalkable { get { return _isWalkable; } set { _isWalkable = false; } }
}
