//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using robotlegs.bender.extensions.commandCenter.api;
using robotlegs.bender.extensions.directCommandMap.api;
using robotlegs.bender.extensions.commandCenter.support;
using robotlegs.bender.framework.impl.guardSupport;

namespace robotlegs.bender.extensions.directCommandMap.impl
{
	[TestFixture]
	public class DirectCommandMapperTest
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private Mock<ICommandMappingList> mockMappingList;

		private Mock<ICommandExecutor> mockExecutor;

		private DirectCommandMapper subject;

		private ICommandMapping caughtMapping = null;

		/*============================================================================*/
		/* Test Setup and Teardown                                                    */
		/*============================================================================*/

		[SetUp]
		public void before()
		{
			mockExecutor = new Mock<ICommandExecutor> ();
			mockMappingList = new Mock<ICommandMappingList> ();
			mockMappingList.Setup (m => m.AddMapping (It.IsAny<ICommandMapping> ())).Callback<ICommandMapping>(r => caughtMapping = r);
		}

		[TearDown]
		public void after()
		{
			caughtMapping = null;
			subject = null;
		}

		/*============================================================================*/
		/* Tests                                                                      */
		/*============================================================================*/

		[Test]
		public void registers_new_commandMapping_with_CommandMappingList()
		{
			CreateMapper<NullCommand>();

			Assert.That(caughtMapping, Is.InstanceOf<ICommandMapping>());
		}

		[Test]
		public void mapping_is_fireOnce_by_default()
		{
			CreateMapper<NullCommand>();

			Assert.That(caughtMapping.FireOnce, Is.True);
		}

		[Test]
		public void withExecuteMethod_sets_executeMethod_of_mapping()
		{
			string executeMethod = "otherThanExecute";
			CreateMapper<NullCommand>().WithExecuteMethod(executeMethod);

			Assert.That(caughtMapping.ExecuteMethod, Is.EqualTo(executeMethod));
		}

		[Test]
		public void withGuards_sets_guards_of_mapping()
		{
			object[] expected = new object[]{typeof(HappyGuard), typeof(GrumpyGuard)};
			CreateMapper<NullCommand>().WithGuards(expected);

			Assert.That(caughtMapping.Guards, Is.EqualTo(expected).AsCollection);
		}

		[Test]
		public void withHooks_sets_hooks_of_mapping()
		{
			object[] expected = new object[]{typeof(ClassReportingCallbackHook), typeof(ClassReportingCallbackHook)};
			CreateMapper<NullCommand>().WithHooks(expected);

			Assert.That(caughtMapping.Hooks, Is.EqualTo(expected).AsCollection);
		}

		[Test]
		public void withPayloadInjection_sets_payloadInjection_of_mapping()
		{
			CreateMapper<NullCommand>().WithPayloadInjection(false);

			Assert.That(caughtMapping.PayloadInjectionEnabled, Is.False);
		}

		[Test]
		public void calls_executor_executeCommands_with_arguments()
		{
			List<ICommandMapping> list = new List<ICommandMapping>();
			mockMappingList.Setup (m => m.GetList ()).Returns (list);
			CreateMapper<NullCommand>().Execute(null);

			mockExecutor.Verify (e => e.ExecuteCommands (It.Is<List<ICommandMapping>> (arg1 => arg1 == list), It.Is<CommandPayload> (arg2 => arg2 == null)), Times.Once);
		}

		[Test]
		public void map_creates_new_mapper_instance()
		{
			IDirectCommandConfigurator newMapper = CreateMapper<NullCommand>().Map<NullCommand2>();

			Assert.That(newMapper, Is.Not.Null);
			Assert.That(newMapper, Is.Not.EqualTo(subject));
		}

		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/

		private DirectCommandMapper CreateMapper<T>()
		{
			subject = new DirectCommandMapper (mockExecutor.Object, mockMappingList.Object, typeof(T));
			return subject;
		}
	}
}

