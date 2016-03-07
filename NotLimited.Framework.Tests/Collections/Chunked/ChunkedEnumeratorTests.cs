using System;
using System.Collections.Generic;
using System.Linq;
using NotLimited.Framework.Collections.Chunked;
using Xunit;
using XunitShould;

namespace NotLimited.Framework.Tests.Collections.Chunked
{
	public class ChunkedEnumeratorTests
	{
		[Fact]
		public void CanEnumerateItemsInSingleBucket()
		{
			var list = new ChunkedList<int>(40);
			list.AddRange(Enumerable.Range(0, 10).ToArray());

			int idx = 0;
			foreach (var item in list)
			{
				item.ShouldEqual(idx);
				idx++;
			}
		}

		[Fact]
		public void CanEnumerateItemsInMultipleBuckets()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());

			int idx = 0;
			foreach (var item in list)
			{
				item.ShouldEqual(idx);
				idx++;
			}
		}

		[Fact]
		public void CanCloneEnumerator()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());

			var en = list.GetEnumerator();
			for (int i = 0; i < 5; i++)
			{
				en.MoveNext().ShouldBeTrue();
				en.Current.ShouldEqual(i);
			}

			en = en.Clone();
			for (int i = 5; i < 10; i++)
			{
				en.MoveNext().ShouldBeTrue();
				en.Current.ShouldEqual(i);
			}
		}

		[Fact]
		public void CantUseWhenListEmpty()
		{
			var list = new ChunkedList<int>();
			var en = list.GetEnumerator();

			en.Count.ShouldEqual(0);
			en.MovePrev().ShouldBeFalse();
			en.MoveNext().ShouldBeFalse();

			Trap.Exception(() => en.FromFirst()).ShouldBeInstanceOf<InvalidOperationException>();
			Trap.Exception(() => en.FromLast()).ShouldBeInstanceOf<InvalidOperationException>();
		}

		[Fact]
		public void CanSetToFirst()
		{
			var list = new ChunkedList<int>();
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();
			while (en.HasMore)
			{
				en.MoveNext();
			}

			en.FromFirst();
			en.HasMore.ShouldBeTrue();
			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(1);
		}

		[Fact]
		public void CanSetToLast()
		{
			var list = new ChunkedList<int>();
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();
			en.FromLast();
			en.HasMore.ShouldBeFalse();
			en.MovePrev().ShouldBeTrue();
			en.Current.ShouldEqual(10);
		}

		[Fact]
		public void CanEnumerateBackwards()
		{
			var list = new ChunkedList<int>();
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();
			en.FromLast();
			en.HasMore.ShouldBeFalse();

			for (int i = 10; i > 0; i--)
			{
				en.MovePrev().ShouldBeTrue();
				en.Current.ShouldEqual(i);
			}
		}

		[Fact]
		public void CanStartFromIndexInMiddle()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());
			var en = list.GetEnumerator();

			en.FromIndex(2).ShouldBeTrue();
			en.Current.ShouldEqual(1);
			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(2);
		}

		[Fact]
		public void CanStartFromIndexInBeginning()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();

			en.FromIndex(0).ShouldBeTrue();
			en.Current.ShouldEqual(0);
			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(1);
		}

		[Fact]
		public void CanStartFromIndexInEndOfBucket()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());
			var en = list.GetEnumerator();

			en.FromIndex(5).ShouldBeTrue();
			en.Current.ShouldEqual(4);
			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(5);
		}

		[Fact]
		public void CanStartFromIndexInBeginningOfBucket()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());
			var en = list.GetEnumerator();

			en.FromIndex(6).ShouldBeTrue();
			en.Current.ShouldEqual(5);
			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(6);
		}

		[Fact]
		public void CanStartFromIndexInEnd()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());
			var en = list.GetEnumerator();

			en.FromIndex(9).ShouldBeTrue();
			en.Current.ShouldEqual(8);
			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(9);
			en.HasMore.ShouldBeFalse();
		}

		[Fact]
		public void CanStartFromIndexPastEnd()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());
			var en = list.GetEnumerator();

			en.FromIndex(10).ShouldBeTrue();
			en.Current.ShouldEqual(9);
			en.MoveNext().ShouldBeFalse();
		}

		[Fact]
		public void CantStartFromIndexFarPastEnd()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();

			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(1);
			en.FromIndex(11).ShouldBeFalse();
			en.Current.ShouldEqual(1);
		}

		[Fact]
		public void CantStartFromNegativeIndex()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();

			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(1);
			en.FromIndex(-1).ShouldBeFalse();
			en.Current.ShouldEqual(1);
		}

		[Fact]
		public void CanGetCount()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();

			en.Count.ShouldEqual(10);
		}

		[Fact]
		public void CanReset()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();

			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(1);
			en.Reset();
			en.Current.ShouldEqual(0);
			en.MoveNext().ShouldBeTrue();
			en.Current.ShouldEqual(1);
		}

		[Fact]
		public void CanComparePositions()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();
			var en2 = list.GetEnumerator();

			en.MoveNext().ShouldBeTrue();
			en2.MoveNext().ShouldBeTrue();

			en.AsComparable().IsPositionEqual(en2.AsComparable()).ShouldBeTrue();
			en2.MoveNext().ShouldBeTrue();
			en.AsComparable().IsPositionEqual(en2.AsComparable()).ShouldBeFalse();
		}

		[Fact]
		public void CanGetEnumeratorOfEnumerator()
		{
			var list = new ChunkedList<int>();
			var en = list.GetEnumerator();
			var en2 = en.GetEnumerator();

			ReferenceEquals(en, en2).ShouldBeTrue();
		}

		[Fact]
		public void CanIterateBackwardsUntilNoMoreItems()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(1, 10).ToArray());
			var en = list.GetEnumerator();

			en.FromLast();
			int idx = 10;
			while (en.MovePrev())
			{
				en.Current.ShouldEqual(idx);
				idx--;
			}
		}

		[Fact]
		public void CanGetPosition()
		{
			var list = new ChunkedList<int>(20);
			list.AddRange(Enumerable.Range(0, 10).ToArray());

			var en = list.GetEnumerator();
			en.Position.ShouldEqual(-1);

			for (int i = 0; i < 5; i++)
			{
				en.MoveNext().ShouldBeTrue();
			}

			en.Position.ShouldEqual(4);

			for (int i = 0; i < 4; i++)
			{
				en.MoveNext().ShouldBeTrue();
			}

			en.Position.ShouldEqual(8);

			while (en.MoveNext()) ;

			en.Position.ShouldEqual(9);
		}
	}
}