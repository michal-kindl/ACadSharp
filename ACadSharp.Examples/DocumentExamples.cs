﻿using ACadSharp.Entities;
using ACadSharp.IO.DWG;
using ACadSharp.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACadSharp.Examples
{
	public static class DocumentExamples
	{
		/// <summary>
		/// Get all the entities in the model
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static IEnumerable<Entity> GetAllEntitiesInModel(string file)
		{
			CadDocument doc = DwgReader.Read(file);

			// Get the model space where all the drawing entities are
			BlockRecord modelSpace = doc.BlockRecords["*Model_Space"];

			// Get all the entities in the model space
			return modelSpace.Entities;
		}

		/// <summary>
		/// Get all the blocks in the model
		/// </summary>
		/// <param name="file"></param>
		/// <param name="blockname"></param>
		/// <returns></returns>
		public static IEnumerable<Insert> GetInsertEntities(string file, string blockname)
		{
			CadDocument doc = DwgReader.Read(file);

			// Get the model space where all the drawing entities are
			BlockRecord modelSpace = doc.BlockRecords["*Model_Space"];

			// Get the insert instance that is using the block that you are looking for
			return modelSpace.Entities.OfType<Insert>().Where(e => e.Block.Name == blockname);
		}

		/// <summary>
		/// Example on how to create entities and add them into the drawing
		/// </summary>
		public static void CreateEntities()
		{
			//Create a new document
			CadDocument doc = new CadDocument();

			//Create a point located in (10, 10, 0)
			Point pt = new Point
			{
				Location = new CSMath.XYZ(10, 10, 0)
			};

			//Create a line from the origin to the point (5, 5, 0)
			Line line = new Line
			{
				StartPoint = CSMath.XYZ.Zero,
				EndPoint = new CSMath.XYZ(5, 5, 0)
			};

			doc.BlockRecords["*Model_Space"].Entities.Add(pt);
			doc.BlockRecords["*Model_Space"].Entities.Add(line);
		}
	}
}
