using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text endScoreText;
    private float score=0;
    private string display = "Gold : {0}";
    void Awake()
    {
        Instance = this;
        
        startPanel.SetActive(true);
    }
    private void OnEnable()
    {
        PlayerController.GoldTaken += IncreaseScore;
     
    }
    private void OnDestroy()
    {
        PlayerController.GoldTaken -= IncreaseScore;
      
    }

    public void StartGame()
    {
    
        startPanel.SetActive(false);
        gamePanel.SetActive(true);

    }
    public void EndGame(bool isWin)
    {
        gamePanel.SetActive(false);
        if (!isWin) endPanel.SetActive(true);
        else winPanel.SetActive(true);

    }
    private void IncreaseScore()
    {
        score += 1;
        scoreText.text = string.Format(display, score);
    }
   public void MultiplyScore(float multiplier)
    {
        score *= multiplier;
        endScoreText.text = string.Format(display, score);
        Debug.Log("Score : " + score);
        EndGame(true);
    }
}
