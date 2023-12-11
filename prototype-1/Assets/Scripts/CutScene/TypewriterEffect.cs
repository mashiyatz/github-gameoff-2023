using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
	Text _text;
	TMP_Text _tmpProText;
	string writer;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

	[SerializeField] bool isNextText = false;
	[SerializeField] bool doReset = false;
	[SerializeField] GameObject[] nextTextbox;

	public bool isFinishedWriting = true;
	[SerializeField]
	private AK.Wwise.Event textSound;

	// Use this for initialization

	private void Awake()
    {
/*		_text = GetComponent<Text>()!;
		_tmpProText = GetComponent<TMP_Text>()!;*/
	}

    void Start()
	{
		_text = GetComponent<Text>()!;
		_tmpProText = GetComponent<TMP_Text>()!;

		if (_text != null)
		{
			writer = _text.text;
			_text.text = "";

			StartCoroutine("TypeWriterText");
		}

		if (_tmpProText != null)
		{
			writer = _tmpProText.text;
			_tmpProText.text = "";

			StartCoroutine("TypeWriterTMP");
		}
	}

	IEnumerator TypeWriterText()
	{
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (_text.text.Length > 0)
			{
				_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
			}
			_text.text += c;
			_text.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
		}
	}

	IEnumerator TypeWriterTMP()
	{
		_tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		textSound.Post(gameObject);
		foreach (char c in writer)
		{
			if (_tmpProText.text.Length > 0)
			{
				_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
			}
			_tmpProText.text += c;
			_tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
		}

		if (isNextText)
        {
			foreach (GameObject go in nextTextbox)
            {
				go.SetActive(true);
            }
        }

		isFinishedWriting = true;
		textSound.Stop(gameObject);
	}

    private void OnEnable()
    {
        if (doReset && _tmpProText != null)
        {
			writer = _tmpProText.text;
			_tmpProText.text = "";

			StartCoroutine("TypeWriterTMP");
		}
    }

    public void Reset()
    {
		writer = _tmpProText.text;
		_tmpProText.text = "";

		StartCoroutine("TypeWriterTMP");
	}

	public void Write(string phrase)
    {
		StopCoroutine("TypeWriterTMP");
		isFinishedWriting = false;
		writer = phrase;
		_tmpProText.text = "";
		StartCoroutine("TypeWriterTMP");
	}
}
