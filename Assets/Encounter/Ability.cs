using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]

public class Ability : ScriptableObject
{
    [SerializeField]
    new string name;

    [SerializeField]
    string description;

    [SerializeField]
    IEffect[] effects;

    public void Cast(ICharacter self, ICharacter other)
    {
        Debug.Log("used: " + name);
    }
}
