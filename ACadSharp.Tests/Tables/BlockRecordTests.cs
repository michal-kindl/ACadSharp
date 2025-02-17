﻿using ACadSharp.Entities;
using ACadSharp.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ACadSharp.Tables.Tests
{
	public class BlockRecordTests
	{
		[Fact()]
		public void BlockRecordTest()
		{
			string name = "my_block";
			BlockRecord record = new BlockRecord(name);

			Assert.Equal(name, record.Name);

			Assert.NotNull(record.BlockEntity);
			Assert.Equal(record.Name, record.BlockEntity.Name);

			Assert.NotNull(record.BlockEnd);
		}

		[Fact()]
		public void AddEntityTest()
		{
			BlockRecord record = new BlockRecord();

			Line l1 = new Line();
			Line l2 = new Line();
			Line l3 = new Line();
			Line l4 = new Line();

			record.Entities.Add(l1);
			record.Entities.Add(l2);
			record.Entities.Add(l3);
			record.Entities.Add(l4);

			foreach (Entity e in record.Entities)
			{
				Assert.Equal(record, e.Owner);
			}
		}

		[Fact()]
		public void NotAllowDuplicatesTest()
		{
			BlockRecord record = new BlockRecord();

			Line l1 = new Line();

			record.Entities.Add(l1);
			Assert.Throws<ArgumentException>(() => record.Entities.Add(l1));
		}
	}
}