using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    #region DoctorInfo

    public InputField doctorName;
    public InputField khoa;
    public InputField hospital;

    #endregion

    #region PharmaInfo

    public InputField pharmaName;
    public InputField nhathuoc;
    public InputField place;

    #endregion

    public Text notice;
    public TestSelector selection;
    private void Start()
    {
        LoadSceneFromOther.isLoad = true;
        //Create new UserInfo Database
        CurrentUser.userInfo = new UserInfo();
    }

    #region Operations

    /// <summary>
    ///     On Register Click
    /// </summary>
    /// <param name="bnt">Bnt.</param>
    private void OnRegisterDoctor(GameObject bnt)
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
    private void OnRegisterPharmacy(GameObject bnt)
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
    /// <summary>
    ///     Raises the checkin click event.
    /// </summary>
    private void OnCheckinLocale(bool value)
    {
        StartCoroutine(StartLocationService());
    }
    public Toggle checkinToggle;
    /// <summary>
    ///     Starts the location service.
    /// </summary>
    /// <returns>The location service.</returns>
    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            checkinToggle.isOn = false;
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
            checkinToggle.isOn = true;
        }

        Input.location.Stop();
    }

    #endregion
}