using UnityEngine;
using System.Collections;
using System.IO;

public class GameApp : MonoBehaviour {
	DownLoadItem item;
	// Use this for initialization
	void Start () {
		string url = "http://localhost:8011/android/download/1.0.1/download.zip";
		CoroutineManager.Init (this.gameObject);	
		DownLoadManager.Instance().Init(true);
		item = DownLoadManager.Instance ().GetDownLoadItem (url, 10);
		item.LoadEvent += (DownLoadItem data) => {
			var bytes = data.www.bytes;
			var path = Application.persistentDataPath + "/download.zip";

			try {
				//Save
			} catch (System.Exception ex) {
				
			}
		};
	}

	// Update is called once per frame
	void Update () {
		/*if (item != null && item.www != null) {
			Debug.Log (item.www.progress * 100 + "%");
		}*/
	}
}
