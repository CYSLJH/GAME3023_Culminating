using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter : MonoBehaviour
{
    [SerializeField]
    protected Ability[] abilities;

    public abstract void TakeTurn(EncounterInstance encounter);
}
