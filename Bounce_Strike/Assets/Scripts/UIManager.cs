using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] private Text destroyedText;


	void OnEnable() {
		EventRelay.OnEventRaised += HandleEvent;
	}

	void OnDisable() {
		EventRelay.OnEventRaised -= HandleEvent;
	}

	void HandleEvent() {
		destroyedText.gameObject.SetActive (true);

		Invoke ("DeactivateText", 1f);
	}

	void DeactivateText() {
		destroyedText.gameObject.SetActive (false);
	}
}
