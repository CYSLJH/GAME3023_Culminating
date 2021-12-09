using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterUnit : MonoBehaviour
{
    public string unitName;

    public int damage;
    public int heal;

    public int maxHP;
    public int currentHP;

    public int maxMana;
    public int currentMana;


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool Heal(int heal)
    {
        currentMana -= heal;

        if (currentMana <= 0)
            return true;
        else
            return false;
    }
}
