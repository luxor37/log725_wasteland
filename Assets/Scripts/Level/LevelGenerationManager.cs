using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Level
{
    public class LevelGenerationManager : MonoBehaviour
    {
        static LevelGenerationManager instance;
        
        [SerializeField]
        List<Rule> Rules;

        [SerializeField]
        Shape AxiomShape;

        GameObject player;

        public static LevelGenerationManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GenerateLevel();
        }

        bool ResultsAreValid(Stack<Shape> shapeStack, Shape predecessor, List<Shape> results)
        {
            foreach(var newShape in results)
            {
                // TODO check size
                foreach(var oldShape in shapeStack)
                {
                    if (oldShape == predecessor)
                    {
                        continue;
                    }

                }
            }
            return true;
        }

        public void GenerateLevel()
        {
            Stack<Shape> shapeStack = new Stack<Shape>();
            Shape axiom = AxiomShape;
            shapeStack.Push(axiom);
            while (shapeStack.Count > 0)
            {
                Shape shape = shapeStack.Pop();
                List<Rule> rulesMatch = new List<Rule>();
                foreach (var rule in Rules)
                {
                    if (rule.PredecessorShape.Symbol == shape.Symbol)
                    {
                        rulesMatch.Add(rule);  
                    }
                }
                if (rulesMatch.Count == 0)
                    break;
                var ruleChosen = rulesMatch[UnityEngine.Random.Range(0, rulesMatch.Count)];
                List<Shape> results = ruleChosen.CalculateRule();
                if (results.Count == 0)
                    continue;

                if (ResultsAreValid(shapeStack, shape, results))
                {
                    foreach( var resShape in results)
                    {
                        if (resShape.ShapeObject)
                            continue;
                        else
                            shapeStack.Push(resShape);
                    }
                }
               
            }
        }
    }
}