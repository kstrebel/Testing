using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The WwiseGlobalAlways GameObject will be created by the WwiseInScene GameObject of whatever the first scene is
 * It won't be deleted between scenes so music can continue to play and SoundBanks can stay loaded
 * This means any SoundBanks that need to stay loaded will go on WwiseGlobalAlways
 * WwiseGlobalAlways will only play its audio for its own listener, so the music will stay seperate from other sounds
 * **/

public class WwiseGlobalAlways : MonoBehaviour
{
    //what music states there are
    public enum MusicState
    {
        None, OP, MainMenu, CharacterSelect, Credits, BattlePlan, BattleReveal, Results, Neutral
    };

    //start with no music
    private MusicState currentMusic = MusicState.None;

    [SerializeField] private AK.Wwise.Event StartMusic;

    //Wwise events expect transition to be controlled in Wwise
    [Header("Wwise event to switch music to: ")]
    [SerializeField] private AK.Wwise.Event StopMusic;
    [SerializeField] private AK.Wwise.Event ToOP;
    [SerializeField] private AK.Wwise.Event ToMainMenu;
    [SerializeField] private AK.Wwise.Event ToCharacterSelect;
    [SerializeField] private AK.Wwise.Event ToCredits;
    [SerializeField] private AK.Wwise.Event ToBattlePlan;
    [SerializeField] private AK.Wwise.Event ToBattleReveal;
    [SerializeField] private AK.Wwise.Event ToResults;

    [Header("Debug")]
    //enable this to see audio debug messages
    public bool ShowAudioDebugMessages;

    //called from WwiseInScene of scene just loaded
    //changes music currently playing
    public void syncSceneMusic(MusicState newSceneMusic)
    {
        if (newSceneMusic != currentMusic)
        {
            switch (newSceneMusic)
            {
                case MusicState.None:
                    StopMusic.Post(gameObject);
                    break;
                case MusicState.OP:
                    ToOP.Post(gameObject);
                    break;
                case MusicState.MainMenu:
                    ToMainMenu.Post(gameObject);
                    break;
                case MusicState.CharacterSelect:
                    ToCharacterSelect.Post(gameObject);
                    break;
                case MusicState.Credits:
                    ToCredits.Post(gameObject);
                    break;
                case MusicState.BattlePlan:
                    ToBattlePlan.Post(gameObject);
                    break;
                case MusicState.BattleReveal:
                    ToBattleReveal.Post(gameObject);
                    break;
                case MusicState.Results:  //this one's different because the victory music is in another switch container
                    StopMusic.Post(gameObject);
                    ToResults.Post(gameObject);
                    break;
            }

            if (currentMusic == MusicState.None)
                StartMusic.Post(gameObject);

            if (ShowAudioDebugMessages)
            {
                Debug.Log("Switched music to " + newSceneMusic.ToString());
            }


            if (newSceneMusic == MusicState.Results)  //this is so it will call the play event next time
                currentMusic = MusicState.None;
            else
                currentMusic = newSceneMusic;
        }
        else if (ShowAudioDebugMessages)
        {
            Debug.Log("Kept music on " + newSceneMusic.ToString());
        }
    }
}
