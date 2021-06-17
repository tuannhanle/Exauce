using System;
using UnityEngine;
using UnityEngine.UI;
using AngleSharp;
using System.Threading.Tasks;
using System.Linq;
using Observer;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Text;

[System.Serializable]
public class CategoriesButton
{
    public Button button = default;
    public string url = default;
    public EventID eventID;
    public Action<string, EventID, Action> action = default;
}

public class CategoriesController : MonoBehaviour
{
    [SerializeField]
    private CategoriesButton _DecouverteButton, _SportButton, _DetenteButton, _ArtButton, _CultureButton, _PersonalLibraryButton;
    private List<CategoriesButton> categoriesButtons = new List<CategoriesButton>();

    private CategoriesButton _DefaultPanel;

    // Start is called before the first frame update
    void Start()
    {
        //_DefaultPanel = _DecouverteButton;
        //_DefaultPanel.button.onClick.Invoke();



        //_DecouverteButton.button.onClick.Invoke();


        OnCategoryButtonClicked("https://data.globalvision.ch/APP/GV/Exauce/", EventID.OnFirstLoad, () => { });
    }

    private void OnEnable()
    {
        //categoriesButtons.AddRange(new List<CategoriesButton>() { _DecouverteButton, _SportButton, _DetenteButton, _ArtButton, _CultureButton, _PersonalLibraryButton });

        //foreach (var categoriesButton in categoriesButtons)
        //{
        //    categoriesButton.action += OnCategoryButtonClicked;
        //    categoriesButton.button.onClick.AddListener(() =>
        //    {
        //        categoriesButton.action?.Invoke(
        //            categoriesButton.url,
        //            categoriesButton.eventID,
        //            () => categoriesButton.action -= OnCategoryButtonClicked
        //            );

        //    });
        //}
    }

    private void OnDisable()
    {
        //categoriesButtons.Clear();
        //foreach (var categoriesButton in categoriesButtons)
        //{
        //    categoriesButton.button.onClick.RemoveAllListeners();
        //}
    }
    async void OnCategoryButtonClicked(string url, EventID eventID, Action callback)
    {
        callback?.Invoke();
        await NetworkController.GetRequest(url,
            callback: async (result) => await GetList(url,result,
                callback: async (videoComponent) =>
                {
                    //foreach (var item in videoComponent)
                    //{
                    //    Debug.Log(item.dateCreated);
                    //}
                    this.PostEvent(eventID, videoComponent);
                }
            ));

    }

    static async Task GetList(string url, string source, Action<List<ParseHTML_To_DTO>> callback)
    {
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(req => req.Content(source));

        var trElements = document.All.Where(tr => tr.LocalName == "tr").Skip(3).ToList();
        var listVideoComponent = new List<ParseHTML_To_DTO>();

        foreach (var trElement in trElements)
        {
            var tdElements = trElement.Children.Where(td => td.LocalName == "td").ToList();
            if (tdElements.Count == 5) // match with 5 <td> tags
            {
                string linkURL = url;
                foreach (var td in tdElements)
                {
                    foreach (var item in td.Children)
                    {
                        if (item.HasAttribute("href"))
                        {
                            linkURL += item.GetAttribute("href").HtmlEncode(System.Text.Encoding.UTF8);
                        }
                    }
                }
                var videoComponent = new ParseHTML_To_DTO(
                   fileName: tdElements[1].TextContent,
                   url: linkURL,
                   dateCreated: tdElements[2].TextContent,
                   size: tdElements[3].TextContent
                   );
                listVideoComponent.Add(videoComponent);
            }
        }
        callback.Invoke(listVideoComponent);

    }
}
