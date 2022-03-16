using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Shape", menuName = "ScriptableObjects/LevelGenerator/Shape", order = 1)]
    public class Shape : ScriptableObject
    {
        public enum AttributeEnum
        {
            MAX_SIZE,
            MIN_SIZE,
        }

        public enum SymbolEnum
        {
            FLOOR,
            CEILING,
            LADDER,
            ENEMY,
            ITEM,
            COIN
        }

        public SymbolEnum Symbol;

        public GameObject ShapeObject; // not just geometric data. instead of abstract shape put gameobject (shortcut)

        [SerializeField]
        public Dictionary<AttributeEnum, int> Attributes;

        private void Awake()
        {
            Attributes = new Dictionary<AttributeEnum, int>();
        }
    }
}