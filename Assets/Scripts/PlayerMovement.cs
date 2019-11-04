using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance;
	public static Vector2 playerTile;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ConvertWorldPositionToTilePosition();
        //Debug.Log(playerTile);
    }

    private void ConvertWorldPositionToTilePosition(){
    	playerTile.x = Mathf.RoundToInt((float)(transform.localPosition.x/100f));
    	playerTile.y = Mathf.RoundToInt((float)(transform.localPosition.y/100f));
    }

    private void ConvertTilePositionToWorldPosition(){
    	transform.position = new Vector3((float)(playerTile.x)*100f, (float)(playerTile.y)*100f);
    }
    public void SetInPosition(){
    	
    }

}
