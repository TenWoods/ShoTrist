using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRotate : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private void Update()
    {
        float rotate_x = transform.rotation.eulerAngles.x;
        Debug.Log(rotate_x);
        if (Mathf.Abs(rotate_x - 90) >= 0.01)
        {
            rotate_x = Mathf.Lerp(rotate_x, 90, Time.deltaTime * rotateSpeed);
            transform.rotation = Quaternion.Euler(rotate_x, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            GameObject.Find("Map").GetComponent<GameManager>().BuildFinish = true;
            gameObject.GetComponent<FloorRotate>().enabled = false;
        }
    }
}
