using System;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static string playerName;
    public InputField nameText;
    public Button fight;
    public Canvas main;
    public Canvas choiceMenu;
    public Text error;
    public Text gameName;
    public Text figure;
    public Button end;
    public  static string playerChoice;
    public bool IsStarted;
    public Button[] signs;
    string[] Figures = new string[]
    {
        "Камень",
        "Бумага",
        "Ножницы",
        "Ящерица",
        "Спок"
    };


    public void OnSubmit()
    {
        if (nameText.text == "")
        {
            error.gameObject.SetActive(true);

        }
        else
        {
            playerName = nameText.text;
            error.gameObject.SetActive(false);
            main.gameObject.SetActive(false);
            choiceMenu.gameObject.SetActive(true);
            gameName.text = "Боец: "+ playerName;
        }
    }

   /* private void SendToDatabase()
    {
        User user = new User();
        RestClient.Post("https://rockpapers-466e3.firebaseio.com/.json", user);
    }*/

    /*public void GameFlow()
    {
        Random bot = new Random() ;
        int botChoice = bot.Next(0,4);
        switch (botChoice)
        {
            case 0:
                if (playerChoice == "p" || playerChoice == "sp")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "r")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                rock.enabled=false;
                break;
            case 1:
                if (playerChoice == "sc" || playerChoice == "l")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "p")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                paper.enabled = false;
                break;
            case 2:
                if (playerChoice == "r" || playerChoice == "sp")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "sc")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                scissors.enabled = false;
                break;
            case 3:
                if (playerChoice == "sc" || playerChoice == "r")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "l")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                lizard.enabled = false;
                break;
            case 4:
                if (playerChoice == "p" || playerChoice == "l")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "sp")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                spok.enabled = false;
                break;

        }
        StartCoroutine(Timer());
    }*/
    public void SendChoose(int choice)
    {
        FirebaseController.Instance.SendChoose(choice);
        figure.text ="Ваша фигура: "+ Figures[choice] +"\nОжидание противника...";
        foreach (var sign in signs)
        {
            sign.gameObject.SetActive(false);
            signs[choice].gameObject.SetActive(true);
        }
    }

    public void EndBattle()
    {
        end.gameObject.SetActive(false);
        foreach(Button sign in signs)
            sign.gameObject.SetActive(true);
        choiceMenu.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
        foreach (var sign in signs)
            sign.interactable = true;
        figure.text = "Выберете фигуру";
    }


}
