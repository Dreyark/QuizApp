using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ModifyQuiz : MonoBehaviour
{
    public db db;
    public Dropdown Kategorie, Poprawna;
    public GameObject odpA, odpB, odpC, odpD, Pytanie, AddCategoryInput;

    public void Droplist()
    {
        Kategorie.options.Clear();
        db.PlayButton();
        foreach (var category in db.lista)
        {
            Kategorie.options.Add(new Dropdown.OptionData() { text = category });
        }
    }


    public void AddQuestion()
    {
        string a, b, c, d, pyt;
        a = odpA.GetComponentInChildren<Text>().text;
        b = odpB.GetComponentInChildren<Text>().text;
        c = odpC.GetComponentInChildren<Text>().text;
        d = odpD.GetComponentInChildren<Text>().text;
        pyt = Pytanie.GetComponentInChildren<Text>().text;
        int kategoria, pop;
        pop = Poprawna.value + 1;
        kategoria = Kategorie.value + 1;
        db.AddQuestion(a, b, c, d, pyt, pop, kategoria);
    }


    public void AddCategory()
    {
        string category;
        category = GetComponentInChildren<Text>().text;
        db.AddCategory(category);
    }
}
