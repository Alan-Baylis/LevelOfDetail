using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DownLoadManager : Singlton<DownLoadManager>{
	//下载中的文件列表
	List<DownLoadItem> loading = new List<DownLoadItem> ();
	//已经下载完成的列表
	List<DownLoadItem> temp = new List<DownLoadItem>();
	//等待下载的文件列表
	List<DownLoadItem> waittingList = new List<DownLoadItem>();

	public int loadCount = 0;
	public int loadSize = 0;

	public void Init(bool autoUpdate){
		if (autoUpdate) {
			CoroutineManager.StartCoroutine (MainLoop ());
		}
	}

	private IEnumerator MainLoop(){
		while (true) {
			this.Update ();
			yield return 0;
		}
	}

	private bool isAssetBundle(string key)
	{
		var url = key.ToLower ();
		return url.EndsWith (".ab") || url.EndsWith (".asset") || url.EndsWith (".assetbundle");
	}


	private void Update(){
		temp.Clear ();
		DownLoadItem item = null;

		for (int i = 0; i < loading.Count; i++) {
			item = loading[i];
			if (item.CheckIsDone()) {
				temp.Add (item);
			}
		}

		for (int i = 0; i < temp.Count; i++) {
			item = temp [i];

			if (isAssetBundle (item.url)) {
				item.bundle = item.www.assetBundle;
			}

			if (item.hasError) {
				Debug.LogError (string.Format ("Download [ {0} ] Error", item.url));
			} else {
				item.LoadCallBack ();
				Debug.Log (string.Format ("Download [ {0} ] successful", item.url));
			}

			loadCount++;
			loading.Remove (item);
		}

		for (int i = 0; i < waittingList.Count; i++) {
			item = waittingList [i];
			waittingList.Remove (item);
			loading.Add (item);
			item.StartDownLoad ();
		}
	}

	public void AddDownLoadItem (string url, int size){
		var item = new DownLoadItem (url, size);
		this.waittingList.Add (item);
	}

	public DownLoadItem GetDownLoadItem(string url, int size){
		var item = new DownLoadItem (url, size);
		this.waittingList.Add (item);
		return item;
	}
}
