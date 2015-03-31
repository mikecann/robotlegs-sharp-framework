//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;
using robotlegs.bender.extensions.eventDispatcher.impl;

namespace robotlegs.bender.extensions.eventDispatcher.support
{
	public class BlankEvent : Event
	{
		public BlankEvent(Enum type):base(type)
		{

		}
	}
}

