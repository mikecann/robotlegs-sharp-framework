//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System;
using robotlegs.bender.extensions.eventDispatcher.api;
using robotlegs.bender.extensions.eventDispatcher.impl;
using robotlegs.bender.extensions.modularity.dsl;

namespace robotlegs.bender.extensions.modularity.impl
{
	public class ModuleConnectionConfigurator : IModuleConnectionAction
	{

		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private EventRelay _channelToLocalRelay;

		private EventRelay _localToChannelRelay;

		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/

		public ModuleConnectionConfigurator(
			IEventDispatcher localDispatcher,
			IEventDispatcher channelDispatcher)
		{
			_localToChannelRelay = new EventRelay(localDispatcher, channelDispatcher).Start();
			_channelToLocalRelay = new EventRelay(channelDispatcher, localDispatcher).Start();
		}

		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public IModuleConnectionAction RelayEvent(Enum eventType)
		{
			_localToChannelRelay.AddType(eventType);
			return this;
		}
			
		public IModuleConnectionAction ReceiveEvent(Enum eventType)
		{
			_channelToLocalRelay.AddType(eventType);
			return this;
		}

		public void Suspend()
		{
			_channelToLocalRelay.Stop();
			_localToChannelRelay.Stop();
		}
			
		public void Resume()
		{
			_channelToLocalRelay.Start();
			_localToChannelRelay.Start();
		}

		public void Destroy()
		{
			_localToChannelRelay.Stop();
			_localToChannelRelay = null;
			_channelToLocalRelay.Stop();
			_channelToLocalRelay = null;
		}
	}
}
