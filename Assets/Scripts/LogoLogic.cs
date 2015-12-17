using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class LogoLogic : MonoBehaviour {

    private float _currentTime, _maxTime;
    public LoadingScreen loadingScreen;


	// Use this for initialization
	void Start () {

        //Chartboost.cacheInterstitial(CBLocation.Default);

        _maxTime = 2.0f;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _maxTime)
        {
            LoadMenu();
        }
	}

    void LoadMenu()
    {
        loadingScreen.loadMenu = true;
        
        //Chartboost.showInterstitial(CBLocation.Default);
    }
}
