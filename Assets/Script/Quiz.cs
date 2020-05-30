using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    int points = 0;
    int index = 0;
    float currentTime = 0f;
    float startTime = 30f;
    public GameObject Button1, Button2, Button3, Button4, Pytanie, image, Podpowiedz, scoreboardButton;
    public menu menu;
    public db baza;
    public Text countdownText;
    public string kategoria;
    public string Scoreboard;
    public Text scoreboardText;
    bool isActive = false;
    bool isHintUsed = false;

    List<string> OdpA = new List<string>();
    List<string> OdpB = new List<string>();
    List<string> OdpC = new List<string>();
    List<string> OdpD = new List<string>();
    List<string> Pyt = new List<string>();
    List<int> Pop = new List<int>();
    List<int> numbers = new List<int>();
    List<string> imageSrc = new List<string>();


    public void Start()
    {
        Podpowiedz.SetActive(true);
        Button1.GetComponent<Button>().onClick.RemoveAllListeners();
        Button2.GetComponent<Button>().onClick.RemoveAllListeners();
        Button3.GetComponent<Button>().onClick.RemoveAllListeners();
        Button4.GetComponent<Button>().onClick.RemoveAllListeners();
        Button1.GetComponent<Button>().onClick.AddListener(() => QuizLogic(1));
        Button2.GetComponent<Button>().onClick.AddListener(() => QuizLogic(2));
        Button3.GetComponent<Button>().onClick.AddListener(() => QuizLogic(3));
        Button4.GetComponent<Button>().onClick.AddListener(() => QuizLogic(4));
        points = 0;
        index = 0;
        drawQuestions();
        SetButtons(index);
        currentTime = startTime;
        isActive = true;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if(isActive)
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            QuizLogic(0);
        }
    }

    public void SetQuiz(string odpA, string odpB, string odpC, string odpD, string pyt, int poprawne, string img)
    {
        OdpA.Add(odpA);
        OdpB.Add(odpB);
        OdpC.Add(odpC);
        OdpD.Add(odpD);
        Pyt.Add(pyt);
        Pop.Add(poprawne);
        imageSrc.Add(img);
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
        if (imageSrc[numbers[i]] == "NULL")
        {
            image.GetComponent<Image>().sprite = null;
            Pytanie.GetComponent<RectTransform>().localPosition = new Vector3(0, 550, 0);
            image.SetActive(false);
        }   
        else
        {
            image.SetActive(true);
            image.GetComponent<RectTransform>().localPosition = new Vector3(0, 200, 0);
            Pytanie.GetComponent<RectTransform>().localPosition = new Vector3(0, 600, 0);
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageSrc[numbers[i]]);
        }
    }

    public void QuizHint()
    {
        bool randomValue = true;
        int value = 0;
        while (randomValue) {
            value = UnityEngine.Random.Range(1, 4);
            if (value != Pop[numbers[index]])
            {
                break;
            }
        }
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
        Button4.SetActive(false);
        Transform panelTransform = GameObject.Find("Buttons").transform;
        int j = 1;
        foreach (Transform child in panelTransform)
        {
            if(j == value || j == Pop[numbers[index]])
            {
                child.gameObject.SetActive(true);
            }
            j++;
        }
        isHintUsed = true;

    }


    private void QuizLogic(int value)
    {
        if (isHintUsed)
        {
            Button1.SetActive(true);
            Button2.SetActive(true);
            Button3.SetActive(true);
            Button4.SetActive(true);
        }
        currentTime = startTime;
        if (Pop[numbers[index]] == value)
        {
            points++;
        }
        if (numbers.Count() > ((index)+1))
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
        scoreboardButton.SetActive(true);
        baza.UpdateScoreboard(kategoria, menu.Uzytkownik, points.ToString());
        scoreboardText.text = "Twój wynik to " + points.ToString()+ " pkt";
        Start();
        points = 0;
        index = 0;
        isActive = false;
        currentTime = 30f;
        OdpA.Clear();
        OdpB.Clear();
        OdpC.Clear();
        OdpD.Clear();
        Pyt.Clear();
        Pop.Clear();
        imageSrc.Clear();
        baza.Scoreboard(kategoria);
        menu.ScoreboardButton();
    }
    public void ScoreboardView()
    {
        scoreboardText.text = Scoreboard;
    }
    public void RestartScoreBoard()
    {
        Scoreboard = "";
    }
}