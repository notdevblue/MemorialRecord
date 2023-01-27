using DG.Tweening;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelName : MonoBehaviour
{
    private readonly static string pushers = "강성아,rlatjdtn,A급몸체,동주초이,주새아,음악선생님,돌고래,VNnotice,illgu,무영,달천이,무한영,소울이,유성,LupusRex,소진,자영,신화성,한승훈,Rein"; //"Twilight,KDiwhY,RomiDal,kmg0924,김류빈,고귬,정우린,BigDrum,DeCKer,큐세,말랑한볼,강병선,SPIEA,헤레이스,시니여비,백곰96,안가령강소은,Chron,코마키,검더리쿠키,카사르,하루별사이,SHak,K.Kaki28,연리화,크롬,하늘페이지,小愛-mellrose,택넴,Taehoon Lim,도뤼도뤼,은영이모,Nuri,Pnamue,xonoa,너임나임,LadyCALLA,nadaimma,PARKING,PARKING,힘이난다,BRISAK,집에 보내줘요,영따리,윤형준,mayouka,요엔,한재진,말롱,7시 알람,설호아,조성빈,약빤토깽이,최정은,whgustn7030,최욱진,일렉트로마스터,설화,감사숭이,김쨈민이,난관,안유현,핸드머신,정진규,진,서현승,데레,return P,Bengi,Kyrien,마용이,리브라시아,길태건,MJ,루덴스,김옥돌,The Lingker,Sinhyun,서해성,수아린,리첸시아,고현욱,우산,지승,alvin1007,lemonhurt,카이너스,마째승,★세실리아★,손민기,홍시티마,마마오이짱,릴리유,최석훈,dding,암어랍스타,kasir,대랭이,zzune,찰론,Qno,늘,C용원,장성민,이루화,문성현,미나,Lempt,선데이라떼,낙은애,쏘가리,붸,공룡,MG,아침소리,하양훈,Kariyon,하사나,겨울새벽,hyunuoo,바닐라에빠짐,저하늘의달,Rylalaop,dos245,단오시랑,Sein세인,곰냥이,카르,팬텀,햄규,연화사,리프,서운하네요_,전민석,딜론,신승엽,라프,박태환,SummerFlo,양유,박보종,이황희,이루다 밝다,empty,이동국,전병욱,흑렬,권영일,소스범벅,friedrose,션,정대원,건태,한유나한유나,태월영,주예랑,jonrok,한빈이,칸야제,0자라0,수남,김안녕,이준협,김성수,사영화,최동주,장원혁,성지용,천현성,최희선,강철호,안현아,권민상,김찬규,박근형,최훈정,한세현,이동혁,허정,한승훈,이재원";

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
