using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
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
        Vector3 dialogBoxPos = otherPos + 200 * Vector3.up;
        transform.position = dialogBoxPos;

        if (other.CompareTag("Boss")) StartCoroutine(TextVisible(dialogArray, true));
        else StartCoroutine(TextVisible(dialogArray));
    }

    IEnumerator TextVisible(string[] dialogArray, bool doesBattleTrigger = false)
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

        if (doesBattleTrigger) { print("checking"); gameManager.GetComponent<SceneManagement>().ChangeToBattleScene(); }
        else gameManager.SetCurrentState(GMScript.STATE.MOVE);
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
