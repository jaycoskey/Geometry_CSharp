// Copyright (c) 2003 by Jay M. Coskey.  All rights reserved.

using System;

namespace Coskey.Geometry
{
	/// <summary>
	/// Summary description for Vector3D.
	/// </summary>
	public class Vector3D
	{
		private double _vx, _vy, _vz;
		public static Vector3D Zero = new Vector3D(0.0, 0.0, 0.0);
		public static Vector3D XAxis = new Vector3D(1.0, 0.0, 0.0);
		public static Vector3D YAxis = new Vector3D(0.0, 1.0, 0.0);
		public static Vector3D ZAxis = new Vector3D(0.0, 0.0, 1.0);
		
		#region Constructors and Destructors
		public Vector3D()
		{
			this.X = this.Y = this.Z = 0.0;
		}
		public Vector3D(double xVal, double yVal, double zVal) 
		{
			this.X = xVal;
			this.Y = yVal;
			this.Z = zVal;
		}
		Vector3D(Vector3D other)
		{
			X = other.X;
			Y = other.Y;
			Z = other.Z;
		}
		#endregion // Constructors and Destructors

		#region Properties
		public double X { get { return _vx; } set { _vx = value; } }
		public double Y { get { return _vy; } set { _vy = value; } }
		public double Z { get { return _vz; } set { _vz = value; } }
		public double Length 
		{
			get { return Math.Sqrt(this.LengthSquared); }
		}
		public double LengthSquared
		{
			get { return (this.X * this.X + this.Y * this.Y +  this.Z * this.Z); }
		}
		#endregion // Properties

		#region Operators
		public static Vector3D operator +(Vector3D a, Vector3D b) 
		{
			return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}
		public static Vector3D operator -(Vector3D a, Vector3D b) 
		{
			return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}
		public static Vector3D operator *(Vector3D v, double s) 
		{
			return new Vector3D(s * v.X, s * v.Y, s * v.Z);
		}
		public static Vector3D operator *(double s, Vector3D v) 
		{
			return v * s;
		}
		public static Vector3D operator /(Vector3D v, double s) 
		{
			return new Vector3D(v.X / s, v.Y / s, v.Z / s);
		}
		#endregion // Operators

		#region Methods
		public static Vector3D CrossProduct(Vector3D a, Vector3D b) 
		{
			Vector3D result = new Vector3D(
				a.Y * b.Z - a.Z * b.Y,  
				a.Z * b.X - a.X * b.Z,  
				a.X * b.Y - a.Y * b.X );
			return result;
		}

		public double DotProduct(Vector3D a, Vector3D b)
		{
			return(a.X * b.X + a.Y * b.Y + a.Z * b.Z); 
		}

		public double ScalarDifference(Vector3D other) 
		{
			return (this - other).Length;
		}

		public static Vector3D Interpolate(double t, Vector3D x, Vector3D y)
		{
			return( (1 - t) * x + t * y ); 
		}

		/// <summary>
		/// Identifies the imaginary subspace of quaternionic space with R^3.
		/// </summary>
		public Quaternion ToQuaternion() 
		{
			Quaternion result = new Quaternion(0.0, this.X, this.Y, this.Z);
			return result;
		}

		public override string ToString() 
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			writer.Write("({0:f}, {1:f}, {2:f})", this.X, this.Y, this.Z);
			return writer.ToString();
		}
		#endregion // Methods
	}
}
