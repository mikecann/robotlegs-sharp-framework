//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;
using robotlegs.bender.extensions.viewManager;
using System.Collections.Generic;
using robotlegs.bender.extensions.viewManager.impl;

namespace robotlegs.bender.extensions.viewManager.support
{
	public class SupportParentFinder : IParentFinder
	{
		public bool Contains (object parentContainer, object childContainer)
		{
			SupportContainer parentSupport = parentContainer as SupportContainer;
			SupportContainer childSupport = childContainer as SupportContainer;

			if (parentSupport == null || childContainer == null)
				return false;

			while (childSupport != null)
			{
				if (childSupport.Parent == parentSupport)
					return true;

				childSupport = childSupport.Parent;
			}
			return false;
		}

		public object FindParent (object childView, Dictionary<object, ContainerBinding> containers)
		{
			return FindParent(childView, new List<ContainerBinding>(containers.Values));
		}

		public object FindParent (object childView, IEnumerable<ContainerBinding> containerBindings)
		{
			SupportContainer supportView = childView as SupportContainer;
			while (supportView != null && supportView.Parent != null)
			{
				foreach (ContainerBinding containerBinding in containerBindings)
				{
					if (containerBinding.Container == supportView.Parent)
					{
						return containerBinding.Container;
					}
				}
				supportView = supportView.Parent;
			}
			return null;
		}
	}
}

