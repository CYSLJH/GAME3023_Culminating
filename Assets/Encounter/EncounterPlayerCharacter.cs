using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPlayerCharacter : ICharacter
{
   [SerializeField]
    EncounterOpponentCharacter opponent;
    [SerializeField]
    EncounterInstance myEncounter;

    public override void TakeTurn(EncounterInstance encounter)
    {
        myEncounter = encounter;
        Debug.Log("Player turn");
    }
}
