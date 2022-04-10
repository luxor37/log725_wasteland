using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Shape", menuName = "ScriptableObjects/LevelGenerator/Shape", order = 1)]
    public class Shape : ScriptableObject
    {
        public bool isTwoLayer = false;

        public enum SymbolEnum
        {
            AXIOM,
            CONTENT,
            ENEMY,
            EMPTY
        }

        [HideInInspector]
        public Vector3 Position;

        public SymbolEnum Symbol;

        public GameObject ShapeObject;

        public Shape(Shape other)
        {
            Position = other.Position;
            Symbol = other.Symbol;
            ShapeObject = other.ShapeObject;
        }
    }
}