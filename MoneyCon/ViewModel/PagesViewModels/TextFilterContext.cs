using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.PagesViewModels
{
    public class TextFilterContext : BindableBase, IPageContext
    {
        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (!value.Equals(_text))
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public bool IsFiltered(Cost cost)
        {
            return string.IsNullOrEmpty(Text) ? false : cost.Desc.Contains(Text);
        }
    }
}
