using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoverable
{
    void OnHoverStart();
    void OnHover();
    void OnHoverLost();
}
