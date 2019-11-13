using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceHandler : MonoBehaviour
{
	public static PieceHandler Instance;

	public GameObject verticalHolder;

	public List<GameObject> placedVerticals = new List<GameObject>();

	public GameObject horizontalHolder;

	public List<GameObject> placedHorizontals = new List<GameObject>();

	//public Tile[] placedTiles;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulatePieces(){
    	PopulateVerticals();
    	PopulateHorizontals();
    }

    public void PopulateVerticals(){
		foreach(Transform child in verticalHolder.transform)
		{
		    //Something(child.gameObject);
		    placedVerticals.Add(child.gameObject);
		}    	
	}

    public void PopulateHorizontals(){
		foreach(Transform child in horizontalHolder.transform)
		{
		    //Something(child.gameObject);
		    placedHorizontals.Add(child.gameObject);
		}    	
	}

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
