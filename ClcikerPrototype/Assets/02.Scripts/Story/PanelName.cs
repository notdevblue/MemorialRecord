using DG.Tweening;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelName : MonoBehaviour
{
    private readonly static string pushers = "������,rlatjdtn,A�޸�ü,��������,�ֻ���,���Ǽ�����,����,VNnotice,illgu,����,��õ��,���ѿ�,�ҿ���,����,LupusRex,����,�ڿ�,��ȭ��,�ѽ���,Rein"; //"Twilight,KDiwhY,RomiDal,kmg0924,�����,���,���츰,BigDrum,DeCKer,ť��,�����Ѻ�,������,SPIEA,�췹�̽�,�ôϿ���,���96,�Ȱ��ɰ�����,Chron,�ڸ�Ű,�˴�����Ű,ī�縣,�Ϸ纰����,SHak,K.Kaki28,����ȭ,ũ��,�ϴ�������,���-mellrose,�ó�,Taehoon Lim,���򵵷�,�����̸�,Nuri,Pnamue,xonoa,���ӳ���,LadyCALLA,nadaimma,PARKING,PARKING,���̳���,BRISAK,���� �������,������,������,mayouka,�俣,������,����,7�� �˶�,��ȣ��,������,����䲤��,������,whgustn7030,�ֿ���,�Ϸ�Ʈ�θ�����,��ȭ,�������,��´����,����,������,�ڵ�ӽ�,������,��,������,����,return P,Bengi,Kyrien,������,�����þ�,���°�,MJ,�絧��,�����,The Lingker,Sinhyun,���ؼ�,���Ƹ�,��þ�þ�,������,���,����,alvin1007,lemonhurt,ī�̳ʽ�,��°��,�ڼ��Ǹ��ơ�,�չα�,ȫ��Ƽ��,��������¯,������,�ּ���,dding,�Ͼ����Ÿ,kasir,�뷩��,zzune,����,Qno,��,C���,�强��,�̷�ȭ,������,�̳�,Lempt,�����̶�,������,���,��,����,MG,��ħ�Ҹ�,�Ͼ���,Kariyon,�ϻ糪,�ܿ����,hyunuoo,�ٴҶ󿡺���,���ϴ��Ǵ�,Rylalaop,dos245,�ܿ��ö�,Sein����,������,ī��,����,�ܱ�,��ȭ��,����,�����ϳ׿�_,���μ�,����,�Ž¿�,����,����ȯ,SummerFlo,����,�ں���,��Ȳ��,�̷�� ���,empty,�̵���,������,���,�ǿ���,�ҽ�����,friedrose,��,�����,����,������������,�¿���,�ֿ���,jonrok,�Ѻ���,ĭ����,0�ڶ�0,����,��ȳ�,������,�輺��,�翵ȭ,�ֵ���,�����,������,õ����,����,��öȣ,������,�ǹλ�,������,�ڱ���,������,�Ѽ���,�̵���,����,�ѽ���,�����";

    [SerializeField] InputField inputField = null;
    [SerializeField] Button btnOK = null;

    private void Awake()
    {
        inputField.onValueChanged.AddListener((text) =>
        {
            text = text.Trim('#');
            text = text.Trim('\'');
            text = text.Trim('"');
            text = text.Trim(',');
            inputField.SetTextWithoutNotify(text);

            btnOK.interactable = text.Trim(' ') != "";
        });

        btnOK.onClick.AddListener(OnClickOKBtn);
    }

    private void OnEnable()
    {
        var splitedData = pushers.Split(',');

        inputField.placeholder.GetComponent<Text>().text = splitedData[UnityEngine.Random.Range(0, splitedData.Length)];
    }

    public Func<bool> FadeInNameBox(float duration)
    {
        gameObject.SetActive(true);
        btnOK.interactable = inputField.text.Trim(' ') != "";
        GetComponent<CanvasGroup>().DOFade(1, duration);
        return () => { return !gameObject.activeSelf; };
    }

    public Func<bool> FadeOutNameBox(float duration)
    {
        GetComponent<CanvasGroup>().DOFade(0, duration).OnComplete(() => gameObject.SetActive(false));
        btnOK.interactable = false;
        return () => { return !gameObject.activeSelf; };
    }

    public void SetNamePanelInMain()
    {
        transform.parent.gameObject.SetActive(true);
        btnOK.onClick.RemoveAllListeners();
        btnOK.onClick.AddListener(() => SaveManager.CharName = inputField.text);
        btnOK.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
        btnOK.onClick.AddListener(() => btnOK.onClick.RemoveAllListeners());
    }

    private void OnClickOKBtn()
    {
        FadeOutNameBox(0.5f);
        SaveManager.CharName = inputField.text;
    }


}
