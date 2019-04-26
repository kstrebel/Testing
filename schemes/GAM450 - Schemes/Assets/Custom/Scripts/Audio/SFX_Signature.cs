using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Signature : MonoBehaviour
{
    [SerializeField] private SFX_Gameplay GameplayAudio;

    public void SFX_PlaySchwing()
    {
        GameplayAudio.PlaySchwing();
    }

    public void VO_PlaySignature()
    {
        GameplayAudio.VO_PlaySignature();
    }
}
