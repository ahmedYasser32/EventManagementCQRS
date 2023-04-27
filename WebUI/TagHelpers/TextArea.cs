using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
  [HtmlTargetElement("Text-Area")]
  public class TextArea : TagHelper
  {
    public ModelExpression? AspFor { get; set; }
    public string? id { get; set; }
    public string? label { get; set; }
    public string? ErrorMessage { get; set; }
    public string? name { get; set; }
    public object? value { get; set; }
    public int cols { get; set; }
    public int rows { get; set; }
    public bool? required { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      string Req = required != null && required == true ? "required" : "";
      string ReqSpan = required != null && required == true ? "required-style" : "";
      if (AspFor != null)
      {
        name = AspFor.Name;
        value = AspFor.Model;
      }
      output.TagName = "div";
      output.Content.SetHtmlContent(@$"
            <div class='information-inputs'>
							<div class='input-wrapper text-area-wrapper'>
                <div class='form-floating {ReqSpan}'>
                  <textarea class='form-control' id='{id}' rows='5' cols='{cols}' name='{name}' value='{value}' placeholder='{label}' {Req}>{value}</textarea>
									<label>{label}</label>
									<span class='text-danger' data-valmsg-for='{name}' data-valmsg-replace='true'></span>
									<div class='invalid-feedback'>
                    {ErrorMessage}
									</div>
                </div>
              </div>
            </div>
						");
      output.Attributes.Add("class", "col-md-12 col-sm-12 col-12");
      output.TagMode = TagMode.StartTagAndEndTag;
    }
  }
}
