using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_ADS
using UnityEngine.Advertisements; // only compile Ads code on supported platforms
#endif

public class GameInstance : MonoBehaviour
{
    public static GameInstance _instance = null;

    [SerializeField]
    private AudioClip[] backgroundTracks;

    private string currentAddedLevel = "Level01";
    private int currentTrack = 0;

    private AudioSource audioSource;

    [SerializeField]
    private Image fadeTexture;

    [SerializeField]
    private float fadeDuration = 3.0f;
    private float currentTimer = 0.0f;

    [SerializeField]
    private bool bTransitioning = false;
    private bool bFadeIn = false;
    private bool bFadeOut = false;
    private bool bTransitionDone = false;

    public Canvas UI;
    public GameObject eventSystem;
    public HUDController hudScript;
    public Text songTitleDisplay;
    public GameObject gameCamera;

    private bool bDebugDisplay = false;

    private GameObject myCamera;

    private PlayerStats stats;
    public PlayerStats STATS { get { return stats; } }

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        else if (_instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(eventSystem);
    }


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = backgroundTracks[GetRandomTrack()];
        audioSource.loop = true;
        audioSource.Play();

        hudScript = UI.GetComponent<HUDController>();
        GameObject go = GameObject.Find("SongTitle");
        songTitleDisplay = go.GetComponent<Text>();

        stats = new PlayerStats();

        GameObject cam = Camera.main.gameObject;
        cam.SetActive(false);

        myCamera = Instantiate(Resources.Load("Prefabs/GameCamera") as GameObject);
        DontDestroyOnLoad(myCamera);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ADS
        ShowDefaultAd();
        ShowRewardedAd();
#endif

        if ((Input.GetKeyDown(KeyCode.O) || Input.GetButton("START")) && !bTransitioning)
        {
            if (SceneManager.GetActiveScene().name != currentAddedLevel)
                LevelTransitionWithFade(currentAddedLevel);

        }

        if (Input.GetButtonDown("L3"))
        {
            audioSource.clip = backgroundTracks[GetNextTrack()];
            audioSource.Play();
        }


        if (bTransitioning)
        {
            if (bFadeOut == true)
            {
                currentTimer += Time.deltaTime;
                fadeOut();
            }
            if (bFadeIn == true)
            {
                currentTimer -= Time.deltaTime;
                fadeIn();
            }

            Debug.Log(currentTimer);
            fadeTexture.color = new Color(fadeTexture.color.r, fadeTexture.color.g, fadeTexture.color.b, Mathf.Clamp01(currentTimer / fadeDuration));
        }

        songTitleDisplay.text = "(" + currentTrack + "-" + audioSource.clip.name + ")";
    }

    public void RestartLevel()
    {
        LevelTransitionWithFade(currentAddedLevel);
    }

    public void DisplayBanner(string displayMessage)
    {
        hudScript.ShowBanner(displayMessage);
    }

    void OnGUI()
    {
        if (bDebugDisplay)
        {
            string displayString = "(" + currentTrack + "-" + audioSource.clip.name + ")";
            GUI.BeginGroup(new Rect(Screen.width * 0.7f, Screen.height * 0.9f, 500.0f, 32.0f));
            GUI.Label(new Rect(0.0f, 0.0f, 500.0f, 32.0f), displayString);
            GUI.EndGroup();
        }
    }

    public void LevelTransitionWithFade(string levelToLoad)
    {
        currentAddedLevel = levelToLoad;
        currentTimer = 0.0f;
        bTransitioning = true;
        bFadeOut = true;
        bTransitionDone = false;

        UI.GetComponent<HUDController>().DisableHUD();

        audioSource.Stop();
        audioSource.clip = backgroundTracks[GetNextTrack()];
        audioSource.Play();

        GameObject cam = GameObject.Find("Camera");
        if (cam)
            cam.SetActive(false);
    }
    void fadeOut()
    {
        if (fadeTexture.color.a >= 1.0f)
        {
            Resources.Load(currentAddedLevel);
            SceneManager.LoadScene(currentAddedLevel);
            bFadeIn = true;
            bFadeOut = false;
            hudScript.EnableHUD();
            hudScript.HideBanner();
            audioSource.clip = backgroundTracks[GetRandomTrack()];
            Camera.main.enabled = false;
        }
    }

    int GetNextTrack()
    {
        if (currentTrack > backgroundTracks.Length - 1)
        {
            return currentTrack = 0;
        }
        else
            return currentTrack++;
    }
    int GetRandomTrack()
    {
        currentTrack = Random.Range(0, backgroundTracks.Length - 1);
        return currentTrack;
    }

    public void PlaySelectedTrack(int index)
    {
        currentTrack = Mathf.Clamp(index, 0, backgroundTracks.Length - 1);
        audioSource.clip = backgroundTracks[currentTrack];
        audioSource.Play();
    }

    void fadeIn()
    {
        if (fadeTexture.color.a <= 0.0f)
        {
            bTransitioning = false;
            bFadeIn = false;
            audioSource.Play();
        }
    }

#if UNITY_ADS
    public void ShowDefaultAd()
    {
        if (!Advertisement.IsReady())
        {
            Debug.Log("Ads not ready for default placement");
            return;
        }

        Advertisement.Show();
    }

    public void ShowRewardedAd()
    {
        const string RewardedPlacementId = "rewardedVideo";

        if (!Advertisement.IsReady(RewardedPlacementId))
        {
            Debug.Log(string.Format("Ads not ready for placement '{0}'", RewardedPlacementId));
            return;
        }

        var options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show(RewardedPlacementId, options);
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
#endif
}
