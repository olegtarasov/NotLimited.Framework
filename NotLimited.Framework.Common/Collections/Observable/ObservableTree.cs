using System.ComponentModel;

namespace NotLimited.Framework.Common.Collections.Observable;

/// <summary>
/// Observable tree.
/// </summary>
public class ObservableTree<T> : INotifyPropertyChanged
{
	#region NPC

	/// <inheritdoc />
	public event PropertyChangedEventHandler? PropertyChanged;

	private void OnPropertyCahnged(string name)
	{
		if (PropertyChanged != null)
			PropertyChanged(this, new PropertyChangedEventArgs(name));
	}

	#endregion

	private bool _isSelected;


	/// <summary>
	/// Ctor.
	/// </summary>
	public ObservableTree(T data)
	{
		Data = data;
	}

	/// <summary>
	/// Node children.
	/// </summary>
	public ObservableList<ObservableTree<T>> Children { get; } = new();

	/// <summary>
	/// Node parent.
	/// </summary>
	public ObservableTree<T>? Parent { get; set; }

	/// <summary>
	/// Node data.
	/// </summary>
	public T? Data { get; set; }

	private string? _name;


	/// <summary>
	/// Node name.
	/// </summary>
	public string? Name
	{
		get => _name;
		set
		{
			_name = value;
			OnPropertyCahnged("Name");
		}
	}

	/// <summary>
	/// Indicates whether the node is selected.
	/// </summary>
	public bool IsSelected
	{
		get => _isSelected;
		set { _isSelected = value; OnPropertyCahnged("IsSelected"); }
	}

	/// <summary>
	/// Adds a child node to this node.
	/// </summary>
	/// <param name="child">Child node.</param>
	public void AddChild(ObservableTree<T> child)
	{
		child.Parent = this;
		Children.Add(child);
	}

	/// <summary>
	/// Adds several children to this node.
	/// </summary>
	/// <param name="children">Child nodes.</param>
	public void AddChildren(IEnumerable<ObservableTree<T>> children)
	{
		foreach (var child in children)
		{
			child.Parent = this;
			Children.Add(child);
		}
	}

	/// <summary>
	/// Removes a child from this node.
	/// </summary>
	/// <param name="child"></param>
	public void RemoveChild(ObservableTree<T> child)
	{
		Children.Remove(child);
		child.Parent = null;
	}

	/// <summary>
	/// Removes this node from its parent's list of children.
	/// </summary>
	public void Remove()
	{
		if (Parent != null)
			Parent.RemoveChild(this);
	}

	/// <summary>
	/// Applies an action to this node and its children using depth-first search.
	/// </summary>
	/// <param name="action">Action to apply.</param>
	public void Traverse(Action<ObservableTree<T>> action)
	{
		var stack = new Stack<ObservableTree<T>>();
			
		stack.Push(this);
		while (stack.Count > 0)
		{
			var tree = stack.Pop();

			if (tree.Children.Count > 0)
				for (int i = 0; i < tree.Children.Count; i++)
					stack.Push(tree.Children[i]);

			action(tree);
		}
	}
}