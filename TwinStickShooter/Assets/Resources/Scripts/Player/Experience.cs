using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private float nextLevelXP = 100.0f;
    public float NEXTLEVELXP
    {
        get { return nextLevelXP; }
        set { nextLevelXP = value; }
    }
    [SerializeField]
    private float currentXP = 0.0f;
    public float CURRENTXP
    {
        get { return currentXP; }
        set { currentXP = value; }
    }

    [SerializeField]
    private int currentLevel = 1;
    public float CURRENTLEVEL { get { return currentLevel; } }

    [SerializeField]
    private int levelCap = 10;
    public float LEVELCAP { get { return levelCap; } }

    private FlashObject flashScript;
    [SerializeField]
    private Color feedbackColor = Color.blue;

    private Health healthScript;

    [SerializeField]
    private bool bDebugDisplay = true;

    // Use this for initialization
    void Start()
    {
        healthScript = this.transform.root.gameObject.GetComponentInChildren<Health>();
        flashScript = this.transform.root.gameObject.GetComponentInChildren<FlashObject>();

        if (flashScript == null)
        {
            this.gameObject.AddComponent<FlashObject>();
            flashScript = GetComponent<FlashObject>();
        }

        if (GameState._instance)
        {
            currentLevel = GameState._instance.stats.CURRENTLEVEL;
            levelCap = GameState._instance.stats.MAXLEVEL;
            nextLevelXP = GameState._instance.stats.XPTONEXTLEVEL;

            healthScript.MAXHP = GameState._instance.stats.MAXHP;
            healthScript.HP = GameState._instance.stats.MAXHP;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (bDebugDisplay)
        {
            float offset = 0;
            float step = 32.0f;
            GUI.BeginGroup(new Rect(Screen.width * 0.8f, 10, 400, 100));
            GUI.Label(new Rect(0.0f, offset, 400, 32), "PLAYER:<" + transform.root.gameObject.name + ">");
            offset += step;
            GUI.Label(new Rect(0.0f, offset, 400, 32), "XP: " + currentXP + "/" + nextLevelXP);
            offset += step;
            GUI.Label(new Rect(0.0f, offset, 400, 32), "LVL: " + currentLevel + "/" + levelCap);
            GUI.EndGroup();
        }
    }

    public void ResetXP()
    {
        currentXP = 0.0f;
    }
    public void ResetLevel()
    {
        currentLevel = 1;
    }

    public void GainXP(float amount)
    {
        if (currentLevel < levelCap)
        {
            currentXP += amount;

            if (LeveledUp())
            {
                float overflow = currentXP - nextLevelXP;
                currentXP = overflow;
                currentLevel++;
                nextLevelXP *= currentLevel;

                //  Increase the hp for the unit

                healthScript.DoLevelUp(currentLevel);

                if (GameState._instance)
                {
                    GameState._instance.stats.CURRENTLEVEL = currentLevel;
                    GameState._instance.stats.XPTONEXTLEVEL = nextLevelXP;
                    GameState._instance.stats.CURRENTXP = currentXP;
                }
            }
        }
    }

    bool LeveledUp()
    {
        return currentXP >= nextLevelXP ? true : false;
    }

}
