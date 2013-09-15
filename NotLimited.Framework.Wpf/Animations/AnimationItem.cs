using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace NotLimited.Framework.Wpf.Animations
{
	public class AnimationItem
	{
		private readonly Storyboard _storyboard;

		internal AnimationItem(Storyboard storyboard)
		{
			_storyboard = storyboard;
			_storyboard.Completed += OnStoryboardComplete;
		}

		public Action BeginAction { get; set; }
		public Action EndAction { get; set; }
		public DependencyObject Control { get; set; }
		public PropertyPath PropertyPath { get; set; }

		public void Start()
		{
			if (BeginAction != null)
				BeginAction();

			_storyboard.Begin();
		}

		public void Stop()
		{
			_storyboard.Stop();
			OnStoryboardComplete(null, EventArgs.Empty);
		}

		private void OnStoryboardComplete(object sender, EventArgs e)
		{
			//var value = Storyboard.Children[0].EvaluateProperty<object>(new PropertyPath("To"));
			//Control.SetProperty(PropertyPath, value);
			//Storyboard.Remove();
			_storyboard.Completed -= OnStoryboardComplete;
			if (EndAction != null)
				EndAction();
		}
	}
}