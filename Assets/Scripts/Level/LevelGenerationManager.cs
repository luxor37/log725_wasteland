using System.Collections.Generic;
using System.Linq;
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

        public static LevelGenerationManager Instance => instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
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

        public void GenerateLevel()
        {
            var shapeStack = new List<Shape>();
            var axiom = AxiomShape;
            shapeStack.Add(axiom);
            var counter = 0;
            while (shapeStack.Count > 0)
            {
                counter ++;
                if(counter > 10)
                    break;
                var rnd = new System.Random();
                var index = rnd.Next(shapeStack.Count);
                var shape = shapeStack[index];

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = Rules.Where(rule => rule.PredecessorShape.Symbol == shape.Symbol).ToList();

                if (rulesMatch.Count == 0)
                    break;
                //Pick a random rule to apply
                var ruleChosen = rulesMatch[UnityEngine.Random.Range(0, rulesMatch.Count)];
                var results = ruleChosen.CalculateRule(shape);
                shapeStack.RemoveAt(index);
                if (results.Count == 0)
                    continue;
                
                shapeStack.AddRange(results);
               
            }
        }
    }
}