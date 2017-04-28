using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlBuilderInterceptor
{
    public class Model
    {
        public int Int1 { get; set; }
        public int Int2 { get; set; }
    }
    public partial class _Default : Page
    {
        public Model Model = new Model {Int1 = 5}; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Binder.DataBind();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            //tb1.DataBind();
            base.OnPreRenderComplete(e);
        }

        public IEnumerable<int> GetFirst()
        {
            return Enumerable.Range(1, 10);
        }

        public IEnumerable<int> GetSecond([Control("ddl1", "SelectedValue")] int? selectedValue)
        {
            //return null;
            if (!selectedValue.HasValue) return Enumerable.Empty<int>();
            return new [] {0}.Concat(Enumerable.Range(1, 1000).Where(i => i.ToString().StartsWith(selectedValue.ToString())));
        }

        protected void OnClick(object sender, EventArgs e)
        {
            Binder.Unbind();
            //UpdateModel(Model, new FormValueProvider(ModelBindingExecutionContext));
        }
    }
}