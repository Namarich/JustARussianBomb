using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CSE_PopUpDialog : CutsceneElementBase
{
    [SerializeField] private TMP_Text popUpText;
    [TextArea] [SerializeField] private string dialogue;
    [SerializeField] private TextPosition textPosition;

    private bool finishedTyping;
    public float charactersPerSecond;

    [SerializeField] private Animator anim;

    private bool isTextActive;

    public bool doesItHaveAPanel = false;

    public GameObject panel;

    public override void Execute()
    {
        StartCoroutine(Wait());
        SetTextPosition();

        anim.Play("FadeIn");
        if (doesItHaveAPanel)
        {
            panel.SetActive(true);
        }
        StartCoroutine(TypeTextUncapped(dialogue));
        isTextActive = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(duration-0.5f);
        anim.Play("FadeOut");
        
        yield return new WaitForSeconds(0.5f);
        if (doesItHaveAPanel)
        {
            panel.SetActive(false);
        }
        cutsceneHandler.PlayNextElement();
    }

    public void SetTextPosition()
    {
        RectTransform rectTransform = popUpText.rectTransform;

        switch (textPosition)
        {
            case TextPosition.Top:
                rectTransform.anchoredPosition = new Vector2(0, 345);
                break;
            case TextPosition.Middle:
                rectTransform.anchoredPosition = new Vector2(0, 0);
                break;
            case TextPosition.Bottom:
                rectTransform.anchoredPosition = new Vector2(0, -313);
                break;
        }
        
    }

    IEnumerator TypeTextUncapped(string line)
    {
        finishedTyping = false;
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;
        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                popUpText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        finishedTyping = true;
    }

}

public enum TextPosition
{
    Top,
    Middle,
    Bottom
}
