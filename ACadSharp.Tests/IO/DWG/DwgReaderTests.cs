﻿using ACadSharp.Header;
using ACadSharp.IO;
using ACadSharp.IO.DWG;
using ACadSharp.Tests.Common;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace ACadSharp.Tests.IO.DWG
{
	public class DwgReaderTests : CadReaderTestsBase<DwgReader>
	{
		public DwgReaderTests(ITestOutputHelper output) : base(output) { }

		[Theory]
		[MemberData(nameof(DwgFilePaths))]
		public override void ReadHeaderTest(string test)
		{
			base.ReadHeaderTest(test);
		}

		[Theory]
		[MemberData(nameof(DwgFilePaths))]
		public override void ReadTest(string test)
		{
			base.ReadTest(test);
		}

		[Theory]
		[MemberData(nameof(DwgFilePaths))]
		public override void AssertDocumentDefaults(string test)
		{
			base.AssertDocumentDefaults(test);
		}

		[Theory]
		[MemberData(nameof(DwgFilePaths))]
		public override void AssertTableHirearchy(string test)
		{
			base.AssertTableHirearchy(test);
		}

		[Theory]
		[MemberData(nameof(DwgFilePaths))]
		public override void AssertBlockRecords(string test)
		{
			base.AssertBlockRecords(test);
		}

		[Theory]
		[MemberData(nameof(DwgFilePaths))]
		public override void AssertDocumentTree(string test)
		{
			base.AssertDocumentTree(test);
		}

		[Theory(Skip = "Long time test")]
		[MemberData(nameof(DwgFilePaths))]
		public void ReadCrcEnabledTest(string test)
		{
			DwgReaderFlags flags = DwgReaderFlags.CheckCrc;

			CadDocument doc = DwgReader.Read(test, flags, this.onNotification);
		}
	}
}
