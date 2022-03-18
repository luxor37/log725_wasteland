using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Place Shape Operation", menuName = "ScriptableObjects/LevelGenerator/Operation", order = 1)]
    public abstract class PlaceShapeOperation : Operation
    {
        public Shape ShapeToPlace;

        public override void Apply(Stack<Shape> stack, List<Shape> results)
        {
            results.Add(ShapeToPlace);
        }
    }
}