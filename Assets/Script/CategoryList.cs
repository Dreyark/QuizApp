using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryList : MonoBehaviour
{
    public GameObject buttonTemplate;
    public void CreateList(int len, List<string> name)
    {

        for (int i = 1; i <= len; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonList>().setText(name[i-1]);
            //button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(button.GetComponent<Button>()));
            //button.GetComponent<ButtonList>().OnClick(name[i - 1]);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

    }

    //public void OnButtonClick(string category)
    //{
    //    Debug.Log(category);
    //    //db.Search_function(category);
    //}

}
