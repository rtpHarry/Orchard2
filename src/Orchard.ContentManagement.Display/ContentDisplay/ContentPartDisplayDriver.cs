﻿using System.Threading.Tasks;
using Orchard.DisplayManagement.Handlers;
using Orchard.DisplayManagement.Views;

namespace Orchard.ContentManagement.Display.ContentDisplay
{
    /// <summary>
    /// Any concrete implementation of this class can provide shapes for any content item which has a specific Part.
    /// </summary>
    /// <typeparam name="TPart"></typeparam>
    public abstract class ContentPartDisplayDriver<TPart> : DisplayDriver<TPart, BuildDisplayContext, BuildEditorContext, UpdateEditorContext>, IContentDisplayDriver where TPart : ContentPart, new()
    {
        public ContentPartDisplayDriver()
        {
            Prefix = typeof(TPart).Name;
        }

        Task<IDisplayResult> IDisplayDriver<ContentItem, BuildDisplayContext, BuildEditorContext, UpdateEditorContext>.BuildDisplayAsync(ContentItem model, BuildDisplayContext context)
        {
            var part = model.As<TPart>();
            if(part != null)
            {
                return DisplayAsync(part, context.Updater);
            }

            return Task.FromResult<IDisplayResult>(null);
        }

        Task<IDisplayResult> IDisplayDriver<ContentItem, BuildDisplayContext, BuildEditorContext, UpdateEditorContext>.BuildEditorAsync(ContentItem model, BuildEditorContext context)
        {
            var part = model.As<TPart>();
            if (part != null)
            {
                return EditAsync(part, context.Updater);
            }

            return Task.FromResult<IDisplayResult>(null);
        }

        Task<IDisplayResult> IDisplayDriver<ContentItem, BuildDisplayContext, BuildEditorContext, UpdateEditorContext>.UpdateEditorAsync(ContentItem model, UpdateEditorContext context)
        {
            var part = model.As<TPart>();
            if (part != null)
            {
                var result = UpdateAsync(part, context.Updater);
                if (context.Updater.ModelState.IsValid)
                {
                    model.Weld(typeof(TPart).Name, part);
                }
                return result;
            }

            return Task.FromResult<IDisplayResult>(null);
        }

    }
}
