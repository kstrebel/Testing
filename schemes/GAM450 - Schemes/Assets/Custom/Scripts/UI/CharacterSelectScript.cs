using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScript : MonoBehaviour
{
    GameObject P1Background;
    GameObject P2Background;

    GameObject P1Question;
    GameObject P2Question;

    GameObject P1_HiroBG;
    GameObject P1_VincentBG;
    GameObject P1_DustinBG;
    GameObject P1_KiraKiraBG;
    GameObject P1_HowardBG;
    GameObject P1_FeltyBG;
    GameObject P1_RandomBG;

    GameObject P2_HiroBG;
    GameObject P2_VincentBG;
    GameObject P2_DustinBG;
    GameObject P2_KiraKiraBG;
    GameObject P2_HowardBG;
    GameObject P2_FeltyBG;
    GameObject P2_RandomBG;

    GameObject ConfirmButton;

    bool player1 = true; //It is player 1's turn to choose

    //audio from here
    [SerializeField] private SFX_CharacterSelect CharSelectAudio;

    // Start is called before the first frame update
    void Start()
    {
        P1Background = transform.Find("P1_Group").Find("Mask").Find("P1_Background").gameObject;
        P2Background = transform.Find("P2_Group").Find("Mask").Find("P2_Background").gameObject;

        P1Question = transform.Find("P1_Group").Find("Mask").Find("NoChoice").gameObject;
        P2Question = transform.Find("P2_Group").Find("Mask").Find("NoChoice").gameObject;

        P1_HiroBG = transform.Find("P1_Group").Find("Mask").Find("Hiro_BG").gameObject;
        P1_VincentBG = transform.Find("P1_Group").Find("Mask").Find("Vincent_BG").gameObject;
        P1_DustinBG = transform.Find("P1_Group").Find("Mask").Find("Dustin_BG").gameObject;
        P1_KiraKiraBG = transform.Find("P1_Group").Find("Mask").Find("KiraKira_BG").gameObject;
        P1_HowardBG = transform.Find("P1_Group").Find("Mask").Find("Howard_BG").gameObject;
        P1_FeltyBG = transform.Find("P1_Group").Find("Mask").Find("Felty_BG").gameObject;
        P1_RandomBG = transform.Find("P1_Group").Find("Mask").Find("Random_BG").gameObject;

        P2_HiroBG = transform.Find("P2_Group").Find("Mask").Find("Hiro_BG").gameObject;
        P2_VincentBG = transform.Find("P2_Group").Find("Mask").Find("Vincent_BG").gameObject;
        P2_DustinBG = transform.Find("P2_Group").Find("Mask").Find("Dustin_BG").gameObject;
        P2_KiraKiraBG = transform.Find("P2_Group").Find("Mask").Find("KiraKira_BG").gameObject;
        P2_HowardBG = transform.Find("P2_Group").Find("Mask").Find("Howard_BG").gameObject;
        P2_FeltyBG = transform.Find("P2_Group").Find("Mask").Find("Felty_BG").gameObject;
        P2_RandomBG = transform.Find("P2_Group").Find("Mask").Find("Random_BG").gameObject;

        ConfirmButton = transform.Find("ConfirmButton").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hiro()
    {
        if(player1 == true)
        {
            P1Background.SetActive(false);
            P1Question.SetActive(false);

            P1_VincentBG.SetActive(false);
            P1_DustinBG.SetActive(false);
            P1_KiraKiraBG.SetActive(false);
            P1_HowardBG.SetActive(false);
            P1_FeltyBG.SetActive(false);
            P1_RandomBG.SetActive(false);

            PlayerGlobals.P1Character = CardDatabase.Characters.Hiro;

            player1 = false;
        }
        else
        {
            P2Background.SetActive(false);
            P2Question.SetActive(false);

            P2_VincentBG.SetActive(false);
            P2_DustinBG.SetActive(false);
            P2_KiraKiraBG.SetActive(false);
            P2_HowardBG.SetActive(false);
            P2_FeltyBG.SetActive(false);
            P2_RandomBG.SetActive(false);

            PlayerGlobals.P2Character = CardDatabase.Characters.Hiro;

            player1 = false;

            // Put in confirm
            ConfirmButton.GetComponent<Animator>().enabled = true;
        }

        CharSelectAudio.VO_CharacterSelected(CardDatabase.Characters.Hiro);
    }

    public void Vincent()
    {
        if(player1 == true)
        {
            P1Background.SetActive(false);
            P1Question.SetActive(false);

            P1_HiroBG.SetActive(false);
            P1_DustinBG.SetActive(false);
            P1_KiraKiraBG.SetActive(false);
            P1_HowardBG.SetActive(false);
            P1_FeltyBG.SetActive(false);
            P1_RandomBG.SetActive(false);

            PlayerGlobals.P1Character = CardDatabase.Characters.Vincent;

            player1 = false;
        }
        else
        {
            P2Background.SetActive(false);
            P2Question.SetActive(false);

            P2_HiroBG.SetActive(false);
            P2_DustinBG.SetActive(false);
            P2_KiraKiraBG.SetActive(false);
            P2_HowardBG.SetActive(false);
            P2_FeltyBG.SetActive(false);
            P2_RandomBG.SetActive(false);

            PlayerGlobals.P2Character = CardDatabase.Characters.Vincent;

            player1 = false;

            // Put in confirm
            ConfirmButton.GetComponent<Animator>().enabled = true;
        }

        CharSelectAudio.VO_CharacterSelected(CardDatabase.Characters.Vincent);
    }

    public void Dustin()
    {
        if(player1 == true)
        {
            P1Background.SetActive(false);
            P1Question.SetActive(false);

            P1_VincentBG.SetActive(false);
            P1_HiroBG.SetActive(false);
            P1_KiraKiraBG.SetActive(false);
            P1_HowardBG.SetActive(false);
            P1_FeltyBG.SetActive(false);
            P1_RandomBG.SetActive(false);

            PlayerGlobals.P1Character = CardDatabase.Characters.Dustin;

            player1 = false;
        }
        else
        {
            P2Background.SetActive(false);
            P2Question.SetActive(false);

            P2_VincentBG.SetActive(false);
            P2_HiroBG.SetActive(false);
            P2_KiraKiraBG.SetActive(false);
            P2_HowardBG.SetActive(false);
            P2_FeltyBG.SetActive(false);
            P2_RandomBG.SetActive(false);

            PlayerGlobals.P2Character = CardDatabase.Characters.Dustin;

            player1 = false;

            // Put in confirm
            ConfirmButton.GetComponent<Animator>().enabled = true;
        }

        CharSelectAudio.VO_CharacterSelected(CardDatabase.Characters.Dustin);
    }

    public void KiraKira()
    {
        if(player1 == true)
        {
            P1Background.SetActive(false);
            P1Question.SetActive(false);

            P1_VincentBG.SetActive(false);
            P1_DustinBG.SetActive(false);
            P1_HiroBG.SetActive(false);
            P1_HowardBG.SetActive(false);
            P1_FeltyBG.SetActive(false);
            P1_RandomBG.SetActive(false);

            PlayerGlobals.P1Character = CardDatabase.Characters.KiraKira;

            player1 = false;
        }
        else
        {
            P2Background.SetActive(false);
            P2Question.SetActive(false);

            P2_VincentBG.SetActive(false);
            P2_DustinBG.SetActive(false);
            P2_HiroBG.SetActive(false);
            P2_HowardBG.SetActive(false);
            P2_FeltyBG.SetActive(false);
            P2_RandomBG.SetActive(false);

            PlayerGlobals.P2Character = CardDatabase.Characters.KiraKira;

            player1 = false;

            // Put in confirm
            ConfirmButton.GetComponent<Animator>().enabled = true;
        }

        CharSelectAudio.VO_CharacterSelected(CardDatabase.Characters.KiraKira);
    }

    public void Howard()
    {
        if(player1 == true)
        {
            P1Background.SetActive(false);
            P1Question.SetActive(false);

            P1_VincentBG.SetActive(false);
            P1_DustinBG.SetActive(false);
            P1_KiraKiraBG.SetActive(false);
            P1_HiroBG.SetActive(false);
            P1_FeltyBG.SetActive(false);
            P1_RandomBG.SetActive(false);

            PlayerGlobals.P1Character = CardDatabase.Characters.HowardSensei;

            player1 = false;
        }
        else
        {
            P2Background.SetActive(false);
            P2Question.SetActive(false);

            P2_VincentBG.SetActive(false);
            P2_DustinBG.SetActive(false);
            P2_KiraKiraBG.SetActive(false);
            P2_HiroBG.SetActive(false);
            P2_FeltyBG.SetActive(false);
            P2_RandomBG.SetActive(false);

            PlayerGlobals.P2Character = CardDatabase.Characters.HowardSensei;

            player1 = false;

            // Put in confirm
            ConfirmButton.GetComponent<Animator>().enabled = true;
        }

        CharSelectAudio.VO_CharacterSelected(CardDatabase.Characters.HowardSensei);
    }

    public void Felty()
    {
        if(player1 == true)
        {
            P1Background.SetActive(false);
            P1Question.SetActive(false);

            P1_VincentBG.SetActive(false);
            P1_DustinBG.SetActive(false);
            P1_KiraKiraBG.SetActive(false);
            P1_HowardBG.SetActive(false);
            P1_HiroBG.SetActive(false);
            P1_RandomBG.SetActive(false);

            PlayerGlobals.P1Character = CardDatabase.Characters.Felty;

            player1 = false;
        }
        else
        {
            P2Background.SetActive(false);
            P2Question.SetActive(false);

            P2_VincentBG.SetActive(false);
            P2_DustinBG.SetActive(false);
            P2_KiraKiraBG.SetActive(false);
            P2_HowardBG.SetActive(false);
            P2_HiroBG.SetActive(false);
            P2_RandomBG.SetActive(false);

            PlayerGlobals.P2Character = CardDatabase.Characters.Felty;

            player1 = false;

            // Put in confirm
            ConfirmButton.GetComponent<Animator>().enabled = true;
        }

        CharSelectAudio.VO_CharacterSelected(CardDatabase.Characters.Felty);
    }

    public void Random()
    {
        if(player1 == true)
        {
            P1Background.SetActive(false);
            P1Question.SetActive(false);

            P1_VincentBG.SetActive(false);
            P1_DustinBG.SetActive(false);
            P1_KiraKiraBG.SetActive(false);
            P1_HowardBG.SetActive(false);
            P1_FeltyBG.SetActive(false);
            P1_HiroBG.SetActive(false);

            PlayerGlobals.P1Character = RandomizeCharacter();

            player1 = false;

            CharSelectAudio.VO_CharacterSelected(PlayerGlobals.P1Character);
        }
        else
        {
            P2Background.SetActive(false);
            P2Question.SetActive(false);

            P2_VincentBG.SetActive(false);
            P2_DustinBG.SetActive(false);
            P2_KiraKiraBG.SetActive(false);
            P2_HowardBG.SetActive(false);
            P2_FeltyBG.SetActive(false);
            P2_HiroBG.SetActive(false);

            PlayerGlobals.P2Character = RandomizeCharacter();

            player1 = false;

            // Put in confirm
            ConfirmButton.GetComponent<Animator>().enabled = true;

            CharSelectAudio.VO_CharacterSelected(PlayerGlobals.P2Character);
        }
    }

    // public void Confirm()
    // {
    //     SceneManager.LoadScene("LoadingScreen_PlayScene");
    // }

    // public void Back()
    // {
    //     SceneManager.LoadScene("Main Menu");
    // }

	CardDatabase.Characters RandomizeCharacter()
	{
		int rng = UnityEngine.Random.Range(1,7);

		switch(rng)
		{
			case 1:
				return CardDatabase.Characters.Hiro;
			case 2:
				return CardDatabase.Characters.Vincent;
			case 3:
				return CardDatabase.Characters.Dustin;
			case 4:
				return CardDatabase.Characters.KiraKira;
			case 5:
				return CardDatabase.Characters.Felty;
			case 6:
				return CardDatabase.Characters.HowardSensei;
		}

		return CardDatabase.Characters.Hiro;
	}
}
