using System;
using System.Linq;
using NotLimited.Framework.Collections.Chunked;
using Xunit;
using XunitShould;

namespace NotLimited.Framework.Tests.Collections.Chunked
{
	public class BucketTests
	{
		[Fact]
		public void CanInitializeBucketWithMaxElements()
		{
			var bucket = new Bucket<int>(10);
			bucket.Buffer.ShouldNotBeNull();
			bucket.Buffer.Length.ShouldEqual(10);
			bucket.FreeSpace.ShouldEqual(10);
			bucket.Index.ShouldEqual(0);
			bucket.IsEmpty.ShouldBeTrue();
		}

		[Fact]
		public void CanAddElements()
		{
			var bucket = new Bucket<int>(10);

			for (int i = 0; i < 5; i++)
			{
				bucket.Add(i);
			}

			for (int i = 0; i < 5; i++)
			{
				bucket[i].ShouldEqual(i);
			}

			bucket.Buffer.ShouldNotBeNull();
			bucket.Buffer.Length.ShouldEqual(10);
			bucket.FreeSpace.ShouldEqual(5);
			bucket.Index.ShouldEqual(5);
			bucket.IsEmpty.ShouldBeFalse();
		}

		[Fact]
		public void CannAddRange()
		{
			var bucket = new Bucket<int>(10);
			bucket.AddRange(Enumerable.Range(0, 5).ToArray());

			for (int i = 0; i < 5; i++)
			{
				bucket[i].ShouldEqual(i);
			}

			bucket.Buffer.ShouldNotBeNull();
			bucket.Buffer.Length.ShouldEqual(10);
			bucket.FreeSpace.ShouldEqual(5);
			bucket.Index.ShouldEqual(5);
			bucket.IsEmpty.ShouldBeFalse();
		}

		[Fact]
		public void CantAddMoreThanCapacity()
		{
			var bucket = new Bucket<int>(5);

			for (int i = 0; i < 5; i++)
			{
				bucket.Add(i);
			}

			Trap.Exception(() => bucket.Add(1)).ShouldBeInstanceOf<InvalidOperationException>();
		}

		[Fact]
		public void CantAddRangeMoreThanCapacity()
		{
			var bucket = new Bucket<int>(5);
			Trap.Exception(() => bucket.AddRange(Enumerable.Range(0, 6).ToArray())).ShouldBeInstanceOf<InvalidOperationException>();
		}

		[Fact]
		public void CanGetLastItem()
		{
			var bucket = new Bucket<int>(5);
			bucket.Add(42);
			bucket.LastItem.ShouldEqual(42);
		}

		[Fact]
		public void CanSetElemetByIndex()
		{
			var bucket = new Bucket<int>(1);
			bucket.Add(42);
			bucket[0].ShouldEqual(42);
			bucket[0] = 1;
			bucket[0].ShouldEqual(1);
		}

		[Fact]
		public void CantSetElementPastCurrentIndex()
		{
			var bucket = new Bucket<int>(2);
			bucket.Add(42);
			Trap.Exception(() => bucket[1] = 1).ShouldBeInstanceOf<IndexOutOfRangeException>();
		}
	}
}