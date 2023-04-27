using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
  [HtmlTargetElement("Text-Box")]
  public class Textbox : TagHelper
  {
    public ModelExpression? AspFor { get; set; }

    public string? Id { get; set; }
    public string? Name { get; set; }
    public object? Value { get; set; }
    public string? label { get; set; }
    public string? type { get; set; }
    public string? col { get; set; }
    public string? step { get; set; }
    public bool? required { get; set; }
    public string? ErrorMessage { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      var content = output.GetChildContentAsync().Result.GetContent();
      string Req = required != null && required == true ? "required" : "";
      string ReqSpan = required != null && required == true ? "required-style" : "";
      type = !string.IsNullOrEmpty(type) ? type : "text";
      if (AspFor != null)
      {
        Name = AspFor.Name;
        Value = AspFor.Model;
      }
      output.TagName = "div";
      output.Content.SetHtmlContent(@$"

            <div class='information-inputs'>
							<div class='input-wrapper text-box-wrapper'>
                <div class='form-floating {ReqSpan}'>
                  <input type = '{type}' class='form-control' id='{Id}' name='{Name}' value='{Value}' step='{step}' placeholder='{label}' {Req} />
                  <label>{label}</label>
									{content}
									<div class='invalid-feedback'>
                    {ErrorMessage}
									</div>
                </div>
              </div>
            </div>
                      ");

      output.Attributes.Add("Id", $"{Id}-wrapper");
      if (!string.IsNullOrEmpty(col))
        output.Attributes.Add("class", col);
      else
        output.Attributes.Add("class", "col-md-6 col-sm-6 col-12");

      output.TagMode = TagMode.StartTagAndEndTag;
    }

  }
}
