//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;

namespace robotlegs.bender.extensions.eventDispatcher.api
{
	public interface IEventDispatcher : IDispatcher
	{
		void AddEventListener<T> (Enum type, Action<T> listener);
		void AddEventListener (Enum type, Action<IEvent> listener);
		void AddEventListener (Enum type, Action listener);
		void AddEventListener(Enum type, Delegate listener);
		void RemoveEventListener<T>(Enum type, Action<T> listener);
		void RemoveEventListener(Enum type, Action<IEvent> listener);
		void RemoveEventListener(Enum type, Action listener);
		void RemoveEventListener(Enum type, Delegate listener);
		void RemoveAllEventListeners();
		bool HasEventListener (Enum type);
	}
}

