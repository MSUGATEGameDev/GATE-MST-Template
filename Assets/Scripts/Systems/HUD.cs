using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// -- System -- Displays text and other important info on-screen.
/// </summary>
public class HUD : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- System --\n" +
        "Displays text and other important info on-screen.";
    #endregion

    #region Instantiating as a Singleton
    // Singletons are where only one instance of this class can exist at one time in the game.
    // This allows other classes to reference it easily.
    public static HUD singleton;
    private void Awake() // The very first function run by a class the instant it is created.
    {
        if (singleton != null)
            Destroy(this);
        singleton = this;
    }
    #endregion

    #region Inspector-Editable Variables
    [Header("Settings")]
    [Tooltip("How long (in seconds) to display notices if not otherwise specified.")] public int defaultNoticeDuration = 5;
    [Tooltip("How long (in seconds) to display announcements if not otherwise specified.")] public int defaultAnnouncementDuration = 5;
    [Header("Components")]
    [Tooltip("Big bold text in center of screen.")][SerializeField] TextMeshProUGUI announcementText;
    [Tooltip("Smaller text below announcement.")][SerializeField] TextMeshProUGUI subtitleText;
    [Tooltip("Small text at the bottom of the screen.")][SerializeField] TextMeshProUGUI noticeText;
    [Tooltip("Contains Health Bar")][SerializeField] GameObject healthBarHolder;
    [Tooltip("Adjustable bar at the top of the screen indicating player's health.")][SerializeField] Transform healthBar;
    [Tooltip("Image representing the health bar, so we can adjust the color.")][SerializeField] Image healthBarImage;
    [Tooltip("Text in the top left for displaying objective completion status.")][SerializeField]                       TextMeshProUGUI objectivesField;
    [Tooltip("List of key objects in top right to allow people to see what keys they've collected.")][SerializeField]   List<GameObject> keysCollected;
    #endregion

    #region Notices & Announcements
    // Internal Variables for Notices and Announcements
    bool presentingNotice = false;
    bool presentingAnnoucements = false;
    List<string> upcomingNotices = new();
    List<int> upcomingNoticeDurations = new();
    List<string> upcomingAnnouncements = new();
    List<string> upcomingSubtitles = new();
    List<int> upcomingAnnouncementDurations = new();
    Coroutine noticeCoroutine;
    Coroutine announcementCoroutine;

    /// <summary>
    /// Displays text at the bottom of the screen for the default duration.
    /// </summary>
    /// <param name="txt">Text to be displayed.</param>
    public static void DisplayNotice(string txt)
    {
        DisplayNotice(txt,false,singleton.defaultAnnouncementDuration);
    }
    /// <summary>
    /// Displays text at the bottom of the screen.
    /// </summary>
    /// <param name="txt">Text to be displayed.</param>
    /// <param name="duration">Amount of time (in seconds) to display the text.</param>
    public static void DisplayNotice(string txt, int duration)
    {
        DisplayNotice(txt,false,duration);
    }
    /// <summary>
    /// Displays text at the bottom of the screen.
    /// </summary>
    /// <param name="txt">Text to be displayed.</param>
    /// <param name="priority">Display text immediately even if other text is present.</param>
    public static void DisplayNotice(string txt, bool priority)
    {
        DisplayNotice(txt, priority, singleton.defaultNoticeDuration);
    }
    /// <summary>
    /// Displays text at the bottom of the screen.
    /// </summary>
    /// <param name="txt">Text to be displayed.</param>
    /// <param name="priority">Display text immediately even if other text is present.</param>
    /// <param name="duration">Amount of time (in seconds) to display the text.</param>
    public static void DisplayNotice(string txt, bool priority, int duration)
    {
        if (priority) // Adds text and timing to the front of the line.
        {
            singleton.upcomingNotices.Insert(0, txt);
            singleton.upcomingNoticeDurations.Insert(0, duration);
        }
        else // Adds text and timing to the back of the line.
        {
            singleton.upcomingNotices.Add(txt);
            singleton.upcomingNoticeDurations.Add(duration);
        }
        singleton.StartNoticeCycle(priority);

    }
    void StartNoticeCycle(bool priority) // Sets up text to display for indicated amount of time.
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
    IEnumerator NoticeCycle() // Displays text for indicated amount of time.
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

    /// <summary>
    /// Displays big text in the center of the screen.
    /// </summary>
    /// <param name="title">Text to display.</param>
    public static void DisplayAnnouncement(string title)
    {
        DisplayAnnouncement(title, "");
    }
    /// <summary>
    /// Displays big text in the center of the screen.
    /// </summary>
    /// <param name="title">Biggest text to display.</param>
    /// <param name="subtitle">Smaller text right below it.</param>
    public static void DisplayAnnouncement(string title,string subtitle)
    {
        DisplayAnnouncement(title, subtitle, false, singleton.defaultAnnouncementDuration);
    }
    /// <summary>
    /// Displays big text in the center of the screen.
    /// </summary>
    /// <param name="title">Biggest text to display.</param>
    /// <param name="subtitle">Smaller text right below it.</param>
    /// <param name="duration">Amount of time (in seconds) to display the text.</param>
    public static void DisplayAnnouncement(string title, string subtitle, int duration)
    {
        DisplayAnnouncement(title,subtitle,false,duration);
    }
    /// <summary>
    /// Displays big text in the center of the screen.
    /// </summary>
    /// <param name="title">Biggest text to display.</param>
    /// <param name="subtitle">Smaller text right below it.</param>
    /// <param name="priority">Display text immediately even if other text is present.</param>
    public static void DisplayAnnouncement(string title, string subtitle, bool priority)
    {
        DisplayAnnouncement(title, subtitle, priority, singleton.defaultAnnouncementDuration);
    }
    /// <summary>
    /// Displays big text in the center of the screen.
    /// </summary>
    /// <param name="title">Biggest text to display.</param>
    /// <param name="subtitle">Smaller text right below it.</param>
    /// <param name="priority">Display text immediately even if other text is present.</param>
    /// <param name="duration">Amount of time (in seconds) to display the text.</param>
    public static void DisplayAnnouncement(string title, string subtitle,bool priority,int duration)
    {
        if (priority)
        {
            singleton.upcomingAnnouncements.Insert(0, title);
            singleton.upcomingSubtitles.Insert(0, subtitle);
            singleton.upcomingAnnouncementDurations.Insert(0, duration);
        }
        else
        {
            singleton.upcomingAnnouncements.Add(title);
            singleton.upcomingSubtitles.Add(subtitle);
            singleton.upcomingAnnouncementDurations.Add(duration);
        }
        singleton.StartAnnouncementCycle(priority);
    }
    void StartAnnouncementCycle(bool priority) // Sets up text to display for indicated amount of time.
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
    IEnumerator AnnouncementCycle() // Displays text for indicated amount of time.
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
    /// <summary>
    /// Displays player health status on-screen. (UNFINISHED)
    /// </summary>
    /// <param name="percentage">Percentage (0-100) of health the player has remaining.</param>
    public static void DisplayHealth(float percentage)
    {
        if(percentage >= 100)
        {
            singleton.healthBarHolder.SetActive(false);
        }
        else
        {
            singleton.healthBarHolder.SetActive(true);
        }
        singleton.healthBar.localScale = new Vector3(percentage / 100,1,1);
        if(percentage > 50)
        {
            singleton.healthBarImage.color = new Color((50-(percentage-50)) / 50, 1, 0);
        }
        else
        {
            singleton.healthBarImage.color = new Color(1, percentage / 50, 0);
        }
    }
    public static void ShowHealthBar()
    {
        singleton.healthBarHolder.SetActive(true);
    }
    /// <summary>
    /// Display a collected key on the screen.
    /// </summary>
    /// <param name="key">Color of collected key.</param>
    public static void DisplayKey(ColorManager.StandardColor key)
    {
        singleton.keysCollected[(int)key].SetActive(true);
    }
    /// <summary>
    /// Looks through the objectives in the Objective Manager and displays progress.
    /// </summary>
    public static void DisplayObjectives()
    {
        string toDisplay = "";
        for (int i = 0; i < ObjectivesManager.counterCounts.Count; i++)
        {
            if (i != 0) toDisplay += "\n";
            toDisplay += ObjectivesManager.counterCounts[i] + "/" + ObjectivesManager.counterGoals[i] + " " + ObjectivesManager.counterDescriptions[i];
        }
        singleton.objectivesField.text = toDisplay;
    }
    #endregion
}
