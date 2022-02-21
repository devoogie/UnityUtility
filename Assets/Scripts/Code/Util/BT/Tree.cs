using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree
    {

        protected Node root;

        public void OnProgress()
        {
            if (root != null)
                root.Evaluate();
        }

        public abstract Node Initialize();

    }

}