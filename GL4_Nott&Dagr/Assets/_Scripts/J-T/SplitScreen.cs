using UnityEngine;
using System;
using System.Collections;

public class SplitScreen : MonoBehaviour {

	/*Reference both the transforms of the two players on screen.
	Necessary to find out their current positions.*/
	public Transform player1;
	public Transform player2;

	//The distance at which the splitscreen will be activated.
	public float splitDistance = 5;

	//The color and width of the splitter which splits the two screens up.
	public Color splitterColor;
	public float splitterWidth;

	//The two cameras, both of which are initalized/referenced in the start function.
	private GameObject camera1;
	private GameObject camera2;

	//The two quads used to draw the second screen, both of which are initalized in the start function.
	private GameObject split;
	private GameObject splitter;
	private GameObject splitter2;

    [SerializeField]
    private GameObject splitParticle;
    [SerializeField]
    private GameObject splitParticle2;
    [SerializeField]
    private GameObject splitParticle3;

    private int numberOfParticleSystems;
    private ParticleSystem[] particles;

    void Start () {

        numberOfParticleSystems = splitParticle.transform.childCount + splitParticle2.transform.childCount + splitParticle3.transform.childCount;

        particles = new ParticleSystem[numberOfParticleSystems];

        particles[0] = splitParticle.transform.GetChild(0).transform.GetComponent<ParticleSystem>();
        particles[1] = splitParticle.transform.GetChild(1).transform.GetComponent<ParticleSystem>();
        particles[2] = splitParticle2.transform.GetChild(0).transform.GetComponent<ParticleSystem>();
        particles[3] = splitParticle2.transform.GetChild(1).transform.GetComponent<ParticleSystem>();
        particles[4] = splitParticle3.transform.GetChild(0).transform.GetComponent<ParticleSystem>();
        particles[5] = splitParticle3.transform.GetChild(1).transform.GetComponent<ParticleSystem>();

		//Referencing camera1 and initalizing camera2.
		camera1 = Camera.main.gameObject;
		camera2 = new GameObject ();
		camera2.AddComponent<Camera> ();
        camera2.AddComponent<FMODUnity.StudioListener>();
        camera2.GetComponent<Camera>().orthographic = true;
        camera2.GetComponent<Camera>().orthographicSize = camera1.GetComponent<Camera>().orthographicSize;
		//Setting up the culling mask of camera2 to ignore the layer "TransparentFX" as to avoid rendering the split and splitter on both cameras.
		camera2.GetComponent<Camera> ().cullingMask = ~(1 << LayerMask.NameToLayer ("TransparentFX"));

        //Setting up the splitter and initalizing the gameobject.
        splitter = GameObject.CreatePrimitive (PrimitiveType.Quad);
		splitter.transform.parent = gameObject.transform;
        splitter.transform.localPosition = new Vector3(0f, 0f, 5f);
        splitter.transform.localScale = new Vector3 (50, splitterWidth/10, 1);
		splitter.transform.localEulerAngles = Vector3.zero;
        splitter.GetComponent<MeshRenderer>().enabled = false;
        splitter.SetActive (false);

        splitParticle.transform.parent = splitter.transform;
        splitParticle.transform.localPosition = new Vector3(0f, 0f, -2.5f);

        splitParticle3.transform.parent = splitter.transform;
        splitParticle3.transform.localPosition = new Vector3(0f, 0f, -2.5f);

        splitter2 = GameObject.CreatePrimitive(PrimitiveType.Quad);
        splitter2.transform.parent = camera2.transform;
        splitter2.transform.localPosition = new Vector3(0f, 0f, 5f);
        splitter2.transform.localScale = new Vector3(50, splitterWidth / 10, 1);
        splitter2.transform.localEulerAngles = Vector3.zero;
        splitter2.GetComponent<MeshRenderer>().enabled = false;

        splitParticle2.transform.parent = splitter2.transform;
        splitParticle2.transform.localPosition = new Vector3(0f, 0f, 2.5f);


        //Setting up the split and initalizing the gameobject.
        split = GameObject.CreatePrimitive (PrimitiveType.Quad);
		split.transform.parent = splitter.transform;
		split.transform.localPosition = new Vector3 (0,-(15/(splitterWidth/10)),0);
		split.transform.localScale = new Vector3 (1, 30/(splitterWidth/10), 1);
		split.transform.localEulerAngles = Vector3.zero;

		//Creates both temporary materials required to create the splitscreen.
		Material tempMat = new Material (Shader.Find ("Unlit/Color"));
		tempMat.color = splitterColor;
		splitter.GetComponent<Renderer>().material = tempMat;
		splitter.GetComponent<Renderer> ().sortingOrder = 2;
		splitter.layer = LayerMask.NameToLayer ("TransparentFX");
        splitter2.GetComponent<Renderer>().material = tempMat;
        splitter2.GetComponent<Renderer>().sortingOrder = 2;
        splitter2.layer = LayerMask.NameToLayer("TransparentFX");
        Material tempMat2 = new Material (Shader.Find ("Mask/SplitScreen"));
		split.GetComponent<Renderer>().material = tempMat2;
		split.layer = LayerMask.NameToLayer ("TransparentFX");
	}

