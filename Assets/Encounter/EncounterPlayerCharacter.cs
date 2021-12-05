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
        opponent = encounter.Opponent;
        Debug.Log("Player turn");
    }

    public void UseAbility(int slot)
    {
        abilities[slot].Cast(this, opponent);
        myEncounter.AdvanceTurns();
    }
}
