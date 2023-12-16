using UnityEngine;
using System.Collections;
/*Cloud Collector on top b/c when game start, cloud collector will contact a cloud and cloud will dissapear 
 and Cloud Spawer on bottom because when we camera move, Cloud Spawer will contact with a last cloud
 and it'll create a new cloud*/
public class CloudCollector : MonoBehaviour {
	/*When Cloud Collector check IS TRIGGER when anything collider cloud collector, and fucntion OnTriggerEnter2D will call
	 immetialy, and b/c CLOUD COLLECTOR IS CHECKED IS TRIGGER, so we must use function OnTriggerEnter2D*/
	void OnTriggerEnter2D(Collider2D targer){
		/*If target tag == Cloud or Deadly that crated in Inspector Panel*/
		if (targer.tag == "Cloud" || targer.tag == "Deadly") {
			/*When Cloud Collector and target collider Cloud or Deadly, Cloud and Deadly will be dissapear => meaning that
			 not active */
			targer.gameObject.SetActive(false);
		
		}
	}
}
