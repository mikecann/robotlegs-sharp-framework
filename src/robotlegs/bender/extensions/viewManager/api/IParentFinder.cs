//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using robotlegs.bender.extensions.viewManager.impl;

namespace robotlegs.bender.extensions.viewManager
{
	public interface IParentFinder
	{
		/// <summary>
		/// Method to check if a container contains another container
		/// </summary>
		/// <param name="container">The parent container</param>
		/// <param name="obj">The child container</param>
		bool Contains (object parentContainer, object childContainer);

		// View Find Parent Container
		/// <summary>
		/// Method to check the view to find it's container (if any)
		/// </summary>
		/// <returns>The parent container object or null</returns>
		/// <param name="child">The child view</param>
		/// <param name="containers">A dictionary of ContainerBindings by Container as a key</param>
		object FindParent (object childView, Dictionary<object, ContainerBinding> containers);
		object FindParent (object childView, IEnumerable<ContainerBinding> containers);
	}
}