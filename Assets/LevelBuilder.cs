using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile{

	// Corresponging GameObject with the sprite
	public GameObject myObject;

	// Used for locating it in Array
	public Vector2 gridPosition;

	// Used for locating it in Screen
	public Vector2 worldPosition;

	// Used to take any special actions if there's any mechanic placed on that tile
	public string type;



	// Initializer with empty tile
	public Tile(GameObject nMyObject, Vector2 nGridPos, Vector2 nWorldPosition){
		myObject = nMyObject;
		gridPosition = nGridPos;
		worldPosition = nWorldPosition;
		type = "None";
	}


	// Initializer with tile mechanic or "type"
	public Tile(GameObject nMyObject, Vector2 nGridPos, Vector2 nWorldPosition, GameObject occupier, string nType){
		myObject = nMyObject;
		gridPosition = nGridPos;
		worldPosition = nWorldPosition;
		type = nType;
	}

}

public class LevelBuilder : MonoBehaviour
{	
	// For referencing
	public static LevelBuilder Instance;

	// These are to be used as proportions when building base
	public Vector2 boardDimension;

	// Will hold the Tile Classes which contain the object, its position, and it's type.
	// The grid position will consist of its 2darray position
	public Tile [,] tiles;

	// Object prefab for instantiation
	public GameObject tileObject;

	// Contains the parent gameobject where the tile gameobjects will be placed in
	public GameObject boardObject;


	// To be called by Game Manager with a seed representing the map, 
	// this is the main function where everything will run in.
	public void BuildMap(string seed){
		boardDimension = BoardSize(seed);
		BuildBase(boardDimension);
	}


	// Returns the size of the inputed seed as Vector2
	private Vector2 BoardSize(string seed){
		return new Vector2(boardDimension.x,boardDimension.y);
	}


	// Used to build the base of the grid and populating tiles array 
	private void BuildBase(Vector2 boardSize){
		tiles = new Tile[(int)boardSize.x, (int)boardSize.y];
	}


	// Created Tile Class at position and adds it to the tiles 2d Array
	private void CreateTile(Vector2 location){

	}


}
