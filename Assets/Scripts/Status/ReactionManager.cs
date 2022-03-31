using System.Collections.Generic;
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

        private static ReactionManager instance = null;

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

        public static ReactionManager Instance
        {
            get
            {
                return instance;
            }

        }

        public List<IStatus> GetReactions(List<IStatus> statuses, StatusController controller)
        {
            List<IStatus> reactions = new List<IStatus>();
            // brute force
            foreach (IStatus stat1 in statuses.ToArray())
            {
                foreach(IStatus stat2 in statuses.ToArray())
                {
                    foreach (Reaction reaction in Reactions)
                    {
                        if (stat1.name == reaction.inputStatus1.ToString()
                            && stat2.name == reaction.inputStatus2.ToString())
                        {
                            // Debug.Log(reaction.inputStatus1.ToString(), this);
                            controller.EndStatus(stat1.name);
                            controller.EndStatus(stat2.name);
                            reactions.Add(StatusManager.Instance.GetNewStatusObject(reaction.name, controller));
                        }
                    }
                }
            }
            return reactions;
        }

     
    }
}

