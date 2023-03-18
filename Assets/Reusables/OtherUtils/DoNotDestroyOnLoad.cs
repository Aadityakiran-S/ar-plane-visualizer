using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InApp.Utils
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
        #region Referances

        public bool isNotToBeDestroyed;

        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}