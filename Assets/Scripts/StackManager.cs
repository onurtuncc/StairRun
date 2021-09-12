using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] private Transform backPack;
    private Movement playerMovement;
    Stack<GameObject> tileStack=new Stack<GameObject>();
    public static event Action<float> OnMultiply = delegate { };
    private float yValue=0;
    private float yDistance = 0.33f;
    private float zDistance = 0.5f;
    private float tileHeight = 0.3f;
    private float refreshTime = 0.2f;
    private float timer = 0f;
    private float dropZ = 0f;
    float multiplier = 1;
    private bool atEnd = false;
    int i = 0;
    
    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
    }
    private void Update()
    {

        if (Input.GetMouseButton(0) && !atEnd)
        {
            dropZ = transform.position.z;
            playerMovement.isFalling = false;
            timer += Time.deltaTime;
            if (timer > refreshTime)
            {
                Debug.Log("Drop stack");
                DropStack();
                timer = 0f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            dropZ = 0;
            playerMovement.isFalling = true;
        }
    }
    public void AddStack(GameObject go)
    {
        go.tag = "Untagged";
        go.transform.rotation = Quaternion.identity;
        tileStack.Push(go);
        go.transform.SetParent(backPack);
        go.transform.localPosition = new Vector3(0, yValue,i%2*zDistance);
        yValue += i%2 *yDistance;
        i++;
    }
    private void DropStack()
    {
        if(tileStack.Count > 0)
        {
            var go = tileStack.Pop();
            SetAsStair(go);
            transform.position += new Vector3(0, tileHeight*1.5f, 0);
            yValue -= i % 2 * yDistance;
            i--;
            dropZ+=zDistance;
      
        }
        else
        {
            Debug.Log("Stack empty");
        }

    }
    private void SetAsStair(GameObject go)
    {
        go.GetComponent<BoxCollider>().isTrigger = false;
        go.transform.parent = null;
        go.transform.localScale += new Vector3(1, 0, 0.6f);
        go.transform.position = new Vector3(transform.position.x, transform.position.y + tileHeight, dropZ);

    }

    public void CreateFinishLine(Transform finishLine)
    {
        
        var z = finishLine.position.z+0.7f;
        var x = 0.55f;
        if(!atEnd) multiplier = CalculateMultiplier();

        atEnd = true;
        StartCoroutine(FinishLine(z,x,multiplier));      
    }
    public float CalculateMultiplier()
    {
        
        if (tileStack.Count >= 10 && tileStack.Count < 20)
        {
            multiplier = 3;
        }
        else if (tileStack.Count >= 20 && tileStack.Count < 30)
        {
            multiplier = 5;
        }
        else if (tileStack.Count >= 30)
        {
            multiplier = 10;
        }
        return multiplier;
    }
    IEnumerator FinishLine(float z, float x,float multiplier)
    {
        float right = 1;
        float index = 0;
        while(tileStack.Count>0 && index<=10)
        {
            var go = tileStack.Pop();
            go.GetComponent<BoxCollider>().isTrigger = false;
            go.transform.parent = null;
            go.transform.position = new Vector3(x * right, -0.2f, z);
            if (index != 0 && index % 2 == 0)
            {
                z += zDistance;
            }
            right *= -1;
            index++;
            yield return new WaitForSeconds(0.05f);
        }
        if (tileStack.Count <= 0)
        {
            FindObjectOfType<GameController>().WonGame(multiplier);
        }
        
    }
    
}
