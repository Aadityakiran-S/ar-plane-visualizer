using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttachable
{
    bool IsAttached { get; set; }
    IAttacher AttachedParent { get; set; }
    void Attach(GameObject attachParent);
    void Detatch();
}
