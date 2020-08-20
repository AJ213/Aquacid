using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Vector3 offset = default;
    [SerializeField] float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
            
        Vector3 goalPos = target.position + offset;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, goalPos, ref velocity, smoothTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown("r")) // restart
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // escape
        {
            Application.Quit();
        }
    }
}