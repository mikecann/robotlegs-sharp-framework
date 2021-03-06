//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;
using NUnit.Framework;
using robotlegs.bender.framework.api;
using robotlegs.bender.framework.impl;
using swiftsuspenders.errors;

namespace robotlegs.bender.extensions.vigilance
{
	[TestFixture]
	public class VigilanceExtensionTest
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private ILogger logger;

		private IInjector injector;

		/*============================================================================*/
		/* Test Setup and Teardown                                                    */
		/*============================================================================*/

		[SetUp]
		public void before()
		{
			IContext context = new Context ().Install<VigilanceExtension>();
			logger = context.GetLogger(this);
			injector = context.injector;
		}

		/*============================================================================*/
		/* Tests                                                                      */
		/*============================================================================*/

		[Test]
		public void extension_does_NOT_throw_for_DEBUG()
		{
			logger.Debug("");
		}

		[Test]
		public void extension_does_NOT_throw_for_INFO()
		{
			logger.Info("");
		}

		[Test, ExpectedException(typeof(VigilantException))]
		public void extension_throws_for_WARNING()
		{
			logger.Warn("");
		}

		[Test, ExpectedException(typeof(VigilantException))]
		public void extension_throws_for_ERROR()
		{
			logger.Error("");
		}

		[Test, ExpectedException(typeof(VigilantException))]
		public void extension_throws_for_FATAL()
		{
			logger.Fatal("");
		}

		[Test, ExpectedException(typeof(InjectorException))]
		public void extension_throws_for_injector_MAPPING_override()
		{
			injector.Map<String>().ToValue("string");
			injector.Map<String>().ToValue("string");
		}
	}
}

