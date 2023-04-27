using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
  [HtmlTargetElement("Standard")]
  public class Standard : TagHelper
  {
    public ModelExpression? SortIndex { get; set; }
    public ModelExpression? Focus { get; set; }
    public ModelExpression? Active { get; set; }
    public ModelExpression? IsAdmin { get; set; }
    public string? Col { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      int _SortIndx = 0;
      string _Focus = "";
      string _Active = "";
      string _IsAdmin = "";
      string Admin = "";

      if (Active != null)
        _Active = (bool)Active.Model ? "checked" : "";
      if (Focus != null)
        _Focus = (bool)Focus.Model ? "checked" : "";      
      if (SortIndex != null)
        _SortIndx = (int)SortIndex.Model;

      if (IsAdmin != null)
      {
        _IsAdmin = (bool)IsAdmin.Model ? "checked" : "";
        Admin = $@"<input class='input-checkbox' value='true' id='Admin' name='Admin' {_IsAdmin} type='checkbox' data-val='true' data-val-required='The Active field is required.'/>
                    <label>
                      مدير النظام
										</label>";
      }

      output.TagName = "div";
      output.Content.SetHtmlContent(@$"

            <div class='information-inputs sorting-input '>
							<div class='input-wrapper'>
                <div class='form-floating'>
                  <input type='number' class='form-control ' name='SortIndex' value='{_SortIndx}' placeholder='الترتيب' />
                  <label>الترتيب</label>
                  <div class='invalid-feedback'>
                  </div>
                </div>
              </div>
            </div>
            <div class='information-inputs sorting-information-inputs'>
              <div class='input-wrapper '>
                <div class='form-floating'>
                  <div class='professional-data-checkbox'>
                    <input class='input-checkbox' value='true' name='Focus' {_Focus} type='checkbox' data-val='true'' data-val-required='The Focus field is required.'/>
                    <label>مميز</label>
                    <input class='input-checkbox' value='true' name='Active' {_Active} type='checkbox' data-val='true' data-val-required='The Active field is required.'/>
                    <label>
                      فعال
										</label>
                    {Admin}
                  </div>
                </div>
              </div>
            </div>
                      ");
      if (!string.IsNullOrEmpty(Col))
        output.Attributes.Add("class", Col);
      else
        output.Attributes.Add("class", "col-md-6 col-sm-12 col-12 player-info-col");

      output.TagMode = TagMode.StartTagAndEndTag;
    }

  }
}
