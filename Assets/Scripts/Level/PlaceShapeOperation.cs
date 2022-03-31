using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Level
{
    [CreateAssetMenu(fileName = "Place Shape Operation", menuName = "ScriptableObjects/LevelGenerator/PlaceShapeOperation", order = 1)]
    public class PlaceShapeOperation : Operation
    {
        public static List<Vector3> occupiedObject = new List<Vector3>();
        public static List<Vector3> occupiedEmpty = new List<Vector3>();

        public Shape ShapeToPlace;

        public override Stack<Shape> Apply(Stack<Shape> stack, List<Shape> results)
        {
            Shape toPlace = CreateInstance<Shape>();
            toPlace.Symbol = ShapeToPlace.Symbol;
            toPlace.ShapeObject = ShapeToPlace.ShapeObject;
            toPlace.isTwoLayer = ShapeToPlace.isTwoLayer;
            toPlace.Position = predecessor.Position;
            if (stack.Count > 0)
            {
                Shape stateShape = stack.Pop();
                toPlace.Position = stateShape.Position;
            }
            if (toPlace.ShapeObject)
            {
                if (!occupiedObject.Any(x => x == toPlace.Position))
                {
                    Debug.Log("Available: " + toPlace.Position + ". Placing " + toPlace.ShapeObject.name);

                    occupiedObject.Add(toPlace.Position);
                    occupiedEmpty.Add(toPlace.Position);
                    

                    if(toPlace.isTwoLayer){
                        Debug.Log("Available: " + new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z) + ". Placing " + toPlace.ShapeObject.name);
                        occupiedObject.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                        occupiedEmpty.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                    }
                    Instantiate(toPlace.ShapeObject, toPlace.Position, toPlace.ShapeObject.transform.rotation);
                }
            } 
            else
            {
                if (!occupiedEmpty.Any(x => x == toPlace.Position))
                {
                    Debug.Log("Available: " + toPlace.Position + ". Placing EMPTY");

                    occupiedEmpty.Add(toPlace.Position);

                    results.Add(toPlace);
                }
                
            }

            return stack;
        }

    }
}