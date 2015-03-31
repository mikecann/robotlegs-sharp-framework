//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;

namespace robotlegs.bender.framework.impl.guardSupport
{
	public class JustTheMiddleManGuard
	{
		[Inject]
		public BossGuard bossDecision;

		public bool Approve()
		{
			return bossDecision.Approve();
		}
	}
}

