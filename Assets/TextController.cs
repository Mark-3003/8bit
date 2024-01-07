using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextController : MonoBehaviour
{
    public TMP_Text text;
    public float talkSpeed;
    public float fadeAwayTime;

    public string currentText;
    public Color textColor;
    public bool refresh;

    float timer;
    int increment;
    private void Awake()
    {
        text = GameObject.Find("FT").GetComponent<TextMeshProUGUI>();
        text.text = "";
    }
    public void SayText(string _text, float fadeTime, float textSpeed, Color _color)
    {
        currentText = _text;
        refresh = true;

        talkSpeed = textSpeed;
        fadeAwayTime = fadeTime;
        textColor = _color;

        text.text = "";
        timer = 0;
        increment = 0;
    }
    private void Update()
    {
        if (refresh)
        {
            text.color = textColor;
            int textLength = currentText.Length;
            float _speed = 1 / talkSpeed;

            timer += Time.deltaTime;
            if (timer >= _speed && increment < textLength)
            {
                timer -= _speed;
                text.text = text.text + currentText[increment];
                increment++;
            }
            if(increment >= textLength)
            {
                refresh = false;
                timer = 0;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= fadeAwayTime)
            {
                text.text = "";
            }
        }
    }
}
