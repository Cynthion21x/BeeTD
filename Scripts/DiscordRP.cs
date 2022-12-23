using UnityEngine;
using Discord;
using System;
using UnityEngine.SceneManagement;

public class DiscordRP : MonoBehaviour {
    private static DiscordRP Instance;

    public Discord.Discord discord;
    public Discord.ActivityManager rp;

    public String Details;
    public String State;
    public long time;

    void Awake()
    {

        DontDestroyOnLoad(this);

        if (Instance == null)
        {

            Instance = this;

        }
        else
        {

            Destroy(this);

        }


    }

    private void Start()
    {
        discord = new Discord.Discord(1016345793852100719, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        rp = discord.GetActivityManager();

        time = DateTimeOffset.Now.ToUnixTimeSeconds();

        rp.ClearActivity((result) => {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Cleared");
            }
            else
            {
                Debug.LogError("Cleared");
            }
        });
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Details = "In Menu";
            State = null;
        }
        else
        {

            Details = GameObject.FindGameObjectWithTag("GameControl").GetComponent<Powers>().power;

            State = "Wave: " + GameObject.Find("Spawn").GetComponent<Spawner>().Wave;
        }

        discord.RunCallbacks();

        var activity = new Discord.Activity
        {
            State = State,
            Details = Details,

            Timestamps = {
                Start = time,
            },

            Assets = {
                  LargeImage = "icon",
                  LargeText = "Its a bee"
             },

            Instance = true,
        };

        rp.UpdateActivity(activity, (result) => {
            if (result == Discord.Result.Ok)
            {
                //Debug.Log("RP Loaded");
            }
            else
            {
                Debug.LogError("Failed to load RP");
            }
        });

    }
}
