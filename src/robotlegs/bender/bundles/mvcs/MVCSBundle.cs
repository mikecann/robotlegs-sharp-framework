//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using robotlegs.bender.extensions.directCommandMap;
using robotlegs.bender.extensions.enhancedLogging;
using robotlegs.bender.extensions.eventCommandMap;
using robotlegs.bender.extensions.eventDispatcher;
using robotlegs.bender.extensions.localEventMap;
using robotlegs.bender.extensions.mediatorMap;
using robotlegs.bender.extensions.modularity;
using robotlegs.bender.extensions.viewManager;
using robotlegs.bender.extensions.viewProcessorMap;
using robotlegs.bender.extensions.vigilance;
using robotlegs.bender.framework.api;
using robotlegs.bender.framework.impl;

namespace robotlegs.bender.bundles.mvcs
{
	public class MVCSBundle : IExtension
	{
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public void Extend(IContext context)
		{
			/**
			 * Robotlegs
			 */
			context.LogLevel = LogLevel.INFO;

			context.Install(typeof(ConsoleLoggingExtension));
			context.Install(typeof(VigilanceExtension));
			context.Install(typeof(InjectableLoggerExtension));
			context.Install(typeof(EventDispatcherExtension));
			context.Install(typeof(ModularityExtension));
			context.Install(typeof(DirectCommandMapExtension));
			context.Install(typeof(EventCommandMapExtension));
			context.Install(typeof(LocalEventMapExtension));
			context.Install(typeof(ViewManagerExtension));
			context.Install(typeof(MediatorMapExtension));
			context.Install(typeof(ViewProcessorMapExtension));

			context.Configure(typeof(FallbackContainerConfig));
		}
	}
}