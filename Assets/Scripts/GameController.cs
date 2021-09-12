using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [SerializeField] private Movement playerMovement;
    

    public void StartGame()
    {
        UIManager.Instance.StartGame();
        playerMovement.GetComponentInChildren<Animator>().SetBool("isStarted", true);
        playerMovement.enabled = true;
    }
    
    
    public void WonGame(float multiply)
    {
        StopPlayer();
        playerMovement.GetComponentInChildren<Animator>().SetTrigger("winTrigger");
        UIManager.Instance.MultiplyScore(multiply);
        UIManager.Instance.EndGame(true);
    }
   
    public void EndGame()
    {
        StopPlayer();
        UIManager.Instance.EndGame(false);
    }
    public void StopPlayer()
    {
        playerMovement.enabled = false;
        playerMovement.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public void GetToFinishLine(Transform finishLine)
    {
        StopPlayer();
        finishLine.tag = "Untagged";
        FindObjectOfType<StackManager>().CreateFinishLine(finishLine);
        playerMovement.enabled = true;
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
