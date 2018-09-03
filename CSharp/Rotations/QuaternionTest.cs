// Copyright (c) 2003 by Jay M. Coskey.  All rights reserved.

using System;

using NUnit.Framework;

namespace Coskey.Geometry
{
	/// <summary>
	/// Summary description for QuaternionTest.
	/// </summary>
	[TestFixture]
	public class QuaternionTest
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
			Quaternion expected = new Quaternion(0.0, 1.0, 1.0, 0.0);
			Quaternion actual = Quaternion.IAxis + Quaternion.JAxis;
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}

		
		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void Conjugation() 
		{
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void Constructors() 
		{
		}

		[Test]
		public void Division() 
		{
			Quaternion expected = new Quaternion(0.0, 0.5, 0.0, 0.0);
			Quaternion actual = Quaternion.IAxis / 2;
			double difference = (expected - actual).Length;
			Assertion.AssertEquals(0.0, difference, tolerance);
		}

		/// <summary>
		/// TODO: JMC: Add test for the case Log(0).
		/// </summary>
		[Test]
		public void ExpAndLog() 
		{
			Quaternion qIEL = Quaternion.Exp(Quaternion.Log(Quaternion.IAxis));
			Quaternion qILE = Quaternion.Log(Quaternion.Exp(Quaternion.IAxis));

			Quaternion qJEL = Quaternion.Exp(Quaternion.Log(Quaternion.JAxis));
			Quaternion qJLE = Quaternion.Log(Quaternion.Exp(Quaternion.JAxis));

			Quaternion qKEL = Quaternion.Exp(Quaternion.Log(Quaternion.KAxis));
			Quaternion qKLE = Quaternion.Log(Quaternion.Exp(Quaternion.KAxis));

			Quaternion qII = Quaternion.Pow(Quaternion.IAxis, 2.0);
			Quaternion qJJ = Quaternion.Pow(Quaternion.JAxis, 2.0);
			Quaternion qKK = Quaternion.Pow(Quaternion.KAxis, 2.0);

			Assertion.AssertEquals(0.0, qIEL.ScalarDifference(qILE), tolerance);
			Assertion.AssertEquals(0.0, qIEL.ScalarDifference(qILE), tolerance);

			Assertion.AssertEquals(0.0, qJEL.ScalarDifference(qJLE), tolerance);
			Assertion.AssertEquals(0.0, qJEL.ScalarDifference(qJLE), tolerance);

			Assertion.AssertEquals(0.0, qKEL.ScalarDifference(qKLE), tolerance);
			Assertion.AssertEquals(0.0, qKEL.ScalarDifference(qKLE), tolerance);

			Assertion.AssertEquals(0.0, qII.ScalarDifference(-Quaternion.RAxis), tolerance);
			Assertion.AssertEquals(0.0, qJJ.ScalarDifference(-Quaternion.RAxis), tolerance);
			Assertion.AssertEquals(0.0, qKK.ScalarDifference(-Quaternion.RAxis), tolerance);
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void Inverse() 
		{
		}

		[Test]
		public void Multiplication() 
		{
			Quaternion II = Quaternion.Product(Quaternion.IAxis, Quaternion.IAxis);
			Quaternion IJ = Quaternion.Product(Quaternion.IAxis, Quaternion.JAxis);
			Quaternion IK = Quaternion.Product(Quaternion.IAxis, Quaternion.KAxis);
			Quaternion JI = Quaternion.Product(Quaternion.JAxis, Quaternion.IAxis);
			Quaternion JJ = Quaternion.Product(Quaternion.JAxis, Quaternion.JAxis);
			Quaternion JK = Quaternion.Product(Quaternion.JAxis, Quaternion.KAxis);
			Quaternion KI = Quaternion.Product(Quaternion.KAxis, Quaternion.IAxis);
			Quaternion KJ = Quaternion.Product(Quaternion.KAxis, Quaternion.JAxis);
			Quaternion KK = Quaternion.Product(Quaternion.KAxis, Quaternion.KAxis);

			Assertion.AssertEquals(0.0, II.ScalarDifference(-Quaternion.RAxis), tolerance);
			Assertion.AssertEquals(0.0, IJ.ScalarDifference(Quaternion.KAxis), tolerance);
			Assertion.AssertEquals(0.0, IK.ScalarDifference(-Quaternion.JAxis), tolerance);
			Assertion.AssertEquals(0.0, JI.ScalarDifference(-Quaternion.KAxis), tolerance);
			Assertion.AssertEquals(0.0, JJ.ScalarDifference(-Quaternion.RAxis), tolerance);
			Assertion.AssertEquals(0.0, JK.ScalarDifference(Quaternion.IAxis), tolerance);
			Assertion.AssertEquals(0.0, KI.ScalarDifference(Quaternion.JAxis), tolerance);
			Assertion.AssertEquals(0.0, KJ.ScalarDifference(-Quaternion.IAxis), tolerance);
			Assertion.AssertEquals(0.0, KK.ScalarDifference(-Quaternion.RAxis), tolerance);
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void ScalarDifference() 
		{
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void Slerp() 
		{
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void StringRepresentation() 
		{
		}

		[Test]
		public void Subtraction() 
		{
			Quaternion expected = new Quaternion(0.0, 1.0, -1.0, 0.0);
			Quaternion actual = Quaternion.IAxis - Quaternion.JAxis;
			Assertion.AssertEquals(0.0, expected.ScalarDifference(actual), tolerance);
		}

		[Test]
		[Ignore("TODO: JMC: Not yet written")]
		public void ToVector3D() 
		{
		}
		#endregion // Tests
	}
}