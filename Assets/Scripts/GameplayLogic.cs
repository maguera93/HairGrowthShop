using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayLogic : MonoBehaviour {

    public enum GameState { MENU, START, ENTER, DESTINATION, DROP, WAITING, ENDING }
    public bool bald;
    public GameState gameState;
    public float baldProvavility, currentTime;
    public GameObject gameOverCanvas, gameplayCanvas, menuCanvas;
    public BaldLogic baldLogic;
    public Text lifesText, scoreText, scoreGameOverText, highScoreText;
    public AudioManager audioManager;
    public Slider timeBar;
    public CoinsAnimation scoreAnimation, failAnimation;
    public ParticleSystem dropParticles, hairParticles, smokeParticles;
    public float maxReacTime, curReacTime, speedDelay;
    public Animator cosaAnimator;
    public FailBackground failBackground;
    public AudioSource audioBackground;
    public AudioClip gameplayAudio, gameoverAudio;
    

    private int _lifes;
    private int _score, _screenShot;
    private int _upgrateLevel;

	// Use this for initialization
	void Start () {
        gameState = GameState.MENU;
        _lifes = 3;
        maxReacTime = 1.8f;
        speedDelay = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        lifesText.text = "lifes: " + _lifes.ToString();
        scoreText.text = _score.ToString() + "$";
        if (_lifes <= 0) _lifes = 0;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.CaptureScreenshot("ScreenShoot" + _screenShot.ToString("000") + ".png");
            _screenShot++;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        switch (gameState)
        {
            case GameState.MENU:

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    
                }
                    

                break;
            case GameState.START:                    
                baldProvavility = Random.Range(0.0F, 1.0F);

                if (baldProvavility > 0.5f)
                {
                    bald = true;
                    baldLogic.ActivateBald();
                }
                else
                {
                    bald = false;
                    baldLogic.ActivateBald();
                    baldLogic.ActivateHairStyle();
                }

                gameState = GameState.ENTER;
                break;
            case GameState.ENTER:

                currentTime += Time.deltaTime;
                if (currentTime >= baldLogic.baldAnimation.length*speedDelay)
                {
                    currentTime = 0;
                    curReacTime = maxReacTime;
                    gameState = GameState.DESTINATION;
                }

                break;
            case GameState.DESTINATION:
                curReacTime -= Time.deltaTime;
                timeBar.value = curReacTime / maxReacTime;
                if (timeBar.value <= 0.0f)
                {
                    if (failAnimation.animActive) failAnimation.ResetAnim();
                    else failAnimation.animActive = true;
                    if (failBackground.animActive) failBackground.ResetAnim();
                    else failBackground.animActive = true;
                    _lifes--;
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    audioManager.Play(audioManager.squicy, audiSor, 1.0f);
                    ExitButton();               
                }

                break;
            case GameState.DROP:

                currentTime += Time.deltaTime;

                if (currentTime >= (baldLogic.dropAnimation.length))
                {
                    currentTime = 0;
                    baldLogic.ShowDrop();
                    hairParticles.Play();
                    smokeParticles.Play();
                    gameState = GameState.WAITING;

                }
                break;

            case GameState.WAITING:

                currentTime += Time.deltaTime;
                
                if (currentTime >= baldLogic.desactivateAnimation.length)
                {
                    baldLogic.DescativateMan();
                    currentTime = 0;
                    _upgrateLevel++;
                    if (maxReacTime >= 0.5f && _upgrateLevel >= 4)
                    {
                        maxReacTime *= 0.95f;
                        _upgrateLevel = 0;

                        if (baldLogic.speed < 1.5f)
                        {
                            baldLogic.speed += 0.1f;
                            speedDelay -= 0.1f;
                        }
                    }

                    if (_lifes <= 0)
                    {
                        audioManager.PlayGameOver(gameoverAudio, audioBackground, 1.0f);
                        gameState = GameState.ENDING;
                    }            
                    else gameState = GameState.START;
                    
                }
                break;
            case GameState.ENDING:
                gameOverCanvas.SetActive(true);
                scoreGameOverText.text = "Score: " + _score.ToString() + "$";
                highScoreText.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString() + "$";
                if (_score > PlayerPrefs.GetFloat("HighScore"))PlayerPrefs.SetFloat("HighScore", _score);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Reset();
                    audioManager.PlayLoop(gameplayAudio, audioBackground, 1.0f);
                    gameOverCanvas.SetActive(false);
                }
                break;
        }
        
    }

    public void ExitButton()
    {
        if (gameState == GameState.DESTINATION && !bald)
        {
            baldLogic.dropGlue = false;
            baldLogic.DesactivateBald();
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.fail[Random.Range(0, audioManager.fail.Length)], audiSor, 1.0f);
            gameState = GameState.WAITING;
        }
        else if (gameState == GameState.DESTINATION && bald)
        {
            baldLogic.dropGlue = false;
            _lifes--;
            baldLogic.DesactivateBald();
            if (failAnimation.animActive) failAnimation.ResetAnim();
            else failAnimation.animActive = true;
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.fail[Random.Range(0, audioManager.fail.Length)], audiSor, 1.0f);
            if (failBackground.animActive) failBackground.ResetAnim();
            else failBackground.animActive = true;
            gameState = GameState.WAITING;
        }

        audioManager.PlayButton();
    }

    public void PillsButton()
    {

        if (!bald && gameState == GameState.DESTINATION)
        {
            _lifes--;
            baldLogic.bald = false;
            gameState = GameState.DROP;
            baldLogic.dropGlue = true;
            baldLogic.DesactivateBald();
            dropParticles.Play();
            if (failAnimation.animActive) failAnimation.ResetAnim();
            else failAnimation.animActive = true;
            if (failBackground.animActive) failBackground.ResetAnim();
            else failBackground.animActive = true;

            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.squicy, audiSor, 1.0f);
        }
        else if (bald && gameState == GameState.DESTINATION)
        {
            if (scoreAnimation.animActive) scoreAnimation.ResetAnim();
            else scoreAnimation.animActive = true;
            _score += 100;
            
            baldLogic.bald = true;
            gameState = GameState.DROP;
            baldLogic.dropGlue = true;
            baldLogic.DesactivateBald();
            dropParticles.Play();

            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.squicy, audiSor, 1.0f);
        }

        audioManager.PlayButton();


    }

    void Reset()
    {
        gameState = GameState.START;
        _lifes = 3;
        maxReacTime = 1.8f;
        speedDelay = 1.0f;
        _score = 0;
        baldLogic.speed = 1;
        currentTime = 0;
    }

    public void PlayButton()
    {
        gameplayCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        cosaAnimator.enabled = true;
        cosaAnimator.Play("cosaAnimation");
        audioManager.PlayLoop(gameplayAudio, audioBackground, 1.0f);
        gameState = GameState.START;
    }

}
