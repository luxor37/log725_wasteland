using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Place Shape Operation", menuName = "ScriptableObjects/LevelGenerator/SplitOperation", order = 1)]
    public class SplitOperation : Operation
    {
        public List<Vector3> splitLoc = new List<Vector3>();
        public Vector3 randomRange = new Vector3();
        public override void Apply(Stack<Shape> stack, List<Shape> results)
        {
            Shape leftShape = CreateInstance<Shape>();
            leftShape.Position = predecessor.Position;
            List<Shape> shapeList = new List<Shape>();
            shapeList.Add(leftShape);
            foreach ( var split in splitLoc)
            {
                Shape rightShape = CreateInstance<Shape>();
                rightShape.Position = predecessor.Position;
                if (!Mathf.Approximately(split.x, 0))
                    rightShape.Position.x = -UnityEngine.Random.Range(split.x - randomRange.x, split.x + randomRange.x + 0.1f);
                if (!Mathf.Approximately(split.y, 0))
                    rightShape.Position.y = UnityEngine.Random.Range(split.y - randomRange.y, split.y + randomRange.y);
                if (!Mathf.Approximately(split.z, 0))
                rightShape.Position.z = UnityEngine.Random.Range(split.z - randomRange.z, split.z + randomRange.z);
                shapeList.Add(rightShape);
            }

           shapeList.Reverse();
           foreach( var s in shapeList)
                stack.Push(s);
        }
    }
}