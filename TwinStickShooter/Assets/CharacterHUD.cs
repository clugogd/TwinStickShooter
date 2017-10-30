using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{

    [SerializeField]
    private int PlayerID = 1;

    [SerializeField]
    private Text levelText;

    [Header("HEALTH")]
    [SerializeField]
    private Text textHP;
    [SerializeField]
    private Image imageHP;

    [Header("ENERGY")]
    [SerializeField]
    private Text textEnergy;
    [SerializeField]
    private Image imageEnergy;

    [Header("EXPERIENCE")]
    [SerializeField]
    private Text textXP;
    [SerializeField]
    private Image imageXP;

    [Header("SKILLS")]
    [SerializeField]
    private Image imageSkill01;
    [SerializeField]
    private Image imageSkill02;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateStats(float hp, float xp, float energy, int level)
    {
        GameState._instance.stats.MAXHP = hp;
        GameState._instance.stats.XPTONEXTLEVEL = xp;
        GameState._instance.stats.MAXENERGY = energy;
        GameState._instance.stats.CURRENTLEVEL = level;
    }

    public void SetHP(int current = 0, int max = 0)
    {
        if (current <= 0 || max <= 0)
            imageHP.fillAmount = 0.0f;

        imageHP.fillAmount = (float)(current) / (float)(max);
        textHP.text = current + "/" + max;
    }
    public void SetXP(int current = 0, int max = 0)
    {
        if (current <= 0 || max <= 0)
            imageXP.fillAmount = 0.0f;

        imageXP.fillAmount = (float)(current) / (float)(max);
        textXP.text = current + "/" + max;
    }
    public void SetEnergy(int current = 0, int max = 0)
    {
        if (current <= 0 || max <= 0)
            imageEnergy.fillAmount = 0.0f;

        imageEnergy.fillAmount = (float)(current) / (float)(max);
        textEnergy.text = current + "/" + max;
    }
    public void SetLevel(int value = 0)
    {
        levelText.text = value.ToString();
    }
    public void SetSkillCooldown(int id = 0, float amount = 1.0f)
    {
        switch (id)
        {
            case 0:
                imageSkill01.fillAmount = amount;
                break;
            case 1:
                imageSkill02.fillAmount = amount;
                break;
        }
    }
}
