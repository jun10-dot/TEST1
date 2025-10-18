using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// materials[0]와 같은식으로 접근하면 메테리얼 배열에 등록된 원본 메테리얼이 변경되므로
// 해당 메테리얼을 참조하는 오브젝트들의 메테리얼이 전부 바뀌게 된다.
// 이를 방지하기 위해 material로 가져옴(원본 메테리얼이 아닌 최종아웃풋 메테리얼이므로 다른 오브젝트에 영향이 없다)
public class BulletCtrl : MonoBehaviour
{
    public float speed = 30f;
    public float range = 400;

    public int power = 10;

    public GameObject ExploPtcl;

    private float dist;
    private float dist2;
    //포톤추가
    //폭탄을 발사한 플레이어의 ID 저장
    public int playerId = -1;
    // Start is called before the first frame update
    void Start()
    {
        //충돌 안해도 시간체크후 삭제
        //Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);    // 월드 좌표의 forward 방향으로 진행 (오브젝트를 돌려도 기존 방향으로 날아감)
        //GetComponent<Rigidbody>().AddForce(transform.forward * speed);  // 오브젝트 로컬 좌표의 forward 방향으로 진행
        //GetComponent<Rigidbody>().AddRelativeForce(transform.forward * speed);  // AddRelativeForce는 월드좌표를 로컬좌표로 변환 (로컬좌표를 넣으면 이상하게 동작함)
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);    // 월드좌표를 로컬좌표로 변환시켜주므로 정상 작동
        // 1. 월드좌표 z축 방향으로 진행.
        // 2. 로컬좌표 z축 방향으로 진행.
        // 3. 이상하게 움직임
        // 4. 로컬좌표 z축 방향으로 진행.

        // 업데이트 주기사이에 충돌을 지나치는 경우 충돌판정이 이루어지지 않기때문에 미세조정이 필요함
        // 리지드바디의 AddForce 해도 되지만 이렇게 Translate 함수로 이동 시켜줘도 된다.
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // 거리 기록
        dist += Time.deltaTime * speed;

        // 만약 내가 설정한 위치에 도달하면 Destroy
        if (dist >= range)
        {
            Instantiate(ExploPtcl, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    // 충돌시 파티클 생성 후 삭제
    /*    void OnTriggerEnter(Collider coll)
        {
            Instantiate(ExploPtcl, transform.position, transform.rotation);
            Destroy(gameObject); 
        }
        */

    // 충돌시 파티클 생성 후 삭제
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExploPtcl, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
