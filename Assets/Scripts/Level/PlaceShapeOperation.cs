using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Place Shape Operation", menuName = "ScriptableObjects/LevelGenerator/PlaceShapeOperation", order = 1)]
    public class PlaceShapeOperation : Operation
    {
        public Shape ShapeToPlace;

        public override void Apply(Stack<Shape> stack, List<Shape> results)
        {
            ShapeToPlace.Position = predecessor.Position;
            if (stack.Count > 0)
            {
                Shape stateShape = stack.Pop();
                ShapeToPlace.Position = stateShape.Position;
            }
            if (ShapeToPlace.ShapeObject)
                Instantiate(ShapeToPlace.ShapeObject, ShapeToPlace.Position, ShapeToPlace.ShapeObject.transform.rotation);
            results.Add(ShapeToPlace);
        }

    }
}