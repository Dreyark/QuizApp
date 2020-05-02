using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonList : MonoBehaviour
{
    public Text myText;
    public db db;

    public void setText(string textString)
    {
        myText.text = textString;
    }

    public void OnClick()
    {
        db.Search_function(myText.text);
    }
}
