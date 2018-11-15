using UnityEngine;
using System.Collections;

public class ClipList
{
    public static string prefix = "Tutorial/";

    public static string AjustTheLength = "AdjustTheLength.mov";
    public static string DragDrop = "DragDrop.mov";
    public static string FillTheBlank = "FillTheBlank.mov";
    public static string GhepHinh = "GhepHinh.mov";
    public static string MultipleChoices = "MultipleChoices.mov";
    public static string SapXep = "SapXep.mov";
    public static string Swipe = "Swipe.mov";

    public static void PlayTutorial(string tuto)
    {
        Handheld.PlayFullScreenMovie(prefix + tuto, Color.black, FullScreenMovieControlMode.Hidden);
    }
    public static void PlayTutorial(TypeClip _type)
    {
        string tuto ="";
        switch (_type)
        {
            case TypeClip.AjustTheLength:
                tuto = ClipList.AjustTheLength;
                break;
            case TypeClip.DragDrop:
                tuto = ClipList.DragDrop;
                break;
            case TypeClip.FillTheBlank:
                tuto = ClipList.FillTheBlank;
                break;
            case TypeClip.GhepHinh:
                tuto = ClipList.MultipleChoices;
                break;
            case TypeClip.MultipleChoices:
                break;
            case TypeClip.SapXep:
                tuto = ClipList.SapXep;
                break;
            case TypeClip.Swipe:
                tuto = ClipList.Swipe;
                break;
            default:
                break;
        }
        if (string.IsNullOrEmpty(tuto) == false)
        {
            ClipList.PlayTutorial(tuto);
        }
    }
}
public enum TypeClip
{
    None,
    AjustTheLength,
    DragDrop,
    FillTheBlank,
    GhepHinh,
    MultipleChoices,
    SapXep,
    Swipe
}
