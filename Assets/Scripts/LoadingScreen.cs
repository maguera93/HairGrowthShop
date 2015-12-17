using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    public enum State { FADEIN, LOADING, FADEOUT }
    public State state;
    public float temp;
    public bool loadMenu;
    public bool loadLevel1, loadLevel2;
    public int showAd;
    public bool isShownAd;
    private int myLevel;
    //private DataLogic dataLogic;
    public float tempInit = 1f;
    public Color color;
    LoadingScreen instance;
    public bool soundON;

    void Awake()
    {
        //destroy the already existing instance, if any
        if (instance)
        {
            Destroy(instance.gameObject);

            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        state = State.FADEIN;        
        temp = tempInit;
        color = GetComponent<Renderer>().material.color;
        /*dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();*/

        DontDestroyOnLoad(transform.gameObject);

	}
	
	// Update is called once per frame
	void Update () {

        if (loadMenu)
        {
            myLevel = 1;
            loadingNexttLevel();
        }
        if (loadLevel1)
        {
            myLevel = 2;
            loadingNexttLevel();
        }
        if (loadLevel2)
        {
            myLevel = 3;
            loadingNexttLevel();
        }

	}

    public void SoundButton()
    {
        if (soundON)
        {
            AudioListener.pause = true;
            soundON = false;
        }
        else
        {
            AudioListener.pause = false;
            soundON = true;
        }
    }


    void loadingNexttLevel()
    {
        switch (state){
        
            case State.FADEOUT:
            color.a = Mathf.Lerp(1, 0, temp / tempInit);
            GetComponent<Renderer>().material.color = color;
            temp -= Time.deltaTime;
			if (temp <= 0) {
                state = State.LOADING;
				temp = 0;
                Debug.Log("your level is loading");
			}
             break;

            case State.LOADING:
            
            Application.LoadLevel(myLevel);
            

             break;
            case State.FADEIN:

            
            color.a = Mathf.Lerp(0, 1, temp / tempInit);
            GetComponent<Renderer>().material.color = color;
            temp -= Time.deltaTime;  
            if (temp < 0)
            {

                state = State.FADEOUT; 
                temp = tempInit;
                loadMenu = false;
                loadLevel1 = false;
                loadLevel2 = false;
                
                //myBool = false;
                //return myBool;
                //Destroy(this.gameObject);
            }
            break;
        }
    }


    void OnLevelWasLoaded(int level)
    {

        if ((level == myLevel) && state == State.LOADING)
        {
            temp = tempInit;
            state = State.FADEIN;
            Debug.Log("your level was loaded!!!");
        }
    }

}
