using System;
using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Rule", menuName = "ScriptableObjects/LevelGenerator/Rule", order = 1)]
    public class Rule : ScriptableObject
    {

        public Shape PredecessorShape;
        public List<Operation> operations;

        private readonly Stack<Shape> stack = new Stack<Shape>();
        
        public Tuple<List<Shape>, bool> CalculateRule(Shape inputShape)
        {
            var result = new List<Shape>();
            foreach(var operation in operations)
            {
                operation.predecessor = inputShape;
                if (!operation.Apply(stack, result)) return new Tuple<List<Shape>, bool>(result, false);
            }
           
            return new Tuple<List<Shape>, bool>(result, true);
        }
    }
}
