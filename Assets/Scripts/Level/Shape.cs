using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Shape", menuName = "ScriptableObjects/LevelGenerator/Shape", order = 1)]
    public class Shape : ScriptableObject
    {
        public bool isTwoLayer = false;
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
            WALL,
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
            EXIT,
            EMPTY
        }

        public Vector3 Position;

        public SymbolEnum Symbol;

        public GameObject ShapeObject; // not just geometric data. instead of abstract shape put gameobject (shortcut)

        [SerializeField]
        public List<AttributeEnum> Attributes;
        public List<int> AttributeValues;

        public Shape(Shape other)
        {
            Position = other.Position;
            Symbol = other.Symbol;
            ShapeObject = other.ShapeObject;
            Attributes = other.Attributes;
            AttributeValues = other.AttributeValues;
        }

        private void Awake()
        {
        }
    }
}