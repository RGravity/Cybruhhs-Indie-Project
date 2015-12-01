using UnityEngine;
using System.Collections;

public class BuildPlacementTilesScript : MonoBehaviour {

    [SerializeField]
    private TowerNoneNumbers _towerNumber;

    public TowerNoneNumbers TowerPlaceNr { get { return _towerNumber; }}
}
