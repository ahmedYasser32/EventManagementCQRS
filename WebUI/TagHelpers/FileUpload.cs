using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
  [HtmlTargetElement("File-Upload")]
  public class FileUpload : TagHelper
  {
    public ModelExpression? AspFor { get; set; }
    public string? Id { get; set; }
    public string? name { get; set; }
    public string? propertyName { get; set; }
    public string? value { get; set; }
    public string? label { get; set; }
    public string? imgPath { get; set; }
    public bool? required { get; set; }
    public string? Col { get; set; }
    public string? ErrorMessage { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      string Req = required != null && required == true ? "required" : "";
      if (AspFor != null)
      {
        name = AspFor.Name;
      }

      output.TagName = "div";
      output.Content.SetHtmlContent(@$"
      <div class='upload-image mb-2'>
          <div class='player-image'>
          <div class='image'>
            <img src='{imgPath}' />
          </div>
         <h6>{label}</h6>
        </div>
        <input id='{name}' type='file' name='{name}' {Req} />
       </div>
      <div class='input-attached-files-wrapper'>
        <div id='uploadedfile-{Id}' class='input-attached-files'>
          <input type='hidden' id='{propertyName}' name='{propertyName}' value='{value}' />
          <h6 class='upload-content' id='title-{Id}'>{value}</h6>
          <div onclick='deleteSingleFile(this)' style='cursor:pointer' class='trash-icon'>
            <img src='/images/black-trash.svg'/>
          </div>
        </div>
      </div>
        <div class='invalid-feedback'>
          {ErrorMessage}
        </div>
");
      if (!string.IsNullOrEmpty(Col))
        output.Attributes.Add("class", Col);
      else
        output.Attributes.Add("class", "col-6");

      output.TagMode = TagMode.StartTagAndEndTag;
    }
  }
}
