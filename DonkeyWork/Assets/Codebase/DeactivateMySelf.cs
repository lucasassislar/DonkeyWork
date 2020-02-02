using UnityEngine;
using System.Collections;

public class DeactivateMySelf : MonoBehaviour {

	public float TimeToDeactivate;
    public bool actuallyDestroyMyself;
	void OnEnable() {
        CancelInvoke();
		Invoke ("DeactivateMyself", TimeToDeactivate);
	    
	}

    private void DeactivateMyself()
    {
        gameObject.SetActive(false);
        if (actuallyDestroyMyself)
        {
            Destroy(gameObject);
        }
    }

    /*
    IEnumerator DeactivateMyself() {
        yield return new WaitForSeconds(TimeToDeactivate);
        gameObject.SetActive(false);
    }*/
}
