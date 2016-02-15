using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NotLimited.Framework.Collections.Chunked;
using Xunit;
using XunitShould;

namespace NotLimited.Framework.Tests.Collections.Chunked
{
	public class ChunkedListTests
	{
		[Fact]
		public void CanSetMaxElementsForInt64()
		{
			var list = new ChunkedList<long>(16);
			list.MaxBucketElements.ShouldEqual(2);
		}

		[Fact]
		public void CanSetMaxElemetsForPointer()
		{
			var list = new ChunkedList<object>(16);
			list.MaxBucketElements.ShouldEqual(2);
		}

		[Fact]
		public void CanAddElements()
		{
			var list = new ChunkedList<int>(40); // 10 of Int32

			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}

			for (int i = 0; i < 5; i++)
			{
				list[i].ShouldEqual(i);
			}

			list.BucketCount.ShouldEqual(1);
			list.IsEmpty.ShouldBeFalse();
			list.Count.ShouldEqual(5);

			var buckets = list.Buckets;
			buckets.ShouldNotBeNull();
			buckets.Count.ShouldEqual(1);

			var bucket = buckets[0];
			bucket.Buffer.ShouldNotBeNull();
			bucket.Buffer.Length.ShouldEqual(10);
			bucket.FreeSpace.ShouldEqual(5);
			bucket.Index.ShouldEqual(5);
			bucket.IsEmpty.ShouldBeFalse();
		}

		[Fact]
		public void CanCreateMoreBuckets()
		{
			var list = new ChunkedList<int>(20); // 5 of Int32

			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			for (int i = 0; i < 10; i++)
			{
				list[i].ShouldEqual(i);
			}

			list.BucketCount.ShouldEqual(2);
			list.IsEmpty.ShouldBeFalse();
			list.Count.ShouldEqual(10);

			var buckets = list.Buckets;
			buckets.ShouldNotBeNull();
			buckets.Count.ShouldEqual(2);

			foreach (var bucket in buckets)
			{
				bucket.Buffer.ShouldNotBeNull();
				bucket.Buffer.Length.ShouldEqual(5);
				bucket.FreeSpace.ShouldEqual(0);
				bucket.Index.ShouldEqual(5);
				bucket.IsEmpty.ShouldBeFalse();
			}
		}

		[Fact]
		public void CanUseContainsToFindElement()
		{
			var list = new ChunkedList<int>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			list.Contains(5).ShouldBeTrue();
		}

		[Fact]
		public void CanClearList()
		{
			var list = new ChunkedList<int>(20);
			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			list.Buckets.Count.ShouldEqual(2);

			list.Clear();

			list.Buckets.Count.ShouldEqual(0);
			list.Count.ShouldEqual(0);
			list.IsEmpty.ShouldBeTrue();
		}

		[Fact]
		public void CantRemoveElements()
		{
			var list = new ChunkedList<int>();
			list.Add(42);

			Trap.Exception(() => list.Remove(42)).ShouldBeInstanceOf<NotSupportedException>();
		}

		[Fact]
		public void CanCopyToArray()
		{
			var list = new ChunkedList<int>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			var arr = new int[10];
			list.CopyTo(arr, 0);

			for (int i = 0; i < 10; i++)
			{
				arr[i].ShouldEqual(i);
			}
		}

		[Fact]
		public void CanCopyToArrayFromIndex()
		{
			var list = new ChunkedList<int>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			var arr = new int[20];
			list.CopyTo(arr, 10);

			for (int i = 10; i < 20; i++)
			{
				arr[i].ShouldEqual(i - 10);
			}
		}

		[Fact]
		public void CantCopyToArrayOfSmallerSize()
		{
			var list = new ChunkedList<int>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			var arr = new int[5];
			Trap.Exception(() => list.CopyTo(arr, 0)).ShouldBeInstanceOf<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void ListIsAlwaysWriteable()
		{
			new ChunkedList<int>().IsReadOnly.ShouldBeFalse();
		}

		[Fact]
		public void CanGetFirstItem()
		{
			var list = new ChunkedList<int>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			list.First.ShouldEqual(0);
		}

		[Fact]
		public void CanGetLastItem()
		{
			var list = new ChunkedList<int>(20); // 5 of Int32

			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			list.Last.ShouldEqual(9);
		}

		[Fact]
		public void CanSetValueByIndex()
		{
			var list = new ChunkedList<int>(20); // 5 of Int32

			for (int i = 0; i < 10; i++)
			{
				list.Add(i);
			}

			list[5] = 42;
			list[5].ShouldEqual(42);
			list.Buckets[1][0].ShouldEqual(42);
		}

		[Fact]
		public void CanAddRange()
		{
			var list = new ChunkedList<int>(20); // 5 of Int32
			list.AddRange(Enumerable.Range(0, 10).ToArray());

			list.BucketCount.ShouldEqual(2);
			list.IsEmpty.ShouldBeFalse();
			list.Count.ShouldEqual(10);

			var buckets = list.Buckets;
			buckets.ShouldNotBeNull();
			buckets.Count.ShouldEqual(2);

			foreach (var bucket in buckets)
			{
				bucket.Buffer.ShouldNotBeNull();
				bucket.Buffer.Length.ShouldEqual(5);
				bucket.FreeSpace.ShouldEqual(0);
				bucket.Index.ShouldEqual(5);
				bucket.IsEmpty.ShouldBeFalse();
			}
		}

		[Fact]
		public void CanAddRangeWithOneBucket()
		{
			var list = new ChunkedList<int>(40); // 10 of Int32
			list.AddRange(Enumerable.Range(0, 10).ToArray());

			list.BucketCount.ShouldEqual(1);
			list.IsEmpty.ShouldBeFalse();
			list.Count.ShouldEqual(10);

			var buckets = list.Buckets;
			buckets.ShouldNotBeNull();
			buckets.Count.ShouldEqual(1);

			var bucket = list.Buckets[0];
			bucket.Buffer.ShouldNotBeNull();
			bucket.Buffer.Length.ShouldEqual(10);
			bucket.FreeSpace.ShouldEqual(0);
			bucket.Index.ShouldEqual(10);
			bucket.IsEmpty.ShouldBeFalse();
		}

		[Fact]
		public void CanRaiseListUpdatedForSingleItem()
		{
			var evt = new ManualResetEventSlim();
			var list = new ChunkedList<int>();
			list.ListUpdated += (sender, args) =>
			{
				args.Count.ShouldEqual(1);
				evt.Set();
			};
			Task.Run(() => list.Add(42));

			evt.Wait();
		}

		[Fact]
		public void CanRaiseListUpdatedForMultipleItem()
		{
			var evt = new ManualResetEventSlim();
			var list = new ChunkedList<int>();
			list.ListUpdated += (sender, args) =>
			{
				args.Count.ShouldEqual(10);
				evt.Set();
			};
			Task.Run(() => list.AddRange(Enumerable.Range(0, 10).ToArray()));

			evt.Wait();
		}
	}
}