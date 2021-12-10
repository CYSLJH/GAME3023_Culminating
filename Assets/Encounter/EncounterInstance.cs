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

    IEnumerator AdvanceTurns()
    {
        bool isDead = EnemyUnit.TakeDamage(PlayerUnit.damage);

        enemyHUD.SetHP(EnemyUnit.currentHP);
        dialogueText.text = "Successful Hit!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }
    IEnumerator EnemyTurn()
    {
        dialogueText.text = "The enemy just stands there, menacingly...";
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You Won!";
            yield return new WaitForSeconds(2f);
            StartCoroutine("LoadLevel");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You Lost! Game Over!";
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void onAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(AdvanceTurns());
    }

    public void onBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerBlock());
    }

    IEnumerator PlayerBlock()
    {
        dialogueText.text = "You Blocked! No Damage Taken!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


    IEnumerator UseAbility()
    {
        if (PlayerUnit.currentMana >= 0)
        {
            dialogueText.text = "You Healed!";
            PlayerUnit.Heal(PlayerUnit.heal);
            playerHUD.SetMana(PlayerUnit.currentMana);
        }
        else
        {
            dialogueText.text = "Not Enough Mana!";
            playerHUD.SetMana(PlayerUnit.currentMana);
        }
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void onHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(UseAbility());
    }

    public void EndEncounter()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        dialogueText.text = "You Fled The Battle!";
        FindObjectOfType<WorldTraveller>().ExitEncounter();
    }

}
