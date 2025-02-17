﻿using ACadSharp.IO;
using ACadSharp.IO.DWG;
using ACadSharp.IO.DXF;
using System;

namespace ACadSharp.Examples
{
	public static class ReaderExamples
	{
		/// <summary>
		/// Read a dxf file
		/// </summary>
		/// <param name="file">dxf file path</param>
		public static void ReadDxf(string file)
		{
			using (DxfReader reader = new DxfReader(file, onNotification))
			{
				CadDocument doc = reader.Read();
			}
		}

		/// <summary>
		/// Read a dwg file
		/// </summary>
		/// <param name="file">dwg file path</param>
		public static void ReadDwg(string file)
		{
			using (DwgReader reader = new DwgReader(file, onNotification))
			{
				CadDocument doc = reader.Read();
			}
		}

		private static void onNotification(object sender, NotificationEventArgs e)
		{
			//Write in the console all the messages
			Console.WriteLine(e.Message);
		}
	}
}
