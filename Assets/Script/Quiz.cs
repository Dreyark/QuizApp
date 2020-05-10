using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    int points = 0;
    int index = 0;
    public GameObject Button1, Button2, Button3, Button4, Pytanie;

    List<string> OdpA = new List<string>();
    List<string> OdpB = new List<string>();
    List<string> OdpC = new List<string>();
    List<string> OdpD = new List<string>();
    List<string> Pyt = new List<string>();
    List<int> Pop = new List<int>();
    List<int> numbers = new List<int>();

    private void Start()
    {
        Button1.GetComponent<Button>().onClick.AddListener(() => QuizLogic(1));
        Button2.GetComponent<Button>().onClick.AddListener(() => QuizLogic(2));
        Button3.GetComponent<Button>().onClick.AddListener(() => QuizLogic(3));
        Button4.GetComponent<Button>().onClick.AddListener(() => QuizLogic(4));
        drawQuestions();
        SetButtons(index);
    }
    public void SetQuiz(string odpA, string odpB, string odpC, string odpD, string pyt, int poprawne)
    {
        OdpA.Add(odpA);
        OdpB.Add(odpB);
        OdpC.Add(odpC);
        OdpD.Add(odpD);
        Pyt.Add(pyt);
        Pop.Add(poprawne);
    }

    private void drawQuestions()
    {
        numbers = new List<int>();
        if (OdpA.Count() > 10)
        {
            for (int i = 0; i < 10;)
            {
                int val = UnityEngine.Random.Range(0, OdpA.Count());
                if (!numbers.Contains(val))
                {
                    numbers.Add(val);
                    i++;
                }
            }
        }
        else
        {
            for (int i = 0; i < OdpA.Count(); i++)
            {
                numbers.Add(i);
            }
        }
    }

    private void SetButtons(int i)
    {
        Button1.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpA[numbers[i]];
        Button2.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpB[numbers[i]];
        Button3.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpC[numbers[i]];
        Button4.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpD[numbers[i]];
        Pytanie.GetComponent<Text>().text = Pyt[numbers[i]];
    }


    private void QuizLogic(int value)
    {
        if(Pop[numbers[index]] == value)
        {
            points++;
        }
        if(Pop.Count()>= (index+2))
        {
            index++;
            SetButtons(index);
        }
        else
        {
            ResultScreen();
        }
    }

    private void ResultScreen()
    {
        throw new NotImplementedException();
    }
}
