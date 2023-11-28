using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogBox;
    public string[] sourceText;
    public GMScript gameManager;
    private Image background;
    public Image tail;

    [SerializeField]
    private float timeBetweenChars;

    void Start()
    {
        background = GetComponent<Image>();
        background.enabled = false;
        tail.enabled = false;
    }

    public void PlayDialog(string[] dialogArray, Transform other)
    {
        if (!background.enabled) background.enabled = true;
        if (!tail.enabled) tail.enabled = true;
        dialogBox.text = "";

        Vector3 otherPos = Camera.main.WorldToScreenPoint(other.position);
        Vector3 dialogBoxPos = otherPos + 100 * Vector3.up;
        transform.position = dialogBoxPos;

        StartCoroutine(TextVisible(dialogArray, other));
    }

    IEnumerator TextVisible(string[] dialogArray, Transform other)
    {
        for (int i = 0; i < dialogArray.Length; i++)
        {
            dialogBox.text = dialogArray[i];
            dialogBox.ForceMeshUpdate();
            int totalVisibleCharacters = dialogBox.textInfo.characterCount;
            int counter = 0;

            while (true)
            {
                int visibleCount = counter % (totalVisibleCharacters + 1);
                dialogBox.maxVisibleCharacters = visibleCount;

                if (visibleCount >= totalVisibleCharacters) break;

                counter += 1;
                if (visibleCount > 0 && (dialogBox.text[visibleCount - 1] == '.' || dialogBox.text[visibleCount - 1] == ',' || dialogBox.text[visibleCount - 1] == '?')) yield return new WaitForSeconds(timeBetweenChars * 5);
                else yield return new WaitForSeconds(timeBetweenChars);
            }
            
            yield return StartCoroutine(WaitForPlayerInput());
        }

        background.enabled = false;
        tail.enabled = false;

        // use switch cases instead?
        // maybe use events so that interaction is controlled from NPCs
        if (other.CompareTag("Boss")) gameManager.GetComponent<SceneManagement>().ChangeToBattleScene();
        else if (other.CompareTag("Quest"))
        {
            var quest = other.gameObject.GetComponent<NPCQuest>();
            if (!quest.soulObject.isAcquired)
            {
                quest.SetSoulObjectActive();
                quest.QuestLogEntry = gameManager.CreateQuestLogEntry(quest.questLogHint[0]);
            } else
            {
                quest.SetQuestComplete();
            }
        }
        else if (other.CompareTag("SoulObject")) other.gameObject.GetComponent<SoulObject>().AcquireSoulObject();
        
        gameManager.SetCurrentState(GMScript.STATE.MOVE);
    }

    IEnumerator WaitForPlayerInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogBox.text = "";
                break;
            }
            yield return null;
        }
    }
}
