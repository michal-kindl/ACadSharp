﻿using ACadSharp.Attributes;

namespace ACadSharp.Entities
{
	/// <summary>
	/// Represents a <see cref="Seqend"/> entity
	/// </summary>
	/// <remarks>
	/// Object name <see cref="DxfFileToken.EntitySeqend"/> <br/>
	/// </remarks>
	[DxfName(DxfFileToken.EntitySeqend)]
	public class Seqend : Entity
	{
		public override ObjectType ObjectType => ObjectType.SEQEND;

		public override string ObjectName => DxfFileToken.EntitySeqend;
	}
}
