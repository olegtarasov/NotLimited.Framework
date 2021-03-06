﻿using System;

namespace NotLimited.Framework.Common.Collections.Observable
{
	[Serializable]
	public sealed class SimpleMonitor : IDisposable
	{
		private int busyCount;

		public bool Busy
		{
			get { return busyCount > 0; }
		}

		public void Enter()
		{
			busyCount++;
		}

		#region Implementation of IDisposable

		public void Dispose()
		{
			busyCount--;
		}

		#endregion
	}
}