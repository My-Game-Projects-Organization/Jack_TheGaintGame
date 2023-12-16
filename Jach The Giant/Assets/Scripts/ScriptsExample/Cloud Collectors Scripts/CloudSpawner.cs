using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	[SerializeField]
	/*Create gameobject array because we have more clouds*/
	private GameObject[] clouds;

	/*distance between cloud because player can stand cloud*/
	private float distanceBetweenClouds = 3f;

	/*create minX and maxX because we don't want to cloud outside in left side
	 and outside in the right side, it only lie in frame of camera*/
	// giới hạn nó tỏng màn hình vs trục X
	private float minX,maxX;

	/*because when start the gane, cloud spawner will collider the last cloud
	 and it will create a new cloud and we will campare CLOUD SPAWNER to 
	 CLOUD TAG it mean that cloud spawner will collider Cloud and it will
	 renew a new cloud*/
	private float lastCloudPositionY;

	/*Set a random cloud in left side or right side*/
	private float controlX;

	[SerializeField]

	private GameObject[] collectables;

	/*Because we need to get player to stand in cloud when we start the game*/
	// cloud clone
	private GameObject player;

	// Use this for initialization
	void Awake () {
		controlX = 3;
		SetMinAndMaxX ();
		CreateClouds ();
		player = GameObject.Find ("Player");
	
		for (int i = 0; i<collectables.Length; i++) {
			collectables[i].SetActive(false);
		}
	}
	void Start(){
		PositionThePlayer ();
	}

	void SetMinAndMaxX(){
		// giới hạn max min posX cho màn camera
		/*Transforms position from screen space into world space.*/
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		maxX = bounds.x - 0.5f;
		minX = -bounds.x + 0.5f;
	}
	/*RANDOM*/
	void Shuffle(GameObject[] arrayToShuffle)
	{
		// tráo ví trí trong mảng obj
		for (int i =0; i<arrayToShuffle.Length; i++) {
			GameObject temp = arrayToShuffle[i];//arrayToShuffle[i] = 3; meaning temp =3;
			int random = Random.Range(i,arrayToShuffle.Length);
			arrayToShuffle[i] = arrayToShuffle[random];//it had a value of arrayToShuffle[i] = 3
			arrayToShuffle[random] = temp;

			 
		}
	}
	void CreateClouds()
	{
		// xáo mảng
		Shuffle (clouds);
		// sao cho pos đầu tiên luôn ở giữa màn hình theo chiều Y
		float positionY = 0f;
		// sử dụng for để căn pos cho cloud đầu tiên và 1 vài cloud tiếp theo dựa vào số lượng trong mảng
		for (int i = 0; i<clouds.Length; i++) {

			Vector3 temp = clouds[i].transform.position;

			temp.y = positionY;

			// lấy random trong khoảng dựa vào controlX
			if(controlX == 0){
				temp.x = Random.Range(0.0f,maxX);
				controlX = 1;
			}else if(controlX==1){
				temp.x = Random.Range(0.0f,minX);
				controlX = 2;
			}else if(controlX==2){
				temp.x = Random.Range(1.0f,maxX);
				controlX = 3;
			}else if(controlX==3){
				temp.x = Random.Range(-1.0f,minX);
				controlX = 0;
			}

			// lastPosCloudY hiện tại của cloud trước, những pos của cloud sau sẽ căn theo pos của cloud trước
			lastCloudPositionY = positionY;

			clouds[i].transform.position = temp;

			positionY -= distanceBetweenClouds;
		}
	}
	void PositionThePlayer(){
		// Khởi tạo mảng các obj darkcloud và cloud bthg
		GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
		GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag ("Cloud");

        // Loại bỏ các darkCloud ra khỏi phạm vi đám mây đầu tiên để ng dùng nhảy lên
		// check xem có pos darkCLoud nào ở vị trí trên cùng và ở giữa ko, nếu có sẽ để cho cloud thường đầu tiên trên cùng lấy pos đó
		// bỏ đi thì có thể sẽ xảy ra trường hợp darkCloud đầu tiên là cloud mak ng dùng nhảy lên
        for (int i = 0; i < darkClouds.Length; i++)
        {
            if (darkClouds[i].transform.position.y == 0f)
            {
                Vector3 t = darkClouds[i].transform.position;
                darkClouds[i].transform.position = new Vector3(cloudsInGame[0].transform.position.x,
                                                               cloudsInGame[0].transform.position.y,
                                                               cloudsInGame[0].transform.position.z);
                cloudsInGame[0].transform.position = t;
            }
        }
        Vector3 temp = cloudsInGame[0].transform.position;

		// Tìm vị trí của nhân vật, do khi start đám mây sẽ đc clone ở mỗi vị trí khác nhau nên sẽ dùng 
		// logic dưới đây để tìm cloud có posY cao nhất, nếu cmt sẽ player sẽ xuất hiện ở những đám mây ở dưới
        for (int i = 1; i < cloudsInGame.Length; i++)
        {
            if (temp.y < cloudsInGame[i].transform.position.y)
            {
                temp = cloudsInGame[i].transform.position;
            }
        }

        // khởi tạo nhân vật ở vị trí trên vị trí đám mây sao cho khi start rơi đúng cloud
        temp.y += 0.8f;
		player.transform.position = temp;
	}
    void OnTriggerEnter2D(Collider2D target)
    {
		// check xem có va chạm trigger vs các obj có tag dưới ko
        if (target.tag == "Cloud" || target.tag == "Deadly")
        {
			// check nếu n là đám mây cuối cùng trong mảng thì ...
            if (target.transform.position.y == lastCloudPositionY)
            {
				// xáo lại các mảng obj
                Shuffle(clouds);
                Shuffle(collectables);

				// lấy pos target trigger vs nó
                Vector3 temp = target.transform.position;

                for (int i = 0; i < clouds.Length; i++)
                {
					// check xem obj này có còn ko hoạt động trong scenes 
                    if (!clouds[i].activeInHierarchy)
                    {
						// tạo lại posX cho nó bằng controlX
                        if (controlX == 0)
                        {
                            temp.x = Random.Range(0.0f, maxX);
                            controlX = 1;
                        }
                        else if (controlX == 1)
                        {
                            temp.x = Random.Range(0.0f, minX);
                            controlX = 2;
                        }
                        else if (controlX == 2)
                        {
                            temp.x = Random.Range(1.0f, maxX);
                            controlX = 3;
                        }
                        else if (controlX == 3)
                        {
                            temp.x = Random.Range(-1.0f, minX);
                            controlX = 0;
                        }

						// gán posY ở dưới cloud trên
                        temp.y -= distanceBetweenClouds;

						// gán lại lastPosCloud
                        lastCloudPositionY = temp.y;

						// hiện nó lên
                        clouds[i].transform.position = temp;
                        clouds[i].SetActive(true);

						// random mảng coins, life
                        int random = Random.Range(0, collectables.Length);

						// check tag
                        if (clouds[i].tag != "Deadly")
                        {
							// check ko hoạt động trong scenes
                            if (!collectables[random].activeInHierarchy)
                            {
								// lấy pos của cloud để set cho pos coins, life ở trên nó
                                Vector3 temp2 = clouds[i].transform.position;
                                temp2.y += 0.7f;

								// check chút :))
                                if (collectables[random].tag == "Life")
                                {
                                    if (PlayerScore.lifeCount < 2)
                                    {
                                        collectables[random].transform.position = temp2;
                                        collectables[random].SetActive(true);
                                    }
                                }
                                else
                                {
                                    collectables[random].transform.position = temp2;
                                    collectables[random].SetActive(true);
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
