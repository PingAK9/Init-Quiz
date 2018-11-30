using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    //Doctor
    public InputField doctorName;
    public InputField khoa;
    public InputField hospital;

    //Pharmar
    public InputField pharmaName;
    public InputField nhathuoc;
    public InputField place;

    public Text notice;
    public QuizSelector selection;
    public GameObject objDoctor;
    public GameObject objPharma;
    private void Start()
    {
        notice.text = "";
        CurrentUser.userInfo = new UserInfo();
    }
    public void StartGame()
    {
        if (objDoctor.activeSelf)
        {
            OnRegisterDoctor();
        }else
        {
            OnRegisterPharmacy();
        }
    }
    void OnRegisterDoctor()
    {
        //For Doctor
        if (string.IsNullOrEmpty(khoa.text) || string.IsNullOrEmpty(doctorName.text) ||
            string.IsNullOrEmpty(hospital.text))
        {
            notice.text = "Thiếu thông tin!";
            return;
        }

        CurrentUser.userInfo.isDoctor = true;
        CurrentUser.userInfo.fullname = doctorName.text;
        CurrentUser.userInfo.hospital = hospital.text;
        CurrentUser.userInfo.major = khoa.text;
        selection.PlayGame();
    }
    void OnRegisterPharmacy()
    {
        //For Pharmacy
        if (string.IsNullOrEmpty(pharmaName.text) || string.IsNullOrEmpty(place.text) ||
            string.IsNullOrEmpty(nhathuoc.text))
        {
            notice.text = "Thiếu thông tin!";
            return;
        }

        CurrentUser.userInfo.isDoctor = false;
        CurrentUser.userInfo.fullname = pharmaName.text;
        CurrentUser.userInfo.hospital = nhathuoc.text;
        CurrentUser.userInfo.place = place.text;

        selection.PlayGame();
    }
    public void OnClickTapPharmacy(bool value)
    {
        if (value)
        {
            objDoctor.SetActive(false);
            objPharma.SetActive(true);
        }
    }
    public void OnClickTapDoctor(bool value)
    {
        if (value)
        {
            objDoctor.SetActive(true);
            objPharma.SetActive(false);
        }
    }
    //
    public void OnCheckinLocale()
    {
        StartCoroutine(StartLocationService());
    }
    public Toggle checkinToggle;
    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            yield return false;
        }

        Input.location.Start();
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield return false;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield return false;
        }
        else
        {
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " +
                  Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " +
                  Input.location.lastData.timestamp);
            CurrentUser.userInfo.lat = Input.location.lastData.latitude;
            CurrentUser.userInfo.lng = Input.location.lastData.longitude;
        }

        Input.location.Stop();
    }

}