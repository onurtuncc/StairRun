using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ground;

    private float totalDistance;

    private void Awake()
    {
        totalDistance = ground.transform.localScale.z * 5;
    }

    private void Update()
    {
        progressBar.fillAmount = player.transform.position.z / totalDistance;
    }
}
