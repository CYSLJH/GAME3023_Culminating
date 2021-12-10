using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create New Enemy")]
public class EnemyBase : ScriptableObject
{
    [SerializeField]
    string name;

    [TextArea]
    [SerializeField]
    string description;

    [SerializeField]
    Sprite sprite;

    [SerializeField]
    EnemyType type;

    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int mana;
}

public enum EnemyType
{
    e1,
    e2,
    e3
}
