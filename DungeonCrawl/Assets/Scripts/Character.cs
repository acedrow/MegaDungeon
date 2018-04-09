using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Monobehavior to be attached to character gameObjects
 * 
 */
public class Character : MonoBehaviour
{
	CharacterSheet characterSheet;
	GameObject currentTile;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public GameObject getTileGameObject ()
	{
		return currentTile;
	}

	public TileData getTileData ()
	{
		return currentTile.GetComponent <TileObject> ().getTileData ();
	}

	//returns the position of the character's currently occupied tile in the room grid
	public Vector2 getPositionInRoom ()
	{
		return currentTile.GetComponent <TileObject> ().getCoordinates ();
	}

	public void setTileGameObject (GameObject t)
	{
		TileObject testTile = t.GetComponent <TileObject> ();
		if (testTile == null) {
			Debug.Log ("Character.setTile no tileObject attached to GameObject");
		} else {
			currentTile = t;
		}
	}
}
