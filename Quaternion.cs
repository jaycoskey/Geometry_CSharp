// Copyright (c) 2003 by Jay M. Coskey.  All rights reserved.

using System;

namespace Coskey.Geometry
{
	/// <summary>
	/// Summary description for Quaternion.
	/// TODO: JMC: Add trig functions of quaternions?
	/// TODO: JMC: Add (nested?) vector quaternion class to represent angular velocity
	/// TODO: JMC: Use Slerp() to implement Bezier curves
	/// </summary>
	public class Quaternion
	{
		private double _qa, _qb, _qc, _qd;
		protected static double tolerance = 0.0001;
		public static Quaternion Zero = new Quaternion(0.0, 0.0, 0.0, 0.0);
		public static Quaternion RAxis = new Quaternion(1.0, 0.0, 0.0, 0.0);
		public static Quaternion IAxis = new Quaternion(0.0, 1.0, 0.0, 0.0);
		public static Quaternion JAxis = new Quaternion(0.0, 0.0, 1.0, 0.0);
		public static Quaternion KAxis = new Quaternion(0.0, 0.0, 0.0, 1.0);

		#region Constructors and Destructors
		public Quaternion()
		{
			this.R = this.I = this.J = this.K = 0.0;
		}

		public Quaternion(double a, double b, double c, double d) 
		{
			this.R = a;
			this.I = b;
			this.J = c;
			this.K = d;
		}
		
		public Quaternion(double scalar)
		{
			this.R = scalar;
			this.I = this.J = this.K = 0;
		}

		public Quaternion(Quaternion other) 
		{
			this.R = other.R;
			this.I = other.I;
			this.J = other.J;
			this.K = other.K;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Using 'R' to denote the component along the real axis is non-standard, but useful.
		/// </summary>
		public double R { get { return _qa; } set { _qa = value; } }
		public double I { get { return _qb; } set { _qb = value; } }
		public double J { get { return _qc; } set { _qc = value; } }
		public double K { get { return _qd; } set { _qd = value; } }
		public double Length { get { return Math.Sqrt(Product(this, this.Conjugate()).R); } }
		public double LengthSquared{ get { return Product(this, this.Conjugate()).R; } }
		#endregion

		#region Operators
		/// <summary>
		/// Unary '+'
		/// </summary>
		public static Quaternion operator +(Quaternion q) 
		{
			return q;
		}

		/// <summary>
		/// Unary '-'
		/// </summary>
		public static Quaternion operator -(Quaternion q) 
		{
			Quaternion result = new Quaternion(-q.R, -q.I, -q.J, -q.K);
			return result;
		}

		public static Quaternion operator +(Quaternion a, Quaternion b) 
		{
			Quaternion result = new Quaternion(a);
			result.R += b.R;
			result.I += b.I;
			result.J += b.J;
			result.K += b.K;
			return result;
		}

		public static Quaternion operator +(Quaternion q, double s) 
		{
			q.R += s;
			return q;
		}

		public static Quaternion operator +(double s, Quaternion q) 
		{
			return q + s;
		}

		public static Quaternion operator -(Quaternion a, Quaternion b) 
		{
			Quaternion result = a + (-b);
			return result;
		}

		public static Quaternion operator -(Quaternion q, double s) 
		{
			q.R -= s;
			return q;
		}

		public static Quaternion operator -(double s, Quaternion q) 
		{
			return (-q + s);
		}

		public static Quaternion operator *(Quaternion q, double s) 
		{
			return new Quaternion(s * q.R, s * q.I, s * q.J, s * q.K);
		}

		public static Quaternion operator *(double s, Quaternion q) 
		{
			return q * s;
		}

		public static Quaternion operator /(Quaternion q, double s) 
		{
			return new Quaternion(q.R / s, q.I / s, q.J / s, q.K / s);
		}
		#endregion // Operators

		#region Methods
		public Quaternion Conjugate()
		{
			return new Quaternion(this.R, -this.I, -this.J, -this.K); 
		}

		public Quaternion Inverse() 
		{
			Quaternion result = this.Conjugate() / this.Length;
			return result;
		}

		public Quaternion Multiply(Quaternion other) 
		{
			return Product(this, other);
		}

		public static Quaternion Product(Quaternion a, Quaternion b) 
		{
			Quaternion result = new Quaternion(
				a.R * b.R - a.I * b.I - a.J * b.J - a.K * b.K,
				a.R * b.I + a.I * b.R + a.J * b.K - a.K * b.J,
				a.R * b.J + a.J * b.R + a.K * b.I - a.I * b.K,
				a.R * b.K + a.K * b.R + a.I * b.J - a.J * b.I);
			return result;
		}

		public double ScalarDifference(Quaternion other) 
		{
			return (this - other).Length;
		}

		public override string ToString() 
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			writer.Write("({0:f} + {1:f}I + {2:f}J + {3:f}K)", this.R, this.I, this.J,	this.K);
			return writer.ToString();
		}

