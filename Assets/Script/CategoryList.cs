using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

    }

}
