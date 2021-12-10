using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class EncounterInstance : MonoBehaviour
{

    public GameObject Player;
    public GameObject Enemy;

    public Text dialogueText;

    EncounterUnit PlayerUnit;
    EncounterUnit EnemyUnit;

    public EncounterUI playerHUD;
    public EncounterUI enemyHUD;

    public BattleState state;

  

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
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        playerHUD.SetHUD(PlayerUnit);
        enemyHUD.SetHUD(EnemyUnit);
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(Player);
        PlayerUnit = playerGO.GetComponent<EncounterUnit>();

        GameObject enemyGO = Instantiate(Enemy);
        EnemyUnit = enemyGO.GetComponent<EncounterUnit>();

        dialogueText.text = EnemyUnit.unitName + " Challanges You!";

        playerHUD.SetHUD(PlayerUnit);
        enemyHUD.SetHUD(EnemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "What will you do?";
    }

    public void AdvanceTurns()
    {
        bool isDead = EnemyUnit.TakeDamage(PlayerUnit.damage);

        enemyHUD.SetHP(EnemyUnit.currentHP);

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
