//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System;
using robotlegs.bender.framework.api;

namespace robotlegs.bender.extensions.viewProcessorMap.support
{
	public interface ITrackingProcessor
	{
		void Process(object view, Type type, IInjector injector);

		void Unprocess(object view, Type type, IInjector injector);
	}

}

