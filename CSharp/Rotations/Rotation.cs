// Copyright (c) 2003 by Jay M. Coskey.  All rights reserved.

using System;

namespace Coskey.Geometry
{
	/// <summary>
	/// Summary description for Rotation.
	/// TODO: JMC: Allow conversion to and from matrices
	/// TODO: JMC: Allow representation via Euler angles
	/// TODO: JMC: Add support for angular velocity (vector quaternion class)
	/// TODO: JMC: Add support for distinguishing between body & space coordinates
	/// </summary>
	public class Rotation
	{
		private Quaternion _quat;

		#region Constructor, Destructors, and Factories
		public Rotation()
		{
			_quat = new Quaternion(1.0, 0.0, 0.0, 0.0);
		}

		public Rotation(Vector3D axis, double angle)
		{
			Vector3D unitLengthAxis = axis / axis.Length;
			double sinOfAngle = Math.Sin(angle/2);
			_quat = new Quaternion(
				Math.Cos(angle/2),
				sinOfAngle * unitLengthAxis.X,
				sinOfAngle * unitLengthAxis.Y,
				sinOfAngle * unitLengthAxis.Z);
		}

		public static Rotation RotationAboutXAxis(double angle) 
		{
			return new Rotation(Vector3D.XAxis, angle);
		}

		public static Rotation RotationAboutYAxis(double angle)
		{
			return new Rotation(Vector3D.YAxis, angle);
		}

		public static Rotation RotationAboutZAxis(double angle)
		{
			return new Rotation(Vector3D.ZAxis, angle);
		}

		#endregion // Constructor, Destructors, and Factories

		#region Properties
		#endregion

		#region Operators
		#endregion

		#region Methods
		/// <summary>
		/// The outcome of rotating a vector using a quaternion
		///		is q * v * q^(-1),
		///		where R^3 is identified with the imaginary subspace quaternionic space.
		/// </summary>
		public Vector3D Rotate(Vector3D vec) 
		{
			Quaternion v = vec.ToQuaternion();
			Quaternion qInv = _quat.Inverse();
			Quaternion qResult = _quat.Multiply(v).Multiply(qInv);
			Vector3D vResult = qResult.ToVector3D();
			return vResult;
		}

		public override string ToString()
		{
			return "Rotation-" + _quat.ToString();
		}
		#endregion // Methods
	}
}
