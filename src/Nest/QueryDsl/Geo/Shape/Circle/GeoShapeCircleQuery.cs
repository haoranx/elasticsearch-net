﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeQuery : IFieldNameQuery
	{
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeCircleQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		ICircleGeoShape Shape { get; set; }
	}

	public class GeoShapeCircleQuery : FieldNameQuery, IGeoShapeCircleQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public ICircleGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeCircleQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeCircleQueryDescriptor<T> 
		: FieldNameQueryDescriptor<GeoShapeCircleQueryDescriptor<T>, IGeoShapeCircleQuery, T>
		, IGeoShapeCircleQuery where T : class
	{
		private IGeoShapeCircleQuery Self => this;
		bool IQuery.Conditionless => GeoShapeCircleQuery.IsConditionless(this);
		ICircleGeoShape IGeoShapeCircleQuery.Shape { get; set; }

		public GeoShapeCircleQueryDescriptor<T> Coordinates(IEnumerable<double> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new CircleGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> Radius(string radius)
		{
			if (Self.Shape == null)
				Self.Shape = new CircleGeoShape();
			Self.Shape.Radius = radius;
			return this;
		}
	}
}