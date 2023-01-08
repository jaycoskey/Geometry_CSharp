// Copyright (c) 2003, by Jay M. Coskey.  All rights reserved.

using System;

using NUnit.Framework;

namespace Coskey.Geometry
{
	/// <summary>
	/// Summary description for Vector3DTest.
	/// </summary>
	[TestFixture]
	public class Vector3DTest
	{
		protected double tolerance;

		[SetUp]
		public void Init() 
		{
			tolerance = Test.Tolerance;
		}

		#region Tests
		[Test]
		public void Addition() 
		{
			Vector3D expected = new Vector3D(1.0, 1.0, 0.0);
			Vector3D actual = Vector3D.XAxis + Vector3D.YAxis;
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}

		[Test]
		public void Subtraction() 
		{
			Vector3D expected = new Vector3D(1.0, -1.0, 0.0);
			Vector3D actual = Vector3D.XAxis - Vector3D.YAxis;
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}

		[Test]
		public void Multiplication() 
		{
			Vector3D expected = new Vector3D(5.0, 0.0, 0.0);
			Vector3D actual = 5 * Vector3D.XAxis;
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}

		[Test]
		public void Division() 
		{
			Vector3D expected = new Vector3D(0.5, 0.0, 0.0);
			Vector3D actual = Vector3D.XAxis / 2;
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void DivisionByZero() 
		{
		}

		[Test]
		public void DotProduct() 
		{
			Vector3D expected = Vector3D.ZAxis;
			Vector3D actual = Vector3D.CrossProduct(Vector3D.XAxis, Vector3D.YAxis);
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}

		[Test]
		public void CrossProduct() 
		{
			Vector3D expected = Vector3D.ZAxis;
			Vector3D actual = Vector3D.CrossProduct(Vector3D.XAxis, Vector3D.YAxis);
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}
		#endregion // Tests
	}
}