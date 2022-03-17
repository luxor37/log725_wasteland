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
            OFFSET_X,
            OFFSET_Y,
        }

        public enum SymbolEnum
        {
            AXIOM,
            FLOOR,
            CEILING,
            CONTENT,
            PLATFORM,
            LADDER,
            PREBOSS,
            BOSS,
            TERRAIN,
            ENEMY,
            ITEM,
            COIN,
            EMPTY
        }

        public SymbolEnum Symbol;

        public GameObject ShapeObject; // not just geometric data. instead of abstract shape put gameobject (shortcut)

        [SerializeField]
        public List<AttributeEnum> Attributes;
        public List<int> AttributeValues;

        private void Awake()
        {
        }
    }
}