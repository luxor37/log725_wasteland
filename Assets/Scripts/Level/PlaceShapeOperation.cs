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

        public override bool Apply(Stack<Shape> stack, List<Shape> results)
        {
            var toPlace = CreateInstance<Shape>();
            toPlace.Symbol = ShapeToPlace.Symbol;
            toPlace.ShapeObject = ShapeToPlace.ShapeObject;
            toPlace.isTwoLayer = ShapeToPlace.isTwoLayer;
            toPlace.Position = predecessor.Position;

            if (stack.Count > 0)
            {
                var stateShape = stack.Pop();
                toPlace.Position = stateShape.Position;
            }
            if (toPlace.ShapeObject)
            {
                //if (occupiedObject.Any(x => x == toPlace.Position)) return false;

                //if(toPlace.isTwoLayer){
                //    if (occupiedObject.Any(x => x == new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z))) return false;

                //    occupiedObject.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                //    occupiedEmpty.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                //}

                if (toPlace.isTwoLayer)
                {
                    //if (occupiedObject.Any(x => x == new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z))) return false;

                    occupiedObject.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                    occupiedEmpty.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                }

                occupiedObject.Add(toPlace.Position);
                occupiedEmpty.Add(toPlace.Position);

                Instantiate(toPlace.ShapeObject, toPlace.Position, toPlace.ShapeObject.transform.rotation);
            } 
            else
            {
                if (occupiedEmpty.Any(x => x == toPlace.Position)) return true;

                occupiedEmpty.Add(toPlace.Position);
                results.Add(toPlace);
            }

            return true;
        }

    }
}