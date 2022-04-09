using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Level
{
    [CreateAssetMenu(fileName = "Place Shape Operation", menuName = "ScriptableObjects/LevelGenerator/PlaceShapeOperation", order = 1)]
    public class PlaceShapeOperation : Operation
    {
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
                if (toPlace.isTwoLayer)
                {
                    if (LevelGenerationManager.OccupiedObject.Any(x => x == new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z))) return false;

                    LevelGenerationManager.OccupiedObject.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                    LevelGenerationManager.OccupiedEmpty.Add(new Vector3(toPlace.Position.x, toPlace.Position.y - 14.75f, toPlace.Position.z));
                }

                LevelGenerationManager.OccupiedObject.Add(toPlace.Position);
                LevelGenerationManager.OccupiedEmpty.Add(toPlace.Position);

                Instantiate(toPlace.ShapeObject, toPlace.Position, toPlace.ShapeObject.transform.rotation);
            } 
            else
            {
                if (LevelGenerationManager.OccupiedEmpty.Any(x => x == toPlace.Position)) return true;

                LevelGenerationManager.OccupiedEmpty.Add(toPlace.Position);
                results.Add(toPlace);
            }

            return true;
        }

    }
}