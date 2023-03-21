using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugController : MonoBehaviour {

    public bool sandboxing = false;
    public Color colour;

    private bool showConsole;
    private bool showHelp;

    private string input;

    public static DebugCommand DestroyAll;
    public static DebugCommand KillAll;
    public static DebugCommand<int> SetHp;
    public static DebugCommand<int> SetBeeCoin;
    public static DebugCommand<int> SetWave;
    public static DebugCommand Help;

    public List<object> commandList;

    public GameObject consoleUI;
    public TMP_InputField inputField;

    public void ToggleConsole() {

        showConsole = !showConsole;

    }

    void Update() {

        if (showConsole && sandboxing) {

            consoleUI.SetActive(true);

        } else {

            consoleUI.SetActive(false);

        }

    }

    void Awake() {

        DestroyAll = new DebugCommand("Destroy_All", "Deletes all enemies. Will not trigger loot drops", "Destroy_All", () => {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach ( GameObject e in enemies ) {

                e.GetComponent<EnemyController>().hp = 0;

            }
        
        });

        KillAll = new DebugCommand("Kill_All", "Kills all enemies.", "Kill_All", () => {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject e in enemies)
            {

                Destroy(e.transform.parent.gameObject);

            }

        });

        SetBeeCoin = new DebugCommand<int>("Set_Bee_Coins", "Set your bee coins to desired input", "Set_Bee_Coins <coins>", (x) => { 
        
            GetComponent<GameManager>().coin = x;

        });

        SetHp = new DebugCommand<int>("Set_Hp", "Set your hp to desired input", "Set_Hp <hp>", (x) => {

            GetComponent<GameManager>().hp = x;

        });

        SetWave = new DebugCommand<int>("Set_Wave", "Set wave number, affects newly spawned enemy strength", "Set_Wave <wave>", (x) => {

            GameObject.Find("Spawn").GetComponent<Spawner>().wave = x;

        });

        Help = new DebugCommand("Help", "Show this message", "Help", () => {

            showHelp = true;

        });

        // ---------

        commandList = new List<object> {

            DestroyAll,
            SetBeeCoin,
            SetHp,
            SetWave,
            Help

        };
    }

   /* void OnGUI() {

        if (!showConsole)
            return;

        float y = 0f;

        if (showHelp) {

            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            y += 100;

        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = colour;

        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 60f), input);

    }*/

    public void SetInput(string text) {

        input = text;

    }

    public void HandleInput() {

        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++) {

            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandID)) {

                if (commandList[i] as DebugCommand != null) {

                    (commandList[i] as DebugCommand).Invoke();

                    inputField.text = "";

                } else if (commandList[i] as DebugCommand<int> != null) {

                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));

                    inputField.text = "";

                }

            }

        }

    }
}
