using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Common.Helpers;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public class GridColumnBuilder<T>
    {
        private readonly Expression<Func<T, object>> _expression;
        private readonly HtmlHelper _helper;

        private GridColumnType _type;
        private Func<object, HelperResult> _template;
        private string _view;
        private string _customTitle;

        public GridColumnBuilder(Expression<Func<T, object>> expression, HtmlHelper helper)
        {
            _expression = expression;
            _helper = helper;
            _type = GridColumnType.Convention;
        }

        public HelperResult GetTitleHtml()
        {
            return TableHelpers.TableHeader(_helper, _expression, _customTitle);
        }

        public HelperResult GetColumnHtml(T model)
        {
            switch (_type)
            {
                case GridColumnType.Template:
                    return new HelperResult(writer =>
                    {
                        writer.WriteLine("<td>");
                        _template(model).WriteTo(writer);
                        writer.WriteLine("</td>");
                    });
                case GridColumnType.FixedView:
                    return GridViewHelper.TableFieldFixedView(_helper, _view, model);
                case GridColumnType.Convention:
                    return GridViewHelper.TableFieldConvention(_helper, typeof(T).Name, _expression.GetMemberName(), model);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public GridColumnBuilder<T> Title(string title)
        {
            _customTitle = title;
            return this;
        }

        public GridColumnBuilder<T> Template(Func<dynamic, HelperResult> template)
        {
            _template = template;
            _type = GridColumnType.Template;
            return this;
        }

        public GridColumnBuilder<T> View(string view)
        {
            _view = view;
            _type = GridColumnType.FixedView;
            return this;
        }
    }
}