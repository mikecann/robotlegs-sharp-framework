//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using robotlegs.bender.extensions.eventDispatcher.api;
using robotlegs.bender.extensions.viewManager;
using robotlegs.bender.extensions.viewManager.api;
using robotlegs.bender.framework.api;

namespace robotlegs.bender.extensions.modularity.impl
{
	public class ViewManagerBasedExistenceWatcher : ContextViewBasedExistenceWatcher
	{

		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private List<object> _acceptedContainers;

		private IViewManager _viewManager;

		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/

		public ViewManagerBasedExistenceWatcher (IContext context, object contextView, IEventDispatcher modularityDispatcher, IParentFinder parentFinder, IViewManager viewManager) : base (context, contextView, modularityDispatcher, parentFinder)
		{
			_viewManager = viewManager;
			_acceptedContainers = new List<object> ();
		}

		/*============================================================================*/
		/* Public Functions                                                          */
		/*============================================================================*/

		public override void Init()
		{
			base.Init ();

			_acceptedContainers = new List<object> (_viewManager.Containers);
			if (_acceptedContainers.Contains (_contextView))
			{
				_acceptedContainers.Remove (_contextView);
			}
			_viewManager.ContainerAdded += OnContainerAdded;
			_viewManager.ContainerRemoved += OnContainerRemoved;
		}

		/*============================================================================*/
		/* Protected Functions                                                          */
		/*============================================================================*/

		protected override void Destroy()
		{
			base.Destroy ();
			_acceptedContainers.Clear ();
			_viewManager.ContainerAdded -= OnContainerAdded;
			_viewManager.ContainerRemoved -= OnContainerRemoved;
		}

		protected void OnContainerAdded (object container)
		{
			_logger.Debug("Adding context existence event listener to container {0}", container);
			_acceptedContainers.Add (container);
		}

		protected void OnContainerRemoved (object container)
		{
			_logger.Debug("Removing context existence event listener from container {0}", container);
			_acceptedContainers.Remove(container);
		}

		protected override bool ValidateContextView(object contextView)
		{
			if (_acceptedContainers.Contains (contextView))
				return true;
			else
				return base.ValidateContextView (contextView);
		}
	}
}

