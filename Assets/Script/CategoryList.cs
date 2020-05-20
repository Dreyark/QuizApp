using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryList : MonoBehaviour
{
    public GameObject buttonTemplate;
    int lastLen = 0;
    public void CreateList(int len, List<string> name)
    {

        for (int i = 1; i <= len; i++)
        {
            lastLen = len;
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonList>().setText(name[i-1]);
            //button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(button.GetComponent<Button>()));
            //button.GetComponent<ButtonList>().OnClick(name[i - 1]);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

    }

    public void removeList()
    {
        //Button[] buttons = 
        //for (int i = 1; i <= lastLen; i++)
        //{
        //    Button buttons = GetComponentInChildren<Button>();
        //    Destroy(buttons);
        //}
        Transform panelTransform = GameObject.Find("ButtonListContent").transform;
        int i = 0;
        foreach (Transform child in panelTransform)
        {
            if (i != 0)
            {
                Destroy(child.gameObject);
            }
            i += 1;
        }
    }

    //public void OnButtonClick(string category)
    //{
    //    Debug.Log(category);
    //    //db.Search_function(category);
    //}

}