	void LateUpdate () {
		//Gets the y axis distance between the two players and just the standard distance.
		float yDistance = player1.position.y - player2.transform.position.y;
		float distance = Vector3.Distance (player1.position, player2.transform.position);

		//Sets the angle of the player up, depending on who's leading on the x axis.
		float angle;
		if (player1.transform.position.x <= player2.transform.position.x) {
			angle = Mathf.Rad2Deg * Mathf.Acos (yDistance / distance);
		} else {
			angle = Mathf.Rad2Deg * Mathf.Asin (yDistance / distance) - 90;
		}

		//Rotates the splitter according to the new angle.
		splitter.transform.localEulerAngles = new Vector3 (0, 0, angle);
		splitter2.transform.localEulerAngles = new Vector3 (0, 0, angle);

        //Gets the exact midpoint between the two players.
        Vector3 midPoint = new Vector3 ((player1.position.x + player2.position.x) / 2, (player1.position.y + player2.position.y) / 2, (player1.position.z + player2.position.z) / 2); 

		//Waits for the two cameras to split and then calcuates a midpoint relevant to the difference in position between the two cameras.
        if (yDistance > splitDistance / 2 || yDistance < -splitDistance / 2)
        {
            //Debug.Log("Y Distance!");
            Vector3 offset = midPoint - player1.position;
            offset.x = Mathf.Clamp(offset.x, -splitDistance / 2, splitDistance / 2);
            offset.y = Mathf.Clamp(offset.y, -splitDistance / 4, splitDistance / 4);
            offset.z = Mathf.Clamp(offset.z, -splitDistance / 2, splitDistance / 2);
            midPoint = player1.position + offset;

            Vector3 offset2 = midPoint - player2.position;
            offset2.x = Mathf.Clamp(offset.x, -splitDistance / 2, splitDistance / 2);
            offset2.y = Mathf.Clamp(offset.y, -splitDistance / 4, splitDistance / 4);
            offset2.z = Mathf.Clamp(offset.z, -splitDistance / 2, splitDistance / 2);
            Vector3 midPoint2 = player2.position - offset2;

            //Sets the splitter and camera to active and sets the second camera position as to avoid lerping continuity errors.
            if (splitter.activeSelf == false)
            {
                splitter.SetActive(true);
                camera2.SetActive(true);

                camera2.transform.position = camera1.transform.position;
                camera2.transform.rotation = camera1.transform.rotation;

                Invoke("PlayParticles", 0.05f);
            }
            else
            {
                //Lerps the second cameras position and rotation to that of the second midpoint, so relative to the second player.
                camera2.transform.position = Vector3.Lerp(camera2.transform.position, midPoint2 + new Vector3(0, 0, -10), Time.deltaTime * 5);
                //Quaternion newRot2 = Quaternion.LookRotation(midPoint2 - camera2.transform.position);
                //camera2.transform.rotation = Quaternion.Lerp(camera2.transform.rotation, newRot2, Time.deltaTime * 5);
            }
        }
		else if (distance > splitDistance)
        {
            //Debug.Log("X Distance!");
            Vector3 offset = midPoint - player1.position;
            offset.x = Mathf.Clamp(offset.x, -splitDistance / 2, splitDistance / 2);
            offset.y = Mathf.Clamp(offset.y, -splitDistance / 2, splitDistance / 2);
            offset.z = Mathf.Clamp(offset.z, -splitDistance / 2, splitDistance / 2);
            midPoint = player1.position + offset;

            Vector3 offset2 = midPoint - player2.position;
            offset2.x = Mathf.Clamp(offset.x, -splitDistance / 2, splitDistance / 2);
            offset2.y = Mathf.Clamp(offset.y, -splitDistance / 2, splitDistance / 2);
            offset2.z = Mathf.Clamp(offset.z, -splitDistance / 2, splitDistance / 2);
            Vector3 midPoint2 = player2.position - offset2;

            //Sets the splitter and camera to active and sets the second camera position as to avoid lerping continuity errors.
            if (splitter.activeSelf == false)
            {
                splitter.SetActive(true);
                camera2.SetActive(true);

                camera2.transform.position = camera1.transform.position;
                camera2.transform.rotation = camera1.transform.rotation;

                Invoke("PlayParticles", 0.05f);
            }
            else
            {
                //Lerps the second cameras position and rotation to that of the second midpoint, so relative to the second player.
                camera2.transform.position = Vector3.Lerp(camera2.transform.position, midPoint2 + new Vector3(0, 0, -10), Time.deltaTime * 5);
                //Quaternion newRot2 = Quaternion.LookRotation(midPoint2 - camera2.transform.position);
                //camera2.transform.rotation = Quaternion.Lerp(camera2.transform.rotation, newRot2, Time.deltaTime * 5);
            }
        }
        else
        {
			//Deactivates the splitter and camera once the distance is less than the splitting distance (assuming it was at one point).
			if (splitter.activeSelf)
            {
				splitter.SetActive (false);
				camera2.SetActive (false);
                StopParticles();
            }
		}

		/*Lerps the first cameras position and rotation to that of the second midpoint, so relative to the first player
		or when both players are in view it lerps the camera to their midpoint.*/
		camera1.transform.position = Vector3.Lerp(camera1.transform.position,midPoint + new Vector3(0,0,-10),Time.deltaTime*5);
        //Quaternion newRot = Quaternion.LookRotation(midPoint - camera1.transform.position);
        //camera1.transform.rotation = Quaternion.Lerp(camera1.transform.rotation, newRot, Time.deltaTime * 5);
    }

    private void PlayParticles()
    {
        for (int i = 0; i < numberOfParticleSystems; i++)
        {
            particles[i].Play();
        }
    }

    private void StopParticles()
    {
        for (int i = 0; i < numberOfParticleSystems; i++)
        {
            particles[i].Stop();
        }
    }
}
