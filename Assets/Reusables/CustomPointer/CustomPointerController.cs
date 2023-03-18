using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPointerController : MonoBehaviour
{
    [SerializeField] CustomPointer pointer;

    // Update is called once per frame
    void Update()
    {
        if (pointer != null)
        {
            

            pointer.SetPointerPosition(Input.mousePosition);
            pointer.CheckHover(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                pointer.Click(Input.mousePosition);
            }
        }
    }
}
