//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using robotlegs.bender.extensions.mediatorMap.api;


namespace robotlegs.bender.extensions.mediatorMap.impl.support
{
	public class MediatorWeakMapTracker
	{

		protected Dictionary<IMediator, bool> _mediators = new Dictionary<IMediator, bool>();

		public Dictionary<IMediator, bool> TrackedMediators
		{
			get
			{
				return _mediators;
			}
		}

		public void TrackMediator(IMediator mediator)
		{
			_mediators.Add (mediator, true);
		}
	}
}