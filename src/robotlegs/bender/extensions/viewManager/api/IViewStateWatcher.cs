//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System;

namespace robotlegs.bender.extensions.viewManager.api
{
	public interface IViewStateWatcher
	{
		bool isAdded { get; }

		event Action<object> added;
		event Action<object> removed;
		event Action<object> disabled;
		event Action<object> enabled;
	}
}

