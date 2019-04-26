using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Options : MonoBehaviour
{
    [SerializeField] private WwiseInScene wwiseInSceneScript;
    private GameObject wwiseGlobalGO;

    private float masterVol = 100f;
    private bool isMuted = false;

    [Header("RTPC Names")]
    [SerializeField] private string MasterVolume;
    [SerializeField] private string MusicVolume;
    [SerializeField] private string VoiceVolume;
    [SerializeField] private string EffectsVolume;

    [Header("GameObjects")]
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider VoiceSlider;
    [SerializeField] private Slider EffectsSlider;
    [SerializeField] private Toggle MuteToggle;

    [Header("Test Events")]
    [SerializeField] private AK.Wwise.Event VoiceVolumeTest;
    [SerializeField] private AK.Wwise.Event SFXVolumeTest;

    private void Start()
    {
        masterVol = MasterSlider.value * 100;

        StartCoroutine(setWwiseGlobal());
    }

    public void SFX_SetMasterVolume()
    {
        masterVol = MasterSlider.value*100;

        Debug.Log(masterVol.ToString());

        AkSoundEngine.SetRTPCValue(MasterVolume, masterVol, wwiseGlobalGO);
    }

    public void SFX_SetMusicVolume()
    {
        AkSoundEngine.SetRTPCValue(MusicVolume, MusicSlider.value * 100, wwiseGlobalGO);
    }

    public void SFX_SetVoiceVolume()
    {
        AkSoundEngine.SetRTPCValue(VoiceVolume, MusicSlider.value * 100, wwiseGlobalGO);
    }

    public void SFX_SetEffectsVolume()
    {
        AkSoundEngine.SetRTPCValue(EffectsVolume, EffectsSlider.value * 100, wwiseGlobalGO);
    }

    public void SFX_ToggleMuteMaster()
    {
        isMuted = MuteToggle.isOn;

        float value = 0f;

        if (!isMuted)
            value = masterVol;

        AkSoundEngine.SetRTPCValue(MasterVolume, value, wwiseGlobalGO);
    }

    public void SFX_TestVoiceVolume()
    {
        VoiceVolumeTest.Post(wwiseGlobalGO);
    }

    public void SFX_TextSFXVolume()
    {
        SFXVolumeTest.Post(wwiseGlobalGO);
    }

    IEnumerator setWwiseGlobal()
    {
        yield return new WaitUntil(() => wwiseInSceneScript.getWwiseGlobal() != null);

        wwiseGlobalGO = wwiseInSceneScript.getWwiseGlobal();
    }
}
