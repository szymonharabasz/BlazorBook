using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Components.RazorComponents
{
	public class CustomCssClassProvider<ProviderType> : ComponentBase
		where ProviderType : FieldCssClassProvider, new()
	{
		[CascadingParameter]
		EditContext? CurrentEditContext { get; set; }
		public ProviderType Provider { get; set; } = new();
        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
			{
				throw new InvalidOperationException(
					$"{nameof(CustomCssClassProvider<ProviderType>)} wyaga" +
					$"kaskadowego parametru typu {nameof(EditContext)}. Możesz" +
					$"na przykład wewnątrz EditForm użyć" +
					$"{nameof(CustomCssClassProvider<ProviderType>)}."
					);
			}
			CurrentEditContext.SetFieldCssClassProvider(Provider);
        }
    }
}

