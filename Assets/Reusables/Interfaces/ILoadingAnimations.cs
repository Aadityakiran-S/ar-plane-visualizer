using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface that is to be implemented by the loadingScreenManager of each game
public interface ILoadingAnimations
{
    void OnInitiate_Loading();
    void OnOccuranceOf_Loading(float amount, bool toRest = false);
    void OnFinished_Loading();
}
