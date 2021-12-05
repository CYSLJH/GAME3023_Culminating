using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterOpponentCharacter : ICharacter
{
    public override void TakeTurn(EncounterInstance encounter)
    {
        StartCoroutine(DelayDecision(encounter));
    }

    IEnumerator DelayDecision(EncounterInstance encounter)
    {
        Debug.Log("opponent turn");
        yield return new WaitForSeconds(5.0f);
        encounter.AdvanceTurns();
    }
}
