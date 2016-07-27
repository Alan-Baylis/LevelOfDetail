using UnityEngine;
using System.Collections;

public class DownLoadItem {
	public string url;
	public AssetBundle bundle;
	public bool hasError;
	public WWW www;
	public int size;
	private float startLoadTime;

	public delegate void LoadCompleteDelegate(DownLoadItem item);
	public event LoadCompleteDelegate LoadEvent;

	public DownLoadItem(string url, int size){
		this.url = url;
		this.size = size;
		this.hasError = false;
	}

	public bool CheckIsDone(){
		if(this.www != null){
			if (this.www.isDone) {
				if (!string.IsNullOrEmpty (this.www.error)) {
					this.hasError = true;
				}
				this.LoadCallBack ();
				return true;
			}

			if (this.www.progress < 1f && this.www.progress > 0f && Time.time - startLoadTime < 10f) {
				return false;
			}
		}
		return true;
	}

	public void StartDownLoad(){
		this.www = new WWW (url);
		this.startLoadTime = Time.time;
	}

	public void StopDownLoad(){
		if (this.www != null){
			this.www.Dispose ();
			this.www = null;
		}
	}

	public void LoadCallBack(){
		if (LoadEvent != null){
			LoadEvent (this);
		}
	}
}