		/// <summary>
		/// Identifies the imaginary subspace of quaternionic space with R^3.
		/// </summary>
		public Vector3D ToVector3D() 
		{
			Vector3D result = new Vector3D(this.I, this.J, this.K);
			return result;
		}

		/// <summary>
		/// Remember: E ** (I * PI) == -1.
		/// In fact, ...
		/// E ** (v * PI) == -1 for any vector quaternion v of length one.
		/// </summary>
		public static Quaternion Exp(Quaternion q) 
		{
			double length = q.Length; 
			Quaternion unitLengthQuaternion = null;

			try { unitLengthQuaternion = q / length; }
			catch { return(Quaternion.RAxis); }
			Quaternion result = Math.Cos(length) * Quaternion.RAxis + Math.Sin(length) * unitLengthQuaternion;
			result *= Math.Exp(q.R);
			return result;
		}

		/// <summary>
		/// TODO: JMC: Document the cut point for the complex log function.
		/// 
		/// If q = r (cos A + v * sin A), then Log(q) = r + v * A,
		/// where r is the norm of q, and v is a vector quaternion.
		/// (A vector quaternion is one with zero real component.)
		/// </summary>
		public static Quaternion Log(Quaternion q) 
		{
			double lengthOfArg = q.Length;
			Quaternion normalizedArg = new Quaternion(q / lengthOfArg);
			Quaternion vectorComponent = new Quaternion(normalizedArg);

			// If the argument is close to being purely real, just return its real Log.
			// This safeguards against NaNs, and saves cycles.
			double realComponent = normalizedArg.R;
			double angle = Math.Acos(realComponent); // Angle between q and unity.
			vectorComponent.R = 0.0;
			double lengthOfVectorComponent = vectorComponent.Length;
			if (lengthOfVectorComponent < Quaternion.tolerance) 
			{
				return new Quaternion(Math.Log(realComponent));
			}
			vectorComponent = vectorComponent / vectorComponent.Length;
			Quaternion result = new Quaternion(Math.Log(lengthOfArg)) + angle * vectorComponent;
			return result;
		}

		/// <summary>
		/// Note: Since quaternions are non-commutative, 
		///		it is NOT always the case that log(x**y) = y * log(x) = log(x) * y.
		/// To make the definition of Pow(x,y) well-defined,
		///		the second argument is restricted to be real.
		/// This is a sufficient, but not necessary condition.
		///
		/// </summary>
		public static Quaternion Pow(Quaternion x, double y) 
		{
			return(Exp(y * Log(x)));
		}

		/// <summary>
		/// Spherical Linear Interpolation ("Slerp").
		/// Slerp of quaternions yeilds a natural transition from one rotation to another.
		/// </summary>
		/// <param name="x">Starting Quaternion</param>
		/// <param name="y">Ending Quaternion</param>
		/// <param name="t">Time, which goes from 0 to 1</param>
		/// <returns></returns>
		public static Quaternion Slerp(Quaternion x, Quaternion y, double t)
		{
			return (Pow(x, 1-t).Multiply(Pow(y, t))); 
		}
		#endregion // Methods
	}
}
