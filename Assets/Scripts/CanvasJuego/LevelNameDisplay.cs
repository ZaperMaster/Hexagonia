using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelNameDisplay : MonoBehaviour
{
        public TMP_Text levelNameText;
    void Start()
    {
        if (levelNameText != null)
        {
            levelNameText.text = SceneManager.GetActiveScene().name;
        }
        else
        {
            Debug.LogError("⚠️ ERROR: No se ha asignado un objeto Text en el Inspector.");
        }
    }
}
