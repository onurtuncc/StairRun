using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action GoldTaken = delegate { };
    private GameController gameController;
    private StackManager stackManager;
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        stackManager = GetComponent<StackManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gold")
        {
            Destroy(other.gameObject);
            GoldTaken.Invoke();
            Debug.Log("Gold taken by " + other.name);

        }
        else if (other.tag == "Obstacle") gameController.EndGame();
        else if (other.tag == "Finish") gameController.GetToFinishLine(other.transform);
        else if (other.tag == "Stack") stackManager.AddStack(other.gameObject);
        else if (other.tag == "End") gameController.StopPlayer();

    }
    
}
