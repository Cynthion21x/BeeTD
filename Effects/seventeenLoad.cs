using UnityEngine;
public class seventeenLoad : MonoBehaviour {
    public GameObject seventeenEffect;
    void Awake() {
        if (PlayerPrefs.HasKey("LoadCount")) {
            PlayerPrefs.SetInt("LoadCount",
                PlayerPrefs.GetInt("LoadCount") + 1
                ); Debug.Log(PlayerPrefs.GetInt("LoadCount"));
        } else {
            PlayerPrefs.SetInt("LoadCount", 1);
        }
        if (PlayerPrefs.GetInt("LoadCount") == 17) {
            seventeenEffect.SetActive(true);
        }           
    }
}
