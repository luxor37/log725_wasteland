using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Status
{
    public class ReactionManager : MonoBehaviour
    {

        public List<Reaction> Reactions;

        [System.Serializable]
        public struct Reaction
        {
            public ItemController.StatusEnum name;
            // public IStatus reactionStatus;
            public ItemController.StatusEnum inputStatus1;
            public ItemController.StatusEnum inputStatus2;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public static ReactionManager Instance { get; private set; }

        public List<IStatus> GetReactions(List<IStatus> statuses, StatusController controller)
        {
            var reactions = new List<IStatus>();
            // brute force
            foreach (var stat1 in statuses.ToArray())
            {
                foreach(var stat2 in statuses.ToArray())
                {
                    foreach (var reaction in Reactions.Where(reaction => stat1.name == reaction.inputStatus1.ToString() && 
                                                                         stat2.name == reaction.inputStatus2.ToString()))
                    {
                        controller.EndStatus(stat1.name);
                        controller.EndStatus(stat2.name);
                        reactions.Add(StatusManager.Instance.GetNewStatusObject(reaction.name, controller));
                    }
                }
            }
            return reactions;
        }
    }
}

