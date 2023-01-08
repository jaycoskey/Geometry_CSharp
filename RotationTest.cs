// Copyright (c) 2003, by Jay M. Coskey.  All rights reserved.

using System;

using NUnit.Framework;

namespace Coskey.Geometry
{
	/// <summary>
	/// Summary description for RotationTest.
	/// </summary>
	[TestFixture]
	public class RotationTest
	{
		protected double tolerance;
		protected Vector3D vec1;

		[SetUp]
		public void Init()
		{
			tolerance = Test.Tolerance;
			vec1 = new Vector3D(1.0, 2.0, 3.0);
		}

		#region Tests

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void Constructors() 
		{
		}

		[Test]
		public void NonCommutativity() 
		{
			Vector3D axis1 = new Vector3D(1.0, -4.0, 9.0);
			Vector3D axis2 = new Vector3D(1.0, 4.0, 9.0);
			double angle1 = Math.PI / 3;
			double angle2 = Math.PI / 6;

			Rotation r1 = new Rotation(axis1, angle1);
			Rotation r2 = new Rotation(axis2, angle2);
			Vector3D result1 = r2.Rotate(r1.Rotate(vec1));
			Vector3D result2 = r1.Rotate(r2.Rotate(vec1));

			Assertion.Assert(result1.ScalarDifference(result2) >= tolerance);
		}

		[Test]
		public void NoRotation() 
		{
			Rotation identity = new Rotation(Vector3D.XAxis, 0.0);
			Vector3D newVec1 = identity.Rotate(vec1);
			System.Diagnostics.Debug.Write("NoRotation: vec1 = "    + vec1.ToString());
			System.Diagnostics.Debug.Write("NoRotation: newVec1 = " + newVec1.ToString());
			Assertion.AssertEquals(0.0, vec1.ScalarDifference(vec1), tolerance);
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void RotataionAboutObliqueAxis() 
		{
		}

		[Test]
		public void RotationAboutXAxis()
		{
			Rotation r = Rotation.RotationAboutXAxis(Math.PI / 2);
			Vector3D result = r.Rotate(Vector3D.YAxis);
			Assertion.AssertEquals(0.0, result.ScalarDifference(Vector3D.ZAxis), tolerance);
		}

		[Test]
		public void RotationAboutYAxis()
		{
			Rotation r = Rotation.RotationAboutYAxis(Math.PI / 2);
			Vector3D result = r.Rotate(Vector3D.ZAxis);
			Assertion.AssertEquals(0.0, result.ScalarDifference(Vector3D.XAxis), tolerance);
		}

		[Test]
		public void RotationAboutZAxis() 
		{
			Rotation r = Rotation.RotationAboutZAxis(Math.PI / 2);
			Vector3D result = r.Rotate(Vector3D.XAxis);
			Assertion.AssertEquals(0.0, result.ScalarDifference(Vector3D.YAxis), tolerance);
		}
		#endregion // Tests
	}
}