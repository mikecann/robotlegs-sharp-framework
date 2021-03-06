//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using robotlegs.bender.extensions.modularity.dsl;

namespace robotlegs.bender.extensions.modularity.api
{
	/// <summary>
	/// Create event relays between modules
	/// </summary>
	public interface IModuleConnector
	{
		/// <summary>
		/// Connects to a specified channel.
		/// </summary>
		/// <param name="channelId">The channel Id.</param>
		IModuleConnectionAction OnChannel(object channelId);

		/// <summary>
		/// Connects to the default channel
		/// </summary>
		IModuleConnectionAction OnDefaultChannel();
	}
}