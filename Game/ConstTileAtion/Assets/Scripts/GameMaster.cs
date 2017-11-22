using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public GameObject HexPrefab;

    //Storage for the Hexbackground GameObjects
    public GameObject [] BackgroundHexs = new GameObject[4];

    //Gameobject which is the parent of all that rings hexes
    public GameObject RingHolder0, RingHolder1, RingHolder2, RingHolder3;
    public GameObject TileHolder;

    [Header("GameInfo")]
    [Range(0,3)]public int LayersBeingUsed;
    public int Moves, NumToWin;
    public bool Clicked = false;
    public int CurrentlySelectedType;
    public GameObject CurrentlySelected;
    public Text MoveCounter;
    public bool EditMode = true;
    public bool DisableHexes = false;
    public Slider LayerSlider;

        // Use this for initialization
    void Start()
    {
        //Call this on start to set the number of hex layers
        LayerSetter();

        //If editmode is true, set a listener on the slider to make it dynamically 
        //adjust the layers shown
        if (EditMode)
        {
            //Sets a listener and a function to execute
            LayerSlider.onValueChanged.AddListener(delegate { LayersBeingUsed = (int)LayerSlider.value; LayerSetter(); });
        }



    }

    //Enables the layers that the game is currently using
    public void LayerSetter()
    {
        //Goes through the array of background peices and 
        //enables them if they are bing used, and disables them if they are not.
        for (int i = 0; i < BackgroundHexs.Length; i++)
        {
            if (LayersBeingUsed >= i)
                BackgroundHexs[i].SetActive(true);
            else
                BackgroundHexs[i].SetActive(false);
        }

        //Goes through each of the hexes that are in the rings and enables/disables
        //them if they aren't in use
        foreach (Transform Hex in RingHolder3.transform)
        {
            if (LayersBeingUsed >= 3)
                Hex.gameObject.SetActive(true);
            else
                Hex.gameObject.SetActive(false);
        }
        foreach (Transform Hex in RingHolder2.transform)
        {
            if (LayersBeingUsed >= 2)
                Hex.gameObject.SetActive(true);
            else
                Hex.gameObject.SetActive(false);
        }
            foreach (Transform Hex in RingHolder1.transform)
        {
            if (LayersBeingUsed >= 1)
                Hex.gameObject.SetActive(true);
            else
                Hex.gameObject.SetActive(false);
        }
        foreach (Transform Hex in RingHolder0.transform)
        {
            if (LayersBeingUsed >= 0)
                Hex.gameObject.SetActive(true);
            else
                Hex.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update ()
    {

    }
    
    public void CheckWin()
    {
        int Checkedcount = 0;
        foreach (Transform Parent in this.transform)
        {
            foreach (Transform Child in Parent.transform)
            {
                if (Child.GetComponent<HexInfo>().Checked)
                {
                    Checkedcount++;
                }
            }
        }
        if (Checkedcount >= NumToWin)
        {
            //Do thing that says you are going to win
            Debug.Log("Winner!");
            ResetGame();
        } 
    }

    private void ResetGame()
    {
        NumToWin = 0;
        Moves = 0;
        foreach (Transform Parent in this.transform)
        {
            foreach (Transform Child in Parent.transform)
            {
                Child.GetComponent<HexInfo>().SetHexSprite();
            }
        }
    }

    public void UpdateMoveCounter()
    {
        MoveCounter.text = Moves.ToString();
    }
}
