//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;
using robotlegs.bender.framework.api;
using robotlegs.bender.extensions.enhancedLogging.impl;

namespace robotlegs.bender.extensions.enhancedLogging
{
	public class InjectableLoggerExtension : IExtension
	{
		public void Extend (IContext context)
		{
			context.injector.Map(typeof(ILogger)).ToProvider(new LoggerProvider(context));
		}
	}
}

