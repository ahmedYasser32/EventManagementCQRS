using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
  [HtmlTargetElement("Dropdown")]
  public class Dropdown : TagHelper
  {
    public ModelExpression? AspFor { get; set; }
    [HtmlAttributeName("asp-items")]
    public SelectList? ListItems { get; set; }
    public string? Label { get; set; }
    public bool? Multiple { get; set; }
    public bool? Required { get; set; }
    public string? Col { get; set; }
    public string? Name { get; set; }
    public string? Id { get; set; }
    public string? ErrorMessage { get; set; }
    public bool? Visable { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      string Req = Required != null && Required == true ? "required" : "";
      string ReqSpan = Required != null && Required == true ? "<span style='color:red'> * </span>" : "";
      string multiple = Multiple != null && Multiple == true ? "multiple" : "";
      string? dispaly = Visable == false ? "none" : "block";
      string Options = "<option></option>";
      string option = "";
      string DataValueRequired = AspFor != null ? $"the {AspFor.Name} field is required." : "";
      string? ModelValue = AspFor != null ? AspFor.Model?.ToString() : "";
      foreach (SelectListItem item in ListItems!)
      {
        if (ModelValue == item.Value || item.Selected)
          option = $"<option selected='selected' value={item.Value}>{item.Text}</option>";
        else
          option = $"<option value={item.Value}>{item.Text}</option>";
        Options += option;
      }

      output.TagName = "div";
      output.Content.SetHtmlContent(@$"
            <div class='information-inputs select-component'>
 
              <div class='input-wrapper '>
             <label class='select-label'>{Label}{ReqSpan}</label>
                <div class='form-floating'>
                    <select data-val='true' data-val-required='{DataValueRequired}' id='{Id}' name='{AspFor?.Name}' class='select2 custom-select' {multiple} {Req}>
                      {Options}
                    </select>
                    <div class='invalid-feedback'>
                      {ErrorMessage}
                    </div>
                </div>
              </div>
            </div>
");
      output.Attributes.Add("Id", $"{Id}-wrapper");
      if (Visable == false)
        output.Attributes.Add("style", "display:none");
      if (!string.IsNullOrEmpty(Col))
        output.Attributes.Add("class", Col);
      else
        output.Attributes.Add("class", "col-md-6 col-sm-6 col-12");
      output.TagMode = TagMode.StartTagAndEndTag;


    }
  }
}
