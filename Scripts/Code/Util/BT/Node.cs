using System.Collections.Generic;
namespace BehaviorTree
{
    public enum NodeState
    {
        FAILURE,
        RUNNING,
        SUCCESS,
    }
    public class Node
    {
        protected NodeState state;

        public Node parent;
        public Node root;
        protected List<Node> children = new List<Node>();

        public Node()
        {
            parent = null;
            root = this;
        }
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        private void _Attach(Node node)
        {
            if(root == null)
                root = this;
            node.parent = this;
            node.root = root;
            children.Add(node);
        }
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        public void SetData(string key, object value)
        {
            root._dataContext[key] = value;
        }
        public object GetData(string key)
        {
            object value = null;
            if (root._dataContext.TryGetValue(key, out value))
                return value;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }


}