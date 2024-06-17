using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LifeAndPointController : MonoBehaviour
{
    [SerializeField] int playerLifes = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI lifesText;
    [SerializeField] TextMeshProUGUI scoresText;
    // Start is called before the first frame update
    void Start()
    {
        lifesText.text = playerLifes.ToString();
        scoresText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoresText.text = score.ToString();
    }
    void takeLife()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        playerLifes--;
        lifesText.text = playerLifes.ToString();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            takeLife();
        }
    }
}
