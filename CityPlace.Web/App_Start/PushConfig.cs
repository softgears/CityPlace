﻿// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: PushConfig.cs
// 
// Created by: ykors_000 at 09.06.2014 16:03
// 
// Property of SoftGears
// 
// ========

using System.IO;
using System.Web;
using CityPlace.Domain.IoC;
using PushSharp;
using PushSharp.Apple;

namespace CityPlace.Web
{
	public static class PushConfig
	{
		/// <summary>
		/// Инициализирует сервисы уведомлений
		/// </summary>
		public static void InitPushServices()
		{
			var push = Locator.GetService<PushBroker>();

			// Берем сертификат
			var appleCertPath = HttpContext.Current.Server.MapPath("/Push/citypush.p12");
			if (File.Exists(appleCertPath))
			{
				var appleCert = File.ReadAllBytes(appleCertPath);
				push.RegisterAppleService(new ApplePushChannelSettings(true,appleCert, System.Configuration.ConfigurationManager.AppSettings["ApplePushPassword"],true));
			}

			push.OnChannelException += (sender, channel, error) => 
			{
				var s =sender.ToString();
			};

			push.OnNotificationFailed += (sender, notification, error) =>
			{
				var s = error.ToString();
			};

			push.OnNotificationSent += (sender, notification) =>
			{
				var s = notification.ToString();
			};
			push.OnNotificationRequeue += (sender, notification) =>
			{
				var s = notification.ToString();
			};

		}
	}
}