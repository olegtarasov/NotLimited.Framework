using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Common.Helpers;
using NotLimited.Framework.Data.Queries;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public class GridBuilder<T>
    {
        private readonly HtmlHelper _helper;
        private readonly IEnumerable<T> _models;
        private readonly HashSet<string> _fields;
        private readonly object _attributes;
        private readonly Pagination _pagination;
        private readonly List<GridColumnBuilder<T>> _columns = new List<GridColumnBuilder<T>>();

        private Func<IDisposable> _form = null;
        private Func<object, HelperResult> _formControls = null;

        public GridBuilder(HtmlHelper helper, IEnumerable<T> models, Pagination pagination = null, object attributes = null, HashSet<string> fields = null)
        {
            _models = models;
            _attributes = attributes;
            _fields = fields;
            _pagination = pagination;
            _helper = helper;
        }

        public GridBuilder<T> Column(Expression<Func<T, object>> expression, Action<GridColumnBuilder<T>> action = null)
        {
            string memberName = expression.GetMemberName();
            if (_fields != null && !_fields.Contains(memberName))
            {
                return this;
            }

            var column = new GridColumnBuilder<T>(expression, _helper);
            if (action != null)
            {
                action(column);
            }

            _columns.Add(column);
            return this;
        }

        public GridBuilder<T> Form(Func<IDisposable> form)
        {
            _form = form;
            return this;
        }

        public GridBuilder<T> FormControls(Func<dynamic, HelperResult> controls)
        {
            _formControls = controls;
            return this;
        }

        public static implicit operator HelperResult(GridBuilder<T> builder)
        {
            var headers = builder._columns.Select(column => column.GetTitleHtml()).ToList();
            var rows = new List<List<HelperResult>>();

            foreach (var model in builder._models)
            {
                var columns = new List<HelperResult>();
                foreach (var column in builder._columns)
                {
                    columns.Add(column.GetColumnHtml(model));
                }
                
                rows.Add(columns);
            }


            return GridViewHelper.Grid(builder._helper, headers, rows, builder._form, builder._formControls, builder._pagination, builder._attributes);
        }
    }
}