using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceHandler : MonoBehaviour
{
	//Singleton
	public static PieceHandler Instance;

	//holds the placed verical gameobjects
	public GameObject verticalHolder;

	//used for quick access to all placedverticals
	public List<GameObject> placedVerticals = new List<GameObject>();

	//holds the placed horizontal gameobjects
	public GameObject horizontalHolder;

	//used for quick access to all placedhorizontals
	public List<GameObject> placedHorizontals = new List<GameObject>();

	//holds the placed tile gameobjects
	public GameObject tileHolder;

	//used for quick access to all placedtiles
	public List<GameObject> placedTiles = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //call this to populate placedverticals and placedhorizontals
    public void PopulatePieces(){
    	PopulateVerticals();
    	PopulateHorizontals();
    }


    //populate a group only with placed verticals as opposed to the grid
    public void PopulateVerticals(){
		foreach(Transform child in verticalHolder.transform)
		{
		    //Something(child.gameObject);
		    placedVerticals.Add(child.gameObject);
		}    	
	}

	//populate a group only with placed horizontals as opposed to the grid
    public void PopulateHorizontals(){
		foreach(Transform child in horizontalHolder.transform)
		{
		    //Something(child.gameObject);
		    placedHorizontals.Add(child.gameObject);
		}    	
	}

	//populate a group only with placed tiles as opposed to the grid
    public void PopulateTiles(){
		foreach(Transform child in horizontalHolder.transform)
		{
		    //Something(child.gameObject);
		    placedHorizontals.Add(child.gameObject);
		}    	
	}

	//This paints the base colors into the gotten secondaryColor
	public void ColorWalls(string newColor){
		for(int i = 0; i<placedVerticals.Count;i++){
			string curColor = placedVerticals[i].transform.GetComponent<Vertical>().color.ToString();
			switch(newColor){
				case "Purple":
					if(curColor == "Red" || curColor == "Blue")
					placedVerticals[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1,0,1,1);
					break;
				case "Orange":
					if(curColor == "Red" || curColor == "Yellow")
					placedVerticals[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1,.64f,0,1);
					break;
				case "Green":
					if(curColor == "Yellow" || curColor == "Blue")
					placedVerticals[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0,1,0,1);
					break;
				default:
					break;
			}
		}
		for(int i = 0; i<placedHorizontals.Count;i++){
			string curColor = placedHorizontals[i].transform.GetComponent<Horizontal>().color.ToString();
			switch(newColor){
				case "Purple":
					if(curColor == "Red" || curColor == "Blue")
					placedHorizontals[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1,0,1,1);
					break;
				case "Orange":
					if(curColor == "Red" || curColor == "Yellow")
					placedHorizontals[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1,.64f,0,1);
					break;
				case "Green":
					if(curColor == "Yellow" || curColor == "Blue")
					placedHorizontals[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0,1,0,1);
					break;
				default:
					break;
			}
		}
	}
}
