﻿// using System;
// using System.Collections.Specialized;
// using System.Configuration;
// using System.Dynamic;
// using System.Linq;
//
// namespace NotLimited.Framework.Common.Misc
// {
// 	public class DynamicConfig : DynamicObject
// 	{
// 		private readonly StringDictionary settings = new StringDictionary();
//
// 		public DynamicConfig()
// 		{
// 			var asses = AppDomain.CurrentDomain.GetAssemblies();
//
// 			for (int i = 0; i < asses.Length; i++)
// 			{
// 				try
// 				{
// 					LoadSettings(asses[i].Location);
// 				}
// 				catch (NotSupportedException)
// 				{
// 				}
// 			}
// 		}
//
// 		public override bool TryGetMember(GetMemberBinder binder, out object result)
// 		{
// 			result = !settings.ContainsKey(binder.Name) ? null : settings[binder.Name];
// 			return true;
// 		}
//
// 		public override bool TrySetMember(SetMemberBinder binder, object value)
// 		{
// 			settings[binder.Name] = value.ToString();
// 			return true;
// 		}
//
// 		private void LoadSettings(string path)
// 		{
// 			try
// 			{
// 				var config = ConfigurationManager.OpenExeConfiguration(path);
//
// 				foreach (var setting in config.AppSettings.Settings.Cast<KeyValueConfigurationElement>())
// 					settings.Add(setting.Key, setting.Value);
// 			}
// 			catch
// 			{
// 			}
// 		}
// 	}
// }