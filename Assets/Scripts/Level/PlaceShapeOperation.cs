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
            Shape toPlace = CreateInstance<Shape>();
            toPlace.Symbol = ShapeToPlace.Symbol;
            toPlace.ShapeObject = ShapeToPlace.ShapeObject;
            toPlace.Position = predecessor.Position;
            if (stack.Count > 0)
            {
                Shape stateShape = stack.Pop();
                toPlace.Position = stateShape.Position;
            }
            if (toPlace.ShapeObject)
            {
                Instantiate(toPlace.ShapeObject, toPlace.Position, toPlace.ShapeObject.transform.rotation);
            } 
            else
            {
                results.Add(toPlace);
            }
            
        }

    }
}