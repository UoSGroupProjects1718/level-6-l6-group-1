using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public GameObject HexPrefab;

    //Storage for the Hexbackground GameObjects
    public GameObject [] BackgroundHexs = new GameObject[4];

    //Gameobject which is the parent of all that rings hexes
    public GameObject RingHolder0, RingHolder1, RingHolder2, RingHolder3;
    public GameObject TileHolder;

    [Header("GameInfo")]
    [Range(0,3)]public int LayersBeingUsed;
    public int MovesLeft, NumToWin;
    public bool Clicked = false;
    public int CurrentlySelectedType;
    public GameObject CurrentlySelected;
    public Text MoveCounter;
    public bool EditMode = true;
    public bool DisableHexes = false;
    public Slider LayerSlider;
    GameObject Persistant;

        // Use this for initialization
    void Start()
    {

        //Find the gameobject that indicates this level was started from the title screen
        Persistant = GameObject.Find("PersistantObject");
        if (Persistant != null)
        {
            EditMode = false;
            //Call the ResetLevel function
            //LoadLevel();
        }
        
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
            WinGame();
        } 
    }

    private void WinGame()
    {
        DisableHexes = true;
        this.GetComponent<UIScripts>().WinMenu.SetActive(true);

    }
    private void LoseGame()
    {
        UIScripts UI = this.GetComponent<UIScripts>();
        UI.LoseMenu.SetActive(true);
    }

    public void UpdateMoveCounter(int MovesReduced)
    {
        MovesLeft -= MovesReduced;
        //If the player has no moves left
        if (MovesLeft < 0)
        {
            LoseGame();
        }
        else
        {
            MoveCounter.text = MovesLeft.ToString();
        }
    }

    public void LoadLevel()
    {
        PersistantInfo LevelInfo = Persistant.GetComponent<PersistantInfo>();
        //Call the Load function with the level information stored in the persistant object
        if (!this.gameObject.GetComponent<SaveAndLoad>().LoadLevel
            (LevelInfo.LevelType, LevelInfo.LevelDifficulty))
        {
            //If the LoadLevel function returns false, increment the level difficulty we are looking for
            LevelInfo.LevelDifficulty++;
            //If the level is now more difficult than we have levels for, move on to the next star sign
            if (LevelInfo.LevelDifficulty > 3)
            {
                //Incrememnt the level type
                LevelInfo.LevelType++;
                //Reset the level difficulty since we are on a new leveltype
                LevelInfo.LevelDifficulty = 0;
            }
            //Reset the level to load the new stuff
            ResetLevel();
        }
        
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("Base Level");
    }
}
