using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    bool Clicked { get; set; }
    void OnClickBehaviour();
}
