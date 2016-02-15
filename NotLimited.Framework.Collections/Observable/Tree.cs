using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NotLimited.Framework.Collections.Observable
{
	public class Tree<T> : INotifyPropertyChanged
	{
		#region NPC
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyCahnged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
		#endregion

		public ObservableList<Tree<T>> Children { get; private set; }
		public Tree<T> Parent { get; set; }
		public T Data { get; set; }

		private string name;

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				OnPropertyCahnged("Name");
			}
		}

		private bool isSelected;
		public bool IsSelected
		{
			get { return isSelected; }
			set { isSelected = value; OnPropertyCahnged("IsSelected"); }
		}
		

		public Tree()
		{
			Children = new ObservableList<Tree<T>>();
		}

		public Tree(T data) : this()
		{
			Data = data;
		}

		public void AddChild(Tree<T> child)
		{
			child.Parent = this;
			Children.Add(child);
		}

		public void AddChildren(IEnumerable<Tree<T>> children)
		{
			foreach (var child in children)
			{
				child.Parent = this;
				Children.Add(child);
			}
		}

		public void RemoveChild(Tree<T> child)
		{
			Children.Remove(child);
			child.Parent = null;
		}

		public void Remove()
		{
			if (Parent != null)
				Parent.RemoveChild(this);
		}

		public void Traverse(Action<T> action)
		{
			var stack = new Stack<Tree<T>>();
			
			stack.Push(this);
			while (stack.Count > 0)
			{
				var tree = stack.Pop();

				if (tree.Children != null && tree.Children.Count > 0)
					for (int i = 0; i < tree.Children.Count; i++)
						stack.Push(tree.Children[i]);

				action(tree.Data);
			}
		}
	}
}