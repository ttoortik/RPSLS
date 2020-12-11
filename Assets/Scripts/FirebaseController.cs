using System;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class FirebaseController : MonoBehaviour
{
    public static FirebaseController Instance;

    private FirebaseApp app;
    private DatabaseReference dbRef;
    public Text figure;
    public Button[] signs;
    public Button end;

    private string dbLink = "https://rockpapers-466e3.firebaseio.com/"; 

    private static int playerID;

    public void BeginFB()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
                InitFB();
            else
                Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        });
    }

    private void InitFB()
    {
        app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl(dbLink);

        dbRef = FirebaseDatabase.DefaultInstance.RootReference.Child("gameroom");

        dbRef.ValueChanged += GameRoomValueChanged;
        RegisterForGame();
    }

    private void RegisterForGame()
    {
        dbRef.Child("player1").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("fail");
                return;
            }

            DataSnapshot snapshot = task.Result;

            if (snapshot.Value.ToString() == "-")
                playerID = 1;
            else
                playerID = 2;

            dbRef.Child("player" + playerID).SetValueAsync("+");
        });
    }

    public void SendChoose(int choose)
    {
        string letter = choose == 0 ? "r" : choose == 1 ? "p" : choose == 2 ? "sc" : choose == 3 ? "l" : "sp";
        dbRef.Child("player" + playerID).SetValueAsync("+");
        dbRef.Child("player" + playerID + "Choice").SetValueAsync(letter);
        foreach (var sign in signs)
            sign.interactable = false;
        end.gameObject.SetActive(true);
    }

    private void GameRoomValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }

        if (e.Snapshot.Child("player1").Value.ToString() != "-" &&
            e.Snapshot.Child("player2").Value.ToString() != "-")
        {
            string p1Choose = e.Snapshot.Child("player1Choice").Value.ToString();
            string p2Choose = e.Snapshot.Child("player2Choice").Value.ToString();

            if (p1Choose != "-" && p2Choose != "-")
            {
                int winnerId = 0;

                switch (p1Choose)
                {
                    case "r":
                        if (p2Choose == "p" || p2Choose == "sp")
                            winnerId = 2;
                        else if (p2Choose == "r")
                            winnerId = 3;
                        else
                            winnerId =1;
                        break;
                    case "p":
                        if (p2Choose == "sc" || p2Choose == "l")
                            winnerId = 2;
                        else if (p2Choose == "p")
                            winnerId = 3;
                        else
                            winnerId = 1;
                        break;
                    case "sc":
                        if (p2Choose == "r" || p2Choose == "sp")
                            winnerId = 2;
                        else if (p2Choose == "sc")
                            winnerId = 3;
                        else
                            winnerId = 1;
                        break;
                    case "l":
                        if (p2Choose == "sc" || p2Choose == "r")
                            winnerId = 2;
                        else if (p2Choose == "l")
                            winnerId = 3;
                        else
                            winnerId =1;
                        break;
                    case "sp":
                        if (p2Choose == "p" || p2Choose == "l")
                            winnerId = 2;
                        else if (p2Choose == "sp")
                            winnerId = 3;
                        else
                            winnerId = 1;
                        break;

                        
                }
                if (winnerId == 3)
                    figure.text = "Получилась ничья :|";
                else if (winnerId == playerID)
                    figure.text = "Вы победили!!! >:)";
                else
                    figure.text = "Вы проиграли :'(";


            }
        }
    }

    public void DeleteInfo()
    {
        dbRef.Child("player" + playerID + "Choice").SetValueAsync("-");
        dbRef.Child("player" + playerID).SetValueAsync("-");
    }
}
    