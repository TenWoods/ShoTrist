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
        float rotate_y = transform.rotation.eulerAngles.y;
        if (Mathf.Abs(rotate_x - 90) >= 0.1 && rotate_x < 90)
        {
            rotate_x += Time.deltaTime * rotateSpeed;
            rotate_y += Time.deltaTime * rotateSpeed / 2;
            transform.rotation = Quaternion.Euler(rotate_x, rotate_y, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            gameObject.GetComponent<FloorRotate>().enabled = false;
        }
    }
}
