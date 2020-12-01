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
    public Button rock;
    public Button paper;
    public Button scissors;
    public Button lizard;
    public Button spok;
    public Button end;
    public  static string playerChoice;
    enum Figures
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spok
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        EndBattle();
    }

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

    private void SendToDatabase()
    {
        User user = new User();
        RestClient.Post("https://rockpapers-466e3.firebaseio.com/.json", user);
    }

    public void GameFlow()
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
        rock.enabled = false;
        paper.enabled = false;
        scissors.enabled = false;
        lizard.enabled = false;
        spok.enabled = false;
        StartCoroutine(Timer());
    }

    public void RockChoose()
    {
        playerChoice = "r";
        paper.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Камень";
        SendToDatabase();
        GameFlow();
    }

    public void PaperChoose()
    {
        playerChoice = "p";
        rock.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Бумага";
        SendToDatabase();
        GameFlow();
    }

    public void ScissorsChoose()
    {
        playerChoice = "sc";
        paper.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Ножницы";
        SendToDatabase();
        GameFlow();
    }

    public void LizardChoose()
    {
        playerChoice = "l";
        paper.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Ящерица";
        SendToDatabase();
        GameFlow();
    }

    public void SpokChoose()
    {
        playerChoice = "sp";
        paper.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Спок";
        SendToDatabase();
        GameFlow();
    }
    public void EndBattle()
    {
        end.gameObject.SetActive(false);
        paper.gameObject.SetActive(true);
        scissors.gameObject.SetActive(true);
        lizard.gameObject.SetActive(true);
        rock.gameObject.SetActive(true);
        spok.gameObject.SetActive(true);
        choiceMenu.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
        rock.enabled = true;
        paper.enabled = true;
        scissors.enabled = true;
        lizard.enabled = true;
        spok.enabled = true;
        figure.text = "Выберете фигуру";
    }

  
}
