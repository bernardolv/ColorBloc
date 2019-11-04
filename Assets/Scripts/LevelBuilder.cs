﻿using System.Collections;
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

	//Store the color of the tile type
	public string color;

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

	public Tile(GameObject nMyObject, Vector2 nGridPos, Vector2 nWorldPosition, GameObject occupier, string nType, string nColor){
		myObject = nMyObject;
		gridPosition = nGridPos;
		worldPosition = nWorldPosition;
		type = nType;
		color = nColor;		
	}

}

public class LevelBuilder : MonoBehaviour
{	
	// For referencing
	public static LevelBuilder Instance;

	// These are to be used as proportions when building array grids (tiles, horizontal and vertical portals)
	public Vector2 boardDimension;

	// Will hold the Tile Classes which contain the object, its position, and it's type.
	// The grid position will consist of its 2darray position
	public Tile [,] tiles;

	// Tile prefab for instantiation
	public GameObject tileObject;

	// Contains the boardsize gameobject for calculating position
	public GameObject boardObject;

	// this will give the unit scale for moving 1 tile. It will depend on boardDimension and Canvas Reference Resolution
	public int unitScale;

	// tile gameobjects instantiated become child of this guy
	public GameObject tileCanvas;

	// Stores the tile gameobjects
	public List<GameObject> tileGameObjects = new List<GameObject>();

	// Stores the row gameobjects
	public List<GameObject> rowGameObjects = new List<GameObject>();

	// Stores the column gameobjects
	public List <GameObject> columnGameObjects = new List<GameObject>();



	void Start(){
		//boardDimension = new Vector2(4,4);
		int tileSize = (int)boardDimension.x * (int)boardDimension.y;
		Debug.Log(tileSize + " number of tiles");
		InitializeTileList(tileSize);
		//BuildBase(boardDimension);
		BuildMap("Test");

	}


	private void InitializeTileList(int size){
		for(int i=0; i<size; i++){
			GameObject newTile = (GameObject)Instantiate(tileObject,new Vector3(0,0,0), tileObject.transform.rotation);
			newTile.transform.parent = tileCanvas.transform;
			newTile.transform.localPosition = new Vector2(0,0);
			newTile.transform.localScale = new Vector3(1,1,1);
			tileGameObjects.Add(newTile);

		}
	}


	// To be called by Game Manager with a seed representing the map, 
	// this is the main function where everything will run in.
	public void BuildMap(string seed){
		//ResetValues();
		boardDimension = BoardSize(seed);
		ResizeAssets();
		BuildBase(boardDimension);
	}


	// Returns the size of the inputed seed as Vector2
	private Vector2 BoardSize(string seed){
		return new Vector2(boardDimension.x,boardDimension.y);
	}


	// Used to build the base of the grid and populating tiles array 
	private void BuildBase(Vector2 boardSize){
		tiles = new Tile[(int)boardSize.x, (int)boardSize.y];
		int tileNumber = 0;
		Debug.Log(tiles[0,0]);
		if(tiles[0,0] == null){
			Debug.Log("C");
		}
		for(int i=0; i<boardSize.x; i++){
			for(int j=0; j<boardSize.y; j++){
				CreateTile(new Vector2(j,i), tileNumber);
				tileNumber++;
			}
		}
	}

	private void ResizeAssets(){

	}

	// Created Tile Class at position and adds it to the tiles 2d Array and world;
	private void CreateTile(Vector2 location, int tileNumber){
		tiles[(int)location.x, (int)location.y] = new Tile(tileGameObjects[tileNumber], location, ScreenPosition(location));
		tileGameObjects[tileNumber].transform.localPosition = ScreenPosition(location);
	}


	// Receives Unit position and returns the on-scren value, which multiplies the unit with the size of the grid in pixels
	private Vector2 ScreenPosition(Vector2 location){
		return new Vector2(location.x*unitScale,-location.y*unitScale);
	}
}
