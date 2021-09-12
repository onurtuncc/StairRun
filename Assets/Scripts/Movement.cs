using UnityEngine;


public class Movement : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField]
    private float forwardSpeed=400f;
    private float startMoveTime = 1f;
    private float gravityScale = 5f;
    public bool isFalling=false;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

   
    void FixedUpdate()
    {
        if (Time.time < startMoveTime) return;
        if (isFalling) gravityScale = Mathf.Lerp(0,5f,1.5f);
        else gravityScale = 0f;
        playerRb.velocity = new Vector3(playerRb.velocity.x, -gravityScale, forwardSpeed);
        
    }
   

    
}
