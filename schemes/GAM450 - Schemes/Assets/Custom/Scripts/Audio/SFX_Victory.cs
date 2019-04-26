using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Victory : MonoBehaviour
{
    [SerializeField] private WwiseInScene wwiseInSceneScript;
    private GameObject wwiseGlobalGO;

    private CardDatabase.Characters Winner;
    private CardDatabase.Characters Loser;

    private bool charsHaveBeenSet = false;

    void Start()
    {
        StartCoroutine(setWwiseGlobal());
    }

    private void OnDestroy()
    {
        AkSoundEngine.PostEvent("Stop_Victory", wwiseGlobalGO);
    }

    public void VO_SetResultsCharacters(CardDatabase.Characters winner, CardDatabase.Characters loser)  //VictoryScreenDisplay SetSprites()
    {
        Winner = winner;
        Loser = loser;
    }

    private string vo_CharacterToString(CardDatabase.Characters c)
    {
        string s = "";

        switch (c)
        {
            case CardDatabase.Characters.Hiro:
                s = "Protag";
                break;
            case CardDatabase.Characters.Vincent:
                s = "Villain";
                break;
            case CardDatabase.Characters.Dustin:
                s = "Doof";
                break;
            case CardDatabase.Characters.KiraKira:
                s = "Idol";
                break;
            case CardDatabase.Characters.HowardSensei:
                s = "Sensei";
                break;
            case CardDatabase.Characters.Felty:
                s = "Bear";
                break;
        }

        return s;

        //AkSoundEngine.PostEvent("Set_Switch_" + s, wwiseGlobalGO);

        //AkSoundEngine.SetSwitch("Character", s, wwiseGlobalGO);
    }

    private void PlayVictory()
    {
        AkSoundEngine.PostEvent("Set_Switch_" + vo_CharacterToString(Winner), wwiseGlobalGO);

        AkSoundEngine.PostEvent("Play_Victory", wwiseGlobalGO);

        AkSoundEngine.PostEvent("Play_" + vo_CharacterToString(Winner) + "_Victory", wwiseGlobalGO, (uint)AkCallbackType.AK_EndOfEvent, endOfVictoryLine, null);
    }

    private void endOfVictoryLine(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkSoundEngine.PostEvent("Play_" + vo_CharacterToString(Loser) + "_Loss", wwiseGlobalGO);
    }

    IEnumerator setWwiseGlobal()
    {
        yield return new WaitUntil(() => wwiseInSceneScript.getWwiseGlobal() != null);

        wwiseGlobalGO = wwiseInSceneScript.getWwiseGlobal();

        PlayVictory();
    }
}
