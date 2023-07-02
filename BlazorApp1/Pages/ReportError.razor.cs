using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class ReportError
    {
        [Parameter]
        public int ErrorCode { get; set; }
        [Parameter]
        public string ErrorDescription { get; set; }
    }
}
