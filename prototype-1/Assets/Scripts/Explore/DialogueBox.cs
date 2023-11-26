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

    [SerializeField]
    private float timeBetweenChars;

    void Start()
    {
        background = GetComponent<Image>();
        background.enabled = false;
    }

    public void PlayDialog(string[] dialogArray)
    {
        if (!background.enabled) background.enabled = true;
        dialogBox.text = "";
        // dialogBox.text = dialogArray[0];
        StartCoroutine(TextVisible(dialogArray));
    }

    IEnumerator TextVisible(string[] dialogArray)
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
