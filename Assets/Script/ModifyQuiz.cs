using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.Data;
using UnityEngine.Assertions.Must;

public class ModifyQuiz : MonoBehaviour
{
    public db db;
    public Dropdown Kategorie, Poprawna;
    public InputField odpA, odpB, odpC, odpD, Pytanie, AddCategoryInput;
    public GameObject categoryAlert, QuestionAlert;
    public menu menu;

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
        a = odpA.text;
        b = odpB.text;
        c = odpC.text;
        d = odpD.text;
        pyt = Pytanie.text;
        int kategoria, pop;
        pop = Poprawna.value + 1;
        kategoria = Kategorie.value + 1;
        if (a == "" || b == "" || c == "" || d == "" || pyt == "")
        {
            QuestionAlert.GetComponent<Text>().text = "Pola nie mogą być puste";
            QuestionAlert.SetActive(true);
        }
        else if (a.Contains("'") || a.Contains(Char.ConvertFromUtf32(34)) || b.Contains("'") || b.Contains(Char.ConvertFromUtf32(34)) || c.Contains("'") || c.Contains(Char.ConvertFromUtf32(34)) || d.Contains("'") || d.Contains(Char.ConvertFromUtf32(34)) ||
            pyt.Contains("'") || pyt.Contains(Char.ConvertFromUtf32(34)))
        {
            QuestionAlert.GetComponent<Text>().text = "Pola nie mogą zawierać znaków takich jak: ' " + Char.ConvertFromUtf32(34);
            QuestionAlert.SetActive(true);
        }
        else
        {
            db.AddQuestion(a, b, c, d, pyt, pop, kategoria);
            menu.Menu();
        }
    }

    public void AddCategory()
    {
        string category;
        category = GetComponentInChildren<Text>().text;
        if (category.Contains("'") || category.Contains(Char.ConvertFromUtf32(34)))
        {
            categoryAlert.GetComponent<Text>().text = "Pole nie może zawierać znaków takich jak: ' " + Char.ConvertFromUtf32(34);
            categoryAlert.SetActive(true);
        }
        else if (category == "")
        {
            categoryAlert.GetComponent<Text>().text = "Pole nie może być puste";
            categoryAlert.SetActive(true);
        }
        else
        {
            db.AddCategory(category);
            menu.Menu();
        }
    }
}
