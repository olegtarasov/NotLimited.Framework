using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Data.Queries;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public class GridBuilder<T>
    {
        public enum GridColumnType
        {
            Template,
            FixedView,
            Convention
        }

        public class GridColumn
        {
            public GridColumn(Expression<Func<T, object>> expression)
            {
                Expression = expression;
                Type = GridColumnType.Convention;
            }

            public GridColumn(Expression<Func<T, object>> expression, Func<object, HelperResult> template)
            {
                Expression = expression;
                Template = template;
                Type = GridColumnType.Template;
            }

            public GridColumn(Expression<Func<T, object>> expression, string view)
            {
                Expression = expression;
                View = view;
                Type = GridColumnType.FixedView;
            }

            public GridColumnType Type;
            public Expression<Func<T, object>> Expression;
            public Func<object, HelperResult> Template;
            public string View;
            public string CustomTitle;
        }

        private readonly HtmlHelper _helper;
        private readonly IEnumerable<T> _models;
        private readonly HashSet<string> _fields;
        private readonly object _attributes;
        private readonly Pagination _pagination;
        private readonly List<GridColumn> _columns = new List<GridColumn>();

        public GridBuilder(HtmlHelper helper, IEnumerable<T> models, Pagination pagination = null, object attributes = null, HashSet<string> fields = null)
        {
            _models = models;
            _attributes = attributes;
            _fields = fields;
            _pagination = pagination;
            _helper = helper;
        }

        public GridBuilder<T> Column(Expression<Func<T, object>> expression, Func<dynamic, HelperResult> template, Action<GridColumn> action = null)
        {
            var column = new GridColumn(expression, template);
            if (action != null)
                action(column);

            _columns.Add(column);
            return this;
        }

        public GridBuilder<T> Column(Expression<Func<T, object>> expression, string view, Action<GridColumn> action = null)
        {
            var column = new GridColumn(expression, view);
            if (action != null)
                action(column);

            _columns.Add(column);
            return this;
        }

        public GridBuilder<T> Column(Expression<Func<T, object>> expression, Action<GridColumn> action = null)
        {
            var column = new GridColumn(expression);
            if (action != null)
                action(column);

            _columns.Add(column);
            return this;
        }

        public static implicit operator HelperResult(GridBuilder<T> builder)
        {
            var headers = builder._columns.Select(column => TableHelpers.TableHeader(builder._helper, column.Expression, column.CustomTitle, builder._fields)).ToList();
            var rows = new List<List<HelperResult>>();

            foreach (var model in builder._models)
            {
                var columns = new List<HelperResult>();
                foreach (var column in builder._columns)
                {
                    if (column.Type == GridColumnType.Convention)
                    {
                        columns.Add(TableHelpers.TableFieldConvention(builder._helper, model, builder._fields, column.Expression));
                    }
                    else if (column.Type == GridColumnType.FixedView)
                    {
                        columns.Add(TableViewHelper.TableFieldFixedView(builder._helper, column.View, model));
                    }
                    else if (column.Type == GridColumnType.Template)
                    {
                        columns.Add(new HelperResult(writer =>
                        {
                            writer.WriteLine("<td>");
                            column.Template(model).WriteTo(writer);
                            writer.WriteLine("</td>");
                        }));
                    }
                }
                
                rows.Add(columns);
            }


            return GridViewHelper.Grid(builder._helper, headers, rows, builder._pagination, builder._attributes);
        }
    }
}