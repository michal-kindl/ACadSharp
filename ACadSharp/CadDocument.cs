﻿using ACadSharp.Blocks;
using ACadSharp.Classes;
using ACadSharp.Entities;
using ACadSharp.Header;
using ACadSharp.Objects;
using ACadSharp.Tables;
using ACadSharp.Tables.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACadSharp
{
	/// <summary>
	/// An AutoCAD drawing
	/// </summary>
	public class CadDocument : IHandledCadObject
	{
		/// <summary>
		/// The document handle is always 0, this field makes sure that no object overrides this value
		/// </summary>
		public ulong Handle { get { return 0; } }

		/// <summary>
		/// Contains all the header variables for this document.
		/// </summary>
		public CadHeader Header { get; set; }

		/// <summary>
		/// Accesses drawing properties such as the Title, Subject, Author, and Keywords properties
		/// </summary>
		public CadSummaryInfo SummaryInfo { get; set; }

		/// <summary>
		/// Dxf classes defined in this document
		/// </summary>
		public DxfClassCollection Classes { get; set; } = new DxfClassCollection();

		/// <summary>
		/// The collection of all registered applications in the drawing
		/// </summary>
		public AppIdsTable AppIds { get; private set; }

		/// <summary>
		/// The collection of all block records in the drawing
		/// </summary>
		public BlockRecordsTable BlockRecords { get; private set; }

		/// <summary>
		/// The collection of all dimension styles in the drawing
		/// </summary>
		public DimensionStylesTable DimensionStyles { get; private set; }

		/// <summary>
		/// The collection of all layers in the drawing
		/// </summary>
		public LayersTable Layers { get; private set; }

		/// <summary>
		/// The collection of all linetypes in the drawing
		/// </summary>
		public LineTypesTable LineTypes { get; private set; }

		/// <summary>
		/// The collection of all text styles in the drawing
		/// </summary>
		public TextStylesTable TextStyles { get; private set; }

		/// <summary>
		/// The collection of all user coordinate systems (UCSs) in the drawing
		/// </summary>
		public UCSTable UCSs { get; private set; }

		/// <summary>
		/// The collection of all views in the drawing
		/// </summary>
		public ViewsTable Views { get; private set; }

		/// <summary>
		/// The collection of all vports in the drawing
		/// </summary>
		public VPortsTable VPorts { get; private set; }

		/// <summary>
		/// The collection of all layouts in the drawing
		/// </summary>
		public Layout[] Layouts { get { return this._cadObjects.Values.OfType<Layout>().ToArray(); } }   //TODO: Layouts have to go to the designed dictionary or blocks

		/// <summary>
		/// The collection of all viewports in the drawing
		/// </summary>
		[Obsolete("Viewports are only used by the R14 versions of dwg")]
		public ViewportCollection Viewports { get; private set; }

		/// <summary>
		/// Root dictionary of the document
		/// </summary>
		public CadDictionary RootDictionary
		{
			get { return this._rootDictionary; }
			internal set
			{
				this._rootDictionary = value;
				this._rootDictionary.Owner = this;
				this.RegisterCollection(_rootDictionary);
			}
		}

		/// <summary>
		/// Collection with all the entities in the drawing
		/// </summary>
		public CadObjectCollection<Entity> ModelEntities { get { return this.BlockRecords[BlockRecord.ModelSpaceName].Entities; } }

		/// <summary>
		/// Model space block record containing the drawing
		/// </summary>
		public BlockRecord ModelSpace { get { return this.BlockRecords[BlockRecord.ModelSpaceName]; } }

		/// <summary>
		/// Default paper space of the model
		/// </summary>
		public BlockRecord PaperSpace { get { return this.BlockRecords[BlockRecord.PaperSpaceName]; } }

		private CadDictionary _rootDictionary = new CadDictionary();

		//Contains all the objects in the document
		private readonly Dictionary<ulong, IHandledCadObject> _cadObjects = new Dictionary<ulong, IHandledCadObject>();

		internal CadDocument(bool createDefaults)
		{
			this._cadObjects.Add(this.Handle, this);

			//Initalize viewports only for management 
			//this.Viewports = new ViewportCollection(this);

			if (createDefaults)
			{
				//Header and summary
				this.Header = new CadHeader();
				this.SummaryInfo = new CadSummaryInfo();

				//The order of the elements is rellevant for the handles assignation

				//Initialize tables
				this.BlockRecords = new BlockRecordsTable(this);
				this.Layers = new LayersTable(this);
				this.DimensionStyles = new DimensionStylesTable(this);
				this.TextStyles = new TextStylesTable(this);
				this.LineTypes = new LineTypesTable(this);
				this.Views = new ViewsTable(this);
				this.UCSs = new UCSTable(this);
				this.VPorts = new VPortsTable(this);
				this.AppIds = new AppIdsTable(this);

				//Root dictionary
				this.RootDictionary = CadDictionary.CreateRoot();

				//Entries
				Layout modelLayout = Layout.Default;
				(this.RootDictionary[CadDictionary.AcadLayout] as CadDictionary).Add(Layout.LayoutModelName, modelLayout);

				//Default variables
				this.AppIds.Add(AppId.Default);

				//Blocks
				BlockRecord model = BlockRecord.ModelSpace;
				model.Layout = modelLayout;
				this.BlockRecords.Add(model);

				this.BlockRecords.Add(BlockRecord.PaperSpace);

				this.LineTypes.Add(LineType.ByLayer);
				this.LineTypes.Add(LineType.ByBlock);
				this.LineTypes.Add(LineType.Continuous);

				this.Layers.Add(Layer.Default);

				this.TextStyles.Add(TextStyle.Default);

				this.DimensionStyles.Add(DimensionStyle.Default);

				this.VPorts.Add(VPort.Default);
			}
		}

		/// <summary>
		/// Creates a document with the default objects
		/// </summary>
		public CadDocument() : this(true) { }

		/// <summary>
		/// Gets an object in the document by it's handle
		/// </summary>
		/// <param name="handle"></param>
		/// <returns>the cadObject or null if doesn't exists in the document</returns>
		public CadObject GetCadObject(ulong handle)
		{
			return this.GetCadObject<CadObject>(handle);
		}

		/// <summary>
		/// Gets an object in the document by it's handle
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="handle"></param>
		/// <returns>the cadObject or null if doesn't exists in the document</returns>
		public T GetCadObject<T>(ulong handle)
			where T : CadObject
		{
			if (this._cadObjects.TryGetValue(handle, out IHandledCadObject obj))
			{
				return obj as T;
			}

			return null;
		}

		private void addCadObject(CadObject cadObject)
		{
			if (cadObject.Document != null)
			{
				throw new ArgumentException($"The item with handle {cadObject.Handle} is already assigned to a document");
			}

			cadObject.Document = this;

			if (cadObject.Handle == 0 || this._cadObjects.ContainsKey(cadObject.Handle))
			{
				var nextHandle = this._cadObjects.Keys.Max() + 1;

				this.Header.HandleSeed = nextHandle + 1;

				cadObject.Handle = nextHandle;
			}

			this._cadObjects.Add(cadObject.Handle, cadObject);
			cadObject.OnReferenceChange += this.onReferenceChanged;

			//TODO: Add the dictionary
			if (cadObject.XDictionary != null)
				this.RegisterCollection(cadObject.XDictionary);

			switch (cadObject)
			{
				case BlockRecord record:
					this.RegisterCollection(record.Entities);
					this.addCadObject(record.BlockEnd);
					this.addCadObject(record.BlockEntity);
					break;
			}
		}

		private void onReferenceChanged(object sender, ReferenceChangedEventArgs e)
		{
			//TODO: Should remove the old one??

			this.addCadObject(e.Item);
		}

		private void onAdd(object sender, ReferenceChangedEventArgs e)
		{
			if (e.Item is CadDictionary dictionary)
			{
				this.RegisterCollection(dictionary);
			}
			else
			{
				this.addCadObject(e.Item);
			}
		}

		internal void RegisterCollection<T>(IObservableCollection<T> collection, bool addElements = true)
			where T : CadObject
		{
			switch (collection)
			{
				case AppIdsTable:
					this.AppIds = (AppIdsTable)collection;
					this.AppIds.Owner = this;
					break;
				case BlockRecordsTable:
					this.BlockRecords = (BlockRecordsTable)collection;
					this.BlockRecords.Owner = this;
					break;
				case DimensionStylesTable:
					this.DimensionStyles = (DimensionStylesTable)collection;
					this.DimensionStyles.Owner = this;
					break;
				case LayersTable:
					this.Layers = (LayersTable)collection;
					this.Layers.Owner = this;
					break;
				case LineTypesTable:
					this.LineTypes = (LineTypesTable)collection;
					this.LineTypes.Owner = this;
					break;
				case TextStylesTable:
					this.TextStyles = (TextStylesTable)collection;
					this.TextStyles.Owner = this;
					break;
				case UCSTable:
					this.UCSs = (UCSTable)collection;
					this.UCSs.Owner = this;
					break;
				case ViewsTable:
					this.Views = (ViewsTable)collection;
					this.Views.Owner = this;
					break;
				case VPortsTable:
					this.VPorts = (VPortsTable)collection;
					this.VPorts.Owner = this;
					break;
			}

			collection.OnAdd += this.onAdd;

			if (collection is CadObject cadObject)
			{
				this.addCadObject(cadObject);
			}

			if (addElements)
			{
				foreach (T item in collection)
				{
					if (item is CadDictionary dictionary)
					{
						this.RegisterCollection(dictionary);
					}
					else
					{
						this.addCadObject(item);
					}
				}
			}
		}
	}
}
