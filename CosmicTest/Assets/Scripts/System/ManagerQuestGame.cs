using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ManagerQuestGame : Singleton<ManagerQuestGame>
{
    [SerializeField] Text textQuest;
    [SerializeField] Text textLifes;
    [SerializeField] Quest[] questions;
    [SerializeField] List<Button> buttons = new List<Button>();
    [SerializeField] string[] enumNum;
    public bool startGame;
    List<string> answersListProvisional = new List<string>();
    List<int> takeList;
    ColorBlock colors;
    int numOfAttemps = 2;
    int numRandom;
    Quest currentQuest;
    public GameObject bodyPanel;
    public GameObject observationsPanel;
    public GameObject errorPanel;
    public GameObject panelSelect;
    public int index;
    public GameObject Crafteo;
    public GameObject point;


    void Start()
    {
        End();
    }

    public void Update()
    {
        if (startGame)
        {
            textLifes.text = "Intentos: " + numOfAttemps;
        }
    }

    public void ChangeStateGame()
    {
        startGame = !startGame;
    }

    public void ChangeStateBoxQuest()
    {
        bool validation = bodyPanel.activeInHierarchy;
        bodyPanel.SetActive(!validation);

        if (bodyPanel.activeInHierarchy)
        {
            startGame = true;
        }
    }

    public IEnumerator GetQuestion()
    {
        if (!bodyPanel.activeInHierarchy)
        {
            ChangeStateBoxQuest();
        }
        ResetButtons();
        currentQuest = questions[index];
        answersListProvisional = currentQuest.answers.ToList<string>();
        RandomButton();
        textQuest.text = currentQuest.fact;
        index++;
        yield return null;
    }

    public void RandomButton()
    {
        takeList = new List<int>(new int[buttons.Count]);
        for (int i = 0; i < buttons.Count; i++)
        {
            numRandom = UnityEngine.Random.Range(1, buttons.Count + 1);
            while (takeList.Contains(numRandom))
            {
                numRandom = UnityEngine.Random.Range(1, buttons.Count + 1);
            }

            takeList[i] = numRandom;
            string test = answersListProvisional[takeList[i] - 1];
            int index = answersListProvisional.IndexOf(test);
            buttons[i].GetComponentInChildren<Text>().text = (enumNum[i] + ". " + test);
            buttons[i].onClick.AddListener(() => EventClickButton(index));
            colors = buttons[i].colors;
            colors.highlightedColor = new Color32(36, 96, 214, 255);

            if (index.Equals(currentQuest.trueAnswer))
            {
                colors.pressedColor = new Color32(0, 255, 0, 255);
                buttons[i].colors = colors;
            }
            else
            {
                colors.pressedColor = new Color32(255, 0, 0, 255);
                buttons[i].colors = colors;
            }
        }
    }

    public void EventClickButton(int indexButton)
    {
        if (indexButton.Equals(currentQuest.trueAnswer))
        {
            currentQuest.answeredWell = true;
            Validate();
        }
        else
        {
            numOfAttemps--;
            Debug.Log(" intentos " + numOfAttemps);

            if (numOfAttemps < 1)
            {
                errorPanel.SetActive(true);
                errorPanel.GetComponentInChildren<Text>().text = ("La Respuesta Correcta Es: " + answersListProvisional[currentQuest.trueAnswer]);
                Validate();
            }
        }
    }

    public void ResetButtons()
    {
        textQuest.text = "";
        numOfAttemps = 3;

        for (int i = 0; i<buttons.Count; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = "";
            buttons[i].onClick.RemoveAllListeners();
        }   
    }


    public void End()
    {
        ResetButtons();
        startGame = false;
        textLifes.text = "";
        answersListProvisional.Clear();
        numOfAttemps = 2;
        index = 0;
        numRandom = 0;
        currentQuest = null ;
        errorPanel.SetActive(false);
        bodyPanel.SetActive(false);
        observationsPanel.SetActive(false);
    }

    private void Validate()
    {
        if (index == questions.Length)
        {
            observationsPanel.SetActive(true);
            Qualify();
            bodyPanel.SetActive(false);
        }
        else
        {
            StartCoroutine(GetQuestion());
        }
    }

    public void Qualify()
    {
        float num = 0;

        for (int i = 0; i < questions.Length; i++)
        {
            if (questions[i].answeredWell)
            {
                num++;
            }
        }


        if (num > (questions.Length/ 2))
        {
            observationsPanel.GetComponentInChildren<Text>().text = " Pocion creada con exito";
            Instantiate(Crafteo, point.transform.position, Quaternion.identity, transform);
        }
        else
        {
            observationsPanel.GetComponentInChildren<Text>().text = "Desperdicio de todos los materiales ";
            ManagerGameLevelBasic.Instance.potion.SetActive(true);
        }

    }



}
