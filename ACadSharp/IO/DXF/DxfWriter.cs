﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACadSharp.IO.DXF
{
	public class DxfWriter : IDisposable
	{
		private CadDocument _document;
		private IDxfStreamWriter _writer;
		private CadObjectHolder _objectHolder = new CadObjectHolder();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="document"></param>
		/// <param name="binary"></param>
		/// <exception cref="NotImplementedException">Binary writer not implemented</exception>
		public DxfWriter(string filename, CadDocument document, bool binary)
			: this(File.Create(filename), document, binary)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="document"></param>
		/// <param name="binary"></param>
		/// <exception cref="NotImplementedException">Binary writer not implemented</exception>
		public DxfWriter(Stream stream, CadDocument document, bool binary)
		{
			var encoding = new UTF8Encoding(false);

			if (binary)
			{
				throw new NotImplementedException();
			}
			else
			{
				this._writer = new DxfAsciiWriter(new StreamWriter(stream, encoding));
			}

			this._document = document;
		}

		public void Write()
		{
			this._objectHolder.Objects.Enqueue(_document.RootDictionary);

			this.writeHeader();

			this.writeDxfClasses();

			this.writeTables();

			this.writeBlocks();

			this.writeEntities();

			this.writeObjects();

			this.writeACDSData();

			this._writer.Write(DxfCode.Start, DxfFileToken.EndOfFile);
		}


		/// <inheritdoc/>
		public void Dispose()
		{
			this._writer.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="document"></param>
		/// <param name="binary"></param>
		/// <exception cref="NotImplementedException"></exception>
		public static void Write(string filename, CadDocument document, bool binary)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="document"></param>
		/// <param name="binary"></param>
		/// <exception cref="NotImplementedException"></exception>
		public static void Write(Stream stream, CadDocument document, bool binary)
		{
			throw new NotImplementedException();
		}

		private void writeHeader()
		{
			new DxfHeaderSectionWriter(this._writer, this._document, this._objectHolder).Write();
		}

		private void writeDxfClasses()
		{
			new DxfDxfClassesSectionWriter(this._writer, this._document, this._objectHolder).Write();
		}

		private void writeTables()
		{
			new DxfTablesSectionWriter(this._writer, this._document, this._objectHolder).Write();
		}

		private void writeBlocks()
		{
			new DxfBlocksSectionWriter(this._writer, this._document, this._objectHolder).Write();
		}

		private void writeEntities()
		{
			new DxfEntitiesSectionWriter(this._writer, this._document, this._objectHolder).Write();
		}

		private void writeObjects()
		{
			new DxfObjectsSectionWriter(this._writer, this._document, this._objectHolder).Write();
		}

		private void writeACDSData()
		{
		}
	}
}
