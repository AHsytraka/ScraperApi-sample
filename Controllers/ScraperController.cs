using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using ScrapySharp.Extensions;

namespace ScraperApi_sample.Controllers;


[ApiController]
[Route("[controller]")]
public class ScraperController : ControllerBase
{
    // GET
    [HttpGet("scrape")]
    public IActionResult Scrape()
    {
        HtmlWeb htmlWeb = new HtmlWeb();
        HtmlDocument htmlDocument = htmlWeb.Load("https://dev.to/alrobilliard/deploying-net-core-to-heroku-1lfe");
        
        //setting the target 
        var headers = htmlDocument.DocumentNode.CssSelect("p");

        //storing
        var titles = new List<Row>();
        foreach(var item in headers)
        {
            titles.Add(new Row{ Title = item.InnerText});
        }

        return Ok(titles);
    }
    
    [HttpGet("image-scraper")]
    public Task<IActionResult> ScrapeImages()
    {
        HtmlWeb htmlWeb = new HtmlWeb();
        HtmlDocument htmlDocument = htmlWeb.Load("https://dev.to/alrobilliard/deploying-net-core-to-heroku-1lfe");
        
        //setting the target 
        var images = htmlDocument.DocumentNode.CssSelect("img");

        //storing
        var imageUrls = new List<string>();
        foreach(var item in images)
        {
            imageUrls.Add(item.Attributes["src"].Value);
        }

        return Task.FromResult<IActionResult>(Ok(imageUrls));
    } 
}

public class Row {
    public string Title {get;set;}
}