using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EncounterInstance : MonoBehaviour
{
    [SerializeField]
    EncounterPlayerCharacter player;
    [SerializeField]
    EncounterOpponentCharacter opponent;

    EncounterUnit PlayerUnit;
    EncounterUnit EnemyUnit;

    public EncounterUI playerHUD;
    public EncounterUI enemyHUD;

    public EncounterOpponentCharacter Opponent
    {
        get { return opponent; }
        private set { opponent = value; }
    }

    public UnityEvent<ICharacter> onCharacterTurnBegin;
    public UnityEvent<ICharacter> onCharacterTurnEnd;
    public UnityEvent<EncounterPlayerCharacter> onPlayerTurnBegin;
    public UnityEvent<EncounterPlayerCharacter> onPlayerTurnEnd;

    int turnNumber = 0;

    [SerializeField]
    ICharacter currentCharacterTurn;

    // Start is called before the first frame update
    void Start()
    {
        currentCharacterTurn = player;
        playerHUD.SetHUD(PlayerUnit);
        enemyHUD.SetHUD(EnemyUnit);
        onPlayerTurnBegin.Invoke(player);
    }

    public void AdvanceTurns()
    {
        bool isDead = EnemyUnit.TakeDamage(PlayerUnit.damage);

        enemyHUD.SetHP(EnemyUnit.currentHP);

        onCharacterTurnEnd.Invoke(currentCharacterTurn);

        if(currentCharacterTurn == player)
        {
            onPlayerTurnEnd.Invoke(player);
            currentCharacterTurn = opponent;
        }
        else
        {
            currentCharacterTurn = player;
            onPlayerTurnBegin.Invoke(player);
        }
        turnNumber++;

        onCharacterTurnBegin.Invoke(currentCharacterTurn);
        currentCharacterTurn.TakeTurn(this);
    }

    public void UseAbility()
    {
        if (PlayerUnit.currentMana >= 0)
        {
            PlayerUnit.Heal(PlayerUnit.heal);
            playerHUD.SetMana(PlayerUnit.currentMana);
        }
        else
        {
            playerHUD.SetMana(PlayerUnit.currentMana);
        }
        onCharacterTurnEnd.Invoke(currentCharacterTurn);

        if (currentCharacterTurn == player)
        {
            onPlayerTurnEnd.Invoke(player);
            currentCharacterTurn = opponent;
        }
        else
        {
            currentCharacterTurn = player;
            onPlayerTurnBegin.Invoke(player);
        }
        turnNumber++;

        onCharacterTurnBegin.Invoke(currentCharacterTurn);
        currentCharacterTurn.TakeTurn(this);
    }

    public void EndEncounter()
    {
        //SceneManager.LoadScene("Overworld");

        FindObjectOfType<WorldTraveller>().ExitEncounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
