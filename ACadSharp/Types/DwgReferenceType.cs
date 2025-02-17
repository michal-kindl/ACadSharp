﻿#region copyright
//Copyright 2021, Albert Domenech.
//All rights reserved. 
//This source code is licensed under the MIT license. 
//See LICENSE file in the project root for full license information.
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace ACadSharp
{
	public enum DwgReferenceType
	{
		/// <summary>
		/// Soft ownership reference: the owner does not need the owned object. The owned object cannot exist by itself.
		/// </summary>
		SoftOwnership = 2,

		/// <summary>
		/// Hard ownership reference: the owner needs the owned object. The owned object cannot exist by itself.
		/// </summary>
		HardOwnership = 3,

		/// <summary>
		/// Soft pointer reference: the referencing object does not need the referenced object and vice versa.
		/// </summary>
		SoftPointer = 4,

		/// <summary>
		/// Hard pointer reference: the referencing object needs the referenced object, but both are owned by another object.
		/// </summary>
		HardPointer = 5,
	}
}
