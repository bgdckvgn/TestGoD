using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<CameraScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform);;
    }
}