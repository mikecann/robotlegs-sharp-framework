//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using robotlegs.bender.extensions.mediatorMap.api;
using robotlegs.bender.extensions.mediatorMap.impl;
using robotlegs.bender.extensions.viewManager.api;
using robotlegs.bender.framework.api;

namespace robotlegs.bender.extensions.mediatorMap
{
	public class MediatorMapExtension : IExtension
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/
		
		private IInjector _injector;
		
		private MediatorMap _mediatorMap;
		
		private IViewManager _viewManager;
		
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public void Extend(IContext context)
		{
			context.BeforeInitializing(BeforeInitializing)
				.BeforeDestroying(BeforeDestroying)
				.WhenDestroying(WhenDestroying);
			_injector = context.injector;
			_injector.Map (typeof(IMediatorMap)).ToSingleton (typeof(MediatorMap));
		}
		
		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/

		private void BeforeInitializing()
		{
			_mediatorMap = _injector.GetInstance(typeof(IMediatorMap)) as MediatorMap;
			_viewManager = _injector.GetInstance(typeof(IViewManager)) as IViewManager;
			if (_viewManager != null)
			{
				_viewManager.AddViewHandler (_mediatorMap);
			}
		}
		
		private void BeforeDestroying()
		{
			_mediatorMap.UnmediateAll ();
			if (_injector.SatisfiesDirectly (typeof(IViewManager))) 
			{
				_viewManager = _injector.GetInstance (typeof(IViewManager)) as IViewManager;
				_viewManager.RemoveViewHandler (_mediatorMap);
			}
		}
		
		private void WhenDestroying()
		{
			if (_injector.SatisfiesDirectly (typeof(IMediatorMap)))
			{
				_injector.Unmap (typeof(IMediatorMap));
			}
		}
	}
}

