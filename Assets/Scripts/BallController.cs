using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {

	[SerializeField] private Transform directionGuide;

	[SerializeField] private float movementSpeed = 10f;

	[SerializeField] private Transform ballSilhouetteParent;


	private bool ballShot = false;

	private int numberOfSpheres;

	private Vector3 directionVector;

	private Vector3 velocity ;

	private Rigidbody rb;


	private int collisionCount = 0;
	private int Count = 0;

	private Vector3 originalPosition;

	[SerializeField] private GameObject winText;

	void Start () {
		winText.SetActive(false);
		winText.GetComponent<Text>().text = "You Won!";
		numberOfSpheres = 10;
		for (int i = 0; i < numberOfSpheres; i++) {
			Transform sphere = Instantiate (directionGuide);
			sphere.transform.SetParent (ballSilhouetteParent);
		}

		rb = GetComponent<Rigidbody> ();

		originalPosition = transform.position;
	}
	

	void Update () {
		Vector3 mouseScreenPosition = Input.mousePosition;	

		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (mouseScreenPosition);
		mouseWorldPosition.z = 0;

		directionVector = (mouseWorldPosition - transform.position).normalized;

		for (int i = 0; i < ballSilhouetteParent.childCount; i++) {
			ballSilhouetteParent.GetChild (i).position = transform.position + directionVector * (i + 1);
		}


	}

	void FixedUpdate() {
		if (Input.GetMouseButtonDown (0) && ballShot == false) {
			ballShot = true;
			velocity = movementSpeed * directionVector;	
			rb.velocity = velocity;

			ballSilhouetteParent.gameObject.SetActive (false);
		}

	}

	IEnumerator Restart()
	{
		winText.SetActive(true);
		Instantiate(Resources.Load("Prefabs/D"), Vector2.down * 2, Quaternion.identity);
		yield return new WaitForSeconds(2.5f);
		winText.GetComponent<Text>().text = "Restarting hold up...";
		yield return new WaitForSeconds(2.5f);
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	void OnCollisionEnter(Collision col) {
		collisionCount++;
		if (collisionCount == 17) {
			collisionCount = 0;
			transform.position = originalPosition;
			rb.velocity = Vector2.zero;
			ballSilhouetteParent.gameObject.SetActive (true);
			ballShot = false;
		}
		if (col.gameObject.tag == "Enemy") {
			Instantiate(Resources.Load("Prefabs/D"), col.transform.position, Quaternion.identity);
			Destroy (col.gameObject);
			Count++;

			EventRelay.RaiseEvent ();

			if (Count == 4)
			{
				StartCoroutine(Restart());
			}
		}
	}
}
