using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelGenerationManager : MonoBehaviour
    {
        static LevelGenerationManager instance;
        
        [SerializeField]
        private List<Rule> Rules;

        public static LevelGenerationManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }
    }
}