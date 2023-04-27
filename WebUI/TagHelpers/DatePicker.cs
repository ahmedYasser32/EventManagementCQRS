using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
  [HtmlTargetElement("Date-Picker")]
  public class DatePicker : TagHelper
  {
    public ModelExpression? AspFor { get; set; }
    public string? id { get; set; }
    public string? name { get; set; }
    public string? value { get; set; }
    public string? placeholder { get; set; }
    public string? label { get; set; }
    public string? col { get; set; }
    public bool? required { get; set; }
    public string? ErrorMessage { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      string content = output.GetChildContentAsync().Result.GetContent();
      string Req = required != null && required == true ? "required" : "";
      string ReqSpan = required != null && required == true ? "required-style" : "";
      if (AspFor != null)
      {
        name = AspFor.Name;
        value = AspFor.Model == null ? "" : ((DateTime)AspFor.Model).ToString("yyyy/MM/dd");
      }
      output.TagName = "div";
      output.Content.SetHtmlContent(@$"
            <div class='information-inputs'>
							<div class='input-wrapper'>
                <div class='form-floating date datepicker-container {ReqSpan}'>
                  <input type = 'text' class='form-control custom-datepicker' id='{id}' name='{name}' autocomplete='off' value='{value}' placeholder='{label}' {Req} />
                  <label>{label}</label>
									{content}
									<span class='text-danger' data-valmsg-for='{name}' data-valmsg-replace='true'></span>
									<div class='invalid-feedback'>
                    {ErrorMessage}
									</div>
                </div>
                  <img class='calendar-icon' src='/images/fi-br-calendar.svg'/>
              </div>
            </div>
                      ");
      if (!string.IsNullOrEmpty(col))
        output.Attributes.Add("class", col);
      else
        output.Attributes.Add("class", "col-md-6 col-sm-6 col-12");

      output.TagMode = TagMode.StartTagAndEndTag;
    }
  }
}
