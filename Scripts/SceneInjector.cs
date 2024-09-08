using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Syrinj
{
    public class SceneInjector : MonoBehaviour
    {
        public static SceneInjector Instance;

        void Awake()
        {
            Instance = this;
            DependencyContainer.Instance.Reset();
            InjectScene();

			SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        void SceneManager_sceneLoaded (Scene scene, LoadSceneMode mode)
        {
			InjectScene();
        }

        public void InjectScene()
        {
            var behaviours = GetAllBehavioursInScene();

            InjectBehaviours(behaviours);
        }

        private MonoBehaviour[] GetAllBehavioursInScene()
        {
#if UNITY_5 
            return GameObject.FindObjectsOfType<MonoBehaviour>();
#else
            return GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
#endif
        }

        private void InjectBehaviours(MonoBehaviour[] behaviours)
        {
            DependencyContainer.Instance.Inject(behaviours);
        }
    }
}
