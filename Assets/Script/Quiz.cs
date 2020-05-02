using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    int points = 0;
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
        LosowaniePytan();
        SetButtons();
    }
    public void SetQuiz(string odpA, string odpB, string odpC, string odpD, string pyt, int poprawne)
    {
        OdpA.Add(odpA);
        OdpB.Add(odpB);
        OdpC.Add(odpC);
        OdpD.Add(odpD);
        Pyt.Add(pyt);
        Pop.Add(poprawne); // OD 1 DO 4
    }

    private void LosowaniePytan()
    {
        numbers = new List<int>();
        if (OdpA.Count() > 10)
        {
            for (int i = 0; i < 10;)
            {
                Debug.Log(OdpA.Count());
                int val = Random.Range(0, OdpA.Count());
                if (!numbers.Contains(val))
                {
                    numbers.Add(val);
                    i++;
                    Debug.Log(val);
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

    private void SetButtons()
    {
        Button1.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpA[numbers[0]];
        Button2.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpB[numbers[0]];
        Button3.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpC[numbers[0]];
        Button4.GetComponent<Button>().GetComponentInChildren<Text>().text = OdpD[numbers[0]];
        Pytanie.GetComponent<Text>().text = Pyt[numbers[0]];
    }

    private void QuizLogic()
    {

    }
}
