using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    private Vector3 newPos;
    private Quaternion newRot;
    [SerializeField]private float lerpSpeed=3f;
    void Start()
    {
        //getting players transform
        offset = transform.position;
    }

  
    void LateUpdate()
    {
        //Following player along
        newPos = target.position + offset;
        newRot = target.rotation;
        transform.position=Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, lerpSpeed * Time.deltaTime);

    }
}
