﻿using UnityEngine;
using FairyGUI;

public class VirtualListMain : MonoBehaviour
{
	GComponent _mainView;
	GList _list;

	void Start()
	{
		Application.targetFrameRate = 60;
		GRoot.inst.SetContentScaleFactor(1136, 640);
		Stage.inst.onKeyDown.Add(OnKeyDown);

		UIPackage.AddPackage("UI/VirtualList");
		UIObjectFactory.SetPackageItemExtension(UIPackage.GetItemURL("VirtualList", "mailItem"), typeof(MailItem));

		_mainView = UIPackage.CreateObject("VirtualList", "Main").asCom;
		_mainView.fairyBatching = true;
		_mainView.SetSize(GRoot.inst.width, GRoot.inst.height);
		_mainView.AddRelation(GRoot.inst, RelationType.Size);
		GRoot.inst.AddChild(_mainView);

		_mainView.GetChild("n6").onClick.Add(() => { _list.AddSelection(500, true); });
		_mainView.GetChild("n7").onClick.Add(() => { _list.scrollPane.ScrollTop(); });
		_mainView.GetChild("n8").onClick.Add(() => { _list.scrollPane.ScrollBottom(); });

		_list = _mainView.GetChild("mailList").asList;
		_list.SetVirtual();

		_list.itemRenderer = RenderListItem;
		_list.numItems = 1000;
	}

	void RenderListItem(int index, GObject obj)
	{
		MailItem item = (MailItem)obj;
		item.setFetched(index % 3 == 0);
		item.setRead(index % 2 == 0);
		item.setTime("5 Nov 2015 16:24:33");
		item.title = index + " Mail title here";
	}

	void OnKeyDown(EventContext context)
	{
		if (context.inputEvent.keyCode == KeyCode.Escape)
		{
			Application.Quit();
		}
	}
}