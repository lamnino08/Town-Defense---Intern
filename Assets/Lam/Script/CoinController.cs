using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float moveDuration = 2f;   // Thời gian di chuyển
    [SerializeField] private float destroyDelay = 1f;    // Thời gian trì hoãn trước khi hủy đối tượng

    public void MoveCoinToPlayer(Vector3 pos)
    {
        // Di chuyển đồng tiền đến vị trí của người chơi
        transform.DOMove(pos, moveDuration)
            .SetEase(Ease.InOutQuad)  // Thay đổi kiểu easing nếu cần
            .OnComplete(() => StartCoroutine(DestroyAfterDelay()));
    }

    private IEnumerator DestroyAfterDelay()
    {
        // Đợi một khoảng thời gian trước khi hủy đối tượng
        Penhouse.instance.AddGold(5);
        GetComponent<AudioSource>().PlayOneShot(AudioAssitance.Instance.GetClipByName("EarnMoney"));
        yield return new WaitForSeconds(destroyDelay);
        
        Destroy(gameObject);
    }
}
