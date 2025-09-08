using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Tools.Algorithms
{
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public TreeNode<T> Parent { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            Value = value;
        }

        public void AddChild(TreeNode<T> child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }
}
