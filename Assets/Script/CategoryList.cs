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
        try { removeList(); 
        }
        catch
        {

        }
        for (int i = 1; i <= len; i++)
        {
            lastLen = len;
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonList>().setText(name[i-1]);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

    }

    public void removeList()
    {
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
}
