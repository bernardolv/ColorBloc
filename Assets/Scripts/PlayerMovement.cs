﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance;
	public List<Vector2> pathTiles;
	public int speed;
	#region ScannerVariables
	Vector2 currentPathTile;
	string currentScanDirection;
	bool finishedScanning;
	int boardSizeX;
	int boardSizeY;
	#endregion

	//Shows status of wether new input is read or not. is true with idle colorbloc, is false when colorbloc moving
	public bool blocIdle;

	string input;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        blocIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if colorbloc idle, try to get input
        if(blocIdle){
        	input = CheckForInput();
        }

        //Debug.Log(input);

        //if input was gotten, scan the tiles to build the pathTiles list.
        if(input!= null){
        	blocIdle = false;
         	ScanPath(input);
         	input = null;
        }

        //Move if there are pending tiles in the path
        if(pathTiles.Count != 0){
        	MoveTowards(pathTiles[0]);
        }
        //if no more pathtiles, give blocidle status
        if(pathTiles.Count == 0 && blocIdle == false)
        	blocIdle = true;

        //if no more tiles on the path, give blocidle state


    }

    private void MoveTowards(Vector2 direction){
    	Debug.Log("moving");
    		Vector3 nextTilePos = new Vector3(direction.x*100, direction.y*-100, 0);
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextTilePos, Time.deltaTime * speed); 
		if(transform.localPosition.Equals(nextTilePos))
			pathTiles.RemoveAt(0);
    }

    //Runs through tiles data and populate the path that the player will then take.
    private void ScanPath(string input){

    	//Get size of board to use as limits while searching
    	boardSizeX = (int)LevelBuilder.Instance.boardDimension.x;
    	boardSizeY = (int)LevelBuilder.Instance.boardDimension.y;

    	//reinitialize the pathtiles list to populate with the path tiles.
    	pathTiles = new List<Vector2>();
    	
    	currentPathTile = ConvertWorldPositionToTilePosition();

    	//initialize direction with input. this string can change if it runs into a direction changing tile
    	string currentScanDirection = input;

    	finishedScanning = false;

    	while(!finishedScanning){
    		ScanNextAndPopulate(currentPathTile, currentScanDirection);
    		// currentPathTile = AddUnitOnDirection(currentPathTile, currentScanDirection);
    	}
    }

    //Scan the next "tile set" of tiles and vert/horizontal depending   
    private void ScanNextAndPopulate(Vector2 originPos, string direction){
    	switch(direction){
    		case "Up":
    			ScanHorizontal(originPos);
    			//scan upwards if => 0
    			if(originPos.y != 0)
    				ScanTile(originPos + Vector2.down);
    			else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.down;
    			break;
    		case "Down":
    			ScanHorizontal(originPos + Vector2.up);
				if(originPos.y != boardSizeY-1)
					ScanTile(originPos + Vector2.up);
				else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.up;
    			break;
    		case "Left":
    			ScanVertical(originPos);
    			if(originPos.x != 0)
    				ScanTile(originPos + Vector2.left);
    			else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.left;
    			break;
    		case "Right":
    			ScanVertical(originPos + Vector2.right);
    			if(originPos.x != boardSizeX-1)
    				ScanTile(originPos + Vector2.right);
    			else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.right;
    			break;
    	}
    }

    private void ScanTile(Vector2 position){
    	Debug.Log(position);
    	Debug.Log("Tile type is +"  + LevelBuilder.Instance.tiles[(int)position.x, (int)position.y].type);
    	
    	pathTiles.Add(position);
    }

    private void ScanHorizontal(Vector2 position){

    }

    private void ScanVertical(Vector2 position){

    }
    //returns string for keyhit with direction name, if none in that frame then return null
    private string CheckForInput(){
    	if (Input.GetKeyDown (KeyCode.W))
    		return "Up";
    	if (Input.GetKeyDown (KeyCode.A))
    		return "Left";
    	if (Input.GetKeyDown (KeyCode.S))
    		return "Down";
    	if (Input.GetKeyDown (KeyCode.D))
    		return "Right";
    	return null;
    }

    //Gives the tile value of player. 0,0 -> 6,6 etc.
    private Vector2 ConvertWorldPositionToTilePosition(){
    	Vector2 posToGive = new Vector2();
    	posToGive.x = Mathf.RoundToInt((float)(transform.localPosition.x/100f));
    	posToGive.y = -Mathf.RoundToInt((float)(transform.localPosition.y/100f));
    	Debug.Log(posToGive);
    	return posToGive;
    }
}
