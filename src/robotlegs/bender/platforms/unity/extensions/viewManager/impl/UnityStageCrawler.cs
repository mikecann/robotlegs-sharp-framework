//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using robotlegs.bender.extensions.mediatorMap.api;
using robotlegs.bender.extensions.viewManager.impl;
using UnityEngine;

namespace robotlegs.bender.platforms.unity.extensions.viewManager.impl
{
	public class UnityStageCrawler : StageCrawler
	{
		protected override void ScanContainer (object container)
		{
			Transform containerTransform = null;
			if (container is GameObject)
			{
				containerTransform = (container as GameObject).transform;
			}
			else if (container is Transform)
			{
				containerTransform = container as Transform;
			}
			else
				return;

			ProcessViewsFromRoot (containerTransform);
		}

		private void ProcessViewsFromRoot(Transform view)
		{
			MonoBehaviour[] viewScripts = view.GetComponentsInChildren<MonoBehaviour>(false);

			foreach (MonoBehaviour viewScript in viewScripts)
			{
				if (viewScript is IView)
				{
					ProcessView (viewScript);
				}
			}
		}
	}
}

