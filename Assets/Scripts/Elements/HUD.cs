using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    #region Singleton
    static HUD current;
    private void Start()
    {
        current = this;
    }
    #endregion
    [Header("Settings")]
    [SerializeField] int noticeDuration = 5;
    [SerializeField] int announcementDuration = 5;
    [Header("Components")]
    [SerializeField] TextMeshProUGUI announcementText;
    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] TextMeshProUGUI noticeText;
    [SerializeField] Transform healthBar;
    [SerializeField] TextMeshProUGUI objectivesField;
    [SerializeField] List<GameObject> keysCollected;

    #region Notices & Announcements
    List<string> upcomingNotices = new();
    List<string> upcomingAnnouncements = new();
    List<string> upcomingSubtitles = new();
    Coroutine noticeCoroutine;
    Coroutine announcementCoroutine;
    public static void DisplayNotice(string txt)
    {
        current.upcomingNotices.Add(txt);
        current.StartNoticeCycle(false);
    }
    public static void DisplayNotice(string txt, bool priority)
    {
        if (priority)
        {
            current.upcomingNotices.Insert(0, txt);
            current.StartNoticeCycle(true);
        }
        else
        {
            DisplayNotice(txt);
        }
        
        
    }
    void StartNoticeCycle(bool priority)
    {
        if (priority)
        {
            if (noticeCoroutine != null) {
                StopCoroutine(noticeCoroutine);
            }
            noticeCoroutine = StartCoroutine(NoticeCycle());
        }
        else
        {
            if (noticeCoroutine == null)
            {
                noticeCoroutine = StartCoroutine(NoticeCycle());
            }
        }

    }
    IEnumerator NoticeCycle()
    {
        while (upcomingNotices.Count > 0)
        {
            noticeText.text = upcomingNotices[0];
            yield return new WaitForSeconds(noticeDuration);
            upcomingNotices.RemoveAt(0);
        }
        noticeText.text = "";
    }
    public static void DisplayAnnouncement(string title,string subtitle)
    {
        current.upcomingAnnouncements.Add(title);
        current.upcomingSubtitles.Add(subtitle);
        current.StartAnnouncementCycle(false);

    }
    public static void DisplayAnnouncement(string title, string subtitle,bool priority)
    {
        if (priority)
        {
            current.upcomingAnnouncements.Insert(0, title);
            current.upcomingSubtitles.Insert(0, subtitle);
            current.StartAnnouncementCycle(true);
        }
        else
        {
            DisplayAnnouncement(title, subtitle);
        }
    }
    void StartAnnouncementCycle(bool priority)
    {
        if (priority)
        {
            if (announcementCoroutine != null)
            {
                StopCoroutine(announcementCoroutine);
            }
            announcementCoroutine = StartCoroutine(AnnouncementCycle());
        }
        else
        {
            if (announcementCoroutine == null)
            {
                announcementCoroutine = StartCoroutine(AnnouncementCycle());
            }
        }

    }
    IEnumerator AnnouncementCycle()
    {
        while (upcomingSubtitles.Count > 0)
        {
            announcementText.text = upcomingAnnouncements[0];
            subtitleText.text  = upcomingSubtitles[0];
            yield return new WaitForSeconds(announcementDuration);
            upcomingAnnouncements.RemoveAt(0);
            upcomingSubtitles.RemoveAt(0);
        }
        announcementText.text = "";
        noticeText.text = "";
    }
    #endregion

    #region Health & Objectives
    public static void DisplayHealth(float percentage)
    {
        // Displays the health bar in 
        current.healthBar.localScale = new Vector3(percentage / 100,1,1);
    }
    public static void DisplayKey(ObjectivesManager.CollecibleKey key)
    {
        current.keysCollected[(int)key].SetActive(true);
    }
    public static void DisplayObjectives()
    {
        string toDisplay = "";
        for (int i = 0; i < ObjectivesManager.counterCounts.Count; i++)
        {
            if (i != 0) toDisplay += "\n";
            toDisplay += ObjectivesManager.counterCounts[i] + "/" + ObjectivesManager.counterGoals[i] + " " + ObjectivesManager.counterDescriptions[i];
        }
        current.objectivesField.text = toDisplay;
    }
    #endregion
}
