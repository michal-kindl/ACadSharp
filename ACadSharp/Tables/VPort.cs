﻿using ACadSharp.Attributes;
using ACadSharp.Entities;
using ACadSharp.IO.Templates;
using ACadSharp.Objects;
using CSMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACadSharp.Tables
{
	/// <summary>
	/// Represents a <see cref="VPort"/> table entry
	/// </summary>
	/// <remarks>
	/// Object name <see cref="DxfFileToken.TableVport"/> <br/>
	/// Dxf class name <see cref="DxfSubclassMarker.VPort"/>
	/// </remarks>
	[DxfName(DxfFileToken.TableVport)]
	[DxfSubClass(DxfSubclassMarker.VPort)]
	public class VPort : TableEntry
	{
		/// <inheritdoc/>
		public override ObjectType ObjectType => ObjectType.VPORT;

		/// <inheritdoc/>
		public override string ObjectName => DxfFileToken.TableVport;

		public static VPort Default { get { return new VPort("*Active"); } }

		/// <summary>
		/// Lower-left corner of viewport
		/// </summary>
		[DxfCodeValue(10, 20)]
		public XY BottomLeft { get; set; }

		/// <summary>
		/// Upper-right corner of viewport
		/// </summary>
		[DxfCodeValue(11, 21)]
		public XY TopRight { get; set; }

		/// <summary>
		/// View center point(in DCS)
		/// </summary>
		[DxfCodeValue(12, 22)]
		public XY Center { get; set; }

		/// <summary>
		/// Snap base point(in DCS)
		/// </summary>
		[DxfCodeValue(13, 23)]
		public XY SnapBasePoint { get; set; }

		/// <summary>
		/// Snap spacing X and Y
		/// </summary>
		[DxfCodeValue(14, 24)]
		public XY SnapSpacing { get; set; }

		/// <summary>
		/// Grid spacing X and Y
		/// </summary>
		[DxfCodeValue(15, 25)]
		public XY GridSpacing { get; set; }

		/// <summary>
		/// View direction from target point(in WCS)
		/// </summary>
		[DxfCodeValue(16, 26, 36)]
		public XYZ Direction { get; set; }

		/// <summary>
		/// View target point(in WCS)
		/// </summary>
		[DxfCodeValue(17, 27, 37)]
		public XYZ Target { get; set; }

		/// <summary>
		/// View height
		/// </summary>
		[DxfCodeValue(40)]  //In the web docs the value is 45
		public double ViewHeight { get; set; } = 10;

		/// <summary>
		/// Aspect ratio
		/// </summary>
		[DxfCodeValue(41)]
		public double AspectRatio { get; set; } = 1;

		/// <summary>
		/// Lens length
		/// </summary>
		[DxfCodeValue(42)]
		public double LensLength { get; set; }

		/// <summary>
		/// Front clipping plane(offset from target point)
		/// </summary>
		[DxfCodeValue(43)]
		public double FrontClippingPlane { get; set; }

		/// <summary>
		/// Back clipping plane(offset from target point)
		/// </summary>
		[DxfCodeValue(44)]
		public double BackClippingPlane { get; set; }

		/// <summary>
		/// Snap rotation angle
		/// </summary>
		[DxfCodeValue(50)]
		public double SnapRotation { get; set; }

		/// <summary>
		/// View twist angle
		/// </summary>
		[DxfCodeValue(51)]
		public double TwistAngle { get; set; }

		/// <summary>
		/// Circle sides
		/// </summary>
		[DxfCodeValue(72)]
		public short CircleZoomPercent { get; set; }

		//331 or 441

		//Soft or hard-pointer ID/handle to frozen layer objects; repeats for each frozen layers

		//1	Plot style sheet

		/// <summary>
		/// Render mode
		/// </summary>
		[DxfCodeValue(281)]
		public RenderMode RenderMode { get; set; }

		/// <summary>
		/// View mode(see VIEWMODE system variable)
		/// </summary>
		[DxfCodeValue(71)]
		public ViewModeType ViewMode { get; set; }

		/// <summary>
		/// UCSICON setting
		/// </summary>
		[DxfCodeValue(74)]
		public UscIconType UcsIconDisplay { get; set; }

		/// <summary>
		/// Snap on/off
		/// </summary>
		[DxfCodeValue(75)]
		public bool SnapOn { get; set; }

		/// <summary>
		/// Grid on/off
		/// </summary>
		[DxfCodeValue(76)]
		public bool ShowGrid { get; set; }

		/// <summary>
		/// Snap style
		/// </summary>
		[DxfCodeValue(77)]
		public bool IsometricSnap { get; set; }

		/// <summary>
		/// Snap style
		/// </summary>
		[DxfCodeValue(78)]
		public short SnapIsoPair { get; set; }

		/// <summary>
		/// UCS origin
		/// </summary>
		[DxfCodeValue(110, 120, 130)]
		public XYZ Origin { get; set; }

		/// <summary>
		/// UCS X-axis
		/// </summary>
		[DxfCodeValue(111, 121, 131)]
		public XYZ XAxis { get; set; }

		/// <summary>
		/// UCS Y-axis
		/// </summary>
		[DxfCodeValue(112, 122, 132)]
		public XYZ YAxis { get; set; }

		/// <summary>
		/// Named Ucs
		/// </summary>
		/// <remarks>
		/// AcDbUCSTableRecord if UCS is a named UCS.If not present, then UCS is unnamed
		/// </remarks>
		[DxfCodeValue(DxfReferenceType.Handle, 345)]
		public UCS NamedUcs { get; set; }

		/// <summary>
		/// Base Ucs
		/// </summary>
		/// <remarks>
		/// AcDbUCSTableRecord of base UCS if UCS is orthographic(79 code is non-zero). 
		/// If not present and 79 code is non-zero, then base UCS is taken to be WORLD
		/// </remarks>
		[DxfCodeValue(DxfReferenceType.Handle, 346)]
		public UCS BaseUcs { get; set; }

		/// <summary>
		/// Orthographic type of UCS
		/// </summary>
		[DxfCodeValue(79)]
		public OrthographicType OrthographicType { get; set; }

		/// <summary>
		/// Elevation
		/// </summary>
		[DxfCodeValue(146)]
		public double Elevation { get; set; }

		//170	Shade plot setting

		/// <summary>
		/// Grid flags
		/// </summary>
		[DxfCodeValue(60)]
		public GridFlags GridFlags { get; set; }

		/// <summary>
		/// Major grid lines
		/// </summary>
		[DxfCodeValue(61)]
		public short MinorGridLinesPerMajorGridLine { get; set; }


		//332	Soft-pointer ID/handle to background object (optional)

		//333	Soft-pointer ID/handle to shade plot object (optional)

		/// <summary>
		/// Visual style object (optional)
		/// </summary>
		/// <remarks>
		/// (optional)
		/// </remarks>
		[DxfCodeValue(DxfReferenceType.Handle, 348)]
		public VisualStyle VisualStyle { get; set; }

		/// <summary>
		/// Default Lighting On flag
		/// </summary>
		[DxfCodeValue(292)]
		public bool UseDefaultLighting { get; set; }

		/// <summary>
		/// Default Lighting type
		/// </summary>
		[DxfCodeValue(282)]
		public DefaultLightingType DefaultLighting { get; set; }

		/// <summary>
		/// Brightness
		/// </summary>
		[DxfCodeValue(141)]
		public double Brightness { get; set; }

		/// <summary>
		/// Contrast
		/// </summary>
		[DxfCodeValue(142)]
		public double Contrast { get; set; }

		/// <summary>
		/// Ambient color(only output when non-black)
		/// </summary>
		[DxfCodeValue(63, 421, 431)]
		public Color AmbientColor { get; set; }

		public VPort() : this(null) { }

		public VPort(string name) : base(name) { }
	}
}
