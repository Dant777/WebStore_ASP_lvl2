using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldBeActive())
                MakeActive(output);

            output.Attributes.RemoveAll("is-active-route");
        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttribute = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttribute == null)
            {
                classAttribute = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttribute);
            }
            else if (classAttribute.Value?.ToString().Contains("active", StringComparison.Ordinal) != true)
            {
                output.Attributes.SetAttribute("class", classAttribute.Value is null
                    ? "active"
                    : classAttribute.Value + " active");
            }
        }

        private bool ShouldBeActive()
        {
            var currentController = ViewContext.RouteData.Values["Controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["Action"].ToString();

            if (!string.IsNullOrWhiteSpace(Controller) && currentController != Controller)
                return false;

            if (!string.IsNullOrWhiteSpace(Action) && Action != currentAction)
                return false;

            return true;
        }
    }
}
