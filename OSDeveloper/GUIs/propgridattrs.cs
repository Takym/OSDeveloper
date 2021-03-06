﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace OSDeveloper.GUIs
{
	internal static class propgridattrs
	{
		internal static ResourceManager GetResourceManager(string resmgr)
		{
			var t = Type.GetType($"{nameof(OSDeveloper)}.{nameof(Resources)}.{resmgr}", false, false);
			if (t != null) {
				var p = t.GetProperty(nameof(ResourceManager), BindingFlags.Static);
				if (p != null) {
					return ((ResourceManager)(p.GetValue(null)));
				}
			}
			return null;
		}

		internal static string GetResourceString(string resmgr, string id)
		{
			var t = Type.GetType($"{nameof(OSDeveloper)}.{nameof(Resources)}.{resmgr}", false, false);
			if (t != null) {
				var p = t.GetProperty(id, BindingFlags.Static | BindingFlags.NonPublic);
				if (p != null) {
					return p.GetValue(null)?.ToString();
				}
			}
			return null;
		}
	}

	public class OsdevCategory : CategoryAttribute
	{
		private readonly string _text;

		public OsdevCategory(string resmgr, string id) : base(id)
		{
			_text = propgridattrs.GetResourceString(resmgr, id);
		}

		protected override string GetLocalizedString(string value)
		{
			return _text ?? base.GetLocalizedString(value);
		}
	}

	public class OsdevDisplayName : DisplayNameAttribute
	{
		private readonly string _text;

		public OsdevDisplayName(string resmgr, string id) : base(id)
		{
			_text = propgridattrs.GetResourceString(resmgr, id);
		}

		public override string DisplayName
		{
			get
			{
				return _text ?? base.DisplayName;
			}
		}
	}

	public class OsdevDescription : DescriptionAttribute
	{
		private readonly string _text;

		public OsdevDescription(string resmgr, string id) : base(id)
		{
			_text = propgridattrs.GetResourceString(resmgr, id);
		}

		public override string Description
		{
			get
			{
				return _text ?? base.Description;
			}
		}
	}
}
