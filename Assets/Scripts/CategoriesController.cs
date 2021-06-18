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
using ExitGames.Client.Photon.StructWrapping;

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
    private CategoriesButton _categoriesButton;

    void Start()
    {
        _categoriesButton.button.onClick.Invoke(); //EventID.OnGetCaterogyList

        this.RegisterListener(EventID.OnDirCaterogy, (o) => {
            var DTO = o as ParseHTML_To_DTO;
            OnCategoryButtonClicked(DTO.url, EventID.OnGetVideoList, callback:() => { 
                
            });



        });

    }

    private void OnEnable()
    {
        _categoriesButton.action += OnCategoryButtonClicked;
        _categoriesButton.button.onClick.AddListener(() =>
            {
                _categoriesButton.action?.Invoke(
                    _categoriesButton.url,
                    _categoriesButton.eventID,
                    () => _categoriesButton.action -= OnCategoryButtonClicked
                );

            });

    }

    private void OnDisable()
    {
        _categoriesButton.button.onClick.RemoveAllListeners();
    }
    async void OnCategoryButtonClicked(string url, EventID eventID, Action callback)
    {
        callback?.Invoke();
        await NetworkController.GetRequest(url,
            callback: async (result) => await GetListFolder(url, result,
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

    static async Task GetListFolder(string url, string source, Action<List<ParseHTML_To_DTO>> callback)
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
