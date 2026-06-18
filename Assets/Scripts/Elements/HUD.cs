using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    #region Singleton
    public static HUD current;
    private void Start()
    {
        current = this;
    }
    #endregion
    [Header("Settings")]
    public int defaultNoticeDuration = 5;
    public int defaultAnnouncementDuration = 5;
    [Header("Components")]
    [SerializeField] TextMeshProUGUI announcementText;
    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] TextMeshProUGUI noticeText;
    [SerializeField] Transform healthBar;
    [SerializeField] TextMeshProUGUI objectivesField;
    [SerializeField] List<GameObject> keysCollected;

    bool presentingNotice = false;
    bool presentingAnnoucements = false;
    #region Notices & Announcements
    List<string> upcomingNotices = new();
    List<int> upcomingNoticeDurations = new();
    List<string> upcomingAnnouncements = new();
    List<string> upcomingSubtitles = new();
    List<int> upcomingAnnouncementDurations = new();
    Coroutine noticeCoroutine;
    Coroutine announcementCoroutine;
    public static void DisplayNotice(string txt)
    {
        DisplayNotice(txt,false,current.defaultAnnouncementDuration);
    }
    public static void DisplayNotice(string txt, int duration)
    {
        DisplayNotice(txt,false,duration);
    }
    public static void DisplayNotice(string txt, bool priority)
    {
        DisplayNotice(txt, priority, current.defaultNoticeDuration);
    }
    public static void DisplayNotice(string txt, bool priority, int duration)
    {
        if (priority)
        {
            current.upcomingNotices.Insert(0, txt);
            current.upcomingNoticeDurations.Insert(0, duration);
        }
        else
        {
            current.upcomingNotices.Add(txt);
            current.upcomingNoticeDurations.Add(duration);
        }
        current.StartNoticeCycle(priority);

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
            if (!presentingNotice)
            {
                if (noticeCoroutine != null)
                {
                    StopCoroutine(noticeCoroutine);
                }
                noticeCoroutine = StartCoroutine(NoticeCycle());
            }
        }

    }
    IEnumerator NoticeCycle()
    {
        presentingNotice = true;
        while (upcomingNotices.Count > 0)
        {
            noticeText.text = upcomingNotices[0];
            yield return new WaitForSeconds(upcomingNoticeDurations[0]);
            upcomingNotices.RemoveAt(0);
            upcomingNoticeDurations.RemoveAt(0);
        }
        noticeText.text = "";
        presentingNotice = false;
    }
    public static void DisplayAnnouncement(string title)
    {
        DisplayAnnouncement(title, "");
    }
    public static void DisplayAnnouncement(string title,string subtitle)
    {
        DisplayAnnouncement(title, subtitle, false, current.defaultAnnouncementDuration);
    }
    public static void DisplayAnnouncement(string title, string subtitle, int duration)
    {
        DisplayAnnouncement(title,subtitle,false,duration);
    }
    public static void DisplayAnnouncement(string title, string subtitle, bool priority)
    {
        DisplayAnnouncement(title, subtitle, priority, current.defaultAnnouncementDuration);
    }
    public static void DisplayAnnouncement(string title, string subtitle,bool priority,int duration)
    {
        if (priority)
        {
            current.upcomingAnnouncements.Insert(0, title);
            current.upcomingSubtitles.Insert(0, subtitle);
            current.upcomingAnnouncementDurations.Insert(0, duration);
        }
        else
        {
            current.upcomingAnnouncements.Add(title);
            current.upcomingSubtitles.Add(subtitle);
            current.upcomingAnnouncementDurations.Add(duration);
        }
        current.StartAnnouncementCycle(priority);
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
            if (!presentingAnnoucements)
            {
                if (announcementCoroutine != null)
                {
                    StopCoroutine(announcementCoroutine);
                }
                announcementCoroutine = StartCoroutine(AnnouncementCycle());
            }
        }

    }
    IEnumerator AnnouncementCycle()
    {
        presentingAnnoucements = true;
        while (upcomingSubtitles.Count > 0)
        {
            announcementText.text = upcomingAnnouncements[0];
            subtitleText.text  = upcomingSubtitles[0];
            yield return new WaitForSeconds(upcomingAnnouncementDurations[0]);
            upcomingAnnouncements.RemoveAt(0);
            upcomingSubtitles.RemoveAt(0);
            upcomingAnnouncementDurations.RemoveAt(0);
        }
        announcementText.text = "";
        subtitleText.text = "";
        presentingAnnoucements = false;
    }
    #endregion

    #region Health & Objectives
    public static void DisplayHealth(float percentage)
    {
        // Displays the health bar in 
        current.healthBar.localScale = new Vector3(percentage / 100,1,1);
    }
    public static void DisplayKey(ObjectivesManager.KeyColor key)
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
