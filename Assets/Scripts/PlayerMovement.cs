using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance;
	public List<Vector2> pathTiles;
	public int speed;
	public bool paused;
	public string color;
	public Image blocColor;

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
        paused = false;
        color = "White";
    }

    // Update is called once per frame
    void Update()
    {
        //if colorbloc idle, try to get input
        if(blocIdle){
        	input = CheckForInput();
        }

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
        if(pathTiles.Count == 0 && blocIdle == false && !paused)
        	blocIdle = true;

        //if no more tiles on the path, give blocidle state


    }

    private void MoveTowards(Vector2 direction){
    	Vector3 nextTilePos = new Vector3(direction.x*100, direction.y*-100, 0);
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextTilePos, Time.deltaTime * speed); 
		if(transform.localPosition.Equals(nextTilePos)){
            ActOnPosition();
            pathTiles.RemoveAt(0);

        }
    }

    private void ActOnPosition(){

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
                if(finishedScanning)
                    break;
    			if(originPos.y != 0)
    				ScanTile(originPos + Vector2.down);
    			else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.down;
    			break;
    		case "Down":
    			ScanHorizontal(originPos + Vector2.up);
                if(finishedScanning)
                    break;
				if(originPos.y != boardSizeY-1)
					ScanTile(originPos + Vector2.up);
				else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.up;
    			break;
    		case "Left":
    			ScanVertical(originPos);
                if(finishedScanning)
                    break;
    			if(originPos.x != 0)
    				ScanTile(originPos + Vector2.left);
    			else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.left;
    			break;
    		case "Right":
    			ScanVertical(originPos + Vector2.right);
                if(finishedScanning)
                    break;
    			if(originPos.x != boardSizeX-1)
    				ScanTile(originPos + Vector2.right);
    			else
    				finishedScanning = true;
    			currentPathTile = originPos + Vector2.right;
    			break;
    	}
    }

    private void ScanTile(Vector2 position){
        switch(LevelBuilder.Instance.tiles[(int)position.x, (int)position.y].type){
            case "None":
                pathTiles.Add(position);
                break;
            case "Goal":
                //Something
                break;
        }
           
    	
    }

    //Scans the assigned horizontal tile and acts on it
    private void ScanHorizontal(Vector2 position){
        Vector2 convertedPosition = new Vector2(position.x, position.y - .5f);
        switch(LevelBuilder.Instance.horizontals[(int)position.x, (int)position.y].type){
            case "None":
                if(position.y>0 && position.y<boardSizeY)
                    pathTiles.Add(convertedPosition); 
                break;
            case "Wall":
                finishedScanning = true;
                break;
            case "Color":
            	if(LevelBuilder.Instance.horizontals[(int)position.x, (int)position.y].color.ToString() == color)
            		finishedScanning = true;
            	else{
            		color = LevelBuilder.Instance.horizontals[(int)position.x, (int)position.y].color.ToString();
					if(position.y>0 && position.y<boardSizeY)
                   		pathTiles.Add(convertedPosition);             		
            	}
            	break;
        }
    }

    //Scans the assigned vertical tile and acts on it
    private void ScanVertical(Vector2 position){
        Vector2 convertedPosition = new Vector2(position.x -.5f, position.y);
        switch(LevelBuilder.Instance.verticals[(int)position.x, (int)position.y].type){
            case "None":
                if(position.x>0 && position.x<boardSizeX)
                    pathTiles.Add(convertedPosition); 
                break;
            case "Wall":
                finishedScanning = true;
                break;
            case "Color":
            	if(LevelBuilder.Instance.verticals[(int)position.x, (int)position.y].color.ToString() == color)
            		finishedScanning = true;
            	else{
            		color = AddColor(LevelBuilder.Instance.verticals[(int)position.x, (int)position.y].color.ToString());
            		Debug.Log(color);
            		AssignColor(color);
            		if(color == "Purple" || color == "Green" || color == "Orange"){
            			ChangeColorWalls(color);
            		}
					if(position.x>0 && position.x<boardSizeX)
                   		pathTiles.Add(convertedPosition);             		
            	}
            	break;
        }
        
    }

    private void ChangeColorWalls(string newColor){
    	boardSizeX = (int)LevelBuilder.Instance.boardDimension.x;
    	boardSizeY = (int)LevelBuilder.Instance.boardDimension.y;

    	for(int i=0; i-1<boardSizeX; i++){
			for(int j = 0; j<boardSizeY; j++){
				if(LevelBuilder.Instance.horizontals[j,i].type == "Color"){
					if((LevelBuilder.Instance.horizontals[j,i].color == Horizontal.Color.Red || LevelBuilder.Instance.horizontals[j,i].color ==Horizontal.Color.Blue) && newColor == "Purple"){
						LevelBuilder.Instance.horizontals[j,i].color = Horizontal.Color.Purple;
					}
				}
			}
		}
    	for(int i=0; i<boardSizeX; i++){
			for(int j = 0; j-1<boardSizeY; j++){
				if(LevelBuilder.Instance.verticals[j,i].type == "Color"){
					if((LevelBuilder.Instance.verticals[j,i].color == Vertical.Color.Red || LevelBuilder.Instance.verticals[j,i].color == Vertical.Color.Blue) && newColor == "Purple"){
						LevelBuilder.Instance.verticals[j,i].color = Vertical.Color.Purple;
					}
				}
			}
		}
    }

    //returns string for keyhit with direction name, if none in that frame then return null
    private string CheckForInput(){
    	if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
    		return "Up";
    	if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
    		return "Left";
    	if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
    		return "Down";
    	if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
    		return "Right";
    	return null;
    }

    private void AssignColor(string newColor){
    	switch(newColor){
    		case "White":
    			blocColor.color = Color.white;
    			break;
    		case "Red":
    			blocColor.color = Color.red;
    			break;
    		case "Blue":
    			blocColor.color = Color.blue;
    			break;
    		case "Yellow":
    			blocColor.color = Color.yellow;
    			break;
    		case "Purple":
    			blocColor.color = new Color(1,0,1,1);
    			break;
    		case "Green":
    			blocColor.color = new Color(0,1,0,1);
    			break;
    		case "Orange":
    			blocColor.color = new Color(1,.64f,0,1);
    			break;
    	}
    }

    private string AddColor(string newColor){
    	switch(color){
    		case "White":
    			return newColor;
    			break;
    		case "Blue":
    			switch(newColor){
    				case "Red":
    					return "Purple";
    				case "Blue":
    					return "Blue";
    				case "Yellow":
    					return "Green";
    			}
    			break;
    		case "Red":
    			switch(newColor){
    				case "Red":
    					return "Red";
    				case "Blue":
    					return "Purple";
    				case "Yellow":
    					return "Orange";
    			}
    			break;
    		case "Yellow":
    			switch(newColor){
    				case "Red":
    					return "Orange";
    				case "Blue":
    					return "Green";
    				case "Yellow":
    					return "Yellow";
    			}
    			break;
    		case "Purple":
    				switch(newColor){
    					case "White":
    						return "White";
    					default:
    							return "Purple";
    				}
    				break;
    		case "Orange":
    				switch(newColor){
    					case "White":
    						return "White";
    					default:
    						return "Orange";
    				}
    				break;
    		case "Green":
    				switch(newColor){
    					case "White":
    						return "White";
    					default:
    						return "Green";
    				}
    				break;
    	}
    	return newColor;
    	//if(color == "White")
    	//	return newColor;
    	//if(color == "Red")

    }
    //Gives the tile value of player. 0,0 -> 6,6 etc.
    private Vector2 ConvertWorldPositionToTilePosition(){
    	Vector2 posToGive = new Vector2();
    	posToGive.x = Mathf.RoundToInt((float)(transform.localPosition.x/100f));
    	posToGive.y = -Mathf.RoundToInt((float)(transform.localPosition.y/100f));
//    	Debug.Log(posToGive);
    	return posToGive;
    }
}
