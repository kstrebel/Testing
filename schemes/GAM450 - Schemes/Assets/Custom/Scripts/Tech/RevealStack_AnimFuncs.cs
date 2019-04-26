using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealStack_AnimFuncs : MonoBehaviour
{
    RevealPhaseLogic revealLog;

	ParticleSystem BlockerPS;
	ParticleSystem AttackerPS;
	ParticleSystem SpellPS;

    [SerializeField] private SFX_Gameplay GameplayAudio;

    // Start is called before the first frame update
    void Start()
    {
        revealLog = GameObject.Find("Game").GetComponent<RevealPhaseLogic>();
		BlockerPS = GameObject.Find("Blockers Clash PS").GetComponent<ParticleSystem>();
		AttackerPS = GameObject.Find("Attackers Clash PS").GetComponent<ParticleSystem>();
		SpellPS = GameObject.Find("Spells Clash PS").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clash()
    {
        revealLog.ClashDamage();

		PlayParticles();
    }   

    public void Dead(int num)
    {
        revealLog.CardHasDied();
        
        // if(num == 1)
        // {
        //     if(transform.Find("Player 1 Pos").GetChild(0) != null)
        //     {
        //         // Destroy(transform.Find("Player 1 Pos").GetChild(0).gameObject);
        //     }
            
        // }
        // else if(num == 2)
        // {
        //     if(transform.Find("Player 2 Pos").GetChild(0) != null)
        //     {
        //         // Destroy(transform.Find("Player 2 Pos").GetChild(0).gameObject);
        //     }
        // }
    }

    public void StartDeathParticles(int num)
    {
        
    }

	void PlayParticles()
	{
		if(revealLog.P1CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Attacker)
		{
			if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Attacker)
			{
				//Attacker PS
				AttackerPS.Play();
			}
			else if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Defender)
			{
				// Defender PS
				BlockerPS.Play();
			}
			else if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Spell)
			{
				// Attacker PS
				AttackerPS.Play();
			}		
		}
		else if(revealLog.P1CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Defender)
		{
			if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Attacker)
			{
				// Defender PS
				BlockerPS.Play();
			}
			else if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Defender)
			{
				// Defender PS
				BlockerPS.Play();
			}
			else if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Spell)
			{
				// Spell PS
				SpellPS.Play();
			}
		}
		else if(revealLog.P1CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Spell)
		{
			if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Attacker)
			{
				// Attacker PS
				AttackerPS.Play();
			}
			else if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Defender)
			{
				// Spell PS
				SpellPS.Play();
			}
			else if(revealLog.P2CurrentCard.WhatCardAmI.Type == CardDatabase.CardTypes.Spell)
			{
				// Spell PS
				SpellPS.Play();
			}
		}

		

	}

    public void SFX_AnticipateDamage()  //clash animation beginning
    {
        GameplayAudio.VO_ChangeRevealStage(SFX_Gameplay.RevealStage.DamageAnticipate);
    }

    public void SFX_CardClash()  //clash animation when cards hit
    {
        GameplayAudio.VO_ChangeRevealStage(SFX_Gameplay.RevealStage.Collide);

        GameplayAudio.SFX_CardImpact();
    }

    public void SFX_GloatAboutDamage()  //clash animation end
    {
        GameplayAudio.VO_ChangeRevealStage(SFX_Gameplay.RevealStage.Gloat);
    }
}