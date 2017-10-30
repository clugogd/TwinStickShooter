using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour {

    [SerializeField]
    private AudioMixer masterMixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider dialogueSlider;
    public Slider effectsSlider;
    public Slider ambientSlider;

	// Use this for initialization
	void Start ()
    {
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void SetMasterLevel(float masterValue)
    {
        masterMixer.SetFloat("masterVolume", masterValue);
    }
    public void SetMusicLevel(float musicValue)
    {
        masterMixer.SetFloat("musicVolume", musicValue);
    }
    public void SetDialogueLevel(float dialogueValue)
    {
        masterMixer.SetFloat("dialogueVolume", dialogueValue);
    }
    public void SetEffectsLevel(float effectsValue)
    {
        masterMixer.SetFloat("effectsVolume", effectsValue);
    }
    public void SetAmbientLevel(float ambientValue)
    {
        masterMixer.SetFloat("ambientVolume", ambientValue);
    }

}
