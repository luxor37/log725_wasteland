using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level
{
    public class LevelGenerationManager : MonoBehaviour
    {
        [SerializeField]
        List<Rule> Rules;

        [SerializeField]
        List<Rule> ContentRules;

        [SerializeField]
        List<Rule> EnnemyRules;

        [SerializeField]
        Shape AxiomShape;

        public int MaxNumberBlockLevel = 10;

        private readonly System.Random rnd = new System.Random();

        public void GenerateLevel()
        {
            var axiom = AxiomShape;
            var terrainNodes = new List<Shape> { axiom };
            var contentNodes = new List<Shape>();
            var ennemyNodes = new List<Shape>();

            var counter = 0;
            while (terrainNodes.Count > 0)
            {
                counter ++;
                if(counter > MaxNumberBlockLevel)
                    break;

                var index = rnd.Next(terrainNodes.Count);
                var shape = terrainNodes[index];

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = Rules.Where(rule => rule.PredecessorShape.Symbol == shape.Symbol).ToList();

                if (rulesMatch.Count == 0)
                    break;
                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                var results = ruleChosen.CalculateRule(shape);
                terrainNodes.RemoveAt(index);
                if (results.Count == 0)
                    continue;
                
                terrainNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.EMPTY || x.Symbol == Shape.SymbolEnum.AXIOM));
                contentNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.CONTENT));
                ennemyNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.ENEMY));
            }

            counter = 0;
            while (contentNodes.Count > 0)
            {
                counter++;
                if (counter > 1000)
                    break;

                var index = rnd.Next(contentNodes.Count);
                var shape = contentNodes[index];

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = ContentRules.Where(rule => rule.PredecessorShape.Symbol == shape.Symbol).ToList();

                if (rulesMatch.Count == 0) break;

                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                ruleChosen.CalculateRule(shape);
                contentNodes.RemoveAt(index);
            }

            counter = 0;
            while (ennemyNodes.Count > 0)
            {
                counter++;
                if (counter > 1000)
                    break;

                var index = rnd.Next(ennemyNodes.Count);
                var shape = ennemyNodes[index];

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = ContentRules.Where(rule => rule.PredecessorShape.Symbol == shape.Symbol).ToList();

                if (rulesMatch.Count == 0) break;

                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                ruleChosen.CalculateRule(shape);
                ennemyNodes.RemoveAt(index);
            }
        }

        void Start()
        {
            GenerateLevel();
        }
    }
}