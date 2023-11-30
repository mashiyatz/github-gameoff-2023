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

    public void PlayDialog(List<string> dialogList, Transform other)
    {
        if (!background.enabled) background.enabled = true;
        if (!tail.enabled) tail.enabled = true;
        dialogBox.text = "";

        Vector3 otherPos = Camera.main.WorldToScreenPoint(other.position);
        Vector3 dialogBoxPos = otherPos + 100 * Vector3.up;
        transform.position = dialogBoxPos;

        StartCoroutine(TextVisible(dialogList, other));
    }

    IEnumerator TextVisible(List<string> dialogList, Transform other)
    {
        for (int i = 0; i < dialogList.Count; i++)
        {
            dialogBox.text = dialogList[i];
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

        InteractionResult(other);

        print("Might be a problem later if people can move before decision is made.");
        gameManager.SetCurrentState(GMScript.STATE.MOVE);
    }

    void InteractionResult(Transform conversationTarget)
    {
        if (conversationTarget.CompareTag("Boss")) gameManager.GetComponent<SceneManagement>().ChangeToBattleScene();
        else if (conversationTarget.CompareTag("Quest"))
        {
            var quest = conversationTarget.gameObject.GetComponent<NPCQuest>();

            if (quest.isQuestCompleted) return;

            if (!quest.soulObject.isAcquired)
            {
                quest.SetSoulObjectActive();
                quest.QuestLogEntry = gameManager.CreateQuestLogEntry(quest.questLogHint[0]);
            }
            else 
            {
                StartCoroutine(quest.DecideOnSoulCapture());
            }
        }
        else if (conversationTarget.CompareTag("SoulObject")) conversationTarget.gameObject.GetComponent<SoulObject>().AcquireSoulObject();
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
