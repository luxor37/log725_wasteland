﻿using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Rule", menuName = "ScriptableObjects/LevelGenerator/Rule", order = 1)]
    public class Rule : ScriptableObject
    {
        // all possible rules
        // axiom -> floor, ceiling, content, background.
        // content -> preboss, boss.
        // preboss -> terrain, enemy, terrain, enemy, terrain, enemy.
        // terrain -> platform.
        // platform -> ladder, coin.
        // platform -> ladder, item.
        // terrain -> empty
        public enum ConditionOperatorEnum
        {
            EQUAL,
            GREATER,
            LESS,
        }

        public Shape PredecessorShape;
        public List<Operation> operations;
      //  public List<Shape> TargetShape;
        public List<Shape.AttributeEnum> Conditions;
        public List<int> ConditionValues;
        public ConditionOperatorEnum Operator;

        Stack<Shape> stack;

        bool CheckPreConditions()
        {
            foreach (var condition in Conditions)
            {
                foreach (var attributeInput in PredecessorShape.Attributes)
                {
                    if (condition != attributeInput)
                    {
                        continue;
                    }

                    bool equal = ConditionValues[(int)condition] == PredecessorShape.AttributeValues[(int)attributeInput];
                    bool less = PredecessorShape.AttributeValues[(int)attributeInput] < ConditionValues[(int)condition];
                    bool greater = PredecessorShape.AttributeValues[(int)attributeInput] > ConditionValues[(int)condition];
                    if (equal && Operator == ConditionOperatorEnum.EQUAL)
                        continue;
                    if (less && Operator == ConditionOperatorEnum.LESS)
                        continue;
                    if (greater && Operator == ConditionOperatorEnum.GREATER)
                        continue;
                    return false;
                }
            }
            return true;
        }

        public List<Shape> CalculateRule()
        {
            var result = new List<Shape>();
            
            if (CheckPreConditions() == false)
                return result;
            foreach(var operation in operations)
            {
                operation.predecessor = PredecessorShape;
                operation.Apply(stack, result);
                Debug.Log(result[0].Symbol);
            }
           
            return result;
        }
    }
}