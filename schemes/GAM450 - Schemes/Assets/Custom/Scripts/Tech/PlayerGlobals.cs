using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerGlobals
{
    // Each player's selected character
    public static CardDatabase.Characters P1Character;
    public static CardDatabase.Characters P2Character;
    // Who is winning: used to determine which victory screen is displayed
    public static int WinningPlayer = 0;

    public static int P1_Victories = 0;
    public static int P2_Victories = 0;

	public static int Round = 1;

    //Audio Options
    public static float MasterVolume = 1f;
    public static float MusicVolume = 1f;
    public static float VoiceVolume = 1f;
    public static float SFXVolume = 1f;

    public static bool Mute = false;
}
