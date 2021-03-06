//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using NUnit.Framework;
using robotlegs.bender.extensions.matching;
using robotlegs.bender.extensions.viewProcessorMap.support;
using robotlegs.bender.framework.api;
using robotlegs.bender.extensions.viewProcessorMap.api;
using robotlegs.bender.extensions.mediatorMap.api;
using System;
using robotlegs.bender.extensions.viewManager.api;
using robotlegs.bender.extensions.viewManager.support;

namespace robotlegs.bender.extensions.viewProcessorMap.impl
{
	public class ViewProcessorFactoryTest
	{

		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private ViewProcessorFactory viewProcessorFactory;

		private TrackingProcessor trackingProcessor;

		private IInjector injector;

		private object view;

		/*============================================================================*/
		/* Test Setup and Teardown                                                    */
		/*============================================================================*/

		[SetUp]
		public void Setup()
		{
			injector = new robotlegs.bender.framework.impl.RobotlegsInjector();
			viewProcessorFactory = new ViewProcessorFactory(injector);
			trackingProcessor = new TrackingProcessor();
			view = new object();
		}

		/*============================================================================*/
		/* Tests                                                                      */
		/*============================================================================*/

		[Test]
		public void RunProcessOnExistingProcessor()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping>();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), trackingProcessor));

			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());
			Assert.That(trackingProcessor.ProcessedViews, Is.EquivalentTo(new object[1]{ view }));
		}

		[Test]
		public void RunProcessOnMultipleProcessors()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping>();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), trackingProcessor));

			TrackingProcessor trackingProcessor2 = new TrackingProcessor();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), trackingProcessor2));

			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());
			Assert.That (trackingProcessor.ProcessedViews, Is.EquivalentTo (new object[1]{view}));
			Assert.That (trackingProcessor2.ProcessedViews, Is.EquivalentTo (new object[1]{view}));
		}

		[Test]
		public void RunUnprocessOnExistingProcessor()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping> ();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), trackingProcessor));

			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());
			viewProcessorFactory.RunUnprocessors(view, view.GetType(), mappings.ToArray());
			Assert.That (trackingProcessor.UnprocessedViews, Is.EquivalentTo(new object[1]{ view }));
		}

		[Test]
		public void RunUnprocessOnMultipleProcessors()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping> ();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), trackingProcessor));

			TrackingProcessor  trackingProcessor2 = new TrackingProcessor();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), trackingProcessor2));

			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());
			viewProcessorFactory.RunUnprocessors(view, view.GetType(), mappings.ToArray());
			Assert.That (trackingProcessor.ProcessedViews, Is.EquivalentTo (new object[1]{view}));
			Assert.That (trackingProcessor2.ProcessedViews, Is.EquivalentTo (new object[1]{view}));
		}

		[Test]
		public void GetsProcessorFromInjectorWhereMapped()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping>();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), typeof(TrackingProcessor)));

			injector.Map(typeof(TrackingProcessor)).ToValue(trackingProcessor);

			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());

			Assert.That (trackingProcessor.ProcessedViews, Is.EquivalentTo (new object[1]{view}));
		}

		[Test]
		public void CreatesProcessorSingletonMappingWhereNotMappedAndRunsProcess()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping>();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), typeof(TrackingProcessor)));

			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());
			TrackingProcessor createdTrackingProcessor = injector.GetInstance(typeof(TrackingProcessor)) as TrackingProcessor;

			Assert.That (createdTrackingProcessor.ProcessedViews, Is.EquivalentTo (new object[1]{ view }));
		}

		[Test]
		public void CreatesProcessorSingletonMappingWhereNotMapped()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping>();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), typeof(TrackingProcessor)));

			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());

			Assert.That (injector.HasDirectMapping (typeof(TrackingProcessor)), Is.True);
		}

		[Test, ExpectedException(typeof(ViewProcessorMapException))]
		public void RequestingAnUnmappedInterfaceThrowsInformativeError()
		{
			List<ViewProcessorMapping> mappings = new List<ViewProcessorMapping>();
			mappings.Add(new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), typeof(ITrackingProcessor)));
	
			viewProcessorFactory.RunProcessors(view, view.GetType(), mappings.ToArray());
		}

		[Test]
		public void RunAllUnprocessors_Runs_All_Unprocessors_For_All_Views()
		{
			TrackingProcessor trackingProcessor2 = new TrackingProcessor();

			SupportView view = new SupportView();
			ObjectWhichExtendsSupportView viewA = new ObjectWhichExtendsSupportView();

			ViewProcessorMapping mapping = new ViewProcessorMapping(new TypeMatcher().AllOf(view.GetType()).CreateTypeFilter(), trackingProcessor);
			ViewProcessorMapping mappingA = new ViewProcessorMapping(new TypeMatcher().AllOf(viewA.GetType()).CreateTypeFilter(), trackingProcessor2);

			viewProcessorFactory.RunProcessors(view, view.GetType(), new ViewProcessorMapping[1] {mapping});
			viewProcessorFactory.RunProcessors(viewA, viewA.GetType(), new ViewProcessorMapping[2] {mapping, mappingA});

			viewProcessorFactory.RunAllUnprocessors();

			Assert.That (trackingProcessor.UnprocessedViews, Is.EquivalentTo (new object[2]{ view, viewA }), "trackingProcessor unprocessed all views");
			Assert.That (trackingProcessor2.UnprocessedViews, Is.EquivalentTo (new object[1]{ viewA }), "trackingProcessor2 unprocessed all views");
		}
	}
}

class ObjectWhichExtendsSupportView : SupportView
{

}

class ObjectB : object
{

}