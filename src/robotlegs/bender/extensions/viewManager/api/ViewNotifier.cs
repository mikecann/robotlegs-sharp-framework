//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using robotlegs.bender.extensions.viewManager.impl;

namespace robotlegs.bender.extensions.viewManager.api
{
	public static class ViewNotifier
	{
		/*============================================================================*/
		/* Public Static Properties                                                   */
		/*============================================================================*/

		public static ContainerRegistry Registry
		{
			get
			{
				return _registry;
			}
		}

		/*============================================================================*/
		/* Private Static Properties                                                  */
		/*============================================================================*/

		private static ContainerRegistry _registry;

		/*============================================================================*/
		/* Public Static Functions                                                    */
		/*============================================================================*/

		public static void SetRegistry(ContainerRegistry containerRegistry)
		{
			_registry = containerRegistry;
		}

		public static void RegisterView(object view)
		{
			if (_registry == null)
				return;

			ContainerBinding binding = _registry.FindParentBinding(view);
			while (binding != null)
			{
				binding.HandleView(view, view.GetType());
				binding = binding.Parent;
			}
		}
	}
}

