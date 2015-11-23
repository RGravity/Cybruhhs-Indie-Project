using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickableTileScript : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMapScript map;

	void OnMouseUp() {
		Debug.Log ("Click!");

		//if(EventSystem.current.IsPointerOverGameObject())
		//	return;

        //map.GeneratePathTo((int)map.EndPosition.x, (int)map.EndPosition.y);
        //

        //map.SetWalkable(tileX, tileY, false);
	}

}
