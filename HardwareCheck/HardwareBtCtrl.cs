﻿using UnityEngine;
using System.Diagnostics;

public class HardwareBtCtrl : MonoBehaviour
{
	public enum BtType
	{
		CloseBt,
		LightStartP1,
		LightStartP2,
		ShuiQiang,
		JianBiBt,
		RestartBt,
		QiNangBt_1,
		QiNangBt_2,
		QiNangBt_3,
		QiNangBt_4,
		QiNangBt_5,
		QiNangBt_6,
		JiaMiCheckBt,
	}
	public BtType BtState = BtType.CloseBt;
	public UILabel QiNangLabel;
	
	public static UILabel StartLedP1;
	public static UILabel StartLedP2;
	public static UILabel ShuiQiangLabel;
	int StartLedNumP1 = 1;
	int StartLedNumP2 = 1;
	int ShuiBengNum;

	void OnClick()
	{
		//Debug.Log("***************BtState "+BtState);
		switch (BtState) {
		case BtType.CloseBt:
			Application.Quit();
			break;

		case BtType.LightStartP1:
			StartLedCheckP1();
			break;

		case BtType.LightStartP2:
			StartLedCheckP2();
			break;

		case BtType.ShuiQiang:
			ShuiBengCheck();
			break;

		case BtType.JianBiBt:
			PcvrSubCoin();
			break;

		case BtType.RestartBt:
			OnClickRestartAppBt();
			break;
			
		case BtType.QiNangBt_1:
		case BtType.QiNangBt_2:
		case BtType.QiNangBt_3:
		case BtType.QiNangBt_4:
		case BtType.QiNangBt_5:
		case BtType.QiNangBt_6:
			OnClickQiNangBt(BtState);
			break;

		case BtType.JiaMiCheckBt:
			pcvr.GetInstance().StartJiaoYanIO();
			break;
		}
	}

	void PcvrSubCoin()
	{
		pcvr.GetInstance().SubPlayerCoin(1);
	}

	void ShuiBengCheck()
	{
		ShuiBengNum++;
		switch (ShuiBengNum) {
		case 1:
			ShuiQiangLabel.text = "水枪等级1";
			pcvr.IsOpenShuiBeng = true;
			pcvr.ShuiBengState = PcvrShuiBengState.Level_1;
			break;

		case 2:
			ShuiQiangLabel.text = "水枪等级2";
			pcvr.IsOpenShuiBeng = true;
			pcvr.ShuiBengState = PcvrShuiBengState.Level_2;
			break;

		case 3:
			ShuiQiangLabel.text = "水枪关闭";
			pcvr.IsOpenShuiBeng = false;
			pcvr.ShuiBengState = PcvrShuiBengState.Level_1;
			ShuiBengNum = 0;
			break;
		}
	}

	void StartLedCheckP1()
	{
		StartLedNumP1++;
		switch (StartLedNumP1) {
		case 1:
			StartLedP1.text = "开始灯P1亮";
			pcvr.StartLightStateP1 = LedState.Liang;
			break;

		case 2:
			StartLedP1.text = "开始灯P1闪";
			pcvr.StartLightStateP1 = LedState.Shan;
			break;

		case 3:
			StartLedP1.text = "开始灯P1灭";
			pcvr.StartLightStateP1 = LedState.Mie;
			StartLedNumP1 = 1;
			break;
		}
	}

	void StartLedCheckP2()
	{
		StartLedNumP2++;
		switch (StartLedNumP2) {
		case 1:
			StartLedP2.text = "开始灯P2亮";
			pcvr.StartLightStateP2 = LedState.Liang;
			break;
			
		case 2:
			StartLedP2.text = "开始灯P2闪";
			pcvr.StartLightStateP2 = LedState.Shan;
			break;
			
		case 3:
			StartLedP2.text = "开始灯P2灭";
			pcvr.StartLightStateP2 = LedState.Mie;
			StartLedNumP2 = 1;
			break;
		}
	}

	void OnClickRestartAppBt()
	{
		Application.Quit();
		RunCmd("start ComTest.exe");
	}

	public static void OnRestartGame()
	{
		if (HardwareCheckCtrl.IsTestHardWare) {
			return;
		}
		Application.Quit();
		RunCmd("start Waterwheel.exe");
	}

	static void RunCmd(string command)
	{
		//實例一個Process類，啟動一個獨立進程    
		Process p = new Process();    //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，    
		//包括了一些屬性和方法，下面我們用到了他的幾個屬性：   
		p.StartInfo.FileName = "cmd.exe";           //設定程序名   
		p.StartInfo.Arguments = "/c " + command;    //設定程式執行參數   
		p.StartInfo.UseShellExecute = false;        //關閉Shell的使用    p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入    p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出   
		p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出    
		p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口    
		p.Start();   //啟動
	}

	void OnClickQiNangBt(BtType state)
	{
		switch (state) {
		case BtType.QiNangBt_1:
			QiNangLabel.text = QiNangLabel.text != "气囊1充气" ? "气囊1充气" : "气囊1放气";
			pcvr.QiNangArray[0] = (byte)(pcvr.QiNangArray[0] != 1 ? 1 : 0);
			break;

		case BtType.QiNangBt_2:
			QiNangLabel.text = QiNangLabel.text != "气囊2充气" ? "气囊2充气" : "气囊2放气";
			pcvr.QiNangArray[1] = (byte)(pcvr.QiNangArray[1] != 1 ? 1 : 0);
			break;

		case BtType.QiNangBt_3:
			QiNangLabel.text = QiNangLabel.text != "气囊3充气" ? "气囊3充气" : "气囊3放气";
			pcvr.QiNangArray[2] = (byte)(pcvr.QiNangArray[2] != 1 ? 1 : 0);
			break;

		case BtType.QiNangBt_4:
			QiNangLabel.text = QiNangLabel.text != "气囊4充气" ? "气囊4充气" : "气囊4放气";
			pcvr.QiNangArray[3] = (byte)(pcvr.QiNangArray[3] != 1 ? 1 : 0);
			break;
			
		case BtType.QiNangBt_5:
			QiNangLabel.text = QiNangLabel.text != "气囊5充气" ? "气囊5充气" : "气囊5放气";
			pcvr.QiNangArray[4] = (byte)(pcvr.QiNangArray[4] != 1 ? 1 : 0);
			break;
			
		case BtType.QiNangBt_6:
			QiNangLabel.text = QiNangLabel.text != "气囊6充气" ? "气囊6充气" : "气囊6放气";
			pcvr.QiNangArray[5] = (byte)(pcvr.QiNangArray[5] != 1 ? 1 : 0);
			break;
		}
	}
}