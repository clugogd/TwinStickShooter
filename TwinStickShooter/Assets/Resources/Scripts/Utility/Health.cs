using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 2.0f;
    public float MAXHP
    {
        get { return maxHP; }
        set { maxHP = value;}
    }
    [SerializeField]
    private float currentHP = 1.0f;
    public float HP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    [SerializeField]
    private bool bDead = false;
    public bool ISDEAD
    {
        get { return bDead; }
    }

    private FlashObject flashScript;
    [SerializeField]
    private Color feedbackColor = Color.red;

    [SerializeField]
    private bool bDebugDisplay = true;

    [SerializeField]
    private AudioClip hitSFX;
    [SerializeField]
    private AudioClip critHitSFX;

    [SerializeField]
    private GameObject hitVFX;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        currentHP = maxHP;
        if (maxHP > 0.0f)
        {
            flashScript = GetComponent<FlashObject>();
            if (flashScript == null)
            {
                this.gameObject.AddComponent<FlashObject>();
                flashScript = GetComponent<FlashObject>();
            }
        }

        audioSource = transform.gameObject.GetComponentInChildren<AudioSource>();
        if (audioSource == null)
           audioSource = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if( bDebugDisplay )
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
                ApplyDamage(10.0f);
        }
    }

    void OnGUI()
    {
        if (bDebugDisplay && transform.gameObject.tag.Contains("Player"))
        {
            float offset = 0;
            float step = 32.0f;
            GUI.BeginGroup(new Rect(Screen.width * 0.8f, 96, 400, 100));
            GUI.Label(new Rect(0.0f, offset, 400, 32), "HP: " + currentHP + "/" + maxHP);
            offset += step;
            GUI.EndGroup();
        }
    }
    public void DoLevelUp(int level)
    {
        float toAdd = (maxHP * level) * 0.1f;
        maxHP += toAdd;
        currentHP = maxHP;
    }

    public void ResetHP()
    {
        currentHP = maxHP;
    }
    public void ApplyDamage(float amount, bool bIsCrit = false)
    {
        currentHP -= amount;
        bDead = currentHP <= 0.0f ? true : false;

        if (!bDead)
        {
            if (flashScript)
                flashScript.doFlashObject(feedbackColor);

            if (bIsCrit)
            {
                if (critHitSFX)
                    audioSource.PlayOneShot(critHitSFX);
            }
            else
            {
                if (hitSFX)
                    audioSource.PlayOneShot(hitSFX);
                if (hitVFX)
                    Destroy(Instantiate(hitVFX, transform.position, Quaternion.identity), 2.0f);
            }
        }
    }

    public void ApplyHeal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, currentHP, maxHP);
    }
}
