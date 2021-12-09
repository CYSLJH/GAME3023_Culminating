using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    EncounterInstance encounter;

    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    float secondsPerCharacter = 0.1f;

    [SerializeField]
    GameObject abilityPanel;

    IEnumerator animateTextCoroutineRef = null;

    public Slider hpSlider;

    public Slider Mana;

    public void SetHUD(EncounterUnit unit)
    {
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

        Mana.maxValue = unit.maxMana;
        Mana.value = unit.currentMana;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetMana(int mana)
    {
        Mana.value = mana;
    }

    // Start is called before the first frame update
    void Start()
    {
        animateTextCoroutineRef = AnimateTextCoroutine("You have encountered a: " + "Foo", secondsPerCharacter);

        StartCoroutine(animateTextCoroutineRef);

        //announce who's turn
        encounter.onCharacterTurnBegin.AddListener(AnnounceCharacterTurnBegin);
        //on player turn begin enable ui
        encounter.onPlayerTurnBegin.AddListener(EnablePLayerUI);
        //on player turn end disable ui
        encounter.onPlayerTurnEnd.AddListener(DisablePlayerUI);
    }

    void AnnounceCharacterTurnBegin(ICharacter characterTurn)
    {
        if(animateTextCoroutineRef != null)
        {
            StopCoroutine(animateTextCoroutineRef);
        }

        animateTextCoroutineRef = AnimateTextCoroutine(characterTurn.name + "'s turn", secondsPerCharacter);
        StartCoroutine(animateTextCoroutineRef);
    }

    void EnablePLayerUI(ICharacter characterTurn)
    {
        abilityPanel.SetActive(true);
    }

    void DisablePlayerUI(ICharacter characterTurn)
    {
        abilityPanel.SetActive(false);
    }

    IEnumerator AnimateTextCoroutine(string message, float secondsPerCharacter)
    {
        //abilityPanel.SetActive(false);

        encounterText.text = "";

        for(int currentCharacter = 0; currentCharacter < message.Length; currentCharacter++)
        {
            encounterText.text += message[currentCharacter];

            yield return new WaitForSeconds(this.secondsPerCharacter);
        }

        //abilityPanel.SetActive(true);
        animateTextCoroutineRef = null;
    }
}
