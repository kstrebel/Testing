using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Gameplay : MonoBehaviour
{
    [SerializeField] private WwiseInScene wwiseInSceneScript;
    private GameObject wwiseGlobalGO;
    private Animator RevealStateMachine;

    [Space]
    [SerializeField] private bool audioDebug;

    public enum RevealStage
    {
        Intro,              //stage 0
        Reveal1,            //stage 1
        Reveal2,            //stage 2
        DamageAnticipate,   //stage 3
        Collide,            //stage 4
        DamageTake,         //stage 5
        Gloat               //stage 6
    }

    [Space]
    [SerializeField] private RevealStage RevealStageNumber = 0;

    private AK.Wwise.Event UIHover;
    private AK.Wwise.Event UISelect;
    private AK.Wwise.Event UIBack;

    [Header("Cards")]
    [SerializeField] private AK.Wwise.Event CardDraw;
    [SerializeField] private AK.Wwise.Event CardImpact;
    [SerializeField] private AK.Wwise.Event CardSpell;

    #region **BoardEffects**
    [Header("Board Effects")]
    [SerializeField] private AK.Wwise.Event Attack;
    [SerializeField] private AK.Wwise.Event Block;
    [SerializeField] private AK.Wwise.Event Spell;
    [SerializeField] private AK.Wwise.Event ScaleMoveBig;
    [SerializeField] private AK.Wwise.Event ScaleMoveSmall;
    [SerializeField] private AK.Wwise.Event ScaleStop;
    [SerializeField] private AK.Wwise.Event Schwing;
    [SerializeField] private AK.Wwise.Event BearSignature;
    [SerializeField] private AK.Wwise.Event IdolSignature;
    [SerializeField] private AK.Wwise.Event DoofSignature;
    [SerializeField] private AK.Wwise.Event ProtagSignature;
    [SerializeField] private AK.Wwise.Event VillainSignature;
    [SerializeField] private AK.Wwise.Event SenseiSignature;
    [SerializeField] private AK.Wwise.Event Pause;
    [SerializeField] private AK.Wwise.Event DamageBlocked;
    [SerializeField] private AK.Wwise.Event CardHover;
    [SerializeField] private AK.Wwise.Event DamageImpact;
    [SerializeField] private AK.Wwise.Event Unpause;
    #endregion

    #region **VO**
    [Header("Prefixes")]
    [SerializeField] private string Prefix;

    [Header("Character Names")]
    [SerializeField] private string VOBear;
    [SerializeField] private string VOIdol;
    [SerializeField] private string VODoof;
    [SerializeField] private string VOProtag;
    [SerializeField] private string VOVillain;
    [SerializeField] private string VOSensei;

    private CardDatabase.Characters player1;
    private CardDatabase.Characters player2;

    private bool p1RevealsFirst = true;
    private bool card1IsRevealing = true;

    private int player1Damage = 0;
    private int player2Damage = 0;
    #endregion

    void Awake()
    {
        StartCoroutine(setWwiseGlobal());

        RevealStateMachine = gameObject.GetComponent<Animator>();
    }

    #region Music
    public void M_ToPlanning()  //GameLogic when PlayerTurn is 0 (right before P1's turn)
    {
        if (wwiseGlobalGO == null)
            return;

        wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().syncSceneMusic(WwiseGlobalAlways.MusicState.BattlePlan);
    }

    public void M_ToReveal()  //GameLogic when PlayerTurn is 4 (right before reveal)
    {
        wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().syncSceneMusic(WwiseGlobalAlways.MusicState.BattleReveal);
    }

    #endregion

    #region UI
    public void PlayUIHover()
    {
        UIHover.Post(wwiseGlobalGO);
    }

    public void PlayUISelect()
    {
        UISelect.Post(wwiseGlobalGO);
    }

    public void PlayUIBack()
    {
        UIBack.Post(wwiseGlobalGO);
    }
    #endregion

    #region Cards
    public void SFX_CardDraw()
    {
        CardDraw.Post(wwiseGlobalGO);

        if (Application.isEditor && (audioDebug || wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages))
            Debug.Log("Playing card draw");
    }

    public void SFX_CardImpact()
    {
        CardImpact.Post(wwiseGlobalGO);

        if (Application.isEditor && (audioDebug || wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages))
            Debug.Log("Playing card impact");
    }

    public void SFX_CardSpell()
    {
        CardSpell.Post(wwiseGlobalGO);

        if (Application.isEditor && (audioDebug || wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages))
            Debug.Log("Playing a spell card");
    }

    public void SFX_PlaySignatureCard(CardDatabase.Characters character)
    {
        string t = "Play_";

        switch (character)
        {
            case CardDatabase.Characters.Hiro:
                t += "ProtagSignature";
                break;
            case CardDatabase.Characters.Vincent:
                t += "VillainSignature";
                break;
            case CardDatabase.Characters.Dustin:
                t += "DoofSignature";
                break;
            case CardDatabase.Characters.KiraKira:
                t += "IdolSignature";
                break;
            case CardDatabase.Characters.HowardSensei:
                t += "SenseiSignature";
                break;
            case CardDatabase.Characters.Felty:
                t += "BearSignature";
                break;
        }

        AkSoundEngine.PostEvent(t, gameObject);
    }
    #endregion

    #region VO_StateMachine
    public void VO_StartRevealStage()
    {
        RevealStateMachine.enabled = false;
        RevealStateMachine.enabled = true;

        VO_ChangeRevealStage(0);
        RevealStateMachine.Play("Start Reveal", -1, 0f);

        //card1IsRevealing = true;

        //RevealStateMachine.SetTrigger("Voice Line Done");
    }

    public void VO_EndRevealStage()
    {
        RevealStateMachine.enabled = false;
    }

    public void VO_ChangeRevealStage(RevealStage stage)  //expand function
    {
        /**
         * 0 - VO_StartRevealStage
         * 1 - RevealPhaseLogic when if card has not been shown yet
         * 2 - RevealPhaseLogic when if card has not been shown yet
         * 3 - Start of clash animation in reveal controller > RevealStack_AnimFuncs
         * 4 - Middle of clash animation in reveal controller > RevealStack_AnimFuncs
         * 5 - RevealPhaseLogic after damage is resolved in ClashDamage()
         * 6 - End of clash animation in reveal controller > RevealStack_AnimFuncs
         * **/

        RevealStageNumber = stage;

        RevealStateMachine.SetInteger("What stage is gameplay", (int)stage);

        if (Application.isEditor && (audioDebug || wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages))
            Debug.Log("Set reveal stage to " + RevealStageNumber.ToString());
    }

    public void VO_SetCharacters(CardDatabase.Characters p1, CardDatabase.Characters p2)  //GameLogic before p1 chooses cards
    {
        player1 = p1;
        player2 = p2;

        card1IsRevealing = true;

        if (Application.isEditor && (audioDebug || (wwiseGlobalGO != null && wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages)))
            Debug.Log("Player 1 set to " + p1.ToString() + "\nPlayer 2 set to " + p2.ToString());
    }

    public void VO_WhoGoesFirst(int player)  //RevealPhaseLogic after determining who goes first
    {
        bool p1First = true;

        if (player == 2)
            p1First = false;
        else if (player == 0)
            Debug.Log("The player going first is 0...");

        p1RevealsFirst = p1First;

        if (Application.isEditor && (audioDebug || wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages))
            Debug.Log("It is " + p1RevealsFirst.ToString() + " that player 1 is going first.");
    }

    public void VO_SetPlayerCard(int player, CardDatabase.CardTypes card)
    {
        int c = -1;

        switch (card)
        {
            case CardDatabase.CardTypes.Attacker:   //type 0
                c = 0;
                break;
            case CardDatabase.CardTypes.Defender:   //type 1
                c = 1;
                break;
            case CardDatabase.CardTypes.Spell:      //type 2
                c = 2;
                break;
            case CardDatabase.CardTypes.Signature:  //type 3
                c = 3;
                break;
            case CardDatabase.CardTypes.Dud:        //type 4
                c = 4;
                break;
        }

        if (player == 1)
        {
            RevealStateMachine.SetInteger("Card 1 Type", c);
        }
        else if (player == 2)
        {
            RevealStateMachine.SetInteger("Card 2 Type", c);
        }
    }

    public void VO_SetPlayerDamage(int p1damage, int p2damage)  //RevealPhaseLogic after determining turn order
    {
        player1Damage = p1damage;
        player2Damage = p2damage;

        int max = p1damage;

        if (p1damage < p2damage)
            max = p2damage;

        RevealStateMachine.SetInteger("Max Player Damage", max);
    }
    #endregion

    #region VO
    #region VO_helpers
    //pls depricate
    private string VO_characterToString(int player)
    {
        string s = null;

        CardDatabase.Characters c = player1;

        if (player == 2)
        {
            c = player2;
        }

        switch (c)
        {
            case CardDatabase.Characters.None:
                Debug.Log("Characters haven't been set in SFX_Gameplay yet.");
                break;
            case CardDatabase.Characters.Hiro:
                s = VOProtag;
                break;
            case CardDatabase.Characters.Vincent:
                s = VOVillain;
                break;
            case CardDatabase.Characters.Dustin:
                s = VODoof;
                break;
            case CardDatabase.Characters.KiraKira:
                s = VOIdol;
                break;
            case CardDatabase.Characters.HowardSensei:
                s = VOSensei;
                break;
            case CardDatabase.Characters.Felty:
                s = VOBear;
                break;
        }

        return s;
    }

    private string VO_characterToString(CardDatabase.Characters player)
    {
        string s = null;

        switch (player)
        {
            case CardDatabase.Characters.None:
                Debug.Log("Characters haven't been set in SFX_Gameplay yet.");
                break;
            case CardDatabase.Characters.Hiro:
                s = VOProtag;
                break;
            case CardDatabase.Characters.Vincent:
                s = VOVillain;
                break;
            case CardDatabase.Characters.Dustin:
                s = VODoof;
                break;
            case CardDatabase.Characters.KiraKira:
                s = VOIdol;
                break;
            case CardDatabase.Characters.HowardSensei:
                s = VOSensei;
                break;
            case CardDatabase.Characters.Felty:
                s = VOBear;
                break;
        }
        return s;
    }

    private CardDatabase.Characters VO_GetTurnPlayer()
    {
        CardDatabase.Characters c = player1;

        if (card1IsRevealing)
        {
            if(!p1RevealsFirst)
            c = player2;

            card1IsRevealing = false;
        }

        else if(!card1IsRevealing)
        {
            if(p1RevealsFirst)
            c = player2;

            card1IsRevealing = true;
        }

        return c;
    }

    private string VO_cardToString(int card)
    {
        string line = "";

        switch (card)
        {
            case 0:
                line = "Attack";
                break;
            case 1:
                line = "Block";
                break;
            case 2:
                line = "Spell";
                break;
            case 3:
                line = "Signature";
                break;
            case 4:
                line = "Awkward";
                break;
        }

        return line;
    }

    private void VO_ConcatAndPlay(string prefix, string character, string action, bool needsCallback = false)
    {
        if (!needsCallback)
        {
            AkSoundEngine.PostEvent((prefix + "_" + character + "_" + action), wwiseGlobalGO);
        }
        else
        {
            AkSoundEngine.PostEvent((prefix + "_" + character + "_" + action), wwiseGlobalGO,
                (uint)AkCallbackType.AK_EndOfEvent, vo_endOfEvent, null);
        }

        if (Application.isEditor && (audioDebug || wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages))
            Debug.Log("Called event " + prefix + "_" + character + "_" + action);
    }

    private void vo_endOfEvent(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (Application.isEditor && (audioDebug || wwiseGlobalGO.GetComponent<WwiseGlobalAlways>().ShowAudioDebugMessages))
            Debug.Log("the ak callback is being called");

        RevealStateMachine.SetTrigger("Voice Line Done");
    }
    #endregion

    #region VO_Play
    public void VO_StartingLine(int player)
    {
        string opponent = VO_characterToString(player2);

        if (player == 2)
            opponent = VO_characterToString(player1);

        AkSoundEngine.SetSwitch("Character", opponent, wwiseGlobalGO);

        VO_ConcatAndPlay(Prefix, VO_characterToString(player), "BattleStart");
    }

    public void VO_PlayRevealIntro(int player)  //RevealPhaseLogic after card effects reset
    {
        VO_ConcatAndPlay(Prefix, VO_characterToString(player), "Scales", true);
    }

    public void VO_PlayReveal(int card)  //all stage 1 and not-unique stage 2 lines
    {
        if (card == 3)  //signature VO line needs to be called earlier, so not here
            return;

        CardDatabase.Characters player = VO_GetTurnPlayer();

        VO_ConcatAndPlay(Prefix, VO_characterToString(player), VO_cardToString(card), true);
    }

    public void VO_PlaySignature()  //goEyes() Go animation
    {
        CardDatabase.Characters player = VO_GetTurnPlayer();

        VO_ConcatAndPlay(Prefix, VO_characterToString(player), "Signature", true);
    }

    public void VO_PlayBlockSuccess()  //stage 2 block successful
    {
        CardDatabase.Characters c = player1;
        if (p1RevealsFirst)
            c = player2;

        if (p1RevealsFirst)
            c = player2;

        VO_ConcatAndPlay(Prefix, VO_characterToString(c), "Block", true);
    }

    public void VO_PlayBlockAwkward()  //stage 2 block awkward
    {
        CardDatabase.Characters c = player1;
        if (p1RevealsFirst)
            c = player2;

        VO_ConcatAndPlay(Prefix, VO_characterToString(c), "Awkward", true);
    }

    public void VO_PlayDamageAnticipate()  //anticipate damage stage
    {
        if (4 < player1Damage)
            VO_ConcatAndPlay(Prefix, VO_characterToString(player1), "Gasp", true);

        if (4 < player2Damage)
            VO_ConcatAndPlay(Prefix, VO_characterToString(player2), "Gasp", true);
    }

    public void VO_PlayDamageTake(int player)  //take damage stage
    {
        string p = VO_characterToString(player1);
        int damage = player1Damage;

        if (player == 2)
        {
            p = VO_characterToString(player2);
            damage = player2Damage;
        }

        if (0 < damage && damage < 5)
            VO_ConcatAndPlay(Prefix, p, "MinorDamage", true);
        else if (5 <= damage)
            VO_ConcatAndPlay(Prefix, p, "MajorDamage", true);
    }

    public void VO_PlayDamageGloat()  //gloat about win stage
    {
        if (5 <= player2Damage)
            VO_ConcatAndPlay(Prefix, VO_characterToString(player1), "Gloat", true);

        if (5 <= player1Damage)
            VO_ConcatAndPlay(Prefix, VO_characterToString(player2), "Gloat", true);
    }
    #endregion

    public void VO_Loss(int player)
    {
        VO_ConcatAndPlay(Prefix, VO_characterToString(player), "Loss");
    }

    public void VO_Select(int player)
    {
        VO_ConcatAndPlay(Prefix, VO_characterToString(player), "Select");
    }

    public void VO_Title(int player)
    {
        VO_ConcatAndPlay(Prefix, VO_characterToString(player), "Title");
    }

    public void VO_Victory(int player)
    {
        VO_ConcatAndPlay(Prefix, VO_characterToString(player), "Victory");
    }
    #endregion

    #region BoardEffects
    public void PlayAttack()
    {
        Attack.Post(wwiseGlobalGO);
    }

    public void PlayBlock()
    {
        Block.Post(wwiseGlobalGO);
    }

    public void PlaySpell()
    {
        Spell.Post(wwiseGlobalGO);
    }

    public void PlayScaleMove(bool doesItMoveALot)
    {
        if (doesItMoveALot)
        {
            ScaleMoveBig.Post(wwiseGlobalGO);
        }
        else
        {
                ScaleMoveSmall.Post(wwiseGlobalGO);
        }
    }

    public void PlayDamageImpact()
    {
        DamageImpact.Post(wwiseGlobalGO);
    }

    public void PlayDamageBlocked()
    {
        DamageBlocked.Post(wwiseGlobalGO);
    }

    public void PlaySchwing()
    {
        Schwing.Post(wwiseGlobalGO);
    }

    public void PlayBearSignature()
    {
        BearSignature.Post(wwiseGlobalGO);
    }

    public void PlayIdolSignature()
    {
        IdolSignature.Post(wwiseGlobalGO);
    }

    public void PlayDoofSignature()
    {
        DoofSignature.Post(wwiseGlobalGO);
    }

    public void PlayProtagSignature()
    {
        ProtagSignature.Post(wwiseGlobalGO);
    }

    public void PlayVillainSignature()
    {
        VillainSignature.Post(wwiseGlobalGO);
    }

    public void PlaySenseiSignature()
    {
        SenseiSignature.Post(wwiseGlobalGO);
    }

    public void PlayScaleStop()
    {
        ScaleStop.Post(wwiseGlobalGO);
    }

    public void PlayCardHover()
    {
        CardHover.Post(wwiseGlobalGO);
    }

    public void PlayPause()
    {
        Pause.Post(wwiseGlobalGO);
    }

    public void PlayUnpause()
    {
        Unpause.Post(wwiseGlobalGO);
    }
    #endregion

    IEnumerator setWwiseGlobal()
    {
        yield return new WaitUntil(() => wwiseInSceneScript.getWwiseGlobal() != null);

        wwiseGlobalGO = wwiseInSceneScript.getWwiseGlobal();

        AK.Wwise.Event[] events = wwiseGlobalGO.GetComponent<SFX_UI>().WGA_GetUIEvents();

        UIHover = events[0];
        UISelect = events[1];
        UIBack = events[2];
    }
}
