using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRelay {

	public delegate void OnEvent();

	public static event OnEvent OnEventRaised;

	public static void RaiseEvent() {
		if (OnEventRaised != null) {
			OnEventRaised ();
		}
	}

}
