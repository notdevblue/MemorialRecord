using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAlarm : MonoBehaviour
{
    private readonly static string pushers = "Twilight,KDiwhY,RomiDal,kmg0924,�����,���,���츰,BigDrum,DeCKer,ť��,�����Ѻ�,������,SPIEA,�췹�̽�,�ôϿ���,���96,�Ȱ��ɰ�����,Chron,�ڸ�Ű,�˴�����Ű,ī�縣,�Ϸ纰����,SHak,K.Kaki28,����ȭ,ũ��,�ϴ�������,���-mellrose,�ó�,Taehoon Lim,���򵵷�,�����̸�,Nuri,Pnamue,xonoa,���ӳ���,LadyCALLA,nadaimma,PARKING,PARKING,���̳���,BRISAK,���� �������,������,������,mayouka,�俣,������,����,7�� �˶�,��ȣ��,������,����䲤��,������,whgustn7030,�ֿ���,�Ϸ�Ʈ�θ�����,��ȭ,�������,��´����,����,������,�ڵ�ӽ�,������,��,������,����,return P,Bengi,Kyrien,������,�����þ�,���°�,MJ,�絧��,�����,The Lingker,Sinhyun,���ؼ�,���Ƹ�,��þ�þ�,������,���,����,alvin1007,lemonhurt,ī�̳ʽ�,��°��,�ڼ��Ǹ��ơ�,�չα�,ȫ��Ƽ��,��������¯,������,�ּ���,dding,�Ͼ����Ÿ,kasir,�뷩��,zzune,����,Qno,��,C���,�强��,�̷�ȭ,������,�̳�,Lempt,�����̶�,������,���,��,����,MG,��ħ�Ҹ�,�Ͼ���,Kariyon,�ϻ糪,�ܿ����,hyunuoo,�ٴҶ󿡺���,���ϴ��Ǵ�,Rylalaop,dos245,�ܿ��ö�,Sein����,������,ī��,����,�ܱ�,��ȭ��,����,�����ϳ׿�_,���μ�,����,�Ž¿�,����,����ȯ,SummerFlo,����,�ں���,��Ȳ��,�̷�� ���,empty,�̵���,������,���,�ǿ���,�ҽ�����,friedrose,��,�����,����,������������,�¿���,�ֿ���,jonrok,�Ѻ���,ĭ����,0�ڶ�0,����,��ȳ�,������,�輺��,�翵ȭ,�ֵ���,�����,������,õ����,����,��öȣ,������,�ǹλ�,������,�ڱ���,������,�Ѽ���,�̵���,����,�ѽ���,�����";

    private void Start()
    {
        if(SaveManager.UnlockedStories.Count == 0)
        {
            SaveManager.UnlockNewChapter();
            CustomSceneManager.StorySceneChangeFromMain(0);
        }
        else if(SaveManager.CharName == "$InitalizedPlayerName$")
        {
            FindObjectOfType<PanelName>(true).SetNamePanelInMain();
        }

        if (SaveManager.IsNewUser)
        {
            SaveManager.InventoryItems = new List<Inventory.ItemParent>() { new Inventory.Item_Memorial(0, 1), new Inventory.Item_Ink(0, 1), new Inventory.Item_ResearchResource(0, 1) };
            SaveManager.CurInk = 1;;
            SaveManager.IsNewUser = false;
        }

    }

    public void CallStoryAlarm(int idx)
    {
        FindObjectOfType<PanelGameNotice>().SetNoticePanel(() => 
        {
            CustomSceneManager.StorySceneChangeFromMain(idx);
        }
        , () => { }
        , "��", "�ƴϿ�", "å �ر�", "���ο� ���丮�� �رݵǾ����ϴ�.\n���� Ȯ���Ͻðڽ��ϱ�?");
    }
}
